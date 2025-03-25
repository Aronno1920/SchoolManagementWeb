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
    public partial class StudentList : System.Web.UI.Page
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
                divDetails.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divSearchPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
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
        #endregion

        #region Button Click Events
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadStudentListInfo();
        }
        
        protected void btnClear_Click(object sender, EventArgs e)
        {
            LoadClassInfo();

            ddlClass.SelectedIndex = -1;
            txtSearchBy.Text = String.Empty;

            ddlSection.DataSource = null;
            ddlSection.DataBind();

            gvStudentList.DataSource = null;
            gvStudentList.DataBind();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSectionInfo(ddlClass.SelectedValue.ToString());
        }

        protected void gvStudentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStudentList.PageIndex = e.NewPageIndex;
            LoadStudentListInfo();
        }

        #endregion Button Click Events

        #region Others Code
        private void LoadStudentListInfo()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("ClassId", ddlClass.SelectedValue);
                _param.Add("SectionId", ddlSection.SelectedValue);
                _param.Add("SearchBy", txtSearchBy.Text);
                _param.Add("Action", "AdvanceSearch");

                _reader = new DataReader();
                DataTable dtStudent = _reader.GetDataTableByStoredProcedure("SP_STUDENT", _param);
                if (dtStudent.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtStudent.Rows.Count.ToString();
                    gvStudentList.DataSource = dtStudent;
                    gvStudentList.DataBind();
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvStudentList.DataSource = null;
                    gvStudentList.DataBind();

                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSearch_OnClick", ex);
                DisplayMessage("Candidate profile has created fail. " + ex.Message);
            }
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

        protected void btnStudentEntry_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StudentEntry.aspx", false);
        }

        protected void gvStudentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                String sStudentCode = gvRow.Cells[2].Text.Replace("&nbsp;", "");

                Session["SCode"] = sStudentCode;
                Response.Redirect("~/StudentEdit.aspx", false);
            }
        }
    }
}