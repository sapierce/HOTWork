using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using HOTBAL;

namespace SDAService
{
    [ServiceContract]
    public interface ISDAService
    {
        [OperationContract]
        [WebGet(UriTemplate = "Arts")]
        List<Art> GetAllArts();
    }
}
