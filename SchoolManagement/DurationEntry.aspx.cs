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
    public partial class DurationEntry : System.Web.UI.Page
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

                LoadDurationInfo();
                LoadTimeHourMinute();
                DurationDetails();
            }

        }

        #region Load Primary Data
        private void LoadDurationInfo()
        {
            DataTable dtOrder = PopulateLists.GetDurationList();
            if (dtOrder.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtOrder, ddlOrder, "DurationName", "DurationId", true, "Select Duration", "0");
            }
            else
            {
                ddlOrder.DataSource = null;
                ddlOrder.DataBind();
            }
        }

        private void LoadTimeHourMinute()
        {
            DataTable dtHours = new DataTable();
            dtHours.Columns.Add("Name", typeof(string));
            dtHours.Columns.Add("ID", typeof(string));
            for (Int32 i = 1; i <= 12; i++)
            {
                if (i.ToString().Length == 1)
                {
                    dtHours.Rows.Add(new object[] { "0" + i, "0" + i });
                }
                else
                {
                    if (i == 60)
                    {
                        dtHours.Rows.Add(new object[] { i - 1, i - 1 });
                    }
                    else
                    {
                        dtHours.Rows.Add(new object[] { i, i });
                    }
                }
            }

            DataTable dtMinute = new DataTable();
            dtMinute.Columns.Add("Name", typeof(string));
            dtMinute.Columns.Add("ID", typeof(string));
            for (Int32 j = 0; j <= 60; j = j + 5)
            {
                if (j.ToString().Length == 1)
                {
                    dtMinute.Rows.Add(new object[] { "0" + j, "0" + j });
                }
                else
                {
                    if (j == 60)
                    {
                        dtMinute.Rows.Add(new object[] { j - 1, j - 1 });
                    }
                    else
                    {
                        dtMinute.Rows.Add(new object[] { j, j });
                    }
                }
            }

            FillList.PopulateDropDownList(dtHours, ddlStartHour, "Name", "ID");
            FillList.PopulateDropDownList(dtHours, ddlEndHour, "Name", "ID");

            FillList.PopulateDropDownList(dtMinute, ddlStartMinute, "Name", "ID");
            FillList.PopulateDropDownList(dtMinute, ddlEndMinute, "Name", "ID");
        }

        protected void DurationDetails()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "Read");

                _reader = new DataReader();
                DataTable dtDuration = _reader.GetDataTableByStoredProcedure("SP_DURATION", _param);
                if (dtDuration.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtDuration.Rows.Count.ToString();
                    gvDuration.DataSource = dtDuration;
                    gvDuration.DataBind();
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvDuration.DataSource = null;
                    gvDuration.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "DurationDetails", ex);
            }
        }
        #endregion

        #region Button Click Events

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            String sTime = ddlStartHour.SelectedValue.ToString() + ":" + ddlStartMinute.SelectedValue.ToString() + "" + ddlStartAMPM.SelectedValue.ToString();
            String eTime = ddlEndHour.SelectedValue.ToString() + ":" + ddlEndMinute.SelectedValue.ToString() + "" + ddlEndAMPM.SelectedValue.ToString();

            Hashtable _param = new Hashtable();
            _param.Add("DurationID", ddlOrder.SelectedValue.ToString());
            _param.Add("StartTime", sTime);
            _param.Add("EndTime", eTime);
            _param.Add("EntryBy", user.UserID.ToString());
            _param.Add("Action", "Update");

            _writer = new DataWriter();
            long affRow = _writer.ExecuteStoredProcedure("SP_DURATION", _param, "AffRow");
            if (affRow > 0)
            {
                ActivityLog.SaveProcess(ActionType.Update, "SP_DURATION", "Update", affRow.ToString());
                DurationDetails();
                ClearControls();
                DisplayMessage("Class Duration has been update successful");
            }
            else
            {
                DisplayMessage("Class Duration update failed. Please try again");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        #endregion

        #region Others Code
        public void ClearControls()
        {
            LoadDurationInfo();
            LoadTimeHourMinute();
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