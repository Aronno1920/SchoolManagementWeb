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
    public partial class SessionFeesDetails : System.Web.UI.Page
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

                LoadClassInfo();
                LoadSessionFeeHead();
                GetFeesDetailsInfo();
                btnUpdate.Visible = false;
            }
        }

        #region Load Primary Data
        protected void LoadClassInfo()
        {
            DataTable dtClasss = PopulateLists.GetClassList();
            if (dtClasss.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtClasss, ddlClass, "ClassName", "ClassId", true, "Select Class", "0");
                FillList.PopulateDropDownList(dtClasss, ddlGridSearch, "ClassName", "ClassId", true, "Select Class", "0");
            }
            else
            {
                ddlClass.DataSource = null;
                ddlClass.DataBind();

                ddlGridSearch.DataSource = null;
                ddlGridSearch.DataBind();
            }
        }

        protected void LoadSessionFeeHead()
        {
            DataTable dtFees = PopulateLists.GetSessionFeeHead();
            if (dtFees.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtFees, ddlHeadName, "SessionHead", "SessionId", true, "Select Fee Head", "0");
            }
            else
            {
                ddlClass.DataSource = null;
                ddlClass.DataBind();
            }
        }

        private void GetFeesDetailsInfo()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "Read");

                _reader = new DataReader();
                DataTable dataTable = _reader.GetDataTableByStoredProcedure("SP_SESSION_DETAILS", _param);
                if (dataTable.Rows.Count > 0)
                {
                    lblRecordCount.Text = dataTable.Rows.Count.ToString();
                    gvFeesList.DataSource = dataTable;
                    gvFeesList.DataBind();
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvFeesList.DataSource = null;
                    gvFeesList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "GetFeesDetailsInfo", ex);
            }
        }
        #endregion

        #region Button Click Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("ClassID", ddlClass.SelectedValue.ToString());
                _param.Add("HeadId", ddlHeadName.SelectedValue.ToString());
                _param.Add("Amount", txtAmount.Text.ToString());
                _param.Add("EntryBy", user.UserID.ToString());
                _param.Add("Action", "CREATE");

                _writer = new DataWriter();
                long affRow = _writer.ExecuteStoredProcedure("SP_SESSION_DETAILS", _param, "AffRow");
                if (affRow > 0)
                {
                    txtAmount.Text = String.Empty;

                    ActivityLog.SaveProcess(ActionType.Insert, "SP_SESSION_DETAILS", "Create", affRow.ToString());
                    ddlClass_SelectedIndexChanged(sender, e);
                    GetFeesDetailsInfo();
                    DisplayMessage("Session Head Amount has been saved Successful");
                }
                else
                {
                    DisplayMessage("Session Head save failed. Please try again");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("DetailsId", hdDetailsId.Value.ToString());
                _param.Add("ClassID", ddlClass.SelectedValue.ToString());
                _param.Add("HeadId", ddlHeadName.SelectedValue.ToString());
                _param.Add("Amount", txtAmount.Text.ToString());
                _param.Add("EntryBy", user.UserID.ToString());
                _param.Add("Action", "UPDATE");

                _writer = new DataWriter();
                long affRow = _writer.ExecuteStoredProcedure("SP_SESSION_DETAILS", _param, "AffRow");
                if (affRow > 0)
                {
                    txtAmount.Text = String.Empty;
                    ddlClass_SelectedIndexChanged(sender, e);
                    GetFeesDetailsInfo();

                    ActivityLog.SaveProcess(ActionType.Update, "SP_SESSION_DETAILS", "UPDATE", hdDetailsId.Value.Trim());
                    DisplayMessage("Session Head fee has been updated Successful");
                    ClearControls();
                }
                else
                {
                    DisplayMessage("Session Head fee update failed. Please try again");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnUpdate_Click", ex);
                DisplayMessage("Opps! An error occurred on Class Update", ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvFeesList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdDetailsId.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");
                    ddlClass.SelectedIndex = ddlClass.Items.IndexOf(ddlClass.Items.FindByValue(gvRow.Cells[1].Text.Replace("&nbsp;", "")));
                    ddlHeadName.SelectedIndex = ddlHeadName.Items.IndexOf(ddlHeadName.Items.FindByValue(gvRow.Cells[2].Text.Replace("&nbsp;", "")));
                    txtAmount.Text = gvRow.Cells[5].Text.Replace("&nbsp;", "");

                    ddlClass_SelectedIndexChanged(sender, e);
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
                if (e.CommandName == "RemoveRow")
                {
                    _writer = new DataWriter();
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("DetailsId", gvRow.Cells[0].Text);
                    _param.Add("ClassID", gvRow.Cells[1].Text);
                    _param.Add("EntryBy", user.GetCookie(CookieKey.UserID.ToString()));
                    _param.Add("Action", "DELETE");

                    long affRow = _writer.ExecuteStoredProcedure("SP_SESSION_DETAILS", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_SESSION_DETAILS", "DELETE", affRow.ToString());
                        GetFeesDetailsInfo();
                        ddlClass_SelectedIndexChanged(sender, e);
                        DisplayMessage("Frees has been deleted successful");
                    }
                    else
                    {
                        DisplayMessage("Frees has been deleted failed. Please try again");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvFeesList_RowCommand", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("ClassID", ddlGridSearch.SelectedValue.ToString());
                _param.Add("SearchBy", txtGridSearch.Text.ToString());
                _param.Add("Action", "SEARCH");

                _reader = new DataReader();
                DataTable dtFees = _reader.GetDataTableByStoredProcedure("SP_SESSION_DETAILS", _param);
                if (dtFees.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtFees.Rows.Count.ToString();
                    gvFeesList.DataSource = dtFees;
                    gvFeesList.DataBind();
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvFeesList.DataSource = null;
                    gvFeesList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnGridSearch_Click", ex);
                DisplayMessage("Fees Schedul has loaded failed. " + ex.Message);
            }
        }

        protected void gvFeesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFeesList.PageIndex = e.NewPageIndex;
            GetFeesDetailsInfo();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlClass.SelectedValue) > 0)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("ClassId", ddlClass.SelectedValue);
                    _param.Add("Action", "TotalSessionFee");

                    _reader = new DataReader();
                    DataTable dtAmount = _reader.GetDataTableByStoredProcedure("SP_SESSION_DETAILS", _param);
                    if (dtAmount.Rows.Count > 0)
                    {
                        txtSessionFee.Text = dtAmount.Rows[0]["Amount"].ToString();
                        txtRestAmount.Text = dtAmount.Rows[0]["RestAmount"].ToString();
                    }
                    else
                    {
                        DisplayMessage("Please Set Session Fee First For "+ddlClass.SelectedItem.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "ddlClass_SelectedIndexChanged", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        #endregion Button Click Events

        #region Others Code
        public void ClearControls()
        {
            ddlClass.SelectedIndex = -1;
            ddlHeadName.SelectedIndex = -1;
            txtAmount.Text = String.Empty;
            txtSessionFee.Text = String.Empty;
            txtRestAmount.Text = String.Empty;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
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