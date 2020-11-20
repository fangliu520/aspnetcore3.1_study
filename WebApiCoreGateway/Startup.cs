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
            //        options.Authority = "http://localhost:7200";//��ȡtoken���ܹ�Կ
            //        options.RequireHttpsMetadata = false;
            //        options.ApiName = "api";
            //        options.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Both;
            //    })
            //.AddJwtBearer(authenticationProviderKey, option =>
            //{
            //    option.Authority = "http://localhost:7200";//��ȡtoken���ܹ�Կ
            //    option.RequireHttpsMetadata = false;
            //    option.Audience = "api";
            //})           
            ;
            //#region JWT��Ȩ��Ȩ
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
             �������⣺
              �����������ͬԴ�������ơ�ͬԴ���ԣ�Sameoriginpolicy����һ��Լ������������������Ҳ������İ�ȫ���ܣ�
              ���ȱ����ͬԴ���ԣ�����������������ܿ��ܶ����ܵ�Ӱ�졣����˵Web�ǹ�����ͬԴ���Ի���֮�ϵģ������ֻ�����ͬԴ���Ե�һ��ʵ�֡�
              ͬԴ���Ի���ֹһ�����javascript�ű�������һ��������ݽ��н�������νͬԴ����ָ��ͬһ���򣩾�������ҳ�������ͬ��Э�飨protocol����������host���Ͷ˿ںţ�port��

            ����������ⷽ����
            1��jsonp ͨ���������ǩȥ����Api,�ܿ��������⣻ͨ���������һЩָ����ǩ <img src> <script src> <frame>ȥ��������
            ��ȡ�������Ժ󣬿���ͨ��һ���ص������������ݽ��н�����Ȼ��ʹ������
            2��ͨ����̨ģ��Http����Api
            
            3��ֱ��ͨ���м��ʵ��

            4��ͨ��ActionFilter ������AOPʵ��  base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*"); ��webapiproduct��Ŀ��CrossDomainFilter
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
