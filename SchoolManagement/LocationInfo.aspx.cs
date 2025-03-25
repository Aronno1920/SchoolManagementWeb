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
    public partial class LocationInfo : System.Web.UI.Page
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
                divDivision.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divDistrict.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divUpazilla.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divPostOffice.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadDivisionInfo();
                ClearDistrict();
                ClearUpazilla();
                ClearPostOffice();
            }
        }

        #region Load Primary Data
        protected void LoadDivisionInfo()
        {
            Hashtable _param = new Hashtable();
            _param.Add("Action", "Division");

            _reader = new DataReader();
            DataTable dtDivision = _reader.GetDataTableByStoredProcedure("SP_LOCATION", _param);
            if (dtDivision.Rows.Count > 0)
            {
                lblRecordCount.Text = dtDivision.Rows.Count.ToString();
                gvDivision.DataSource = dtDivision;
                gvDivision.DataBind();
            }
            else
            {
                lblRecordCount.Text = "0";
                gvDivision.DataSource = null;
                gvDivision.DataBind();
            }
        }
        protected void ClearDistrict()
        {
            lblTotalDistrict.Text = "0";
            lblDivisionName.Text = String.Empty;
            gvDistrict.DataSource = null;
            gvDistrict.DataBind();
        }
        protected void ClearUpazilla()
        {
            lblTotalUpazilla.Text = "0";
            lblDistrictName.Text = String.Empty;
            gvUpazilla.DataSource = null;
            gvUpazilla.DataBind();
        }
        protected void ClearPostOffice()
        {
            lblTotalPostOffice.Text = "0";
            lblUpazillaName.Text = String.Empty;
            gvPostOffice.DataSource = null;
            gvPostOffice.DataBind();
        }
        #endregion

        #region Button Click Events
        protected void gvDivision_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectRow")
                {
                    ClearDistrict();
                    ClearUpazilla();
                    ClearPostOffice();

                    GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("SearchBy", gvRow.Cells[0].Text);
                    _param.Add("Action", "DistrictByDivision");

                    _reader = new DataReader();
                    DataTable dtDistrict = _reader.GetDataTableByStoredProcedure("SP_LOCATION", _param);
                    if(dtDistrict.Rows.Count > 0)
                    {
                        lblTotalDistrict.Text = dtDistrict.Rows.Count.ToString();
                        lblDivisionName.Text = " - "+ ((LinkButton)e.CommandSource).Text;
                        gvDistrict.DataSource = dtDistrict;
                        gvDistrict.DataBind();
                    }  
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvDivision_RowCommand", ex);
            }
        }

        protected void gvDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectRow")
                {
                    ClearUpazilla();
                    ClearPostOffice();
                    GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("SearchBy", gvRow.Cells[0].Text);
                    _param.Add("Action", "UpazillaByDistrict");

                    _reader = new DataReader();
                    DataTable dtUpazilla = _reader.GetDataTableByStoredProcedure("SP_LOCATION", _param);
                    if (dtUpazilla.Rows.Count > 0)
                    {
                        lblTotalUpazilla.Text = dtUpazilla.Rows.Count.ToString();
                        lblDistrictName.Text = " - " + ((LinkButton)e.CommandSource).Text;
                        gvUpazilla.DataSource = dtUpazilla;
                        gvUpazilla.DataBind();
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvDistrict_RowCommand", ex);
            }
        }

        protected void gvUpazilla_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                if (e.CommandName == "SelectRow")
                {
                    ClearPostOffice();
                    GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("SearchBy", gvRow.Cells[0].Text);
                    _param.Add("Action", "PostOfficeByUpazilla");

                    _reader = new DataReader();
                    DataTable dtPostOffice = _reader.GetDataTableByStoredProcedure("SP_LOCATION", _param);
                    if (dtPostOffice.Rows.Count > 0)
                    {
                        lblTotalPostOffice.Text = dtPostOffice.Rows.Count.ToString();
                        lblUpazillaName.Text = " - " + ((LinkButton)e.CommandSource).Text;
                        gvPostOffice.DataSource = dtPostOffice;
                        gvPostOffice.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvUpazilla_RowCommand", ex);
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