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
    public partial class AcademicYearEntry : System.Web.UI.Page
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

                LoadYearInfo();
                btnUpdate.Visible = false;
            }
        }

        #region Button Click Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("SessionYear", txtSessionYear.Text);
                _param.Add("SessionTitle", txtSessionTitle.Text);
                _param.Add("AdmissionFees", txtAdmissionFees.Text);
                _param.Add("EntryBy", user.UserID.ToString());
                _param.Add("Action", "Create");

                _writer = new DataWriter();
                long affRow = _writer.ExecuteStoredProcedure("SP_ACADEMIC_YEAR", _param, "AffRow");
                if (affRow > 0)
                {
                    ActivityLog.SaveProcess(ActionType.Insert, "SP_ACADEMIC_YEAR", "Create", affRow.ToString());
                    DisplayMessage("Academic Year has been saved successful");
                    
                    ClearControls();
                    LoadYearInfo();
                }
                else
                {
                    DisplayMessage("Academic Year has been failed to save. Please try again");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
                DisplayMessage("Opps! An error occurred on Academic Year", ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("YearId", hdYearId.Value.Trim());
                _param.Add("SessionYear", txtSessionYear.Text);
                _param.Add("SessionTitle", txtSessionTitle.Text);
                _param.Add("AdmissionFees", txtAdmissionFees.Text);
                _param.Add("EntryBy", user.UserID.ToString());
                _param.Add("Action", "Update");

                _writer = new DataWriter();
                long affRow = _writer.ExecuteStoredProcedure("SP_ACADEMIC_YEAR", _param, "AffRow");
                if (affRow > 0)
                {
                    ActivityLog.SaveProcess(ActionType.Update, "SP_SECTION", "Update", hdYearId.Value.Trim());
                    DisplayMessage("Academic Year has been updated successful");
                    
                    ClearControls();
                    LoadYearInfo();
                }
                else
                {
                    DisplayMessage("Academic Year has been failed to update. Please try again");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnUpdate_Click", ex);
                DisplayMessage("Opps! An error occurred on Academic Year", ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvYear_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdYearId.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");
                txtSessionYear.Text = gvRow.Cells[1].Text.Replace("&nbsp;", "");
                txtSessionTitle.Text = gvRow.Cells[2].Text.Replace("&nbsp;", "");
                txtAdmissionFees.Text = gvRow.Cells[3].Text.Replace("&nbsp;", "");
                btnSave.Visible = false;
                btnUpdate.Visible = true;
            }
        }

        #endregion Button Click Events

        #region Others Code

        public void ClearControls()
        {
            hdYearId.Value = String.Empty;
            txtSessionYear.Text = String.Empty;
            txtSessionTitle.Text = String.Empty;
            txtAdmissionFees.Text = String.Empty;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        private void LoadYearInfo()
        {
            DataTable dtClass = PopulateLists.GetSessionList();
            if (dtClass.Rows.Count > 0)
            {
                lblRecordCount.Text = dtClass.Rows.Count.ToString();
                gvYear.DataSource = dtClass;
                gvYear.DataBind();
            }
            else
            {
                lblRecordCount.Text = "0";
                gvYear.DataSource = null;
                gvYear.DataBind();
            }
        }
        #endregion


        protected void gvYear_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadYearInfo();
            gvYear.PageIndex = e.NewPageIndex;
            gvYear.DataBind();
        }

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