using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Configurations;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.DependencyResolvers;
using Core.Utilities;
using Core.Utilities.Extensions;
using Core.Utilities.IoC;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
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
            services.AddDbContext<MySite2Context>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<MySite2Context>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 9;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            });


            services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOption"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                var tokenOptions = Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
                opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience[0],
                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers().AddNewtonsoftJson(options=> options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            ValidatorOptions.Global.LanguageManager.Enabled = false;

            services.AddScoped<IAboutService, AboutManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IMinistrationService, MinistrationManager>();
            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<ISliderService, SliderManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IReplyService, ReplyManager>();
            services.AddScoped<ISiteInformationService, SiteInformationManager>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITagService, TagManager>();
            services.AddScoped<ILogService, LogManager>();
            services.AddScoped<IUserRefreshTokenService, UserRefreshTokenManager>();
            services.AddScoped<ITokenService, TokenManager>();


            services.AddScoped<IAboutDal, EfAboutDal>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<IMinistrationDal, EfMinistrationDal>();
            services.AddScoped<IContactDal, EfContactDal>();
            services.AddScoped<IBlogDal, EfBlogDal>();
            services.AddScoped<ISliderDal, EfSliderDal>();
            services.AddScoped<ICommentDal, EfCommentDal>();
            services.AddScoped<IReplyDal, EfReplyDal>();
            services.AddScoped<ISiteInformationDal, EfSiteInformationDal>();
            services.AddScoped<ITagDal, EfTagDal>();
            services.AddScoped<ILogDal, EfLogDal>();
            services.AddScoped<IUserRefreshTokenDal, EfUserRefreshTokenDal>();

           
            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    Configuration["EmailSender:Host"],
                    Configuration.GetValue<int>("EmailSender:Port"),
                    Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    Configuration["EmailSender:UserName"],
                    Configuration["EmailSender:Password"]
                )
            );
            services.AddTransient<MsSqlLogger>();
            services.AddDependencyResolvers(Configuration,new ICoreModule[] {
               new CoreModule()
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //HttpContextHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureCustomExceptionMiddleware();
            app.UseHttpsRedirection();
            
            var path = Path.Combine(env.ContentRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "Uploads")),
                RequestPath = "/Uploads"
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
