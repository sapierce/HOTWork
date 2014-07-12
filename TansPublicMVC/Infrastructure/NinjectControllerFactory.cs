using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Moq;
using HOTDAL;
using System.Collections.Generic;
using System.Linq;

namespace TansPublicMVC.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext
            requestContext, Type controllerType)
        {

            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            Mock<ITansProduct> mock = new Mock<ITansProduct>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product { ProductName = "Tingle Lotion", ProductPrice = 25 },
                new Product { ProductName = "Non-tingle Lotion", ProductPrice = 15 },
                new Product { ProductName = "Goggles", ProductPrice = 3 }
            }.AsQueryable());

            ninjectKernel.Bind<ITansProduct>().ToConstant(mock.Object);
        }
    }
}