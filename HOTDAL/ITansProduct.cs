using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOTDAL
{
    public interface ITansProduct
    {
        IQueryable<Product> Products { get; }
    }
}
