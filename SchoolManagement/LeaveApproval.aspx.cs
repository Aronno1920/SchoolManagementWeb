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
    public partial class LeaveApproval : System.Web.UI.Page
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

                LoadLeaveReason();
                LoadPendingLeave();
            }
        }

        #region Load Primary Data
        protected void LoadLeaveReason()
        {
            DataTable dtReason = PopulateLists.GetLeaveReasonList();
            if (dtReason.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtReason, ddlReasonType, "Reason", "ReasonId", true, "Select leave reason", "0");
            }
            else
            {
                ddlReasonType.DataSource = null;
                ddlReasonType.DataBind();
            }
        }

        private void LoadPendingLeave()
        {
            Hashtable _param = new Hashtable();
            _param.Add("Action", "PendingLeave");

            _reader = new DataReader();
            DataTable dtHoliday = _reader.GetDataTableByStoredProcedure("SP_LEAVE", _param);
            if (dtHoliday.Rows.Count > 0)
            {
                lblRecordCount.Text = dtHoliday.Rows.Count.ToString();
                gvLeave.DataSource = dtHoliday;
                gvLeave.DataBind();
            }
            else
            {
                lblRecordCount.Text = "0";
                gvLeave.DataSource = null;
                gvLeave.DataBind();
            }
        }
        #endregion

        #region Button Click Events
        protected void btnLoadStudent_Click(object sender, EventArgs e)
        {
            SearchStudentByCode();
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            Hashtable _param = new Hashtable();
            _param.Add("LeaveId", hdLeaveId.Value.Trim());
            _param.Add("StudentId", hdStudentId.Value.Trim());
            _param.Add("ReasonId", ddlReasonType.SelectedValue.Trim());
            _param.Add("StartDate", txtStartDate.Text.Trim());
            _param.Add("EndDate", txtEndDate.Text.Trim());
            _param.Add("Remarks", txtRemarks.Text.ToString());
            _param.Add("EntryBy", user.UserID.ToString());
            _param.Add("Action", "ApproveLeave");

            _writer = new DataWriter();
            long affRow = _writer.ExecuteStoredProcedure("SP_LEAVE", _param, "AffRow");
            if (affRow > 0)
            {
                ActivityLog.SaveProcess(ActionType.Approved, "SP_LEAVE", "ApproveLeave", affRow.ToString());
                ClearControls();
                LoadPendingLeave();
                DisplayMessage("Leave application has been approved successful");
            }
            else
            {
                DisplayMessage("Leave application has been approved failed. Please try again");
            }
        }

        protected void btnDisapproved_Click(object sender, EventArgs e)
        {
            Hashtable _param = new Hashtable();
            _param.Add("LeaveId", hdLeaveId.Value.Trim());
            _param.Add("Remarks", txtRemarks.Text.ToString());
            _param.Add("EntryBy", user.UserID.ToString());
            _param.Add("Action", "DisapproveLeave");

            _writer = new DataWriter();
            long affRow = _writer.ExecuteStoredProcedure("SP_LEAVE", _param, "AffRow");
            if (affRow > 0)
            {
                ActivityLog.SaveProcess(ActionType.Disapproved, "SP_LEAVE", "DisapproveLeave", affRow.ToString());
                ClearControls();
                LoadPendingLeave();
                DisplayMessage("Leave application has been disapproved Successful");
            }
            else
            {
                DisplayMessage("disapproved save failed. Please try again");
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvLeave_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdLeaveId.Value = gvRow.Cells[1].Text;
                    txtStudentCode.Text = gvRow.Cells[2].Text;
                    ddlReasonType.SelectedIndex = ddlReasonType.Items.IndexOf(ddlReasonType.Items.FindByText(gvRow.Cells[6].Text));
                    txtStartDate.Text = gvRow.Cells[7].Text;
                    txtEndDate.Text = gvRow.Cells[8].Text;
                    txtRemarks.Text = gvRow.Cells[9].Text.Replace("&nbsp;","");

                    SearchStudentByCode();
                }           
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvLeave_RowCommand", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        #endregion Button Click Events

        #region Others Code

        public void ClearControls()
        {
            hdLeaveId.Value = String.Empty;
            hdStudentId.Value = String.Empty;
            txtStudentCode.Text = String.Empty;
            txtStudentName.Text = String.Empty;
            txtBanglaName.Text = String.Empty;
            txtClass.Text = String.Empty;
            txtSection.Text = String.Empty;
            txtRollNo.Text = String.Empty;
            txtContact.Text = String.Empty;

            ddlReasonType.SelectedIndex = -1;
            txtStartDate.Text = String.Empty;
            txtEndDate.Text = String.Empty;
            txtRemarks.Text = String.Empty;
        }

        protected void SearchStudentByCode()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("StudentId", txtStudentCode.Text.Trim());
                _param.Add("Action", "SearchStudent");

                _reader = new DataReader();
                DataTable dtStudent = _reader.GetDataTableByStoredProcedure("SP_LEAVE", _param);
                if (dtStudent.Rows.Count > 0)
                {
                    hdStudentId.Value = dtStudent.Rows[0]["StudentId"].ToString();
                    txtStudentName.Text = dtStudent.Rows[0]["StudentName"].ToString();
                    txtBanglaName.Text = dtStudent.Rows[0]["StudentNameBangla"].ToString();
                    txtClass.Text = dtStudent.Rows[0]["ClassName"].ToString();
                    txtSection.Text = dtStudent.Rows[0]["SectionName"].ToString();
                    txtRollNo.Text = dtStudent.Rows[0]["RollNo"].ToString();
                    txtContact.Text = dtStudent.Rows[0]["ContactNo"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "SearchStudentByCode", ex);
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