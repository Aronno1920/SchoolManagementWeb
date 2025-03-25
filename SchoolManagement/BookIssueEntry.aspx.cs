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
    public partial class BookIssueEntry : System.Web.UI.Page
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

                //LoadBrandInfo();
                btnUpdate.Visible = false;
            }

        }

        #region Button Click Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            _reader = new DataReader();

            Hashtable _param = new Hashtable();
            _param.Add("BrandID", txtBrandID.Text.Trim());
            _param.Add("BrandName", txtBrandName.Text.Trim());
            _param.Add("EntryUserId", user.UserID.ToString());
            _param.Add("EntryBy", user.UserName.ToString());
            _param.Add("Action", "SaveBrand");

            long affRow = _writer.ExecuteStoredProcedure("SP_VMS_BRAND_MODEL_COLOR_VENDOR", _param, "AffRow");
            if (affRow > 0)
            {
                ActivityLog.SaveProcess(ActionType.Insert, "SP_ACADEMIC_YEAR", "Create", affRow.ToString());
                DisplayMessage("Brand Save Successful");

                LoadBrandInfo();
                ClearControls();
            }
            else
            {
                DisplayMessage("Brand save failed. Please try again");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            _writer = new DataWriter();

            Hashtable _param = new Hashtable();
            _param.Add("BrandID", txtBrandID.Text.Trim());
            _param.Add("BrandName", txtBrandName.Text.Trim());
            _param.Add("EntryBy", user.UserName.ToString());
            _param.Add("Action", "UpdateBrand");

            long affRow = _writer.ExecuteStoredProcedure("SP_VMS_BRAND_MODEL_COLOR_VENDOR", _param, "AffRow");
            if (affRow > 0)
            {
                ActivityLog.SaveProcess(ActionType.Insert, "SP_ACADEMIC_YEAR", "Create", affRow.ToString());
                DisplayMessage("Brand update successful");

                LoadBrandInfo();
                ClearControls();
            }
            else
            {
                DisplayMessage("Brand update failed. Please try again");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvBrand_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    txtBrandID.Text = gvRow.Cells[0].Text;
                    txtBrandName.Text = gvRow.Cells[1].Text;
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (e.CommandName == "RemoveRow")
                {

                    _writer = new DataWriter();
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("BrandId", gvRow.Cells[0].Text);
                    _param.Add("EntryBy", user.UserName.ToString());
                    _param.Add("Action", "DeleteBrand");

                    long affRow = _writer.ExecuteStoredProcedure("SP_VMS_BRAND_MODEL_COLOR_VENDOR", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_ACADEMIC_YEAR", "Create", affRow.ToString());
                        DisplayMessage("Brand delete successful");
                        LoadBrandInfo();
                    }
                    else
                    {
                        DisplayMessage("Brand delete failed. Please try again");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvBrand_RowCommand_RemoveRow", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        #endregion Button Click Events

        #region Others Code

        public void ClearControls()
        {
            txtBrandID.Text = String.Empty;
            txtBrandName.Text = String.Empty;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        private void LoadBrandInfo()
        {
            DataTable dtBrand = PopulateLists.GetClassList();
            if (dtBrand.Rows.Count > 0)
            {
                lblRecordCount.Text = dtBrand.Rows.Count.ToString();
                gvBrand.DataSource = dtBrand;
                gvBrand.DataBind();
            }
            else
            {
                lblRecordCount.Text = "0";
                gvBrand.DataSource = null;
                gvBrand.DataBind();

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

        protected void gvBrand_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadBrandInfo();
            gvBrand.PageIndex = e.NewPageIndex;
            gvBrand.DataBind();
        }
    }
}