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
    public partial class UserGroup : System.Web.UI.Page
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
                divGroup.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divGroupDetails.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divMenus.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divMenuDetails.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadGroupsInfo();
                btnUpdate.Visible = false;
            }
        }

        #region Button Click Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("GroupName", txtGroupName.Text);
                _param.Add("EntryBy", user.UserID.ToString());
                _param.Add("Action", "Create");

                _writer = new DataWriter();
                long affRow = _writer.ExecuteStoredProcedure("SP_USER_GROUP", _param, "AffRow");
                if (affRow > 0)
                {
                    ActivityLog.SaveProcess(ActionType.Insert, "SP_USER_GROUP", "Create", affRow.ToString());
                    ClearControls();
                    LoadGroupsInfo();
                    DisplayMessage("User Group Save Successful");
                }
                else
                {
                    DisplayMessage("User Group delete failed. Please try again");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
                DisplayMessage("Opps! An error occurred on User Group Save", ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("GroupId", hdGroupId.Value.Trim());
                _param.Add("GroupName", txtGroupName.Text);
                _param.Add("EntryBy", user.UserID.ToString());
                _param.Add("Action", "Update");

                _writer = new DataWriter();
                long affRow = _writer.ExecuteStoredProcedure("SP_USER_GROUP", _param, "AffRow");
                if (affRow > 0)
                {
                    ActivityLog.SaveProcess(ActionType.Update, "SP_ACADEMIC_YEAR", "Update", affRow.ToString());
                    ClearControls();
                    LoadGroupsInfo();
                    DisplayMessage("User Group update Successful");
                }
                else
                {
                    DisplayMessage("User Group update failed. Please try again");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnUpdate_Click", ex);
                DisplayMessage("Opps! An error occurred on User Group Update", ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdGroupId.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");
                    txtGroupName.Text = gvRow.Cells[1].Text.Replace("&nbsp;", "");
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (e.CommandName == "RemoveRow")
                {

                    _writer = new DataWriter();
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    Hashtable _param = new Hashtable();
                    _param.Add("GroupId", gvRow.Cells[0].Text);
                    _param.Add("EntryBy", user.UserName.ToString());
                    _param.Add("Action", "Delete");

                    long affRow = _writer.ExecuteStoredProcedure("SP_USER_GROUP", _param, "AffRow");
                    if (affRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Delete, "SP_USER_GROUP", "Delete", affRow.ToString());
                        LoadGroupsInfo();
                        DisplayMessage("User Group delete successful");
                    }
                    else
                    {
                        DisplayMessage("Opps! An error occurred User Group Delete");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadBatch", ex);
                DisplayMessage("Contact to Software Administrator", ex.Message);
            }
        }

        #endregion Button Click Events

        #region Others Code
        public void ClearControls()
        {
            hdGroupId.Value = String.Empty;
            txtGroupName.Text = String.Empty;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        private void LoadGroupsInfo()
        {
            Hashtable _param = new Hashtable();
            _param.Add("Action", "Read");

            _reader = new DataReader();
            DataTable dtGroup = _reader.GetDataTableByStoredProcedure("SP_USER_GROUP", _param);
            if (dtGroup.Rows.Count > 0)
            {
                lblRecordCount.Text = dtGroup.Rows.Count.ToString();
                FillList.PopulateDropDownList(dtGroup, ddlGroupName, "GroupName", "GroupId", true, "Select Group", "0");
                gvGroup.DataSource = dtGroup;
                gvGroup.DataBind();
            }
            else
            {
                lblRecordCount.Text = "0";
                gvGroup.DataSource = null;
                gvGroup.DataBind();
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

        protected void btnLoadPermission_Click(object sender, EventArgs e)
        {
            Hashtable _param = new Hashtable();
            _param.Add("GroupId", ddlGroupName.SelectedValue.ToString());
            _param.Add("Action", "MenuList");

            _reader = new DataReader();
            DataTable dtMenu = _reader.GetDataTableByStoredProcedure("SP_USER_GROUP", _param);
            if (dtMenu.Rows.Count > 0)
            {
                gvMenu.DataSource = dtMenu;
                gvMenu.DataBind();
            }
            else
            {
                gvMenu.DataSource = null;
                gvMenu.DataBind();
            }
        }

        protected void cbIsAllow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                int index = row.RowIndex;
                CheckBox chSelect = (CheckBox)gvMenu.Rows[index].FindControl("cbIsAllow");

                Hashtable _param = new Hashtable();
                _param.Add("GroupId", ddlGroupName.SelectedValue.ToString());
                _param.Add("MenuId", gvMenu.Rows[index].Cells[0].Text.ToString());
                _param.Add("Action", ((chSelect.Checked) ? "MenuAllow" : "MenuDisallow"));

                _writer = new DataWriter();
                _writer.ExecuteStoredProcedure("SP_USER_GROUP", _param);

                ActivityLog.SaveProcess(ActionType.Insert, "SP_USER_GROUP", "MenuAllow", ddlGroupName.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.LoginID, Page.Title, "cbIsAllow_CheckedChanged", ex);
            }
        }
    }
}