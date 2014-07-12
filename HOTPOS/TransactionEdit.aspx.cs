using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace HOTPOS
{
    public partial class TransactionEdit : System.Web.UI.Page
    {
        HOTDAL.SDAFunctionsClass functionsClass = new HOTDAL.SDAFunctionsClass();
        HOTDAL.SDASQL sqlClass = new HOTDAL.SDASQL();
        CultureInfo ci = new CultureInfo("en-us");

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "HOT Self Defense - Edit Transaction Details";

        }
    }
}