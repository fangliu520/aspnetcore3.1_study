using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using WebApiCoreGateway.Utility;

namespace WebApiCoreGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();
            var authenticationProviderKey = "UserGatewayKey";

            //services.AddAuthentication("Bearer")
            //.AddIdentityServerAuthentication(authenticationProviderKey, o =>
            //{
            //    o.Authority = "http://localhost:7200";
            //    o.ApiName = "api";
            //    o.SupportedTokens = SupportedTokens.Both;
            //    o.ApiSecret = "secret";
            //    o.RequireHttpsMetadata = false;
            //});
            //services.AddAuthentication()//jwt  josn web token
            //.AddJwtBearer(authenticationProviderKey, x =>
            //{
            //    x.Authority = "http://localhost:7200";                   
            //    x.Audience = "api";
            //    x.RequireHttpsMetadata = false;

            //});
            //var AuthenticationProviderKey = "UserGatewayKey";
            //services.AddAuthentication("Bearer")//jwt  josn web token
            //    .AddIdentityServerAuthentication(AuthenticationProviderKey, options =>
            //    {
            //        options.Authority = "http://localhost:7200";//获取token解密公钥
            //        options.RequireHttpsMetadata = false;
            //        options.ApiName = "api";
            //        options.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Both;
            //    })
            //.AddJwtBearer(authenticationProviderKey, option =>
            //{
            //    option.Authority = "http://localhost:7200";//获取token解密公钥
            //    option.RequireHttpsMetadata = false;
            //    option.Audience = "api";
            //})           
            ;

           

            services.AddOcelot()
            .AddConsul()
            .AddPolly()
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {         
            app.UseOcelot();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
