using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using IdentityServer4Demo.OauthServer.Config;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using IdentityServer4;
using IdentityServer4Demo.OauthServer.DbContexts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityServer4Demo.OauthServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = @"Server=DESKTOP-ACOU86Q\SQLEXPRESS;Database=IdentityServerDemo4OauthServer;Trusted_Connection=True;MultipleActiveResultSets=true";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // configure identity server, but EF stores for clients and resources
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(builder =>
                    builder.UseSqlServer(connectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalStore(builder =>
                    builder.UseSqlServer(connectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
          
            app.UseStaticFiles();

            app.UseIdentity();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = "http://localhost:5000",
                RequireHttpsMetadata = false,

                ClientId = "mvc",
                SaveTokens = true
            });

            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                DisplayName = "Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,

                ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com",
                ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo"
            });

            app.UseFacebookAuthentication(new FacebookOptions
            {
                AuthenticationScheme = "Facebook",
                DisplayName = "Facebook",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,


                AppId = "224801881295183",
                AppSecret = "fb6982c70d322e07ce31e458821b16c5"
            });


            //  app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default_route",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Home" }
                );
            });
        }
    }
}
