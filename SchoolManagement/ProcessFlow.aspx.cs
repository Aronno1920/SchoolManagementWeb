using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreLibrary;

namespace SchoolManagement
{
    public partial class ProcessFlow : System.Web.UI.Page
    {
        Cookie user = new Cookie();

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Check User Login Status and Menu Permission
            if (String.IsNullOrEmpty(user.GetCookie(CookieKey.UserID.ToString())) || user.GetCookie(CookieKey.UserID.ToString()) == "0")
            {
                Response.Redirect(String.Format("~/Login.aspx"), false);
            }
            #endregion

            if (!IsPostBack)
            {
                divAcademicSetup.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divBasicSetup.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divCandidateSetup.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divStudentSetup.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divAccountSetup.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divUserSetup.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
            }
        }
    }
}