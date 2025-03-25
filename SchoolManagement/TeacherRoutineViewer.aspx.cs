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
    public partial class TeacherRoutineViewer : System.Web.UI.Page
    {
        Cookie user = new Cookie();
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

                LoadTeacherInfo();
            }
        }

        #region Load Primary Data

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

        private void LoadRoutineInfo()
        {
            DataTable dtRoutine = PopulateLists.GetRoutineByTeacher(ddlTeacher.SelectedValue);
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
        #endregion
    }
}