using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Autofac;
using AppNetCore.Service.SqlHelper;

namespace WebCoreIOC
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
            services.AddControllersWithViews();
            #region Autofac

            #region autofac 获取服务
            //ContainerBuilder containerBuilder = new ContainerBuilder();
            //containerBuilder.RegisterType<TestServiceB>().As<ITestServiceB>();
            //IContainer container = containerBuilder.Build();
            //ITestServiceB serviceB=   container.Resolve<ITestServiceB>();//获取服务
            //serviceB.show(); 
            #endregion

            #region 构造函数注入

            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TestServiceB>().As<ITestServiceB>();
            containerBuilder.RegisterType<TestServiceC>().As<ITestServiceC>();

            IContainer container = containerBuilder.Build();
            ITestServiceC serviceC = container.Resolve<ITestServiceC>();
            serviceC.show();

            #endregion
            #endregion

            #region JWT鉴权授权
            var ValidAudience = Configuration["audience"];
            var ValidIssuer = Configuration["issuer"];
            var SecurityKey = Configuration["SecurityKey"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//jwt  josn web token
            .AddJwtBearer(x =>
           {
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidAudience = ValidAudience,
                   ValidIssuer = ValidIssuer,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey))
               };

           });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
