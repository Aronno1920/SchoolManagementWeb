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
    public partial class AdmissionForm : System.Web.UI.Page
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
                LoadSessionList();
                LoadFeesForCandidate();
                LoadAdmissionYearInfo();

                LoadPreviousFormInfo();
                btnUpdate.Visible = false;
            }
        }

        #region Load Primary Data
        private void LoadClassInfo()
        {
            DataTable dtClasss = PopulateLists.GetClassList();
            if (dtClasss.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtClasss, ddlClassName, "ClassName", "ClassId", true, "Select Class", "0");
                FillList.PopulateDropDownList(dtClasss, ddlGridSearch, "ClassName", "ClassId", true, "Select Class", "0");
            }
            else
            {
                ddlClassName.DataSource = null;
                ddlClassName.DataBind();

                ddlGridSearch.DataSource = null;
                ddlGridSearch.DataBind();
            }
        }

        protected void LoadSessionList()
        {
            DataTable dtSession = PopulateLists.GetSessionList();
            if (dtSession.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtSession, ddlSessionYear, "SessionTitle", "SessionYear");
            }
            else
            {
                ddlSessionYear.DataSource = null;
                ddlSessionYear.DataBind();
            }
        }
        
        protected void LoadAdmissionYearInfo()
        {
            DataTable dtAdmissionYearFees = PopulateLists.GetCurrentAdmissionYearFees();
            if (dtAdmissionYearFees.Rows.Count > 0)
            {
                txtAmount.Text = dtAdmissionYearFees.Rows[0]["AdmissionFees"].ToString();
                txtPaidAmount.Text = dtAdmissionYearFees.Rows[0]["AdmissionFees"].ToString();
            }
        }

        protected void LoadFeesForCandidate()
        {
            DataTable dtCollectionType = PopulateLists.GetFeesForCandidate();
            if (dtCollectionType.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtCollectionType, ddlPaymentType, "AccountName", "AccountId");
                ddlPaymentType.SelectedIndex = 0;
            }
            else
            {
                ddlPaymentType.DataSource = null;
                ddlPaymentType.DataBind();
            }
        }

        private void LoadPreviousFormInfo()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ReadAll");

                _reader = new DataReader();
                DataTable dtFormInfo = _reader.GetDataTableByStoredProcedure("SP_ADMISSION_FORM", _param);
                if (dtFormInfo.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtFormInfo.Rows.Count.ToString();
                    gvPreviousForm.DataSource = dtFormInfo;
                    gvPreviousForm.DataBind();
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvPreviousForm.DataSource = null;
                    gvPreviousForm.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadPreviousFormInfo", ex);
                DisplayMessage("Opps! An error occurred. ", ex.Message);
            }
        }
        #endregion

        #region Button Click Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("FormNo", txtFormNo.Text.ToString());
                    _param.Add("Name", txtName.Text.ToString());
                    _param.Add("ContactNo", txtContactNo.Text.ToString());
                    _param.Add("ClassID", ddlClassName.SelectedValue.Trim());
                    _param.Add("SessionYear", ddlSessionYear.SelectedValue.ToString());
                    _param.Add("FeesId", ddlPaymentType.SelectedValue.ToString());
                    _param.Add("FeeTitle", ddlPaymentType.SelectedItem.Text.ToString());
                    _param.Add("Amount", txtAmount.Text.ToString());
                    _param.Add("PaidAmount", txtPaidAmount.Text.ToString());
                    _param.Add("Remarks", txtRemarks.Text.ToString());
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "Create");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_ADMISSION_FORM", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_ADMISSION_FORM", "Create", affRow.ToString());
                        DisplayMessage("Admission form has been save successfully");

                        ClearControls();
                        LoadPreviousFormInfo();
                    }
                    else
                    {
                        DisplayMessage("Admission form has been failed to save. Please try again");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
                DisplayMessage("Opps! An error occurred. ", ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("CandidateId", hdCandidateId.Value.ToString());
                    _param.Add("FormNo", txtFormNo.Text.ToString());
                    _param.Add("Name", txtName.Text.ToString());
                    _param.Add("ContactNo", txtContactNo.Text.ToString());
                    _param.Add("ClassID", ddlClassName.SelectedValue.Trim());
                    _param.Add("SessionYear", ddlSessionYear.SelectedValue.ToString());
                    _param.Add("PaidAmount", txtPaidAmount.Text.ToString());
                    _param.Add("Remarks", txtRemarks.Text.ToString());
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "Update");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_ADMISSION_FORM", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Update, "SP_ADMISSION_FORM", "Update", affRow.ToString());
                        DisplayMessage("Admission form has been update successfully");

                        LoadPreviousFormInfo();
                        ClearControls();
                    }
                    else
                    {
                        DisplayMessage("Admission form has been update failed. Please try again");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
                DisplayMessage("Opps! An error occurred. ", ex.Message);
            }


        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvPreviousForm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdCandidateId.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");
                    txtFormNo.Text = gvRow.Cells[3].Text.Replace("&nbsp;", "");
                    txtName.Text = gvRow.Cells[4].Text.Replace("&nbsp;", "");
                    txtContactNo.Text = gvRow.Cells[5].Text.Replace("&nbsp;", "");
                    ddlSessionYear.SelectedIndex = ddlSessionYear.Items.IndexOf(ddlSessionYear.Items.FindByText(gvRow.Cells[6].Text));
                    ddlClassName.SelectedIndex = ddlClassName.Items.IndexOf(ddlClassName.Items.FindByValue(gvRow.Cells[1].Text));

                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvSection_RowCommand_RemoveRow", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void gvPreviousForm_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadPreviousFormInfo();
            gvPreviousForm.PageIndex = e.NewPageIndex;
            gvPreviousForm.DataBind();
        }
        #endregion

        #region Validation and Others Code
        public void ClearControls()
        {
            txtFormNo.Text = String.Empty;
            txtName.Text = String.Empty;
            txtContactNo.Text = String.Empty;
            txtRemarks.Text = String.Empty;
            ddlSessionYear.SelectedIndex = -1;
            ddlClassName.SelectedIndex = -1;

            btnSave.Visible = true;
            btnUpdate.Visible = false;
            LoadAdmissionYearInfo();
        }

        protected ValidationProcess IsValidationPassed()
        {
            ValidationProcess result = new ValidationProcess();
            if (String.IsNullOrEmpty(txtFormNo.Text))
            {
                txtFormNo.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Form No.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtName.Text))
            {
                txtName.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Student Name.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtContactNo.Text))
            {
                txtContactNo.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Guardian Contact No.";
                return result;
            }
            else if (ddlClassName.SelectedValue == "0")
            {
                ddlClassName.Focus();
                result.IsValid = false;
                result.Message = "Please Select Class.";
                return result;
            }
            else if (ddlSessionYear.SelectedValue == "0")
            {
                ddlSessionYear.Focus();
                result.IsValid = false;
                result.Message = "Please Select Session/Academic Year.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtPaidAmount.Text))
            {
                txtPaidAmount.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Paid Amount.";
                return result;
            }

            return result;
        }
        #endregion

        #region Search Box Related Methods
        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("ClassID", ddlGridSearch.SelectedValue.Trim());
                _param.Add("SearchBy", txtGridSearch.Text.Trim());
                _param.Add("Action", "Search");

                _reader = new DataReader();
                DataTable dtSubject = _reader.GetDataTableByStoredProcedure("SP_SECTION", _param);
                if (dtSubject.Rows.Count > 0)
                {
                    gvPreviousForm.DataSource = dtSubject;
                    gvPreviousForm.DataBind();
                }
                else
                {
                    gvPreviousForm.DataSource = null;
                    gvPreviousForm.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSearch_OnClick", ex);
                DisplayMessage("Candidate profile has created fail. " + ex.Message);
            }
        }

        protected void btnGridSearchClear_Click(object sender, EventArgs e)
        {
            ddlGridSearch.SelectedIndex = -1;
            txtGridSearch.Text = String.Empty;
            LoadPreviousFormInfo();
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