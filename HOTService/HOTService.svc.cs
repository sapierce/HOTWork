using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using HOTBAL;

namespace HOTService
{
    public class HOTService : IHOTService
    {
        SDAMethods method = new SDAMethods();
        public List<Art> GetAllArts()
        {
            return method.GetAllSDAArts();
        }
    }
}
