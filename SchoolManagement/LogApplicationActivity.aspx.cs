using CoreLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class LogApplicationActivity : System.Web.UI.Page
    {
        Cookie user = new Cookie();
        DataReader _reader = new DataReader();
        String sPageName = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Check User Login Status and Menu Permission
            if (String.IsNullOrEmpty(user.GetCookie(CookieKey.UserID.ToString())) || user.GetCookie(CookieKey.UserID.ToString()) == "0")
            {
                Response.Redirect(String.Format("~/Login.aspx"), false);
            }
            try
            {
                DataTable dtMenu = (DataTable)Session["Menu"];
                sPageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                bool isPermitted = dtMenu.AsEnumerable().Any(row => sPageName == row.Field<String>("TargetUrl"));
                if (!isPermitted)
                {
                    ActivityLog.SaveProcess(ActionType.AccessDenied, sPageName);
                    Response.Redirect(String.Format("~/UnauthorizedPage.aspx"), false);
                }
            }
            catch
            {
                Response.Redirect(String.Format("~/Login.aspx"), false);
            }
            #endregion

            if (!IsPostBack)
            {
                divDetailsPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());

                LoadSoftwareAuditLog();
            }
        }

        #region Load Primary Data
        private void LoadSoftwareAuditLog()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "AuditLog");

                _reader = new DataReader();
                DataTable dtLog = _reader.GetDataTableByStoredProcedure("SP_DEVELOPER_HELPER", _param);
                if (dtLog.Rows.Count > 0)
                {
                    gvLogInfo.DataSource = dtLog;
                    gvLogInfo.DataBind();
                    lblRecordCount.Text = dtLog.Rows.Count.ToString();
                }
                else
                {
                    gvLogInfo.DataSource = null;
                    gvLogInfo.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadSoftwareAuditLog", ex);
                DisplayMessage("An error occured! ", ex.Message.ToString());
            }
        }
        #endregion

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

        protected void gvLogInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLogInfo.PageIndex = e.NewPageIndex;
            LoadSoftwareAuditLog();
        }
    }
}