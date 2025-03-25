using System;
using System.Collections;
using System.Data;
using System.Net;
using System.Web.UI;
using CoreLibrary;


namespace SchoolManagement
{
    public partial class StudentLogin : System.Web.UI.Page
    {
        DataReader _reader = new DataReader();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["UserInfo"] = null;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("StudentCode", txtUserName.Text.Trim().ToString());
                _param.Add("Action", "Login");
                DataTable dt = _reader.GetDataTableByStoredProcedure("SP_STUDENT_PANEL", _param);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["StudentCode"].ToString().Equals(txtPassword.Text.ToString()))
                    {
                        CoreLibrary.Cookie userCookie = new CoreLibrary.Cookie();
                        userCookie.SetCookie(CookieKey.UserID.ToString(), dt.Rows[0]["StudentId"].ToString());
                        userCookie.SetCookie(CookieKey.LoginID.ToString(), dt.Rows[0]["StudentCode"].ToString());
                        userCookie.SetCookie(CookieKey.UserName.ToString(), dt.Rows[0]["StudentName"].ToString());
                        userCookie.SetCookie(CookieKey.UserRole.ToString(), dt.Rows[0]["ClassName"].ToString()+" ["+ dt.Rows[0]["SectionName"].ToString() + "]");

                        LoadAuthrizedMenu();
                        ActivityLog.SaveProcess(ActionType.Login, "SP_USER_LOGIN", "Student Login", dt.Rows[0]["StudentCode"].ToString());

                        Response.Redirect("Dashboard.aspx", false);
                    }
                    else
                    {
                        txtPassword.Text = String.Empty;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Password. Please try again');", true);
                        return;
                    }
                }
                else
                {
                    DisplayMessage("You are not authorized user for SMS. Please Contact with System Administrator");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(txtUserName.Text, Page.Title, "btnLogin_Click", ex);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                return;
            }
        }

        private void LoadAuthrizedMenu()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "MenuList");

                _reader = new DataReader();
                DataTable dtMenu = _reader.GetDataTableByStoredProcedure("SP_STUDENT_PANEL", _param);
                if (dtMenu.Rows.Count > 0)
                {
                    Session["Menu"] = dtMenu;
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(txtUserName.Text, Page.Title, "LoadAuthrizedMenu", ex);
                DisplayMessage("Menu load failed.", ex.Message.ToString());
            }
        }

        #region Common Method for Display Message
        private void DisplayMessage(String sMessage)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('" + sMessage.ToString() + "');", true);
            return;
        }
        private void DisplayMessage(String sTypeofData, String exMessage)
        {
            String strMessage = String.Format(sTypeofData + " \nError: {0}", exMessage);
            strMessage = strMessage.Replace("\r\n", "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('" + strMessage.ToString() + "');", true);
            return;
        }
        #endregion
    }
}