using CoreLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class MasterPageViewList : System.Web.UI.MasterPage
    {
        Cookie user = new Cookie();
        String sPageName = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            sPageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(user.GetCookie(CookieKey.LoginID.ToString())))
                {
                    lblLogedUser.Text = user.UserName;
                    lblUserType.Text = user.UserRole;
                    imgProfilePhoto.ImageUrl = user.GetCookie(CookieKey.ProfilePhoto.ToString());
                    lblApplicationName.Text = user.GetCookie(CookieKey.ApplicationName.ToString());
                    lblSchoolName.Text = user.GetCookie(CookieKey.SchoolName.ToString());

                    GenerateMenuList();
                }
                else
                {
                    Response.Redirect(String.Format("~/Login.aspx"), false);
                }
            }
        }

        private void GenerateMenuList()
        {
            try
            {
                DataTable dtTopMenuOnly = (DataTable)Session["Menu"];
                topMenu.InnerHtml = MenuDAO.GetTopLevelMenuHTML(dtTopMenuOnly, user.GetCookie(CookieKey.HomePage.ToString()));
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "GenerateMenuList", ex);
                Response.Redirect(String.Format("~/Login.aspx"), false);
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            ActivityLog.SaveProcess(ActionType.Logout);

            user.ClearCookie();
            user.RemoveCookie();
            Session.RemoveAll();
            Session.Abandon();
            Session.Clear();
            Session.Remove("UserInfo");
            Response.Redirect("~/Login.aspx");
        }
    }
}