using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using AppNetCore.Interface.Extend;
using AppNetCore.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebCoreTest3._1.Utility;
using log4net;
using AppNetCore.Service.Utility;
using AppNetCore.Utility;
using AppNetCore.Utility.Redis;
using Microsoft.Extensions.Logging;

namespace WebCoreTest3._1
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
            services.Configure<DBConnectionOption>(Configuration.GetSection("ConnectionString"));
            services.Configure<RedisConnectionOption>(Configuration.GetSection("RedisConnectionString"));
            services.AddTransient<IRedisHelper, RedisHelper>();
            services.AddTransient<IBaseService, BaseService>();
            services.AddTransient<ICustomConnectionFactory, CustomConnectionFactory>();
            services.AddTransient<IDbConnection, SqlConnection>();
            services.AddTransient<IDapperHelper, DapperHelper>();
            services.AddTransient<IExpressionToSqlWhereHelper, ExpressionToSqlWhereHelper>();
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });
            services.AddControllersWithViews().AddRazorRuntimeCompilation();//�޸���ͼ��ˢ������������仯


            ///ȫ��ע���쳣Filter
            //services.AddControllersWithViews(options =>
            //{
            //    options.Filters.Add(typeof(ExceptionFilterAttribute)); //ȫ��ע���쳣
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory">log4net�ڶ��ַ���</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory  loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            loggerFactory.AddLog4Net("log4net.config");
            app.UseMiddleware<RefuseStealChainMiddleWare>();//�������м��
            app.UseStaticFiles();

            app.UseRouting();

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
