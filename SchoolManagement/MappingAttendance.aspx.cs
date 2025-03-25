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
    public partial class MappingAttendance : System.Web.UI.Page
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
                LoadTeacherInfo();
                LoadAttendanceOfficeList();
            }
        }

        #region Load Primary Data &  Others Code

        private void LoadClassInfo()
        {
            DataTable dtClasss = PopulateLists.GetClassList();
            if (dtClasss.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtClasss, ddlClass, "ClassName", "ClassId", true, "Select Class", "0");
                FillList.PopulateDropDownList(dtClasss, ddlGridSearch, "ClassName", "ClassId", true, "Select Class", "0");
            }
            else
            {
                ddlClass.DataSource = null;
                ddlClass.DataBind();

                ddlGridSearch.DataSource = null;
                ddlGridSearch.DataBind();
            }
        }

        private void LoadTeacherInfo()
        {
            DataTable dtTeacher = PopulateLists.GetUserList();
            if (dtTeacher.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtTeacher, ddlOfficer, "EmployeeName", "EmployeeId", true, "Select Any", "0");
            }
            else
            {
                ddlOfficer.DataSource = null;
                ddlOfficer.DataBind();
            }
        }

        private void LoadAttendanceOfficeList()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "AttendanceOfficerList");

                _reader = new DataReader();
                DataTable dtTeacher = _reader.GetDataTableByStoredProcedure("SP_ATTENDANCE_OFFICER", _param);
                if (dtTeacher.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtTeacher.Rows.Count.ToString();
                    gvMapping.DataSource = dtTeacher;
                    gvMapping.DataBind();
                }
                else
                {
                    gvMapping.DataSource = null;
                    gvMapping.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadAttendanceOfficeList", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        public void ClearControls()
        {
            ddlClass.SelectedIndex = -1;
            ddlSection.SelectedIndex = -1;
            ddlOfficer.SelectedIndex = -1;
        }
        #endregion

        #region Button Click Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("TeacherId", ddlOfficer.SelectedValue);
                _param.Add("ClassId", ddlClass.SelectedValue);
                _param.Add("SectionId", ddlSection.SelectedValue);
                _param.Add("EntryBy", user.UserID.ToString());
                _param.Add("Action", "SaveOfficer");

                _writer = new DataWriter();
                long affRow = _writer.ExecuteStoredProcedure("SP_ATTENDANCE_OFFICER", _param, "AffRow");
                if (affRow > 0)
                {
                    ActivityLog.SaveProcess(ActionType.Insert, "SP_ATTENDANCE_OFFICER", "SaveOfficer", affRow.ToString());
                    DisplayMessage("Attendance Officer has been save Successful");
                    LoadAttendanceOfficeList();
                    ClearControls();
                }
                else
                {
                    DisplayMessage("Attendance Officer save failed. Please try again");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvMapping_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("RuleId", gvRow.Cells[0].Text);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "DeleteOfficer");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_ATTENDANCE_OFFICER", _param, "AffRow");
                    if (affRow > 0)
                    {
                        DisplayMessage("Attendance Officer has been delete successful");
                        ActivityLog.SaveProcess(ActionType.Delete, "SP_ATTENDANCE_OFFICER", "DeleteOfficer", affRow.ToString());
                        LoadAttendanceOfficeList();
                    }
                    else
                    {
                        DisplayMessage("Attendance Officer delete failed. Please try again");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvMapping_RowCommand", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void gvMapping_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMapping.PageIndex = e.NewPageIndex;
            gvMapping.DataBind();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtSection = PopulateLists.GetSectionListByClass(ddlClass.SelectedValue);
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
        
        #endregion Button Click Events

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