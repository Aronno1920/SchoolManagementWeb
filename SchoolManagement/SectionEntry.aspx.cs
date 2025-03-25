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
    public partial class SectionEntry : System.Web.UI.Page
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

                LoadSectionInfo();
                LoadClassInfo();
                btnUpdate.Visible = false;
            }
        }

        #region Load Primary Data
        private void LoadSectionInfo()
        {
            DataTable dtSubject = PopulateLists.GetSectionListByClass();
            if (dtSubject.Rows.Count > 0)
            {
                lblRecordCount.Text = dtSubject.Rows.Count.ToString();
                gvSection.DataSource = dtSubject;
                gvSection.DataBind();
            }
            else
            {
                lblRecordCount.Text = "0";
                gvSection.DataSource = null;
                gvSection.DataBind();
            }
        }

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
        #endregion

        #region Button Click Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ValidationProcess result = IsValidationPassed();
            if (result.IsValid == true)
            {
                Hashtable _param = new Hashtable();
                _param.Add("ClassID", ddlClassName.SelectedValue.Trim());
                _param.Add("SectionName", txtSectionName.Text.Trim());
                _param.Add("SectionNameBangla", txtSectionNameBangla.Text.Trim());
                _param.Add("SectionNameAlias", txtSectionNameAlias.Text.ToString());
                _param.Add("EntryBy", user.UserID.ToString());
                _param.Add("Action", "Create");

                _writer = new DataWriter();
                long affRow = _writer.ExecuteStoredProcedure("SP_SECTION", _param, "AffRow");
                if (affRow > 0)
                {
                    ActivityLog.SaveProcess(ActionType.Insert, "SP_SECTION", "Create", affRow.ToString());
                    DisplayMessage("Section has been save successful");

                    ClearControls();
                    LoadSectionInfo();
                }
                else
                {
                    DisplayMessage("Section has been save failed. Please try again");
                }
            }
            else
            {
                DisplayMessage(result.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ValidationProcess result = IsValidationPassed();
            if (result.IsValid == true)
            {
                Hashtable _param = new Hashtable();
                _param.Add("SectionID", hdSectionId.Value.Trim());
                _param.Add("ClassID", ddlClassName.SelectedValue.Trim());
                _param.Add("SectionName", txtSectionName.Text.Trim());
                _param.Add("SectionNameBangla", txtSectionNameBangla.Text.Trim());
                _param.Add("SectionNameAlias", txtSectionNameAlias.Text.ToString());
                _param.Add("EntryBy", user.UserID.ToString());
                _param.Add("Action", "Update");

                _writer = new DataWriter();
                long affRow = _writer.ExecuteStoredProcedure("SP_SECTION", _param, "AffRow");
                if (affRow > 0)
                {
                    ActivityLog.SaveProcess(ActionType.Update, "SP_SECTION", "Update", hdSectionId.Value.Trim());
                    DisplayMessage("Section has been update successful");

                    LoadSectionInfo();
                    ClearControls();
                }
                else
                {
                    DisplayMessage("Section has been update failed. Please try again");
                }
            }
            else
            {
                DisplayMessage(result.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvSection_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdSectionId.Value = gvRow.Cells[0].Text;
                    ddlClassName.SelectedIndex = ddlClassName.Items.IndexOf(ddlClassName.Items.FindByText(gvRow.Cells[1].Text));
                    txtSectionName.Text = gvRow.Cells[2].Text.Replace("&nbsp;","");
                    txtSectionNameBangla.Text = gvRow.Cells[3].Text.Replace("&nbsp;", "");
                    txtSectionNameAlias.Text = gvRow.Cells[4].Text.Replace("&nbsp;", "");
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (e.CommandName == "RemoveRow")
                {
                    _writer = new DataWriter();
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("SectionID", gvRow.Cells[0].Text);
                    _param.Add("EntryBy", user.UserName.ToString());
                    _param.Add("Action", "Delete");

                    long affRow = _writer.ExecuteStoredProcedure("SP_SECTION", _param, "AffRow");
                    if (affRow > 0)
                    {
                        LoadSectionInfo();

                        ActivityLog.SaveProcess(ActionType.Inactive, "SP_SECTION", "Delete", gvRow.Cells[0].Text);
                        DisplayMessage("Class section has been delete successful");
                    }
                    else
                    {
                        DisplayMessage("Class section delete failed. Please try again");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvSection_RowCommand", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void gvSection_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadSectionInfo();
            gvSection.PageIndex = e.NewPageIndex;
            gvSection.DataBind();
        }
        #endregion

        #region Others Code
        public void ClearControls()
        {
            hdSectionId.Value = String.Empty;
            ddlClassName.SelectedIndex = -1;
            txtSectionName.Text = String.Empty;
            txtSectionNameBangla.Text = String.Empty;
            txtSectionNameAlias.Text = String.Empty;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        protected ValidationProcess IsValidationPassed()
        {
            ValidationProcess result = new ValidationProcess();
            if (String.IsNullOrEmpty(ddlClassName.Text))
            {
                ddlClassName.Focus();
                result.IsValid = false;
                result.Message = "Please Select Class.";
                return result;
            }
            else if (String.IsNullOrEmpty(ddlClassName.Text))
            {
                txtSectionName.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Section Name.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtSectionName.Text))
            {
                txtSectionName.Focus();
                result.IsValid = false;
                result.Message = "Section Name should only contain letters.";
                return result;
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
                    gvSection.DataSource = dtSubject;
                    gvSection.DataBind();
                }
                else
                {
                    gvSection.DataSource = null;
                    gvSection.DataBind();
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
            LoadSectionInfo();
        }
        #endregion
    }
}