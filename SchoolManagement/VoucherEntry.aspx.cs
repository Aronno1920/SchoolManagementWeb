using System;
using System.Web;
using CoreLibrary;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace SchoolManagement
{
    public partial class VoucherEntry : System.Web.UI.Page
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
                divMasterPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divDetailsPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divButtonPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divVoucherList.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divEntryPanel.Attributes.Add("class", "card card-bordered style-default-bright");
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadDateControl();
                LoadVoucherType();
                LoadAccountsHead();
                LoadCollectionHistory();
                LoadVoucherHistory();

                txtTotalDebit.Text = "0";
                txtTotalCredit.Text = "0";
            }
        }

        #region Load Primary Data
        private void LoadDateControl()
        {
            DateTime nowDate = DateTime.Now;
            txtVoucherDate.Text = nowDate.ToString("dd-MMM-yyyy");
        }

        protected void LoadVoucherType()
        {
            DataTable dtVoucher = PopulateLists.GetVoucherTypeInfo();
            if (dtVoucher.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtVoucher, ddlVoucherType, "VoucherType", "VoucherTypeID", true, "Select Voucher Type", "0");
            }
            else
            {
                ddlVoucherType.DataSource = null;
                ddlVoucherType.DataBind();
            }
        }

        protected void LoadAccountsHead()
        {
            DataTable dtAccountHead = PopulateLists.GetAccountHeadForVoucher();
            if (dtAccountHead.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtAccountHead, ddlAccountHead, "AccountName", "AccountID", true, "Select Account Head", "0");
            }
            else
            {
                ddlAccountHead.DataSource = null;
                ddlAccountHead.DataBind();
            }
        }

        protected void LoadVoucherHistory()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "LoadDraftVoucher");

                _reader = new DataReader();
                DataTable dtVoucher = _reader.GetDataTableByStoredProcedure("SP_VOUCHER_ENTRY", _param);
                if (dtVoucher.Rows.Count > 0)
                {
                    gvVoucherList.DataSource = dtVoucher;
                    gvVoucherList.DataBind();
                }
                else
                {
                    gvVoucherList.DataSource = null;
                    gvVoucherList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadVoucherHistory", ex);
            }
        }
        #endregion

        #region Click Event Methods
        protected void AddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = ValidationForAccountHead();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("VoucherId", hdVoucherId.Value.ToString());
                    _param.Add("VoucherTypeId", ddlVoucherType.SelectedValue.ToString());
                    _param.Add("VoucherDate", txtVoucherDate.Text.ToString());
                    _param.Add("Remarks", txtDescription.Text.ToString());
                    _param.Add("AccountID", ddlAccountHead.SelectedValue.ToString());
                    _param.Add("AccountHead", ddlAccountHead.SelectedItem.Text);
                    _param.Add("DebitAmount", txtDebitAmount.Text.ToString());
                    _param.Add("CreditAmount", txtCreditAmount.Text.ToString());
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "CreateVoucher");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_VOUCHER_ENTRY", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        hdVoucherId.Value = iAffRow.ToString();
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_VOUCHER_ENTRY", "CreateVoucher", hdVoucherId.Value);
                        CalculateTotalDebitCredit(txtDebitAmount.Text.ToString(), txtCreditAmount.Text.ToString());
                        LockMasterControls();
                        ClearAccountInfo();
                        LoadPendingAccountHead();
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "AddDetails_Click", ex);
                DisplayMessage("Opps! An error occurred on voucher entry", ex.Message);
            }
        }

        protected void gvVoucher_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeletePending")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("VoucherId", gvRow.Cells[0].Text);
                    _param.Add("DetailId", gvRow.Cells[1].Text);
                    _param.Add("Action", "DeleteAccountHead");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_VOUCHER_ENTRY", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Delete, "SP_VOUCHER_ENTRY", "DeleteAccountHead", gvRow.Cells[1].Text);
                        LoadPendingAccountHead();
                        CalculateAferDelete(gvRow.Cells[5].Text, gvRow.Cells[6].Text);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvVoucher_RowCommand", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = ValidationForSubmit();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("VoucherId", hdVoucherId.Value);
                    _param.Add("DebitAmount", txtTotalDebit.Text);
                    _param.Add("CreditAmount", txtTotalCredit.Text);
                    _param.Add("Action", "DraftVoucher");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_VOUCHER_ENTRY", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_VOUCHER_ENTRY", "DraftVoucher", hdVoucherId.Value);
                        UnlockMasterControls();
                        ClearAllControls();
                        ClearAccountInfo();
                        LoadVoucherHistory();
                        DisplayMessage("Voucher has been saved successfully.");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = ValidationForSubmit();
                if (result.IsValid == true)
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("VoucherId", hdVoucherId.Value);
                    _param.Add("DebitAmount", txtTotalDebit.Text);
                    _param.Add("CreditAmount", txtTotalCredit.Text);
                    _param.Add("Action", "SubmitVoucher");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_VOUCHER_ENTRY", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_VOUCHER_ENTRY", "SubmitVoucher", hdVoucherId.Value);
                        UnlockMasterControls();
                        ClearAllControls();
                        ClearAccountInfo();
                        LoadVoucherHistory();
                        DisplayMessage("Voucher has been submitted successfully.");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSubmit_Click", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("VoucherId", hdVoucherId.Value.ToString());
                _param.Add("Action", "DeleteVoucher");

                _writer = new DataWriter();
                long iAffRow = _writer.ExecuteStoredProcedure("SP_VOUCHER_ENTRY", _param, "AffRow");
                if (iAffRow > 0)
                {
                    ActivityLog.SaveProcess(ActionType.Delete, "SP_VOUCHER_ENTRY", "DeleteVoucher", hdVoucherId.Value);
                    DisplayMessage("Voucher has been submitted successfully.");

                    UnlockMasterControls();
                    LoadDateControl();
                    LoadVoucherType();
                    LoadAccountsHead();
                    LoadVoucherHistory();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnDelete_Click", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            UnlockMasterControls();
            ClearAllControls();
            ClearAccountInfo();
            LoadVoucherHistory();
        }

        protected void ddlAccountHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtBalace = PopulateLists.GetCurrentBalace(ddlAccountHead.SelectedValue);
            if (dtBalace.Rows.Count > 0)
            {
                txtCurrentBalance.Text = dtBalace.Rows[0]["TotalAmount"].ToString();
            }
            else
            {
                txtCurrentBalance.Text = String.Empty;
            }
        }

        protected void gvVoucherList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("VoucherId", gvRow.Cells[0].Text.ToString());
                    _param.Add("Action", "PendingSubmitted");

                    _reader = new DataReader();
                    DataSet dsVoucher = _reader.GetDataSetByStoredProcedure("SP_VOUCHER_ENTRY", _param);
                    if (dsVoucher.Tables[0].Rows.Count > 0)
                    {
                        hdVoucherId.Value = dsVoucher.Tables[0].Rows[0]["VoucherMasterID"].ToString();
                        ddlVoucherType.SelectedIndex = ddlVoucherType.Items.IndexOf(ddlVoucherType.Items.FindByValue(dsVoucher.Tables[0].Rows[0]["VoucherTypeId"].ToString()));
                        txtVoucherDate.Text = dsVoucher.Tables[0].Rows[0]["VoucherDate"].ToString();
                        txtVoucherNo.Text = dsVoucher.Tables[0].Rows[0]["VoucherNo"].ToString();
                        txtDescription.Text = dsVoucher.Tables[0].Rows[0]["EntryRemarks"].ToString();
                        txtTotalDebit.Text = dsVoucher.Tables[0].Rows[0]["TotalDebitAmount"].ToString();
                        txtTotalCredit.Text = dsVoucher.Tables[0].Rows[0]["TotalCreditAmount"].ToString();
                        LockMasterControls();

                        txtDescription.Enabled = true;
                        txtVoucherDate.Enabled = true;
                    }

                    if (dsVoucher.Tables[1].Rows.Count > 0)
                    {
                        gvVoucher.DataSource = dsVoucher.Tables[1];
                        gvVoucher.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvBrand_RowCommand_RemoveRow", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }
        #endregion

        #region Other Methods and Validation Check
        protected void LockMasterControls()
        {
            ddlVoucherType.Enabled = false;
            txtVoucherDate.Enabled = false;
            txtDescription.Enabled = false;
        }

        protected void UnlockMasterControls()
        {
            ddlVoucherType.Enabled = true;
            txtVoucherDate.Enabled = true;
            txtDescription.Enabled = true;
        }

        protected void ClearAllControls()
        {
            ddlVoucherType.SelectedIndex = -1;
            txtVoucherNo.Text = String.Empty;
            txtDescription.Text = String.Empty;
            txtTotalDebit.Text = String.Empty;
            txtTotalCredit.Text = String.Empty;
            hdVoucherId.Value = String.Empty;

            LoadDateControl();
            gvVoucher.DataSource = null;
            gvVoucher.DataBind();
        }

        protected void ClearAccountInfo()
        {
            ddlAccountHead.SelectedIndex = -1;
            txtCurrentBalance.Text = String.Empty;
            txtDebitAmount.Text = String.Empty;
            txtCreditAmount.Text = String.Empty;
        }

        protected void LoadCollectionHistory()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ReadAll");

                _reader = new DataReader();
                DataTable dtVouchers = _reader.GetDataTableByStoredProcedure("SP_VOUCHER_HISTORY", _param);
                if (dtVouchers.Rows.Count > 0)
                {
                    gvVoucherList.DataSource = dtVouchers;
                    gvVoucherList.DataBind();
                }
                else
                {
                    gvVoucherList.DataSource = null;
                    gvVoucherList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadCollectionHistory", ex);
                DisplayMessage("Error occured!. " + ex.Message);
            }
        }

        protected void CalculateTotalDebitCredit(String sDebit, String sCredit)
        {
            Int32 iTotalDebit = Int32.Parse(txtTotalDebit.Text) + Int32.Parse(String.IsNullOrEmpty(sDebit) ? "0" : sDebit);
            txtTotalDebit.Text = iTotalDebit.ToString();

            Int32 iTotalCredit = Int32.Parse(txtTotalCredit.Text) + Int32.Parse(String.IsNullOrEmpty(sCredit) ? "0" : sCredit);
            txtTotalCredit.Text = iTotalCredit.ToString();
        }

        protected void CalculateAferDelete(String sDebit, String sCredit)
        {
            Int32 iTotalDebit = Int32.Parse(txtTotalDebit.Text) - Int32.Parse(String.IsNullOrEmpty(sDebit) ? "0" : sDebit);
            txtTotalDebit.Text = iTotalDebit.ToString();

            Int32 iTotalCredit = Int32.Parse(txtTotalCredit.Text) - Int32.Parse(String.IsNullOrEmpty(sCredit) ? "0" : sCredit);
            txtTotalCredit.Text = iTotalCredit.ToString();
        }

        protected void LoadPendingAccountHead()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("VoucherId", hdVoucherId.Value.ToString());
                _param.Add("Action", "PendingSubmitted");

                _reader = new DataReader();
                DataSet dsHead = _reader.GetDataSetByStoredProcedure("SP_VOUCHER_ENTRY", _param);
                if (dsHead.Tables[1].Rows.Count > 0)
                {
                    txtVoucherNo.Text = dsHead.Tables[0].Rows[0]["VoucherNo"].ToString();
                    gvVoucher.DataSource = dsHead.Tables[1];
                    gvVoucher.DataBind();
                }
                else
                {
                    txtVoucherNo.Text = String.Empty;
                    gvVoucher.DataSource = null;
                    gvVoucher.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadPendingAccountHead", ex);
            }
        }

        protected ValidationProcess ValidationForAccountHead()
        {
            ValidationProcess result = new ValidationProcess();
            if (ddlVoucherType.SelectedValue == "0")
            {
                ddlVoucherType.Focus();
                result.IsValid = false;
                result.Message = "Please Select Voucher Type.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtVoucherDate.Text))
            {
                txtVoucherDate.Focus();
                result.IsValid = false;
                result.Message = "Please Select Voucher Date";
                return result;
            }
            else if (ddlAccountHead.SelectedValue == "0")
            {
                result.IsValid = false;
                result.Message = "Please Select Account Head";
                return result;
            }
            else if (String.IsNullOrEmpty(txtDebitAmount.Text) && String.IsNullOrEmpty(txtCreditAmount.Text))
            {
                result.IsValid = false;
                result.Message = "Please Enter Debit or Credit Amount.";
                return result;
            }
            else if (!String.IsNullOrEmpty(txtDebitAmount.Text) && !Validator.IsNumericOnly(txtDebitAmount.Text))
            {
                txtDebitAmount.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Only Numeric Debit Amount.";
                return result;
            }
            else if (!String.IsNullOrEmpty(txtCreditAmount.Text) && !Validator.IsNumericOnly(txtCreditAmount.Text))
            {
                txtCreditAmount.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Only Numeric Credit Amount.";
                return result;
            }

            return result;
        }

        protected ValidationProcess ValidationForSubmit()
        {
            //6 - Journal Voucher
            ValidationProcess result = new ValidationProcess();
            if (ddlVoucherType.SelectedValue != "6" && !txtTotalDebit.Text.Equals(txtTotalCredit.Text))
            {
                result.IsValid = false;
                result.Message = "Debit Amount and Credit Amount is not equal. Voucher can't submit.";
                return result;
            }
            return result;
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