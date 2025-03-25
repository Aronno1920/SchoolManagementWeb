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
    public partial class SessionFeesBalance : System.Web.UI.Page
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
                divEntry.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divDetails.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadClassInfo();
                LoadFeesType();
                LoadMonthsInfo();
                
                GetFeesScheduleInfo();
            }

        }

        #region Load Primary Data
        protected void LoadFeesType()
        {
            DataTable dtFees = PopulateLists.GetFeesType();
            if (dtFees.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtFees, ddlFeesName, "AccountName", "AccountId", true, "Select Fees Type", "0");
            }
            else
            {
                ddlClass.DataSource = null;
                ddlClass.DataBind();
            }
        }
        protected void LoadClassInfo()
        {
            DataTable dtClasss = PopulateLists.GetClassList();
            if (dtClasss.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtClasss, ddlClass, "ClassName", "ClassId", true, "Select Class", "0");
                FillList.PopulateDropDownList(dtClasss, ddlGridSearch, "ClassName", "ClassId", true, "Select Class", "0");
            }
            else
            {
                ddlClass.DataSource = null;
                ddlClass.DataBind();

                ddlGridSearch.DataSource = null;
                ddlGridSearch.DataBind();
            }
        }
        protected void LoadMonthsInfo()
        {
            DataTable dtClasss = PopulateLists.GetMonthList();
            if (dtClasss.Rows.Count > 0)
            {
                cbMonthNo.DataSource = dtClasss;
                cbMonthNo.DataValueField = "MonthId";
                cbMonthNo.DataTextField = "MonthName";
                cbMonthNo.DataBind();
            }
            else
            {
                cbMonthNo.DataSource = null;
                cbMonthNo.DataBind();
            }
        }
        #endregion

        #region Button Click Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean isMonthNo = false;
                long affRow = 0;
                foreach (ListItem month in cbMonthNo.Items)
                {
                    if (month.Selected)
                    {
                        Hashtable _param = new Hashtable();
                        _param.Add("ClassId", ddlClass.SelectedValue.ToString());
                        _param.Add("MonthNo", month.Value.ToString());
                        _param.Add("FeesId", ddlFeesName.SelectedValue.ToString());
                        _param.Add("Amount", txtAmount.Text.ToString());
                        _param.Add("EntryBy", user.UserID.ToString());
                        _param.Add("Action", "Create");

                        _writer = new DataWriter();
                        affRow = _writer.ExecuteStoredProcedure("SP_FEES_SCHEDULE", _param, "AffRow");
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_FEES_SCHEDULE", "Create", affRow.ToString());
                        isMonthNo = true;
                    }
                }

                if (isMonthNo == false)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("ClassID", ddlClass.SelectedValue.ToString());
                    _param.Add("MonthNo", "0");
                    _param.Add("FeesId", ddlFeesName.SelectedValue.ToString());
                    _param.Add("Amount", txtAmount.Text.ToString());
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "Create");

                    _writer = new DataWriter();
                    affRow = _writer.ExecuteStoredProcedure("SP_FEES_SCHEDULE", _param, "AffRow");
                    ActivityLog.SaveProcess(ActionType.Insert, "SP_FEES_SCHEDULE", "Create", affRow.ToString());
                }
                if (affRow > 0)
                {
                    ClearControls();
                    GetFeesScheduleInfo();
                    DisplayMessage("Class wise Fees save Successful");
                }
                else
                {
                    DisplayMessage("Fees save failed. Please try again");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvFeesList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveRow")
                {
                    _writer = new DataWriter();
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("ScheduleId", gvRow.Cells[0].Text);
                    _param.Add("ClassId", gvRow.Cells[1].Text);
                    _param.Add("MonthNo", gvRow.Cells[2].Text);
                    _param.Add("EntryBy", user.GetCookie(CookieKey.UserID.ToString()));
                    _param.Add("Action", "DELETE");

                    long affRow = _writer.ExecuteStoredProcedure("SP_FEES_SCHEDULE", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Delete, "SP_ACADEMIC_YEAR", "DELETE", affRow.ToString());
                        GetFeesScheduleInfo();
                        DisplayMessage("Frees Schedule has been deleted successful");
                    }
                    else
                    {
                        DisplayMessage("Frees Schedule has been deleted failed. Please try again");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvFeesList_RowCommand", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        #endregion Button Click Events

        #region Others Code

        public void ClearControls()
        {
            ddlClass.SelectedIndex = -1;
            ddlFeesName.SelectedIndex = -1;
            txtAmount.Text = String.Empty;
            cbMonthNo.ClearSelection();
            btnSave.Visible = true;
        }

        private void GetFeesScheduleInfo()
        {
            DataTable dtFees = PopulateLists.GetFeesScheduleInfo();
            if (dtFees.Rows.Count > 0)
            {
                lblRecordCount.Text = dtFees.Rows.Count.ToString();
                gvFeesList.DataSource = dtFees;
                gvFeesList.DataBind();
            }
            else
            {
                lblRecordCount.Text = "0";
                gvFeesList.DataSource = null;
                gvFeesList.DataBind();

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

        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("ClassID", ddlGridSearch.SelectedValue.ToString());
                _param.Add("SearchBy", txtGridSearch.Text.ToString());
                _param.Add("Action", "Search");

                _reader = new DataReader();
                DataTable dtFees = _reader.GetDataTableByStoredProcedure("SP_FEES_SCHEDULE", _param);
                if (dtFees.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtFees.Rows.Count.ToString();
                    gvFeesList.DataSource = dtFees;
                    gvFeesList.DataBind();
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvFeesList.DataSource = null;
                    gvFeesList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnGridSearch_Click", ex);
                DisplayMessage("Fees Schedul has loaded failed. " + ex.Message);
            }
        }

        protected void gvFeesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFeesList.PageIndex = e.NewPageIndex;
            GetFeesScheduleInfo();
        }
    }
}