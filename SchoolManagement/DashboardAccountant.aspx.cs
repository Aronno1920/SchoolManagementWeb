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
    public partial class DashboardAccountant : System.Web.UI.Page
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
                //LoadVehicleStatus();
            }
        }

        private void LoadVehicleStatus()
        {
            Hashtable _param = new Hashtable();
            _param.Add("Report","");
            
            _reader = new DataReader();
            DataSet dtInfo = _reader.GetDataSetByStoredProcedure("SP_DASHBOARD",_param);

            if (dtInfo.Tables[1].Rows.Count > 0)
            {
                gvCompanyWise.DataSource = dtInfo.Tables[1];
                gvCompanyWise.DataBind();
            }

            if (dtInfo.Tables[2].Rows.Count > 0)
            {
                gvLocationWise.DataSource = dtInfo.Tables[2];
                gvLocationWise.DataBind();
            }

            if (dtInfo.Tables[3].Rows.Count > 0)
            {
                gvVehicleType.DataSource = dtInfo.Tables[3];
                gvVehicleType.DataBind();
            }

            if (dtInfo.Tables[4].Rows.Count > 0)
            {
                gvWorkshopVehicle.DataSource = dtInfo.Tables[4];
                gvWorkshopVehicle.DataBind();
            }

            if (dtInfo.Tables[5].Rows.Count > 0)
            {
                gvServiceWiseVehicle.DataSource = dtInfo.Tables[5];
                gvServiceWiseVehicle.DataBind();
            }
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