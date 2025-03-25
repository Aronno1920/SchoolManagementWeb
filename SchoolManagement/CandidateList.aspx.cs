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
    public partial class CandidateList : System.Web.UI.Page
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
                divSearchPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divDetailsPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadAdmissionYear();
                LoadPreviousSchoolInfo();
                LoadCandidateInfo();
            }
        }

        #region Load Primary Data
        private void LoadCandidateInfo()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("AdmissionYear", ddlAdmissionYear.SelectedValue);
                _param.Add("SchoolId", ddlPreviousSchool.SelectedValue.Trim());
                _param.Add("SearchBy", txtGridSearch.Text.Trim());
                _param.Add("Action", "AdvanceSearch");

                _reader = new DataReader();
                DataTable dtSubject = _reader.GetDataTableByStoredProcedure("SP_CANDIDATE", _param);
                if (dtSubject.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtSubject.Rows.Count.ToString();
                    gvCandidate.DataSource = dtSubject;
                    gvCandidate.DataBind();
                }
                else
                {
                    gvCandidate.DataSource = null;
                    gvCandidate.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadCandidateInfo", ex);
                DisplayMessage("An error occured! ", ex.Message.ToString());
            }
        }

        private void LoadAdmissionYear()
        {
            DataTable dtAdmissionYear = PopulateLists.GetSessionList();
            if (dtAdmissionYear.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtAdmissionYear, ddlAdmissionYear, "SessionTitle", "SessionYear");
                ddlAdmissionYear.SelectedIndex = 0;
            }
            else
            {
                ddlAdmissionYear.DataSource = null;
                ddlAdmissionYear.DataBind();
            }
        }

        private void LoadPreviousSchoolInfo()
        {
            DataTable dtSchool = PopulateLists.GetSchoolList();
            if (dtSchool.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtSchool, ddlPreviousSchool, "SchoolName", "SchoolId", true, "Select Any School", "0");
            }
            else
            {
                ddlPreviousSchool.DataSource = null;
                ddlPreviousSchool.DataBind();
            }
        }
        #endregion

        #region Search Box Related Methods
        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            LoadCandidateInfo();
        }

        protected void btnGridSearchClear_Click(object sender, EventArgs e)
        {
            ddlPreviousSchool.SelectedIndex = -1;
            txtGridSearch.Text = String.Empty;
            LoadCandidateInfo();
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

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }

        protected void gvCandidate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCandidate.PageIndex = e.NewPageIndex;
            LoadCandidateInfo();
        }

        //private void ExporttoExcel(DataTable table)
        //{
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.ClearContent();
        //    HttpContext.Current.Response.ClearHeaders();
        //    HttpContext.Current.Response.Buffer = true;
        //    HttpContext.Current.Response.ContentType = "application/ms-excel";
        //    HttpContext.Current.Response.Write("@");
        //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename = Reports.xls");
        //    HttpContext.Current.Response.Charset = "utf-8";
        //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //    //sets font
        //    HttpContext.Current.Response.Write("<font style="font-size:10.0pt; font-family:Calibri; ">");
        //    HttpContext.Current.Response.Write("<br><br><br>");
        //    //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        //    HttpContext.Current.Response.Write("<table border="1" bgcolor="#ffffff";bordercolor="#000000" cellspacing="0" cellpadding="0" style="font-size:10.0pt; font-family:Calibri; background:white;"> <tr>");
        //////am getting my grid's column headers
        ////int columnscount = g .Columns.Count;

        //    //for (int j = 0; j < columnscount; j++)
        //    //{      //write in new column
        //    //    HttpContext.Current.Response.Write("<td>");
        //    //    //Get column headers  and make it as bold in excel columns
        //    //    HttpContext.Current.Response.Write("");
        //    //    HttpContext.Current.Response.Write(GridView_Result.Columns[j].HeaderText.ToString());
        //    //    HttpContext.Current.Response.Write("");
        //    //    HttpContext.Current.Response.Write("</td>");
        //    //}
        //    HttpContext.Current.Response.Write("</tr>");
        //    foreach (DataRow row in table.Rows)
        //    {//write in new row
        //        HttpContext.Current.Response.Write("<tr>");

        //        HttpContext.Current.Response.Write("<td>");
        //        HttpContext.Current.Response.Write(row[0].ToString());
        //        HttpContext.Current.Response.Write("</td>");
        //        HttpContext.Current.Response.Write("<td");
        //        HttpContext.Current.Response.Write(row[1].ToString());
        //        HttpContext.Current.Response.Write("</td>");
        //        HttpContext.Current.Response.Write("<td>");
        //        HttpContext.Current.Response.Write(row[2].ToString());
        //        HttpContext.Current.Response.Write("</td>");
        //        HttpContext.Current.Response.Write("<td");
        //        HttpContext.Current.Response.Write(row[3].ToString());
        //        HttpContext.Current.Response.Write("</td>");
        //        HttpContext.Current.Response.Write("<td>");
        //        HttpContext.Current.Response.Write(row[4].ToString());
        //        HttpContext.Current.Response.Write("</td>");
        //        HttpContext.Current.Response.Write("<td>");
        //        HttpContext.Current.Response.Write(row[5].ToString());
        //        HttpContext.Current.Response.Write("</td>");

        //        HttpContext.Current.Response.Write("</tr>");
        //    }
        //    HttpContext.Current.Response.Write("</table>");
        //    HttpContext.Current.Response.Write("</br></br></br></font>");
        //    HttpContext.Current.Response.Flush();
        //    HttpContext.Current.Response.End();
        //}
    }
}