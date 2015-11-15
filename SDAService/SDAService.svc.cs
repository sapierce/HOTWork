using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using HOTBAL;

namespace SDAService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SDAService : ISDAService
    {
        SDAMethods method = new SDAMethods();

        public List<Art> GetAllArts()
        {
            return method.GetAllSDAArts();
        }
    }
}
