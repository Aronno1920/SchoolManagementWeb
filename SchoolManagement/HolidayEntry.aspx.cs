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
    public partial class HolidayEntry : System.Web.UI.Page
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

                LoadPendingHoliday();
                btnUpdate.Visible = false;
            }
        }
        #region Load Primary Data

        private void LoadPendingHoliday()
        {
            Hashtable _param = new Hashtable();
            _param.Add("Action", "PendingHoliday");

            _reader = new DataReader();
            DataTable dtHoliday = _reader.GetDataTableByStoredProcedure("SP_HOLIDAY", _param);
            if (dtHoliday.Rows.Count > 0)
            {
                lblRecordCount.Text = dtHoliday.Rows.Count.ToString();
                gvHoliday.DataSource = dtHoliday;
                gvHoliday.DataBind();
            }
            else
            {
                lblRecordCount.Text = "0";
                gvHoliday.DataSource = null;
                gvHoliday.DataBind();
            }
        }
        #endregion

        #region Button Click Events
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Hashtable _param = new Hashtable();
            _param.Add("StartDate", txtStartDate.Text.Trim());
            _param.Add("EndDate", txtEndDate.Text.Trim());
            _param.Add("Remarks", txtRemarks.Text.ToString());
            _param.Add("EntryBy", user.UserID.ToString());
            _param.Add("Action", "Create");

            _writer = new DataWriter();
            long affRow = _writer.ExecuteStoredProcedure("SP_HOLIDAY", _param, "AffRow");
            if (affRow > 0)
            {
                ActivityLog.SaveProcess(ActionType.Insert, "SP_HOLIDAY", "Create", affRow.ToString());
                ClearControls();
                LoadPendingHoliday();
                DisplayMessage("Holiday Save Successful");
            }
            else
            {
                DisplayMessage("Holiday save failed. Please try again");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Hashtable _param = new Hashtable();
            _param.Add("HolidayId", hdHolidayId.Value.Trim());
            _param.Add("StartDate", txtStartDate.Text.Trim());
            _param.Add("EndDate", txtEndDate.Text.Trim());
            _param.Add("Remarks", txtRemarks.Text.ToString());
            _param.Add("EntryBy", user.UserID.ToString());
            _param.Add("Action", "Update");

            _writer = new DataWriter();
            long affRow = _writer.ExecuteStoredProcedure("SP_HOLIDAY", _param, "AffRow");
            if (affRow > 0)
            {
                ActivityLog.SaveProcess(ActionType.Update, "SP_HOLIDAY", "Update", affRow.ToString());
                ClearControls();
                LoadPendingHoliday();
                DisplayMessage("Holiday Save Successful");
            }
            else
            {
                DisplayMessage("Holiday save failed. Please try again");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvHoliday_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdHolidayId.Value = gvRow.Cells[0].Text;
                    txtStartDate.Text = gvRow.Cells[1].Text;
                    txtEndDate.Text = gvRow.Cells[2].Text;
                    txtRemarks.Text = gvRow.Cells[3].Text;

                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (e.CommandName == "RemoveRow")
                {

                    _writer = new DataWriter();
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("HolidayId", gvRow.Cells[0].Text);
                    _param.Add("Action", "Delete");

                    long affRow = _writer.ExecuteStoredProcedure("SP_HOLIDAY", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Delete, "SP_HOLIDAY", "Delete", affRow.ToString());
                        LoadPendingHoliday();
                        DisplayMessage("Holiday delete successful");
                    }
                    else
                    {
                        DisplayMessage("Holiday delete failed. Please try again");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvHoliday_RowCommand", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        #endregion Button Click Events

        #region Others Code

        public void ClearControls()
        {
            hdHolidayId.Value = String.Empty;
            txtStartDate.Text = String.Empty;
            txtEndDate.Text = String.Empty;
            txtRemarks.Text = String.Empty;
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
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