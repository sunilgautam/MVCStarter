using NetBlogger.Domain.Abstract;
using NetBlogger.Domain.Concrete;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace NetBlogger.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product> {
            //                                        new Product { Name = "Football", Price = 25 },
            //                                        new Product { Name = "Surf board", Price = 179 },
            //                                        new Product { Name = "Running shoes", Price = 95 }
            //                                    }.AsQueryable());

            ninjectKernel.Bind<IPostRepository>().To<PostRepository>();
            ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
        }
    }
}