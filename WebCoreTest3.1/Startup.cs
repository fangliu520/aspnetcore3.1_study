using System.Data;
using System.Data.SqlClient;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using AppNetCore.Interface.Extend;
using AppNetCore.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebCoreTest3._1.Utility;
using AppNetCore.Service.Utility;
using AppNetCore.Utility;
using AppNetCore.Utility.Redis;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.FileProviders;

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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();//修改视图后，刷新浏览器看到变化


            ///全局注册异常Filter
            //services.AddControllersWithViews(options =>
            //{
            //    options.Filters.Add(typeof(ExceptionFilterAttribute)); //全局注册异常
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory">log4net第二种方法</param>
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
            app.UseMiddleware<RefuseStealChainMiddleWare>();//防盗链中间件
            app.UseStaticFiles();
            //执行静态文件 把wwwroot文件夹拷贝到bin目录下
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot"))//执行文件下的wwwroot 文件夹
            //}); 

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
