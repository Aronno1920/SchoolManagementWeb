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
    public partial class CandidateMarksEntry : System.Web.UI.Page
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

                LoadAdmissionYear();
                LoadPreviousSchoolInfo();
                LoadCandidateInfo();
            }
        }

        #region Load Primary Data
        private void LoadCandidateInfo()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("AdmissionYear", ddlAdmissionYear.SelectedValue);
                _param.Add("SchoolId", ddlPreviousSchool.SelectedValue.Trim());
                _param.Add("SearchBy", txtGridSearch.Text.Trim());
                _param.Add("Action", "SearchForMarksEntry");

                _reader = new DataReader();
                DataTable dtSubject = _reader.GetDataTableByStoredProcedure("SP_CANDIDATE", _param);
                if (dtSubject.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtSubject.Rows.Count.ToString();
                    gvCandidate.DataSource = dtSubject;
                    gvCandidate.DataBind();
                }
                else
                {
                    gvCandidate.DataSource = null;
                    gvCandidate.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadCandidateInfo", ex);
            }
        }

        private void LoadAdmissionYear()
        {
            DataTable dtAdmissionYear = PopulateLists.GetSessionList();
            if (dtAdmissionYear.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtAdmissionYear, ddlAdmissionYear, "SessionTitle", "SessionYear");
                ddlAdmissionYear.SelectedIndex = 0;
            }
            else
            {
                ddlAdmissionYear.DataSource = null;
                ddlAdmissionYear.DataBind();
            }
        }

        private void LoadPreviousSchoolInfo()
        {
            DataTable dtSchool = PopulateLists.GetSchoolList();
            if (dtSchool.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtSchool, ddlPreviousSchool, "SchoolName", "SchoolId", true, "Select school", "0");
            }
            else
            {
                ddlPreviousSchool.DataSource = null;
                ddlPreviousSchool.DataBind();
            }
        }
        #endregion

        #region Click Event Methods
        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            LoadCandidateInfo();
        }

        protected void btnGridSearchClear_Click(object sender, EventArgs e)
        {
            ddlPreviousSchool.SelectedIndex = -1;
            txtGridSearch.Text = String.Empty;
            LoadCandidateInfo();
        }

        protected void gvCandidate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SaveRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    String sCandidateId = gvRow.Cells[1].Text;
                    String sMarks = ((TextBox)gvRow.FindControl("txtMarks")).Text.ToString();

                    long iResult = MarksEntry(sCandidateId, sMarks);
                    if (iResult > 0)
                    {
                        DisplayMessage("Candidate Exam Marks has been saved successful");
                    }
                    else
                    {
                        DisplayMessage("Exam Marks has been saved fail");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvCandidate_RowCommand_SaveRow", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                long iResult = 0;
                foreach (GridViewRow gvRow in gvCandidate.Rows)
                {
                    String sCandidateId = gvRow.Cells[1].Text;
                    String sMarks = ((TextBox)gvRow.FindControl("txtMarks")).Text.ToString();

                    iResult = MarksEntry(sCandidateId, sMarks);
                }
                if (iResult > 0)
                {
                    DisplayMessage("Marks save successful");
                }
                else
                {
                    DisplayMessage("Marks save failed");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected long MarksEntry(String sCandidateId, String sMarks)
        {
            long iResult = 0;
            try
            {
                ValidationProcess result = IsValidEntry(sMarks);
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("CandidateId", sCandidateId);
                    _param.Add("Marks", sMarks);
                    _param.Add("Action", "UpdateMarks");

                    _writer = new DataWriter();
                    iResult = _writer.ExecuteStoredProcedure("SP_CANDIDATE", _param, "AffRow");
                    ActivityLog.SaveProcess(ActionType.Update, "SP_CANDIDATE", "UpdateMarks", sCandidateId);
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                iResult = -99;
                ErrorTracking.SaveError(user.UserID, sPageName, "MarksEntry", ex);
            }
            return iResult;
        }

        protected ValidationProcess IsValidEntry(String sMarks)
        {
            ValidationProcess result = new ValidationProcess();
            if (!Validator.IsNumericOnly(sMarks))
            {
                result.IsValid=false;
                result.Message = "Please enter only numeric value";
            }
            return result;
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

        protected void btnCandidateList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CollectionHistory.aspx", false);
        }
    }
}