using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<EfAboutDal>().As<IAboutDal>().InstancePerLifetimeScope();
            //builder.RegisterType<AboutManager>().As<IAboutService>().InstancePerLifetimeScope();

            //builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerLifetimeScope();
            //builder.RegisterType<CategoryManager>().As<ICategoryService>().InstancePerLifetimeScope();

            //builder.RegisterType<EfMinistrationDal>().As<IMinistrationDal>().InstancePerLifetimeScope();
            //builder.RegisterType<MinistrationManager>().As<IMinistrationService>().InstancePerLifetimeScope();

            //builder.RegisterType<EfContactDal>().As<IContactDal>().InstancePerLifetimeScope();
            //builder.RegisterType<ContactManager>().As<IContactService>().InstancePerLifetimeScope();

            //builder.RegisterType<EfBlogDal>().As<IBlogDal>().InstancePerLifetimeScope();
            //builder.RegisterType<BlogManager>().As<IBlogService>().InstancePerLifetimeScope();

            //builder.RegisterType<EfSliderDal>().As<ISliderDal>().InstancePerLifetimeScope();
            //builder.RegisterType<SliderManager>().As<ISliderService>().InstancePerLifetimeScope();

            //builder.RegisterType<EfCommentDal>().As<ICommentDal>().InstancePerLifetimeScope();
            //builder.RegisterType<CommentManager>().As<ICommentService>().InstancePerLifetimeScope();

            //builder.RegisterType<EfReplyDal>().As<IReplyDal>().InstancePerLifetimeScope();
            //builder.RegisterType<ReplyManager>().As<IReplyService>().InstancePerLifetimeScope();

            //builder.RegisterType<EfSiteInformationDal>().As<ISiteInformationDal>().InstancePerLifetimeScope();
            //builder.RegisterType<SiteInformationManager>().As<ISiteInformationService>().InstancePerLifetimeScope();

            //builder.RegisterType<EfTagDal>().As<ITagDal>().InstancePerLifetimeScope();
            //builder.RegisterType<TagManager>().As<ITagService>().InstancePerLifetimeScope();

            //builder.RegisterType<EfUserRefreshTokenDal>().As<IUserRefreshTokenDal>().InstancePerLifetimeScope();
            //builder.RegisterType<UserRefreshTokenManager>().As<IUserRefreshTokenService>().InstancePerLifetimeScope();

            //builder.RegisterType<EfLogDal>().As<ILogDal>().InstancePerLifetimeScope();
            //builder.RegisterType<LogManager>().As<ILogService>().InstancePerLifetimeScope();

            //builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerLifetimeScope();
            //builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            //builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            //builder.RegisterType<TokenManager>().As<ITokenService>().InstancePerLifetimeScope();
            //builder.RegisterType<LoggerService>().As<ILoggerService>().SingleInstance();

            

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg => {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                            {
                                Selector = new AspectInterceptorSelector()
                            }).SingleInstance().InstancePerDependency();
        }
    }
}
