using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HOTDAL;

namespace TansPublicMVC.Controllers
{
    public class ProductController : Controller
    {
        private ITansProduct tansProduct;

        public ProductController(ITansProduct tansProductRepository)
        {
            this.tansProduct = tansProductRepository;
        }

        public ViewResult List()
        {
            return View(tansProduct.Products);
        }
    }
}
