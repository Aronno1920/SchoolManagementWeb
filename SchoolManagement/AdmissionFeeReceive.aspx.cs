using CoreLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class AdmissionFeeReceive : System.Web.UI.Page
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
                divCandidateListPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadEmployeeList();
            }
        }

        #region Load Primary Data
        private void LoadEmployeeList()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "EmployeeList");

                _reader = new DataReader();
                DataTable dtEmployeeList = _reader.GetDataTableByStoredProcedure("SP_FORM_FEES_COLLECTION", _param);
                if (dtEmployeeList.Rows.Count > 0)
                {
                    FillList.PopulateDropDownList(dtEmployeeList, ddlEntryBy, "EmployeeName", "EntryBy", true, "Select", "0");
                }
                else
                {
                    ddlEntryBy.DataSource = null;
                    ddlEntryBy.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadEmployeeList", ex);
            }
        }
        #endregion

        #region Click Event Methods

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("EntryBy", ddlEntryBy.SelectedValue.ToString());
                _param.Add("Action", "LoadNotCollectList");

                _reader = new DataReader();
                DataSet dsCandidate = _reader.GetDataSetByStoredProcedure("SP_FORM_FEES_COLLECTION", _param);
                if (dsCandidate.Tables[0].Rows.Count > 0)
                {
                    gvCandidate.DataSource = dsCandidate.Tables[0];
                    gvCandidate.DataBind();

                    lblRecordCount.Text = dsCandidate.Tables[0].Rows.Count.ToString();
                    txtTotalAmount.Text = dsCandidate.Tables[1].Rows[0]["TotalAmount"].ToString();
                }
                else
                {
                    gvCandidate.DataSource = null;
                    gvCandidate.DataBind();
                    lblRecordCount.Text = "0";
                    txtTotalAmount.Text = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnLoad_Click", ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvCandidate.Rows)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("CandidateId", row.Cells[0].Text);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "CollectFee");

                    _writer = new DataWriter();
                    _writer.ExecuteStoredProcedure("SP_FORM_FEES_COLLECTION", _param);
                }
                AutoVoucherEntry();
                btnLoad_Click(sender, e);

                ActivityLog.SaveProcess(ActionType.Insert, "SP_FORM_FEES_COLLECTION", "CollectFee", "0");
                DisplayMessage("Candidate form free has been collcetd.");
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
            }
        }

        protected void AutoVoucherEntry()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("TotalAmount", txtTotalAmount.Text.ToString());
                _param.Add("EntryBy", user.UserID.ToString());
                _param.Add("Action", "AutoVoucherEntry");

                _writer = new DataWriter();
                long iAffRow = _writer.ExecuteStoredProcedure("SP_FORM_FEES_COLLECTION", _param, "AffRow");
                if(iAffRow > 0)
                {
                    ActivityLog.SaveProcess(ActionType.Insert, "SP_FORM_FEES_COLLECTION", "AutoVoucherEntry", iAffRow.ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "AutoVoucherEntry", ex);
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
    }
}