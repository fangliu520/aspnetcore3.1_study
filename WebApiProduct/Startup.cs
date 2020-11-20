using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApiProduct.Utility;
using static WebApiProduct.Utility.CustomApiVersion;

namespace WebApiProduct
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                typeof(ApiVersionCommon).GetEnumNames().ToList().ForEach(v=> {
                    c.SwaggerDoc($"{v}", new OpenApiInfo { Title = "WebApiProduct", Version = $"{v}", Description = "restful�ܹ��µĲ�Ʒ����" });
                });

                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiProduct", Version = "v1", Description = "restful�ܹ��µĲ�Ʒ����" });
            });
            #region JWT��Ȩ��Ȩ
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                typeof(ApiVersionCommon).GetEnumNames().ToList().ForEach(v => {
                    c.SwaggerEndpoint($"/swagger/{v}/swagger.json", $"WebApiProduct {v}");
                });
              //  c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiProduct V1");
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            this.Configuration.ConsulRegist();
        }
    }
}
