using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
           // var authenticationProviderKey = "UserGatewayKey";

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
            //#region JWT鉴权授权
            //var ValidAudience = Configuration["audience"];
            //var ValidIssuer = Configuration["issuer"];
            //var SecurityKey = Configuration["SecurityKey"];
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//jwt  josn web token
            //.AddJwtBearer(x =>
            //{
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidAudience = ValidAudience,
            //        ValidIssuer = ValidIssuer,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey))
            //    };

            //});
            //#endregion


            services.AddOcelot()
            .AddConsul()
            .AddPolly();
            /*
             跨域问题：
              出于浏览器的同源策略限制。同源策略（Sameoriginpolicy）是一种约定，它是浏览器最核心也最基本的安全功能，
              如果缺少了同源策略，则浏览器的正常功能可能都会受到影响。可以说Web是构建在同源策略基础之上的，浏览器只是针对同源策略的一种实现。
              同源策略会阻止一个域的javascript脚本和另外一个域的内容进行交互。所谓同源（即指在同一个域）就是两个页面具有相同的协议（protocol），主机（host）和端口号（port）

            解决跨域问题方案：
            1、jsonp 通过浏览器标签去请求Api,避开跨域问题；通过浏览器的一些指定标签 <img src> <script src> <frame>去跨域请求，
            获取到数据以后，可以通过一个回调函数来把数据进行解析，然后使用数据
            2、通过后台模拟Http请求Api
            
            3、直接通过中间件实现

            4、通过ActionFilter 可以用AOP实现  base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*"); 在webapiproduct项目中CrossDomainFilter
            */
            services.AddCors(options => {
                options.AddPolicy("any", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("any");
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
