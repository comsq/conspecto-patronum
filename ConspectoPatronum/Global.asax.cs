using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Common;
using ConspectoPatronum.Domain;
using ConspectoPatronum.Core.Repositories;
using ConspectoPatronum.Core.Services;
using ConspectoPatronum.Services;

namespace ConspectoPatronum
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            base.OnApplicationStarted();
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IRepository<Subject>>().To<Repository<Subject>>();
            kernel.Bind<IRepository<Image>>().To<Repository<Image>>();
            kernel.Bind<IRepository<Teacher>>().To<Repository<Teacher>>();
            kernel.Bind<IRepository<Comment>>().To<Repository<Comment>>();
            kernel.Bind<ISubjectsService>().To<SubjectsService>();
            kernel.Bind<IImagesService>().To<ImagesService>();
            kernel.Bind<ITeachersService>().To<TeachersService>();
            kernel.Bind<ICommentsService>().To<CommentsService>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            return kernel;
        }
    }
}
