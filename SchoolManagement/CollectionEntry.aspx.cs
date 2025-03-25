using CoreLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class CollectionEntry : System.Web.UI.Page
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
                divStudentInfo.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divFeesCollection.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divUnpaid.Attributes.Add("class", "card card-bordered style-warning");
                divPaid.Attributes.Add("class", "card card-bordered style-success");
                ActivityLog.SaveProcess(ActionType.Access, sPageName);
            }
        }

        #region Button Click Events

        protected void btnLoadStudent_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("StudentCode", txtStudentCode.Text.Trim());
                _param.Add("Action", "SearchStudent");

                _reader = new DataReader();
                DataTable dtStudent = _reader.GetDataTableByStoredProcedure("SP_FEES_COLLECTION", _param);
                if (dtStudent.Rows.Count > 0)
                {
                    txtStudentName.Text = dtStudent.Rows[0]["StudentName"].ToString();
                    txtBanglaName.Text = dtStudent.Rows[0]["StudentNameBangla"].ToString();
                    txtClass.Text = dtStudent.Rows[0]["ClassName"].ToString();
                    txtSection.Text = dtStudent.Rows[0]["SectionName"].ToString();
                    txtRollNo.Text = dtStudent.Rows[0]["RollNo"].ToString();
                    txtContact.Text = dtStudent.Rows[0]["ContactNo"].ToString();
                }
                else
                {
                    btnClear_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnLoadStudent_Click", ex);
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("StudentCode", txtStudentCode.Text.ToString());
                _param.Add("Action", "LoadPaidUnpaid");

                _reader = new DataReader();
                DataSet dtFeesList = _reader.GetDataSetByStoredProcedure("SP_FEES_COLLECTION", _param);
                if (dtFeesList.Tables[0].Rows.Count > 0)
                {
                    gvUnpaidFees.DataSource = dtFeesList.Tables[0];
                    gvUnpaidFees.DataBind();
                }
                else
                {
                    gvUnpaidFees.DataSource = null;
                    gvUnpaidFees.DataBind();
                }

                if (dtFeesList.Tables[1].Rows.Count > 0)
                {
                    gvPaidFees.DataSource = dtFeesList.Tables[1];
                    gvPaidFees.DataBind();

                    MergeRows(gvPaidFees);
                }
                else
                {
                    gvPaidFees.DataSource = null;
                    gvPaidFees.DataBind();
                }

                LoadPendingCollection();
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnLoad_Click", ex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtStudentCode.Text = String.Empty; ;
            txtStudentName.Text = String.Empty;
            txtBanglaName.Text = String.Empty;
            txtClass.Text = String.Empty;
            txtSection.Text = String.Empty;
            txtRollNo.Text = String.Empty;
            txtContact.Text = String.Empty;
            hdCollectionId.Value = String.Empty;

            gvUnpaidFees.DataSource = null;
            gvUnpaidFees.DataBind();
            gvPaidFees.DataSource = null;
            gvPaidFees.DataBind();
            gvSelectedFees.DataSource = null;
            gvSelectedFees.DataBind();

            txtCollectionCode.Text = String.Empty;
            txtCollectionDate.Text = String.Empty;
            txtTotalAmount.Text = String.Empty;
            txtTotalDiscount.Text = String.Empty;
            txtTotalPaidAmount.Text = String.Empty;
        }

        protected void btnCollectionSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("MasterId", hdCollectionId.Value);
                _param.Add("StudentCode", txtStudentCode.Text.ToString());
                _param.Add("Action", "CollectionSubmit");

                _writer = new DataWriter();
                _writer.ExecuteStoredProcedure("SP_FEES_COLLECTION", _param);
                DisplayMessage("Student payment has been collected successful.");

                btnClear_Click(sender, e);
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnCollectionSubmit_Click", ex);
                DisplayMessage("Error occured!. " + ex.Message);
            }
        }

        protected void cbSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtStudentCode.Text))
                {
                    GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                    int index = row.RowIndex;
                    CheckBox chSelect = (CheckBox)gvUnpaidFees.Rows[index].FindControl("cbSelect");
                    if (chSelect.Checked)
                    {
                        Hashtable _param = new Hashtable();
                        _param.Add("MasterId", hdCollectionId.Value);
                        _param.Add("StudentCode", txtStudentCode.Text.ToString());
                        _param.Add("ScheduleId", gvUnpaidFees.Rows[index].Cells[1].Text.ToString());
                        _param.Add("FeesTitle", gvUnpaidFees.Rows[index].Cells[3].Text.ToString());
                        _param.Add("Amount", gvUnpaidFees.Rows[index].Cells[4].Text.ToString());
                        _param.Add("WaiverAmount", gvUnpaidFees.Rows[index].Cells[5].Text.ToString());
                        _param.Add("PaidAmount", gvUnpaidFees.Rows[index].Cells[6].Text.ToString());
                        _param.Add("AccountId", gvUnpaidFees.Rows[index].Cells[7].Text.ToString());
                        _param.Add("EntryBy", user.GetCookie(CookieKey.UserID.ToString()));
                        _param.Add("Action", "CollectionEntry");

                        _writer = new DataWriter();
                        _writer.ExecuteStoredProcedure("SP_FEES_COLLECTION", _param);
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_FEES_COLLECTION", "CollectionEntry", hdCollectionId.Value);

                        LoadPendingCollection();
                    }
                    else
                    {

                    }
                }
                else
                {
                    DisplayMessage("Error occured! Please load Student Information ance again.");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "cbSelect_CheckedChanged", ex);
                DisplayMessage("Collection received fail. " + ex.Message);
            }
        }

        protected void gvSelectedFees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeletePending")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("MasterId", gvRow.Cells[0].Text);
                    _param.Add("DetailId", gvRow.Cells[1].Text);
                    _param.Add("ScheduleId", gvRow.Cells[2].Text);
                    _param.Add("StudentCode", txtStudentCode.Text.ToString());
                    _param.Add("Action", "CollectionDelete");

                    _writer = new DataWriter();
                    _writer.ExecuteStoredProcedure("SP_FEES_COLLECTION", _param);
                    ActivityLog.SaveProcess(ActionType.Delete, "SP_FEES_COLLECTION", "CollectionDelete", gvRow.Cells[0].Text);
                    LoadPendingCollection();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvCandidate_RowCommand_SaveRow", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        protected void btnCollectionList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CollectionHistory.aspx", false);
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

        #region Load Data
        protected void LoadPendingCollection()
        {
            try
            {
                if (!String.IsNullOrEmpty(txtStudentCode.Text.ToString()))
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("StudentCode", txtStudentCode.Text.ToString());
                    _param.Add("Action", "PendingCollection");

                    _reader = new DataReader();
                    DataSet dsPendingList = _reader.GetDataSetByStoredProcedure("SP_FEES_COLLECTION", _param);
                    if (dsPendingList.Tables[0].Rows.Count > 0)
                    {
                        hdCollectionId.Value = dsPendingList.Tables[0].Rows[0]["CollectionMasterId"].ToString();
                        txtCollectionCode.Text = dsPendingList.Tables[0].Rows[0]["CollectionCode"].ToString();
                        txtCollectionDate.Text = dsPendingList.Tables[0].Rows[0]["CollectionDate"].ToString();
                    }
                    else
                    {
                        hdCollectionId.Value = String.Empty;
                        txtCollectionCode.Text = String.Empty;
                        txtCollectionDate.Text = String.Empty;
                    }

                    if (dsPendingList.Tables[1].Rows.Count > 0)
                    {
                        gvSelectedFees.DataSource = dsPendingList.Tables[1];
                        gvSelectedFees.DataBind();
                    }
                    else
                    {
                        gvSelectedFees.DataSource = null;
                        gvSelectedFees.DataBind();
                    }

                    if (dsPendingList.Tables[2].Rows.Count > 0)
                    {
                        txtTotalAmount.Text = dsPendingList.Tables[2].Rows[0]["TotalAmount"].ToString();
                        txtTotalDiscount.Text = dsPendingList.Tables[2].Rows[0]["TotalDiscount"].ToString();
                        txtTotalPaidAmount.Text = dsPendingList.Tables[2].Rows[0]["TotalPaidAmount"].ToString();
                    }
                    else
                    {
                        txtTotalAmount.Text = String.Empty;
                        txtTotalDiscount.Text = String.Empty;
                        txtTotalPaidAmount.Text = String.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadPendingCollection()", ex);
                DisplayMessage("Error occured!. " + ex.Message);
            }
        }

        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = 0; i < 2; i++)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }
        #endregion
    }
}