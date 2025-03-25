using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreLibrary;

namespace SchoolManagement
{
    public partial class PasswordReset : System.Web.UI.Page
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
                divDetailsPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divEntryPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                sPageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                txtPassword.Attributes["value"] = txtPassword.Text;
                txtConfirmPassword.Attributes["value"] = txtConfirmPassword.Text;

                LoadEmployeeList();
            }
        }

        #region Load Primary Data

        protected void LoadEmployeeList()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ReadEmployeeList");

                _reader = new DataReader();
                DataTable dsEmployee = _reader.GetDataTableByStoredProcedure("SP_EMPLOYEE_INFO", _param);

                if (dsEmployee.Rows.Count > 0)
                {
                    lblRowCount.Text = dsEmployee.Rows.Count.ToString();
                    gvEmployeeList.DataSource = dsEmployee;
                    gvEmployeeList.DataBind();
                }
                else
                {
                    lblRowCount.Text = "0";
                    gvEmployeeList.DataSource = null;
                    gvEmployeeList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadEmployeeList", ex);
                DisplayMessage("Emmployee List can't loaded. " + ex.Message);
            }
        }

        protected void LoadEmployeeProfile()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("EmployeeId", hdEmployeeID.Value);
                _param.Add("Action", "LoadProfile");

                _reader = new DataReader();
                DataTable dtProfile = _reader.GetDataTableByStoredProcedure("SP_MANAGE_PASSWORD", _param);
                if (dtProfile.Rows.Count > 0)
                {
                    txtEmployeeName.Text = dtProfile.Rows[0]["EmployeeName"].ToString();
                    txtEmployeeNID.Text = dtProfile.Rows[0]["EmployeeNID"].ToString();
                    txtIndexNo.Text = dtProfile.Rows[0]["IndexNumber"].ToString();
                    txtFatherName.Text = dtProfile.Rows[0]["FatherName"].ToString();
                    txtMotherName.Text = dtProfile.Rows[0]["MotherName"].ToString();
                    txtContactNo.Text = dtProfile.Rows[0]["ContactNo"].ToString();
                    txtLoginID.Text = dtProfile.Rows[0]["LoginId"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadEmployeeProfile", ex);
                DisplayMessage("Emmployee profile load failed. " + ex.Message);
            }
            // 
        }
        #endregion

        #region Button Click Events

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    String ePassword = UtilityClass.Encrypt(txtPassword.Text.ToString(), true);

                    Hashtable _param = new Hashtable();
                    _param.Add("EmployeeId", hdEmployeeID.Value.Trim());
                    _param.Add("Password", ePassword);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "ChangePassword");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_MANAGE_PASSWORD", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Update, "SP_MANAGE_PASSWORD", "ChangePassword", hdEmployeeID.Value);
                        DisplayMessage("Login Password has been saved successful.");
                    }
                    else
                    {
                        DisplayMessage("Login Password has been save failed. Please try again.");
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
                DisplayMessage("Emmployee created failed. " + ex.Message);
            }
        }

        protected void gvEmployeeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdEmployeeID.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");

                LoadEmployeeProfile();
            }
        }
        #endregion

        #region Other Methods / Clear Methods

        public void ClearControls()
        {
            txtEmployeeName.Text = String.Empty;
            txtEmployeeNID.Text = String.Empty;
            txtFatherName.Text = String.Empty;
            txtContactNo.Text = String.Empty;
        }

        protected ValidationProcess IsValidationPassed()
        {
            ValidationProcess result = new ValidationProcess();
            if (String.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                txtPassword.Focus();
                result.IsValid = false;
                result.Message = "Password can not be empty.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                txtConfirmPassword.Focus();
                result.IsValid = false;
                result.Message = "Confirm password can not be empty.";
                return result;
            }
            else if (!txtPassword.Text.Equals(txtConfirmPassword.Text.Trim()))
            {
                txtConfirmPassword.Focus();
                result.IsValid = false;
                result.Message = "Confirm password dose not match with new password. Enter correct confirm password.";
                return result;
            }

            return result;
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