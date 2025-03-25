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
    public partial class VoucherHistory : System.Web.UI.Page
    {
        Cookie user = new Cookie();
        DataReader _reader = new DataReader();
        DataWriter _writer = new DataWriter();
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
                divSearchPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divActionPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divVoucherList.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadCollectionHistory();
            }
        }

        #region Button Click Events
        protected void LoadCollectionHistory()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ReadAll");

                _reader = new DataReader();
                DataTable dtVouchers = _reader.GetDataTableByStoredProcedure("SP_VOUCHER_HISTORY", _param);
                if (dtVouchers.Rows.Count > 0)
                {
                    lblTotalRecord.Text = dtVouchers.Rows.Count.ToString();
                    gvVoucherList.DataSource = dtVouchers;
                    gvVoucherList.DataBind();
                }
                else
                {
                    gvVoucherList.DataSource = null;
                    gvVoucherList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadCollectionHistory", ex);
                DisplayMessage("Error occured!. " + ex.Message);
            }
        }

        protected void btnAddVoucher_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/VoucherEntry.aspx", false);
        }
        #endregion Button Click Events

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