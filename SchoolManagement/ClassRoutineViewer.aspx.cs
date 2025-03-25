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
    public partial class ClassRoutineViewer : System.Web.UI.Page
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

                LoadClassInfo();
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

        private void LoadRoutineBanglaInfo()
        {
            DataTable dtRoutine = PopulateLists.GetRoutineBanglaByClassSection(ddlClass.SelectedValue, ddlSection.SelectedValue);
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
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSectionInfo(ddlClass.SelectedValue.ToString());
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadRoutineInfo();
        }

        protected void btnLoadBangla_Click(object sender, EventArgs e)
        {
            LoadRoutineBanglaInfo();
        }
        #endregion
    }
}