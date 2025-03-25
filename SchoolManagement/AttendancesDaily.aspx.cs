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
    public partial class AttendancesDaily : System.Web.UI.Page
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

                LoadDateControl();
                LoadClassInfo();
            }
        }

        #region Load Primary Data
        private void LoadDateControl()
        {
            DateTime nowDate = DateTime.Now;
            txtDate.Text = nowDate.ToString("dd-MMM-yyyy");
        }

        private void LoadClassInfo()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("TeacherId", user.UserID.ToString());
                _param.Add("Action", "PermittedClass");

                _reader = new DataReader();
                DataTable dtClasss = _reader.GetDataTableByStoredProcedure("SP_ATTENDANCE_DAILY", _param);
                if (dtClasss.Rows.Count > 0)
                {
                    FillList.PopulateDropDownList(dtClasss, ddlClass, "ClassName", "ClassId", true, "Select Class", "0");
                }
                else
                {
                    ddlClass.DataSource = null;
                    ddlClass.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadClassInfo", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        #endregion

        #region Button Click & Selected Index Changed Events
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadStudentInfo();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    foreach (GridViewRow gvRow in gvStudentList.Rows)
                    {
                        Boolean IsChecked = ((CheckBox)gvRow.FindControl("cbIsPresent")).Checked;

                        if (IsChecked)
                        {
                            Hashtable _param = new Hashtable();
                            _param.Add("StudentId", gvRow.Cells[1].Text);
                            _param.Add("Date", txtDate.Text.Trim());
                            _param.Add("EntryBy", user.UserID.ToString());
                            _param.Add("Action", "AttendStudents");

                            _writer = new DataWriter();
                            _writer.ExecuteStoredProcedure("SP_ATTENDANCE_DAILY", _param);
                        }
                    }
                    ActivityLog.SaveProcess(ActionType.Insert, "SP_ATTENDANCE_DAILY", "AttendStudents", "");
                    DisplayMessage("Daily Attendance has been process successful");
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("TeacherId", user.UserID.ToString());
                _param.Add("ClassId", ddlClass.SelectedValue.ToString());
                _param.Add("Action", "PermittedSection");

                _reader = new DataReader();
                DataTable dtSection = _reader.GetDataTableByStoredProcedure("SP_ATTENDANCE_DAILY", _param);
                if (dtSection.Rows.Count > 0)
                {
                    FillList.PopulateDropDownList(dtSection, ddlSection, "SectionName", "SectionId", true, "Select Section", "0");
                }
                else
                {
                    ddlSection.DataSource = null;
                    ddlSection.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "ddlClass_SelectedIndexChanged", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllControls();
        }
        #endregion

        #region Validateion and Others Methods

        protected void btnMonthly_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AttendancesMonthly.aspx", false);
        }

        private void LoadStudentInfo()
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("ClassId", ddlClass.SelectedValue);
                    _param.Add("SectionId", ddlSection.SelectedValue);
                    _param.Add("Action", "LoadStudents");

                    _reader = new DataReader();
                    DataTable dtStudents = _reader.GetDataTableByStoredProcedure("SP_ATTENDANCE_DAILY", _param);
                    if (dtStudents.Rows.Count > 0)
                    {
                        lblRecordCount.Text = dtStudents.Rows.Count.ToString();
                        gvStudentList.DataSource = dtStudents;
                        gvStudentList.DataBind();
                    }
                    else
                    {
                        lblRecordCount.Text = "0";
                        gvStudentList.DataSource = null;
                        gvStudentList.DataBind();

                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadStudentInfo", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected ValidationProcess IsValidationPassed()
        {
            ValidationProcess result = new ValidationProcess();
            if (String.IsNullOrEmpty(ddlClass.SelectedValue))
            {
                ddlClass.Focus();
                result.IsValid = false;
                result.Message = "Please contact with software administrator.";
                return result;
            }
            else if (ddlClass.SelectedValue == "0")
            {
                ddlClass.Focus();
                result.IsValid = false;
                result.Message = "Please Select Any Class.";
                return result;
            }

            return result;
        }

        protected void ClearAllControls()
        {
            ddlClass.SelectedIndex = -1;
            ddlSection.SelectedIndex = -1;

            gvStudentList.DataSource = null;
            gvStudentList.DataBind();
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