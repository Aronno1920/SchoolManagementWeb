using System;
using System.Collections;
using System.Data;
using System.Net;
using System.Web.UI;
using CoreLibrary;

namespace SchoolManagement
{
    public partial class Login : System.Web.UI.Page
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
                String ePassword = UtilityClass.Encrypt(txtPassword.Text.ToString(), true);

                Hashtable _param = new Hashtable();
                _param.Add("LoginID", txtUserName.Text.Trim().ToString());
                _param.Add("Action", "Login");
                DataSet dsInfo = _reader.GetDataSetByStoredProcedure("SP_USER_LOGIN", _param);
                if (dsInfo.Tables[0].Rows.Count > 0)
                {
                    if (dsInfo.Tables[0].Rows[0]["Password"].ToString().Equals(ePassword))
                    {
                        CoreLibrary.Cookie userCookie = new CoreLibrary.Cookie();
                        userCookie.SetCookie(CookieKey.UserID.ToString(), dsInfo.Tables[0].Rows[0]["EmployeeID"].ToString());
                        userCookie.SetCookie(CookieKey.LoginID.ToString(), dsInfo.Tables[0].Rows[0]["LoginId"].ToString());
                        userCookie.SetCookie(CookieKey.UserName.ToString(), dsInfo.Tables[0].Rows[0]["EmployeeName"].ToString());
                        userCookie.SetCookie(CookieKey.UserRole.ToString(), dsInfo.Tables[0].Rows[0]["UserType"].ToString());
                        userCookie.SetCookie(CookieKey.Email.ToString(), dsInfo.Tables[0].Rows[0]["Email"].ToString());
                        userCookie.SetCookie(CookieKey.Mobile.ToString(), dsInfo.Tables[0].Rows[0]["ContactNo"].ToString());
                        userCookie.SetCookie(CookieKey.ApplicationTheme.ToString(), dsInfo.Tables[0].Rows[0]["UsedTheme"].ToString());
                        userCookie.SetCookie(CookieKey.HomePage.ToString(), dsInfo.Tables[0].Rows[0]["HomePage"].ToString());
                        if (String.IsNullOrEmpty(dsInfo.Tables[0].Rows[0]["ProfilePhoto"].ToString()))
                        {
                            userCookie.SetCookie(CookieKey.ProfilePhoto.ToString(), "~/ProfilePhoto/Employee/Avatar.png");
                        }
                        else
                        {
                            userCookie.SetCookie(CookieKey.ProfilePhoto.ToString(), dsInfo.Tables[0].Rows[0]["ProfilePhoto"].ToString());
                        }
                        userCookie.SetCookie(CookieKey.ApplicationName.ToString(), dsInfo.Tables[1].Rows[0]["SystemShortName"].ToString());
                        userCookie.SetCookie(CookieKey.SchoolName.ToString(), dsInfo.Tables[1].Rows[0]["SchoolName"].ToString());

                        LoadAuthrizedMenu(dsInfo.Tables[0].Rows[0]["EmployeeID"].ToString());

                        ActivityLog.SaveProcess(ActionType.Login);

                        Response.Redirect(dsInfo.Tables[0].Rows[0]["HomePage"].ToString(), false);
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

        private void LoadAuthrizedMenu(String sUserID)
        {
            try
            {
                _reader = new DataReader();

                Hashtable _param = new Hashtable();
                _param.Add("LoginID", sUserID);
                _param.Add("Action", "MenuList");

                DataTable dtMenu = _reader.GetDataTableByStoredProcedure("SP_USER_LOGIN", _param);
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