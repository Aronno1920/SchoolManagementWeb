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
    public partial class ChartOfAccountsInfo : System.Web.UI.Page
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
                divEntryPanel.Attributes.Add("class", "card card-bordered style-default-bright");
                divAccountTypeEntryPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divAccountGroupEntryPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divAccountHeadEntryPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());

                divTypePanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divGroupPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divAccountsPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadTypeInfo();
                ClearGroup();
                ClearAccounts();
                btnUpdateType.Visible = false;
                btnUpdateGroup.Visible = false;
                btnUpdateAccount.Visible = false;
            }
        }

        #region Load Primary Data
        protected void LoadTypeInfo()
        {
            Hashtable _param = new Hashtable();
            _param.Add("Action", "ReadType");

            _reader = new DataReader();
            DataTable dtType = _reader.GetDataTableByStoredProcedure("SP_CHART_OF_ACCOUNTS", _param);
            if (dtType.Rows.Count > 0)
            {
                lblRecordCount.Text = dtType.Rows.Count.ToString();
                gvType.DataSource = dtType;
                gvType.DataBind();

                FillList.PopulateDropDownList(dtType, ddlTypeNameGroup, "AccountName", "AccountID", true, "Select Type", "0");
                FillList.PopulateDropDownList(dtType, ddlTypeNameAccount, "AccountName", "AccountID", true, "Select Type", "0");
            }
            else
            {
                lblRecordCount.Text = "0";
                gvType.DataSource = null;
                gvType.DataBind();
            }
        }

        protected void LoadGroupInfoByType(String sTypeId, String sTypeName)
        {
            Hashtable _param = new Hashtable();
            _param.Add("SearchBy", sTypeId);
            _param.Add("Action", "ReadGroupByType");

            _reader = new DataReader();
            DataTable dtGroup = _reader.GetDataTableByStoredProcedure("SP_CHART_OF_ACCOUNTS", _param);
            if (dtGroup.Rows.Count > 0)
            {
                lblTotalGroup.Text = dtGroup.Rows.Count.ToString();
                lblTypeName.Text = " - " + sTypeName;
                gvGroup.DataSource = dtGroup;
                gvGroup.DataBind();
            }
        }

        protected void LoadAccountInfoByGroup(String sGroupId, String sGroupName)
        {
            Hashtable _param = new Hashtable();
            _param.Add("SearchBy", sGroupId);
            _param.Add("Action", "ReadAccountByGroup");

            _reader = new DataReader();
            DataTable dtAccounts = _reader.GetDataTableByStoredProcedure("SP_CHART_OF_ACCOUNTS", _param);
            if (dtAccounts.Rows.Count > 0)
            {
                lblTotalAccounts.Text = dtAccounts.Rows.Count.ToString();
                lblGroupName.Text = " - " + sGroupName;
                gvAccounts.DataSource = dtAccounts;
                gvAccounts.DataBind();
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

        #region Other Methods
        protected void ClearGroup()
        {
            lblTotalGroup.Text = "0";
            lblTypeName.Text = String.Empty;
            gvGroup.DataSource = null;
            gvGroup.DataBind();
        }

        protected void ClearAccounts()
        {
            lblTotalAccounts.Text = "0";
            lblGroupName.Text = String.Empty;
            gvAccounts.DataSource = null;
            gvAccounts.DataBind();
        }

        protected void ClearTypeControls()
        {
            txtTypeName.Text = String.Empty;
            cbActiveType.Checked = false;
            btnSaveType.Visible = true;
            btnUpdateType.Visible = false;
        }

        protected void ClearGroupControls()
        {
            ddlTypeNameGroup.SelectedIndex = -1;
            txtGroupName.Text = String.Empty;
            cbActiveGroup.Checked = false;
            btnSaveGroup.Visible = true;
            btnUpdateGroup.Visible = false;
        }

        protected void ClearAccountsControls()
        {
            ddlTypeNameAccount.SelectedIndex = -1;
            ddlGroupNameAccount.SelectedIndex = -1;
            txtAccountName.Text = String.Empty;
            cbActiveAccount.Checked = false;
            btnSaveAccount.Visible = true;
            btnUpdateAccount.Visible = false;
        }

        protected ValidationProcess IsValidateType()
        {
            ValidationProcess result = new ValidationProcess();
            if (String.IsNullOrEmpty(txtTypeName.Text))
            {
                txtTypeName.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Account Type Name.";
                return result;
            }
            return result;
        }

        protected ValidationProcess IsValidateGroup()
        {
            ValidationProcess result = new ValidationProcess();
            if (ddlTypeNameGroup.SelectedValue == "0")
            {
                ddlTypeNameGroup.Focus();
                result.IsValid = false;
                result.Message = "Please Select Account Type Name.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtGroupName.Text))
            {
                ddlTypeNameGroup.Focus();
                result.IsValid = false;
                result.Message = "Please Select Account Type Name.";
                return result;
            }
            return result;
        }

        protected ValidationProcess IsValidateAccounts()
        {
            ValidationProcess result = new ValidationProcess();
            if (ddlTypeNameAccount.SelectedValue == "0")
            {
                ddlTypeNameAccount.Focus();
                result.IsValid = false;
                result.Message = "Please Select Account Type Name.";
                return result;
            }
            else if (ddlGroupNameAccount.SelectedValue == "0")
            {
                ddlGroupNameAccount.Focus();
                result.IsValid = false;
                result.Message = "Please Select Account Group Name.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtAccountName.Text))
            {
                txtAccountName.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Account Head Name.";
                return result;
            }
            return result;
        }

        #endregion

        #region Accounts Type Related Methods
        protected void btnSaveType_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidateType();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("AccountName", txtTypeName.Text);
                    _param.Add("Status", cbActiveType.Checked);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "SaveType");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_CHART_OF_ACCOUNTS", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_CHART_OF_ACCOUNTS", "SaveType", affRow.ToString());
                        LoadTypeInfo();
                        ClearTypeControls();
                        DisplayMessage("Accounts Type has been saved successful.");
                    }
                    else
                    {
                        DisplayMessage("Accounts type save failed. Please try again");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSaveType_Click", ex);
                DisplayMessage("Opps! An error occurred on Accounts type Save", ex.Message);
            }
        }

        protected void btnUpdateType_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidateType();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("AccountID", hdTypeId.Value);
                    _param.Add("AccountName", txtTypeName.Text);
                    _param.Add("Status", cbActiveType.Checked);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "UpdateType");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_CHART_OF_ACCOUNTS", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Update, "SP_CHART_OF_ACCOUNTS", "UpdateType", hdTypeId.Value);
                        LoadTypeInfo();
                        ClearTypeControls();
                        DisplayMessage("Accounts Type has been updated successful.");
                    }
                    else
                    {
                        DisplayMessage("Accounts type save updated. Please try again");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnUpdateType_Click", ex);
                DisplayMessage("Opps! An error occurred on Accounts type Save", ex.Message);
            }
        }

        protected void gvType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdTypeId.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");
                    txtTypeName.Text = ((LinkButton)gvRow.FindControl("lbtnSelect")).Text.Replace("&nbsp;", "");
                    cbActiveType.Checked = Convert.ToBoolean(gvRow.Cells[3].Text.Replace("&nbsp;", ""));
                    btnSaveType.Visible = false;
                    btnUpdateType.Visible = true;
                }
                else if (e.CommandName == "SelectRow")
                {
                    ClearGroup();
                    ClearAccounts();
                    GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    LoadGroupInfoByType(gvRow.Cells[0].Text, ((LinkButton)e.CommandSource).Text);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvDivision_RowCommand", ex);
            }
        }

        protected void btnClearType_Click(object sender, EventArgs e)
        {
            ClearTypeControls();
        }
        #endregion

        #region Accounts Group Related Methods
        protected void btnSaveGroup_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidateGroup();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("TypeID", ddlTypeNameGroup.SelectedValue.ToString());
                    _param.Add("AccountName", txtGroupName.Text);
                    _param.Add("Status", cbActiveGroup.Checked);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "SaveGroup");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_CHART_OF_ACCOUNTS", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_CHART_OF_ACCOUNTS", "SaveGroup", affRow.ToString());
                        ClearGroupControls();
                        DisplayMessage("Accounts Group has been saved successful.");
                    }
                    else
                    {
                        DisplayMessage("Accounts Group save failed. Please try again");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSaveGroup_Click", ex);
                DisplayMessage("Opps! An error occurred on Accounts Group Save", ex.Message);
            }
        }

        protected void btnUpdateGroup_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidateGroup();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("AccountID", hdGroupId.Value);
                    _param.Add("TypeID", ddlTypeNameGroup.SelectedValue.ToString());
                    _param.Add("AccountName", txtGroupName.Text);
                    _param.Add("Status", cbActiveGroup.Checked);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "UpdateGroup");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_CHART_OF_ACCOUNTS", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Update, "SP_CHART_OF_ACCOUNTS", "UpdateGroup", hdGroupId.Value);
                        ClearTypeControls(); ;
                        DisplayMessage("Accounts Group has been updated successful.");
                    }
                    else
                    {
                        DisplayMessage("Accounts Group save updated. Please try again");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnUpdateGroup_Click", ex);
                DisplayMessage("Opps! An error occurred on Accounts Group Save", ex.Message);
            }
        }

        protected void gvGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdGroupId.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");
                    ddlTypeNameGroup.SelectedIndex = ddlTypeNameGroup.Items.IndexOf(ddlTypeNameGroup.Items.FindByValue(gvRow.Cells[1].Text.Replace("&nbsp;", "")));
                    txtGroupName.Text = ((LinkButton)gvRow.FindControl("lbtnSelect")).Text.Replace("&nbsp;", "");
                    cbActiveGroup.Checked = Convert.ToBoolean(gvRow.Cells[4].Text.Replace("&nbsp;", ""));
                    
                    btnSaveGroup.Visible = false;
                    btnUpdateGroup.Visible = true;
                }
                else if (e.CommandName == "SelectRow")
                {
                    ClearAccounts();
                    GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    LoadAccountInfoByGroup(gvRow.Cells[0].Text, ((LinkButton)e.CommandSource).Text);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvGroup_RowCommand", ex);
            }
        }

        protected void btnClearGroup_Click(object sender, EventArgs e)
        {
            ClearGroupControls();
        }

        #endregion

        #region Accounts Head Related Methods
        protected void btnSaveAccount_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidateAccounts();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("GroupID", ddlGroupNameAccount.SelectedValue.ToString());
                    _param.Add("AccountName", txtAccountName.Text);
                    _param.Add("Status", cbActiveAccount.Checked);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "SaveAccount");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_CHART_OF_ACCOUNTS", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_CHART_OF_ACCOUNTS", "SaveAccount", affRow.ToString());
                        ClearAccountsControls();
                        //asdftioasd fhlkjddf
                        DisplayMessage("Accounts has been saved successful.");
                    }
                    else
                    {
                        DisplayMessage("Accounts save failed. Please try again");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSaveGroup_Click", ex);
                DisplayMessage("Opps! An error occurred on Accounts Save", ex.Message);
            }
        }

        protected void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidateAccounts();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("AccountID", hdAccountHead.Value);
                    _param.Add("GroupID", ddlGroupNameAccount.SelectedValue.ToString());
                    _param.Add("AccountName", txtAccountName.Text);
                    _param.Add("Status", cbActiveAccount.Checked);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "UpdateAccount");

                    _writer = new DataWriter();
                    long affRow = _writer.ExecuteStoredProcedure("SP_CHART_OF_ACCOUNTS", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Update, "SP_CHART_OF_ACCOUNTS", "UpdateAccount", hdAccountHead.Value);
                        ClearAccountsControls();
                        DisplayMessage("Accounts has been saved successful.");
                    }
                    else
                    {
                        DisplayMessage("Accounts has been failed. Please try again");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSaveGroup_Click", ex);
                DisplayMessage("Opps! An error occurred on Accounts Save", ex.Message);
            }
        }

        protected void btnClearAccount_Click(object sender, EventArgs e)
        {

        }

        protected void ddlTypeNameAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("SearchBy", ddlTypeNameAccount.SelectedValue.ToString());
                _param.Add("Action", "ReadGroupByType");

                _reader = new DataReader();
                DataTable dtGroupName = _reader.GetDataTableByStoredProcedure("SP_CHART_OF_ACCOUNTS", _param);
                if (dtGroupName.Rows.Count > 0)
                {
                    FillList.PopulateDropDownList(dtGroupName, ddlGroupNameAccount, "AccountName", "AccountID", true, "Select Group", "0");
                }
                else
                {
                    ddlGroupNameAccount.DataSource = null;
                    ddlGroupNameAccount.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "ddlTypeNameAccount_SelectedIndexChanged", ex);
            }
        }

        protected void gvAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdAccountID.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");
                    ddlTypeNameAccount.SelectedIndex = ddlTypeNameAccount.Items.IndexOf(ddlTypeNameAccount.Items.FindByValue(gvRow.Cells[1].Text.Replace("&nbsp;", "")));
                    ddlTypeNameAccount_SelectedIndexChanged(sender, e);
                    ddlGroupNameAccount.SelectedIndex = ddlGroupNameAccount.Items.IndexOf(ddlGroupNameAccount.Items.FindByValue(gvRow.Cells[2].Text.Replace("&nbsp;", "")));
                    txtAccountName.Text = gvRow.Cells[4].Text.Replace("&nbsp;", "");
                    cbActiveAccount.Checked = Convert.ToBoolean(gvRow.Cells[5].Text.Replace("&nbsp;", ""));

                    btnSaveAccount.Visible = false;
                    btnUpdateAccount.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvGroup_RowCommand", ex);
            }
        }

        #endregion
    }
}