using CoreLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class AttendancesDetails : System.Web.UI.Page
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

                LoadDateControls();
            }

        }

        #region Button Click Events
        protected void LoadDateControls()
        {
            DateTime nowDate = DateTime.Now;

            txtStartDate.Text = nowDate.ToString("01-MMM-yyyy"); ;
            txtEndDate.Text = nowDate.ToString("dd-MMM-yyyy");
        }
        #endregion

        #region Button Click Events
        protected void btnLoadStudent_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("StudentId", txtStudentCode.Text.Trim());
                _param.Add("Action", "SearchStudent");

                _reader = new DataReader();
                DataTable dtStudent = _reader.GetDataTableByStoredProcedure("SP_LEAVE", _param);
                if (dtStudent.Rows.Count > 0)
                {
                    hdStudentId.Value = dtStudent.Rows[0]["StudentId"].ToString();
                    txtStudentName.Text = dtStudent.Rows[0]["StudentName"].ToString();
                    txtBanglaName.Text = dtStudent.Rows[0]["StudentNameBangla"].ToString();
                    txtClass.Text = dtStudent.Rows[0]["ClassName"].ToString();
                    txtSection.Text = dtStudent.Rows[0]["SectionName"].ToString();
                    txtRollNo.Text = dtStudent.Rows[0]["RollNo"].ToString();
                    txtContact.Text = dtStudent.Rows[0]["ContactNo"].ToString();
                }
                else
                {
                    ClearStudentInformation();
                }

                ClearStudentAttendance();
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSearchStudent_Click", ex);
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("StudentId", hdStudentId.Value.ToString());
                _param.Add("StartDate", txtStartDate.Text.ToString());
                _param.Add("EndDate", txtEndDate.Text.ToString());

                _reader = new DataReader();
                DataSet dtAttendance = _reader.GetDataSetByStoredProcedure("SP_ATTENDANCE_DETAILS", _param);
                if (dtAttendance.Tables[0].Rows.Count > 0)
                {
                    gvAttendance.DataSource = dtAttendance.Tables[0];
                    gvAttendance.DataBind();

                    txtPresent.Text = dtAttendance.Tables[1].Rows[0]["Present"].ToString();
                    txtLeave.Text = dtAttendance.Tables[1].Rows[0]["Leave"].ToString();
                    txtAbsent.Text = dtAttendance.Tables[1].Rows[0]["Absent"].ToString();
                    txtWeekend.Text = dtAttendance.Tables[1].Rows[0]["Weekend"].ToString();
                    txtHoliday.Text = dtAttendance.Tables[1].Rows[0]["Holiday"].ToString();
                    txtTotalDays.Text = dtAttendance.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    ClearStudentAttendance();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnLoad_Click", ex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtStudentCode.Text = String.Empty; ;
            ClearStudentInformation();
            ClearStudentAttendance();
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

        #region Others Code
        protected void ClearStudentInformation()
        {
            hdStudentId.Value = String.Empty;
            txtStudentName.Text = String.Empty;
            txtBanglaName.Text = String.Empty;
            txtClass.Text = String.Empty;
            txtSection.Text = String.Empty;
            txtRollNo.Text = String.Empty;
            txtContact.Text = String.Empty;
        }
        
        protected void ClearStudentAttendance()
        {
            gvAttendance.DataSource = null;
            gvAttendance.DataBind();

            txtPresent.Text = "0";
            txtLeave.Text = "0";
            txtAbsent.Text = "0";
            txtWeekend.Text = "0";
            txtHoliday.Text = "0";
            txtTotalDays.Text = "0";
        }

        protected void gvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String typename = e.Row.Cells[3].Text;

                foreach (TableCell cell in e.Row.Cells)
                {
                    if (typename == "Holiday")
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#8087b5");
                    }
                    else if (typename == "Weekend")
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#5fb68e");
                    }
                    else if (typename == "Leave")
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#c88dc8");
                    }
                    else if (typename == "Absent")
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#FFC125");
                    }
                }
            }
        }
        #endregion
    }
}