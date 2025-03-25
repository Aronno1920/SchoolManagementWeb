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
    public partial class CollectionHistory : System.Web.UI.Page
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
                divCollectionList.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divCollectionDetails.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadDateController();
                LoadClassInfo();
            }
        }

        #region Load Primary Data
        private void LoadDateController()
        {
            DateTime toDate = DateTime.Now;
            DateTime fromDate = new DateTime(toDate.Year, toDate.Month, 1);

            txtFromDate.Text = fromDate.ToString("dd-MMM-yyyy");
            txtToDate.Text = toDate.ToString("dd-MMM-yyyy");
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
        
        private void LoadSectionInfo()
        {
            DataTable dtSection = PopulateLists.GetSectionListByClass(ddlClass.SelectedValue.ToString());
            if (dtSection.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtSection, ddlSection, "SectionName", "SectionId", true, "Select Section", "0");
            }
            else
            {
                ddlSection.DataSource = null;
                ddlSection.DataBind();
            }
        }
        #endregion

        #region Button/GridButton Click Events

        protected void btnLoadSummary_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("StudentCode", txtStudentCode.Text.ToString());
                _param.Add("ClassId", ddlClass.SelectedValue.ToString());
                _param.Add("SectionId", ddlSection.SelectedValue.ToString());
                _param.Add("FromDate", txtFromDate.Text.ToString());
                _param.Add("ToDate", txtToDate.Text.ToString());
                _param.Add("Action", "AdvanceSearch");

                _reader = new DataReader();
                DataTable dtCollectionList = _reader.GetDataTableByStoredProcedure("SP_COLLECTION_HISTORY", _param);
                if (dtCollectionList.Rows.Count > 0)
                {
                    lblTotalRecord.Text = dtCollectionList.Rows.Count.ToString();
                    gvCollectionList.DataSource = dtCollectionList;
                    gvCollectionList.DataBind();
                }
                else
                {
                    gvCollectionList.DataSource = null;
                    gvCollectionList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnLoadSummary_Click", ex);
                DisplayMessage("Error occured!. " + ex.Message);
            }
        }

        protected void btnAddCollection_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CollectionEntry.aspx", false);
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSectionInfo();
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

        protected void gvCollectionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DetailsRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("CollectionId", gvRow.Cells[0].Text);
                    _param.Add("Action", "CollectionDetails");

                    _reader = new DataReader();
                    DataTable dtDetails = _reader.GetDataTableByStoredProcedure("SP_COLLECTION_HISTORY", _param);
                    if (dtDetails.Rows.Count > 0)
                    {
                        lblCollectionCode.Text = " - " + gvRow.Cells[1].Text;
                        gvCollectionDetails.DataSource = dtDetails;
                        gvCollectionDetails.DataBind();
                    }
                    else
                    {
                        lblCollectionCode.Text = String.Empty;
                        gvCollectionDetails.DataSource = null;
                        gvCollectionDetails.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvDivision_RowCommand", ex);
            }
        }
    }
}