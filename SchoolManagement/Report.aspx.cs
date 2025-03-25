using CoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class Report : System.Web.UI.Page
    {
        Cookie user = new Cookie();

        protected void Page_Load(object sender, EventArgs e)
        {
            divVehicles.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
            divSetup.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());

            divServices.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
            divTransaction.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
        }
    }
}