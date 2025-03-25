using CoreLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class OtherSchoolInfo : System.Web.UI.Page
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
                divEntryPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divDetailsPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadSchoolList();
                btnUpdate.Visible = false;
            }
        }

        #region Load Primary Data
        protected void LoadSchoolList()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "SchoolListGrid");

                _reader = new DataReader();
                DataSet dsOtherSchool = _reader.GetDataSetByStoredProcedure("SP_OTHER_SCHOOL", _param);
                if(dsOtherSchool.Tables[0].Rows.Count>0)
                {
                    gvOtherSchoolList.DataSource = dsOtherSchool.Tables[0];
                    gvOtherSchoolList.DataBind();
                }
                else
                {
                    gvOtherSchoolList.DataSource = null;
                    gvOtherSchoolList.DataBind();
                }
            }
            catch(Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadSchoolList", ex);
                DisplayMessage("School can't loaded. " + ex.Message);
            }
        }
        #endregion

        #region Button Click and Selected Index Change Events

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                    Hashtable _param = new Hashtable();
                    _param.Add("SchoolName", textInfo.ToTitleCase(txtSchoolName.Text.ToString()));
                    _param.Add("Location", textInfo.ToTitleCase(txtLoacation.Text.ToString()));
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "CREATE");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_OTHER_SCHOOL", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_OTHER_SCHOOL", "CREATE", iAffRow.ToString());
                        DisplayMessage("Other School has been saved successful.");
                        btnClear_OnClick(sender, e);
                    }
                    else
                    {
                        DisplayMessage("Other School has been save failed. Please try again");
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
                DisplayMessage("Emmployee created failed. " + ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnClearSearch_OnClick(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            try
            {
                //DataTable dtVehicle = PopulateLists.SearchVehicleByLns_Reg_Eng(txtSearch.Text.ToString());
                //if (dtVehicle.Rows.Count > 0)
                //{
                //    gvVehicle.DataSource = dtVehicle;
                //    gvVehicle.DataBind();
                //}
                //else
                //{
                //    gvVehicle.DataSource = null;
                //    gvVehicle.DataBind();
                //}
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSearch_OnClick", ex);
                DisplayMessage("Teacher profile has created fail. " + ex.Message);
            }
        }
        #endregion

        #region GridView Command and Index Changing
        protected void gvOtherSchoolList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadSchoolList();
            gvOtherSchoolList.PageIndex = e.NewPageIndex;
            gvOtherSchoolList.DataBind();
        }

        protected void gvOtherSchoolList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdSchoolId.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");
                txtSchoolName.Text = gvRow.Cells[1].Text.Replace("&nbsp;", "");
                txtLoacation.Text = gvRow.Cells[2].Text.Replace("&nbsp;", "");
                
                btnSave.Visible = false;
                btnUpdate.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                Hashtable _param = new Hashtable();
                _param.Add("SchoolID", gvRow.Cells[0].Text);
                _param.Add("Action", "DELETE");

                _writer = new DataWriter();
                _writer.ExecuteStoredProcedure("SP_OTHER_SCHOOL", _param);
                LoadSchoolList();
            }
        }
        #endregion

        #region Other Methods / Clear Methods
        public void ClearControls()
        {
            hdSchoolId.Value = String.Empty;
            txtSchoolName.Text = String.Empty;
            txtLoacation.Text = String.Empty;

            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void ClearSearchControls()
        {
            txtGridSearch.Text = String.Empty;
        }

        protected ValidationProcess IsValidationPassed()
        {
            ValidationProcess result = new ValidationProcess();
            if (String.IsNullOrEmpty(txtSchoolName.Text))
            {
                txtSchoolName.Focus();
                result.IsValid = false;
                result.Message = "Please Enter School Name";
                return result;
            }
            else if (String.IsNullOrEmpty(txtLoacation.Text))
            {
                txtLoacation.Focus();
                result.IsValid = false;
                result.Message = "Please Enter School Location";
                return result;
            }

            return result;
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
    }
}