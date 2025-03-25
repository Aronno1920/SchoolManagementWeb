using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreLibrary;

namespace SchoolManagement
{
    public partial class MigrationProcess : System.Web.UI.Page
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
                divAcademicPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divCandidateListPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadClassInfo();
                LoadAdmissionYearInfo();
                LoadCandidateList();
            }
        }

        #region Load Primary Data
        private void LoadClassInfo()
        {
            DataTable dtClasss = PopulateLists.GetClassForCandidate();
            if (dtClasss.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtClasss, ddlClass, "ClassName", "ClassId",true,"Select Class","0");
            }
            else
            {
                ddlClass.DataSource = null;
                ddlClass.DataBind();
            }
        }

        protected void LoadAdmissionYearInfo()
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

        private void LoadSectionInfo(String sClassId)
        {
            DataTable dtSection = PopulateLists.GetSectionListByClass(sClassId);
            if (dtSection.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtSection, ddlSection, "SectionName", "SectionId", true, "Select section", "0");
            }
            else
            {
                ddlSection.DataSource = null;
                ddlSection.DataBind();
            }
        }

        protected void LoadCandidateList()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("AdmissionYear", ddlAdmissionYear.SelectedValue);
                _param.Add("CandidateCode", txtCandidateId.Text);
                _param.Add("MarksFrom", txtMarksFrom.Text);
                _param.Add("MarksTo", txtMarksTo.Text);
                _param.Add("OrderBy", ddlMarksOrder.SelectedValue);
                _param.Add("Action", "Search");

                _reader = new DataReader();
                DataTable dtCandidates = _reader.GetDataTableByStoredProcedure("SP_CANDIDATE_MIGRATION", _param);
                if (dtCandidates.Rows.Count > 0)
                {
                    lblRowCount.Text = dtCandidates.Rows.Count.ToString();
                    gvCandidate.DataSource = dtCandidates;
                    gvCandidate.DataBind();
                }
                else
                {
                    lblRowCount.Text = "0";
                    gvCandidate.DataSource = null;
                    gvCandidate.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadCandidateList", ex);
            }
        }
        #endregion

        #region Button Click and Selected Index Change Events
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSectionInfo(ddlClass.SelectedValue.ToString());
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadCandidateList();
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    Int32 totalCapacity = Int32.Parse(txtCapacity.Text);
                    Int32 totalRow = gvCandidate.Rows.Count;
                    Int32 totalstudent = (totalCapacity > totalRow ? totalRow : totalCapacity) ;

                    for (Int32 rowIndex = 0; rowIndex <= totalstudent - 1; rowIndex++)
                    {
                        GridViewRow gvRow = gvCandidate.Rows[rowIndex];

                        Hashtable _param = new Hashtable();
                        _param.Add("AdmissionYear", ddlAdmissionYear.SelectedValue);
                        _param.Add("CandidateID", gvRow.Cells[2].Text.ToString());
                        _param.Add("ClassId", ddlClass.SelectedValue);
                        _param.Add("SectionId", ddlSection.SelectedValue);
                        _param.Add("EntryBy", user.UserID.ToString());
                        _param.Add("Action", "Migration");

                        _writer = new DataWriter();
                        long iResult = _writer.ExecuteStoredProcedure("SP_CANDIDATE_MIGRATION", _param, "AffRow");

                        ActivityLog.SaveProcess(ActionType.Update, "SP_CANDIDATE_MIGRATION", "Migration", gvRow.Cells[2].Text.ToString());
                    }
                    ClearControls();
                    LoadCandidateList();
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnProcess_Click", ex);
            }
        }

        protected void btnCandidateList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CollectionHistory.aspx", false);
        }
        #endregion

        #region Other Methods / Clear Methods

        protected ValidationProcess IsValidationPassed()
        {
            ValidationProcess result = new ValidationProcess();
            if (ddlClass.SelectedValue == "0")
            {
                result.IsValid = false;
                result.Message = "Please Select Class";
                return result;
            }
            else if (ddlSection.SelectedValue == "0")
            {
                result.IsValid = false;
                result.Message = "Please Select Section";
                return result;
            }
            else if (String.IsNullOrEmpty(txtCapacity.Text) || !Validator.IsIntNumberOnly(txtCapacity.Text))
            {
                result.IsValid = false;
                result.Message = "Please Enter Student Capacity";
                return result;
            }
            else if (gvCandidate.Rows.Count == 0)
            {
                result.IsValid = false;
                result.Message = "Please Load Candidate First. Then try again.";
                return result;
            }
            return result;
        }
        
        protected void ClearControls()
        {
            ddlClass.SelectedIndex = -1;
            ddlSection.SelectedIndex = -1;
            txtCapacity.Text = String.Empty;
        }
        #endregion

        #region Common Method for Display Message

        private void DisplayMessage(String sMessage)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message",
                "alert('" + sMessage.ToString() + "');", true);
            return;
        }

        private void DisplayMessage(String sTypeofData, String exMessage)
        {
            String strMessage = String.Format(sTypeofData + " \nError: {0}", exMessage);
            strMessage = strMessage.Replace("\r\n", "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message",
                "alert('" + strMessage.ToString() + "');", true);
            return;
        }

        #endregion
    }
}