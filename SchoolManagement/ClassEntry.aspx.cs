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
    public partial class ClassEntry : System.Web.UI.Page
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
                btnUpdate.Visible = false;
            }
        }

        #region Button Click Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("ClassName", txtClassName.Text);
                    _param.Add("ClassNameBangla", txtClassNameBangla.Text);
                    _param.Add("ClassNameAlias", txtClassAlias.Text);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "Create");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_CLASS", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_CLASS", "Create", affRow.ToString());
                        DisplayMessage("Class has been saved successful.");

                        ClearControls();
                        LoadClassInfo();
                    }
                    else
                    {
                        DisplayMessage("Class save failed. Please try again");
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
                DisplayMessage("Opps! An error occurred on Class Save", ex.Message);
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
                    _param.Add("ClassId", hdClassId.Value.Trim());
                    _param.Add("ClassName", txtClassName.Text);
                    _param.Add("ClassNameBangla", txtClassNameBangla.Text);
                    _param.Add("ClassNameAlias", txtClassAlias.Text);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "Update");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_CLASS", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Update, "SP_CLASS", "Update", hdClassId.Value.Trim());
                        DisplayMessage("Class update Successful");

                        ClearControls();
                        LoadClassInfo();
                    }
                    else
                    {
                        DisplayMessage("Class update failed. Please try again");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnUpdate_Click", ex);
                DisplayMessage("Opps! An error occurred on Class Update", ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvClass_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdClassId.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");
                    txtClassName.Text = gvRow.Cells[1].Text.Replace("&nbsp;", "");
                    txtClassNameBangla.Text = gvRow.Cells[2].Text.Replace("&nbsp;", "");
                    txtClassAlias.Text = gvRow.Cells[3].Text.Replace("&nbsp;", "");
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (e.CommandName == "RemoveRow")
                {
                    _writer = new DataWriter();
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("ClassId", gvRow.Cells[0].Text);
                    _param.Add("EntryBy", user.UserName.ToString());
                    _param.Add("Action", "Delete");

                    long affRow = _writer.ExecuteStoredProcedure("SP_CLASS", _param, "AffRow");
                    if (affRow > 0)
                    {
                        LoadClassInfo();

                        ActivityLog.SaveProcess(ActionType.Inactive, "SP_CLASS", "Delete", gvRow.Cells[0].Text);
                        DisplayMessage("Class has been deleted successfully.");
                    }
                    else
                    {
                        DisplayMessage("Opps! An error occurred on delete");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadBatch", ex);
                DisplayMessage("Contact to Software Administrator", ex.Message);
            }
        }

        protected void gvClass_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClass.PageIndex = e.NewPageIndex;
            LoadClassInfo();
        }

        #endregion Button Click Events

        #region Others Code
        public void ClearControls()
        {
            hdClassId.Value = String.Empty;
            txtClassName.Text = String.Empty;
            txtClassNameBangla.Text = String.Empty;
            txtClassAlias.Text = String.Empty;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        private void LoadClassInfo()
        {
            DataTable dtClass = PopulateLists.GetClassList();
            if (dtClass.Rows.Count > 0)
            {
                lblRecordCount.Text = dtClass.Rows.Count.ToString();
                gvClass.DataSource = dtClass;
                gvClass.DataBind();
            }
            else
            {
                lblRecordCount.Text = "0";
                gvClass.DataSource = null;
                gvClass.DataBind();
            }
        }

        protected ValidationProcess IsValidationPassed()
        {
            ValidationProcess result = new ValidationProcess();
            if (String.IsNullOrEmpty(txtClassName.Text))
            {
                txtClassName.Focus();
                result.IsValid = false;
                result.Message = "Please enter Class Name.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtClassName.Text))
            {
                txtClassName.Focus();
                result.IsValid = false;
                result.Message = "Class Name should only contain letters.";
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

    }
}