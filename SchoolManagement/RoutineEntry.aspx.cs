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
    public partial class RoutineEntry : System.Web.UI.Page
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
                LoadDaysInfo();
                LoadTeacherInfo();
                LoadClassOrderInfo();
            }
        }

        #region Load Primary Data

        private void LoadClassInfo()
        {
            DataTable dtClasss = PopulateLists.GetClassList();
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

        private void LoadDaysInfo()
        {
            DataTable dtClasss = PopulateLists.GetDayList();
            if (dtClasss.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtClasss, ddlDay, "DayName", "DayId", true, "Select Day", "0");
            }
            else
            {
                ddlDay.DataSource = null;
                ddlDay.DataBind();
            }
        }

        private void LoadTeacherInfo()
        {
            DataTable dtTeacher = PopulateLists.GetTeacherList();
            if (dtTeacher.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtTeacher, ddlTeacher, "TeacherName", "TeacherId", true, "Select teacher", "0");
            }
            else
            {
                ddlTeacher.DataSource = null;
                ddlTeacher.DataBind();
            }
        }

        private void LoadClassOrderInfo()
        {
            DataTable dtOrder = PopulateLists.GetDurationList();
            if (dtOrder.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtOrder, ddlOrder, "DurationName", "DurationId", true, "Select Duration", "0");
            }
            else
            {
                ddlOrder.DataSource = null;
                ddlOrder.DataBind();
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

        private void LoadSubjectInfo(String sClassId)
        {
            DataTable dtSubject = PopulateLists.GetSubjectListByClass(sClassId);
            if (dtSubject.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtSubject, ddlSubject, "SubjectName", "SubjectId", true, "Select subject", "0");
            }
            else
            {
                ddlSubject.DataSource = null;
                ddlSubject.DataBind();
            }
        }

        private void LoadRoutineInfo()
        {
            DataTable dtRoutine = PopulateLists.GetRoutineInfoByClassSection(ddlClass.SelectedValue, ddlSection.SelectedValue);
            if (dtRoutine.Rows.Count > 0)
            {
                gvRoutine.DataSource = dtRoutine;
                gvRoutine.DataBind();
            }
            else
            {
                gvRoutine.DataSource = null;
                gvRoutine.DataBind();
            }
        }
        #endregion

        #region Button Click Events
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadRoutineInfo();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckTeacherAvailability())
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("ClassId", ddlClass.SelectedValue.ToString());
                    _param.Add("SectionId", ddlSection.SelectedValue.ToString());
                    _param.Add("DayId", ddlDay.SelectedValue.ToString());
                    _param.Add("OrderId", ddlOrder.SelectedValue.ToString());
                    _param.Add("SubjectId", ddlSubject.SelectedValue.ToString());
                    _param.Add("EmployeeID", ddlTeacher.SelectedValue.ToString());
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "Create");

                    _writer = new DataWriter();
                    _writer.ExecuteStoredProcedure("SP_ROUTINE", _param);

                    ActivityLog.SaveProcess(ActionType.Insert, "SP_ROUTINE", "Create", ddlClass.SelectedValue.ToString());
                    ClearControls();
                    LoadRoutineInfo();
                    DisplayMessage("Class Routine Save Successful");
                }

            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
                DisplayMessage("Class Routine Save failed. " + ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("ClassId", ddlClass.SelectedValue.ToString());
                _param.Add("SectionId", ddlSection.SelectedValue.ToString());
                _param.Add("DayId", ddlDay.SelectedValue.ToString());
                _param.Add("OrderId", ddlOrder.SelectedValue.ToString());
                _param.Add("Action", "Delete");

                _writer = new DataWriter();
                _writer.ExecuteStoredProcedure("SP_ROUTINE", _param);

                ActivityLog.SaveProcess(ActionType.Delete, "SP_ROUTINE", "Delete", ddlClass.SelectedValue.ToString());
                LoadRoutineInfo();
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSearch_OnClick", ex);
                DisplayMessage("Candidate profile has created fail. " + ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllControls();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSectionInfo(ddlClass.SelectedValue.ToString());
            LoadSubjectInfo(ddlClass.SelectedValue.ToString());
        }
        #endregion

        #region Others Code
        public void ClearControls()
        {
            ddlOrder.SelectedIndex = -1;
            ddlSubject.SelectedIndex = -1;
            ddlTeacher.SelectedIndex = -1;
        }

        public void ClearAllControls()
        {
            ddlClass.SelectedIndex = -1;
            ddlOrder.SelectedIndex = -1;
            ddlSection.SelectedIndex = -1;
            ddlSubject.SelectedIndex = -1;
            ddlDay.SelectedIndex = -1;
            ddlTeacher.SelectedIndex = -1;

            gvRoutine.DataSource = null;
            gvRoutine.DataBind();
        }

        private Boolean CheckTeacherAvailability()
        {
            Boolean isValid = false;
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("DayId", ddlDay.SelectedValue.ToString());
                _param.Add("OrderId", ddlOrder.SelectedValue.ToString());
                _param.Add("EmployeeID", ddlTeacher.SelectedValue.ToString());
                _param.Add("Action", "Available");

                _reader = new DataReader();
                DataTable dtInfo = _reader.GetDataTableByStoredProcedure("SP_ROUTINE", _param);
                if (dtInfo.Rows.Count > 0)
                {
                    String sMessage = ddlTeacher.SelectedItem.Text + " has been assigned for "+ dtInfo.Rows[0]["SubjectName"] + " at "+ dtInfo.Rows[0]["ClassName"] +" ("+ dtInfo.Rows[0]["SectionName"] + ")";
                    DisplayMessage(sMessage);
                }
                else
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSearch_OnClick", ex);
                DisplayMessage("CheckTeacherAvailability", ex.Message.ToString());
            }
            return isValid;
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