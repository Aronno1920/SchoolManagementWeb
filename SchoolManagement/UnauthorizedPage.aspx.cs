using CoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class UnauthorizedPage : System.Web.UI.Page
    {
        Cookie user = new Cookie();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divButton.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
            }
        }
    }
}