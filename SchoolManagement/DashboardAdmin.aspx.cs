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
    public partial class DashboardAdmin : System.Web.UI.Page
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
                LoadAdminDashboard();
            }
        }

        private void LoadAdminDashboard()
        {
            Hashtable _param = new Hashtable();
            _param.Add("Report","");
            
            _reader = new DataReader();
            DataSet dsInfo = _reader.GetDataSetByStoredProcedure("SP_DASHBOARD_ADMIN", _param);
            if (dsInfo.Tables[0].Rows.Count > 0)
            {
                lblActiveStudent.Text = dsInfo.Tables[0].Rows[0]["TotalStudent"].ToString();
                lblNewStudent.Text = dsInfo.Tables[0].Rows[0]["ActiveStudent"].ToString();
                lblTotalCandidate.Text = dsInfo.Tables[0].Rows[0]["SSCCandidate"].ToString();
                lblTotalPresent.Text = dsInfo.Tables[0].Rows[0]["PresentStudent"].ToString();
                lblTotalAbsent.Text = dsInfo.Tables[0].Rows[0]["AbsentStudent"].ToString();
                lblSSCCandidate.Text = dsInfo.Tables[0].Rows[0]["ActiveTeacher"].ToString();
            }

            if (dsInfo.Tables[1].Rows.Count > 0)
            {
                lblDailyIncome.Text = dsInfo.Tables[1].Rows[0]["DailyIncome"].ToString();
                lblDailyExpense.Text = dsInfo.Tables[1].Rows[0]["DailyExpense"].ToString();
                lblMonthlyIncome.Text = dsInfo.Tables[1].Rows[0]["MonthlyIncome"].ToString();
                lblMonthlyExpense.Text = dsInfo.Tables[1].Rows[0]["MonthlyExpense"].ToString();
            }

            if (dsInfo.Tables[2].Rows.Count > 0)
            {
                gvActiveStudent.DataSource = dsInfo.Tables[2];
                gvActiveStudent.DataBind();
            }

            if (dsInfo.Tables[3].Rows.Count > 0)
            {
                gvTotalPresentAbsent.DataSource = dsInfo.Tables[3];
                gvTotalPresentAbsent.DataBind();
            }

            if (dsInfo.Tables[5].Rows.Count > 0)
            {
                gvCurrentBalance.DataSource = dsInfo.Tables[5];
                gvCurrentBalance.DataBind();
            }

            //if (dsInfo.Tables[3].Rows.Count > 0)
            //{
            //    gvVehicleType.DataSource = dsInfo.Tables[3];
            //    gvVehicleType.DataBind();
            //}

            //if (dsInfo.Tables[4].Rows.Count > 0)
            //{
            //    gvWorkshopVehicle.DataSource = dsInfo.Tables[4];
            //    gvWorkshopVehicle.DataBind();
            //}

            //if (dsInfo.Tables[5].Rows.Count > 0)
            //{
            //    gvServiceWiseVehicle.DataSource = dsInfo.Tables[5];
            //    gvServiceWiseVehicle.DataBind();
            //}
        }

        #region Common Method for Display Message
        protected enum MessageType { Success, Warning, Information, Error }
        protected void DisplayMessage(Enum eMessageType, String sMessage)
        {
            switch (eMessageType.ToString())
            {
                case "Success":
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('"+ sMessage + "')", true);
                    break;
                case "Warning":
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.warning('" + sMessage + "')", true);
                    break;
                case "Information":
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.info('" + sMessage + "')", true);
                    break;
            }
            return;
        }
        protected void DisplayMessage(Enum eMessageType, String sMessage, String sErrorMessage)
        {
            String strMessage = String.Format(sMessage + ".. Error: "+ sErrorMessage);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.error('"+ strMessage + "')", true);
            return;
        }
        #endregion
    }
}