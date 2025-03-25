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
    public partial class SubjectEntry : System.Web.UI.Page
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

                LoadSubjectInfo();
                LoadClassInfo();
                btnUpdate.Visible = false;
            }
        }

        #region Load Primary Data
        private void LoadSubjectInfo()
        {
            DataTable dtSubject = PopulateLists.GetSubjectListByClass();
            if (dtSubject.Rows.Count > 0)
            {
                lblRecordCount.Text = dtSubject.Rows.Count.ToString();
                gvSubject.DataSource = dtSubject;
                gvSubject.DataBind();
            }
            else
            {
                lblRecordCount.Text = "0";
                gvSubject.DataSource = null;
                gvSubject.DataBind();

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

        #region Button Click and Selected Index Change Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Hashtable _param = new Hashtable();
            _param.Add("ClassID", ddlClassName.SelectedValue.Trim());
            _param.Add("SubjectCode", txtSubjectCode.Text.Trim());
            _param.Add("SubjectNameBD", txtSubjectNameBangla.Text.Trim());
            _param.Add("SubjectNameEN", txtSubjectName.Text.Trim());
            _param.Add("EntryBy", user.UserID.ToString());
            _param.Add("Action", "Create");

            _writer = new DataWriter();
            long affRow = _writer.ExecuteStoredProcedure("SP_SUBJECT", _param, "AffRow");
            if (affRow > 0)
            {
                ActivityLog.SaveProcess(ActionType.Insert, "SP_SUBJECT", "Create", affRow.ToString().Trim());
                DisplayMessage("Subject Save Successful");

                ClearControls();
                LoadSubjectInfo();
            }
            else
            {
                DisplayMessage("Subject save failed. Please try again");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Hashtable _param = new Hashtable();
            _param.Add("SubjectID", hdSubjectId.Value.Trim());
            _param.Add("ClassID", ddlClassName.SelectedValue.Trim());
            _param.Add("SubjectCode", txtSubjectCode.Text.Trim());
            _param.Add("SubjectNameBD", txtSubjectNameBangla.Text.Trim());
            _param.Add("SubjectNameEN", txtSubjectName.Text.Trim());
            _param.Add("EntryBy", user.UserID.ToString());
            _param.Add("Action", "Update");

            _writer = new DataWriter();
            long affRow = _writer.ExecuteStoredProcedure("SP_SUBJECT", _param, "AffRow");
            if (affRow > 0)
            {
                ActivityLog.SaveProcess(ActionType.Update, "SP_SUBJECT", "Update", hdSubjectId.Value.Trim());
                DisplayMessage("Subject update successful");

                LoadSubjectInfo();
                ClearControls();
            }
            else
            {
                DisplayMessage("Subject update failed. Please try again");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvSubject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdSubjectId.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");
                    ddlClassName.SelectedIndex = ddlClassName.Items.IndexOf(ddlClassName.Items.FindByText(gvRow.Cells[1].Text));
                    txtSubjectCode.Text = gvRow.Cells[2].Text.Replace("&nbsp;","");
                    txtSubjectName.Text = gvRow.Cells[3].Text.Replace("&nbsp;", "");
                    txtSubjectNameBangla.Text = gvRow.Cells[4].Text.Replace("&nbsp;", "");

                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (e.CommandName == "RemoveRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("SubjectID", gvRow.Cells[0].Text);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "Delete");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_SUBJECT", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Inactive, "SP_SUBJECT", "Delete", gvRow.Cells[0].Text);
                        LoadSubjectInfo();
                        DisplayMessage("Subject delete successful");
                    }
                    else
                    {
                        DisplayMessage("Subject delete failed. Please try again");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvSubject_RowCommand", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void gvSubject_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadSubjectInfo();
            gvSubject.PageIndex = e.NewPageIndex;
            gvSubject.DataBind();
        }

        #endregion

        #region Other Methods / Clear Methods

        public void ClearControls()
        {
            hdSubjectId.Value = String.Empty;
            ddlClassName.SelectedIndex = -1;
            txtSubjectCode.Text = String.Empty;
            txtSubjectNameBangla.Text = String.Empty;
            txtSubjectName.Text = String.Empty;
            btnSave.Visible = true;
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

        #region Search Box Related Methods
        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _reader = new DataReader();

                Hashtable _param = new Hashtable();
                _param.Add("ClassID", ddlGridSearch.SelectedValue.Trim());
                _param.Add("SearchBy", txtGridSearch.Text.Trim());
                _param.Add("Action", "Search");

                DataTable dtSubject = _reader.GetDataTableByStoredProcedure("SP_SUBJECT", _param);
                if (dtSubject.Rows.Count > 0)
                {
                    gvSubject.DataSource = dtSubject;
                    gvSubject.DataBind();
                }
                else
                {
                    gvSubject.DataSource = null;
                    gvSubject.DataBind();
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
            LoadSubjectInfo();
        }
        #endregion
    }
}