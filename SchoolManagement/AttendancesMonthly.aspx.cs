using CoreLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class AttendancesMonthly : System.Web.UI.Page
    {
        Cookie user = new Cookie();
        DataReader _reader = new DataReader();
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

        #region Load Primary Data and Others Code

        private void LoadDateControl()
        {
            DateTime nowDate = DateTime.Now;
            txtStartDate.Text = nowDate.ToString("01-MMM-yyyy");
            txtEndDate.Text = nowDate.ToString("dd-MMM-yyyy");
        }

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

        public void ClearControls()
        {
            ddlClass.SelectedIndex = -1;
            ddlSection.SelectedIndex = -1;
        }

        public void ExportGridviewToExcel(String sFileName)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + sFileName.ToString() + "");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    gvAttendance.AllowPaging = false;

                    foreach (TableCell cell in gvAttendance.HeaderRow.Cells)
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#bcd2ee");
                        cell.ForeColor = Color.Black;
                        cell.Font.Size = FontUnit.Small;
                        cell.Font.Bold = true;
                        cell.Font.Name = "Tahoma";
                        cell.Wrap = true;
                    }
                    foreach (GridViewRow row in gvAttendance.Rows)
                    {
                        row.BackColor = Color.White;

                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex == gvAttendance.Rows.Count-1)
                            {
                                cell.BackColor = ColorTranslator.FromHtml("#FFFAC6");
                                cell.ForeColor = Color.Black;
                                cell.Font.Size = FontUnit.Small;
                                cell.Font.Bold = true;
                                cell.Font.Name = "Tahoma";
                                cell.Wrap = true;
                            }
                        }
                    }

                    gvAttendance.RenderControl(hw);
                    //Response.Write("Monthly Attendance Sheet");
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "ExportGridviewToExcel", ex);
                DisplayMessage(ex.Message.ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected DataTable CountTotalPresentAbsent(DataTable dtAttendance)
        {
            DataTable finalData = dtAttendance;
            Int32 iColumn = finalData.Columns.Count;

            #region Add 3 Column - Calculate student wise total Present,Leave,Absent
            finalData.Columns.Add("Present", typeof(string));
            finalData.Columns.Add("Leave", typeof(string));
            finalData.Columns.Add("Absent", typeof(string));
            foreach (DataRow row in finalData.Rows)
            {
                Int32 iPresent = 0;
                Int32 iLeave = 0;
                Int32 iAbsent = 0;

                for (Int32 i = 3; i <= iColumn; i++)
                {
                    if (row[i].ToString() == "P")
                    {
                        iPresent++;
                    }
                    else if (row[i].ToString() == "L")
                    {
                        iLeave++;
                    }
                    else if (row[i].ToString() == "A")
                    {
                        iAbsent++;
                    }
                }
                row["Present"] = iPresent;
                row["Leave"] = iLeave;
                row["Absent"] = iAbsent;
            }
            #endregion

            #region Add 1 Row - Calculate daily total Present student count
            //Add 3 0 and Calculate Total Present,Leave,Absent
            DataRow dr = finalData.NewRow();
            dr[0] = "";
            dr[1] = "";
            dr[2] = "Total Present Student";
            finalData.Rows.Add(dr);

            for (Int32 cIndex = 3; cIndex <= iColumn; cIndex++)
            {
                Int32 iPresent = 0;
                DataColumn column = finalData.Columns[cIndex];

                foreach (DataRow row in finalData.Rows)
                {
                    if (row[column].ToString() == "P")
                        iPresent++;
                }
                dr[column] = iPresent;
            }
            #endregion

            return finalData;
        }
        #endregion

        #region Button Click Events

        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("ClassId", ddlClass.SelectedValue);
                _param.Add("SectionId", ddlSection.SelectedValue);
                _param.Add("StartDate", txtStartDate.Text);
                _param.Add("EndDate", txtEndDate.Text);

                _reader = new DataReader();
                DataTable dtAttendance = _reader.GetDataTableByStoredProcedure("SP_ATTENDANCE_MONTHLY", _param);
                DataTable finalData = CountTotalPresentAbsent(dtAttendance);

                if (finalData.Rows.Count > 0)
                {
                    lblRecordCount.Text = finalData.Rows.Count.ToString();
                    gvAttendance.DataSource = finalData;
                    gvAttendance.DataBind();

                    GridViewRow lastrow = gvAttendance.Rows[(gvAttendance.Rows.Count) - 1];
                    lastrow.BackColor = System.Drawing.Color.FromName("#FFFAC6");
                    for (int i = 0; i < lastrow.Cells.Count; i++)
                    {
                        lastrow.Cells[i].Font.Bold = true;
                    }
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvAttendance.DataSource = null;
                    gvAttendance.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnLoadData_Click", ex);
            }
        }

        protected void btnExportData_Click(object sender, EventArgs e)
        {
            DateTime dStartDate = Convert.ToDateTime(txtStartDate.Text);
            String fileName = "Monthly_" + dStartDate.ToString("yyyy") + "_" + dStartDate.ToString("MMM") + "_"+ddlClass.SelectedItem.Text+ "_" + ddlSection.SelectedItem.Text + ".xls";

            ExportGridviewToExcel(fileName);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
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

        protected void btnDaily_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AttendancesDaily.aspx", false);
        }
    }
}