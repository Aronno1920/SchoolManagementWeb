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
    public partial class StudentPromotion : System.Web.UI.Page
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

                //LoadVehicleType();
                //LoadBrandList();
                //LoadDateControls();

                //LoadVehicleList();
            }

        }

        #region Data Load 

        private void LoadVehicleType()
        {
            try
            {
                //DataTable dtVehicleType = PopulateLists.GetAllVehicleType();
                //FillList.PopulateDropDownList(dtVehicleType, ddlVehicleType, "Name", "ID", true, "Select Vehicle Type", "0");
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadPendingAccountHead", ex);
                DisplayMessage("Vehicle type not load. " + ex.Message);
            }
        }

        private void LoadBrandList()
        {
            try
            {
                DataTable dtBrand = PopulateLists.GetClassList();
                FillList.PopulateDropDownList(dtBrand, ddlBrand, "Name", "ID", true, "Select Brand", "0");
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadPendingAccountHead", ex);
                DisplayMessage("Brand not load. " + ex.Message);
            }
        }

        private void LoadVehicleList()
        {
            try
            {
                _reader = new DataReader();

                Hashtable _param = new Hashtable();
                _param.Add("TypeId", ddlVehicleType.SelectedValue);
                _param.Add("BrandId", ddlBrand.SelectedValue);
                _param.Add("Action", "AllVehicleList");

                DataTable dtRequistion =
                    _reader.GetDataTableByStoredProcedure("SP_VMS_READ_DELETE_VEHICLE", _param);
                if (dtRequistion.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtRequistion.Rows.Count.ToString();
                    gvVehicleList.DataSource = dtRequistion;
                    gvVehicleList.DataBind();
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvVehicleList.DataSource = null;
                    gvVehicleList.DataBind();
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message",
                "alert('" + sMessage.ToString() + "');", true);
            return;
        }

        private void DisplayMessage(String sTypeofData, String exMessage)
        {
            String strMessage = String.Format(sTypeofData + " \nError: {0}", exMessage);
            strMessage = strMessage.Replace("\r\n", "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message",
                "alert('" + strMessage.ToString() + "');", true);
            return;
        }

        #endregion

        #region Button Click Events

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            LoadVehicleList();
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ddlVehicleType.SelectedIndex = 0;
            ddlBrand.SelectedIndex = 0;

            LoadVehicleList();
        }

        #endregion

        protected void gvVehicleList_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void gvVehicleList_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadVehicleList();
            gvVehicleList.PageIndex = e.NewPageIndex;
            gvVehicleList.DataBind();
        }
    }
}