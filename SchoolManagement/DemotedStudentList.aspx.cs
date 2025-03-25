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
    public partial class DemotedStudentList : System.Web.UI.Page
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
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadStudentListInfo();
            }
        }

        #region Button Click Events
        private void LoadStudentListInfo()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "StudentList");

                _reader = new DataReader();
                DataTable dtStudent = _reader.GetDataTableByStoredProcedure("SP_DEMOTED_STUDENT", _param);
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
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadStudentListInfo", ex);
                DisplayMessage("Demoted student not loaded. " + ex.Message);
            }
        }

        protected void gvStudentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlClassName = (e.Row.FindControl("ddlClass") as DropDownList);

                    DataTable dtClasss = PopulateLists.GetClassList();
                    if (dtClasss.Rows.Count > 0)
                    {
                        FillList.PopulateDropDownList(dtClasss, ddlClassName, "ClassName", "ClassId", true, "Select Class", "0");
                    }
                    else
                    {
                        ddlClassName.DataSource = null;
                        ddlClassName.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvStudentList_RowDataBound", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void gvStudentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "StudentPromotion")
                {
                    GridViewRow gvRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    int RowIndex = gvRow.RowIndex;

                    DropDownList ddlClassName = (gvRow.FindControl("ddlClass") as DropDownList);

                    Hashtable _param = new Hashtable();
                    _param.Add("StudentId", gvRow.Cells[1].Text);
                    _param.Add("ClassId", ddlClassName.SelectedValue.ToString());
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "StudentPromotion");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_DEMOTED_STUDENT", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_DEMOTED_STUDENT", "gvStudentList_RowCommand", affRow.ToString().Trim());
                        DisplayMessage("Student has been re-promoted successful");
                        LoadStudentListInfo();
                    }
                    else
                    {
                        DisplayMessage("Student has been re-promoted failed. Please try again");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvStudentList_RowCommand", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
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
    }
}