using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreLibrary;

namespace SchoolManagement
{
    public partial class EmployeeInfo : System.Web.UI.Page
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
                divUserPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divButtonPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divDetailsPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadUserGroup();
                LoadEmployeeList();
                btnUpdate.Visible = false;
            }
        }

        #region Load Primary Data
        protected void LoadUserGroup()
        {
            DataTable dtUserGroup = PopulateLists.GetUserGroupAll();
            if (dtUserGroup.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtUserGroup, ddlUserGroup, "GroupName", "GroupId", true, "Select User Group", "0");
            }
            else
            {
                ddlUserGroup.DataSource = null;
                ddlUserGroup.DataBind();
            }
        }

        protected void LoadEmployeeList()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ReadEmployeeList");

                _reader = new DataReader();
                DataTable dsEmployee = _reader.GetDataTableByStoredProcedure("SP_EMPLOYEE_INFO", _param);

                if (dsEmployee.Rows.Count > 0)
                {
                    gvTeacherList.DataSource = dsEmployee;
                    gvTeacherList.DataBind();
                }
                else
                {
                    gvTeacherList.DataSource = null;
                    gvTeacherList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadEmployeeList", ex);
                DisplayMessage("Emmployee List can't loaded. " + ex.Message);
            }
        }

        protected void LoadSelectedEmployee(String sEmployeeId)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("EmployeeId", sEmployeeId);
                _param.Add("Action", "ReadSelectedEmployee");

                _reader = new DataReader();
                DataTable dtEmployee = _reader.GetDataTableByStoredProcedure("SP_EMPLOYEE_INFO", _param);
                if (dtEmployee.Rows.Count > 0)
                {
                    hdTeacherId.Value = dtEmployee.Rows[0]["EmployeeID"].ToString();
                    txtEmployeeName.Text = dtEmployee.Rows[0]["EmployeeName"].ToString();
                    txtIndexNo.Text = dtEmployee.Rows[0]["IndexNumber"].ToString();
                    txtEmployeeBanglaName.Text = dtEmployee.Rows[0]["EmployeeNameBangla"].ToString();
                    txtEmployeeNID.Text = dtEmployee.Rows[0]["EmployeeNID"].ToString();
                    txtContactNo.Text = dtEmployee.Rows[0]["ContactNo"].ToString();
                    txtDateOfBirth.Text = dtEmployee.Rows[0]["BirthDate"].ToString();
                    txtSpouseName.Text = dtEmployee.Rows[0]["SpouseName"].ToString();

                    if (dtEmployee.Rows[0]["Gender"].ToString().Equals("Female"))
                    { rbFemale.Checked = true; }
                    else
                    { rbMale.Checked = true; }

                    txtFatherName.Text = dtEmployee.Rows[0]["FatherName"].ToString();
                    txtFatherBanglaName.Text = dtEmployee.Rows[0]["FatherNameBangla"].ToString();
                    txtMotherName.Text = dtEmployee.Rows[0]["MotherName"].ToString();
                    txtMotherBanglaName.Text = dtEmployee.Rows[0]["MotherNameBangla"].ToString();
                    txtPresentAddress.Text = dtEmployee.Rows[0]["PresentAddress"].ToString();
                    txtPermanentAdress.Text = dtEmployee.Rows[0]["PermanentAddress"].ToString();

                    imgProfilePhoto.ImageUrl = dtEmployee.Rows[0]["ProfilePhoto"].ToString();
                    hdImagePath.Value = dtEmployee.Rows[0]["ProfilePhoto"].ToString();
                    ddlEmployeeType.SelectedValue = dtEmployee.Rows[0]["EmployeeType"].ToString();
                    ddlUserGroup.SelectedValue = dtEmployee.Rows[0]["UserGroupId"].ToString();

                    txtLoginID.Text = String.Empty;
                    txtPassword.Text = String.Empty;
                    txtLoginID.Enabled = false;
                    txtPassword.Enabled = false;

                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadEmployeeList", ex);
                DisplayMessage("Emmployee List can't loaded. " + ex.Message);
            }
        }

        #endregion

        #region Button Click Events
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    UploadProfilePhoto();
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                    String ePassword = UtilityClass.Encrypt(txtPassword.Text.ToString(), true);

                    Hashtable _param = new Hashtable();
                    _param.Add("EmployeeName", textInfo.ToTitleCase(txtEmployeeName.Text));
                    _param.Add("EmployeeNameBangla", txtEmployeeBanglaName.Text);
                    _param.Add("EmployeeNID", txtEmployeeNID.Text);
                    _param.Add("IndexNo", txtIndexNo.Text);
                    _param.Add("SpouseName", txtSpouseName.Text);
                    _param.Add("FatherName", textInfo.ToTitleCase(txtFatherName.Text));
                    _param.Add("FatherNameBangla", txtFatherBanglaName.Text);
                    _param.Add("MotherName", textInfo.ToTitleCase(txtMotherName.Text));
                    _param.Add("MotherNameBangla", txtMotherBanglaName.Text);
                    _param.Add("PresentAddress", txtPresentAddress.Text);
                    _param.Add("PermanentAddress", txtPermanentAdress.Text);
                    _param.Add("ContactNo", txtContactNo.Text);
                    _param.Add("Gender", (rbFemale.Checked == true ? rbFemale.Text : rbMale.Text));
                    _param.Add("BirthDate", txtDateOfBirth.Text);
                    _param.Add("ProfilePhoto", hdImagePath.Value.ToString());
                    _param.Add("LoginId", txtLoginID.Text.ToString());
                    _param.Add("Password", ePassword);
                    _param.Add("EmployeeType", ddlEmployeeType.SelectedValue.ToString());
                    _param.Add("UserGroupId", ddlUserGroup.SelectedValue.ToString());
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "CREATE");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_EMPLOYEE_INFO", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_EMPLOYEE_INFO", "CREATE", iAffRow.ToString());
                        DisplayMessage("Employee has been saved successful");

                        LoadEmployeeList();
                        btnClear_OnClick(sender, e);
                    }
                    else
                    {
                        DisplayMessage("Employee has been save failed. Please try again");
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
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    UploadProfilePhoto();
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                    Hashtable _param = new Hashtable();
                    _param.Add("EmployeeId", hdTeacherId.Value);
                    _param.Add("EmployeeName", textInfo.ToTitleCase(txtEmployeeName.Text));
                    _param.Add("EmployeeNameBangla", txtEmployeeBanglaName.Text);
                    _param.Add("EmployeeNID", txtEmployeeNID.Text);
                    _param.Add("IndexNo", txtIndexNo.Text);
                    _param.Add("SpouseName", txtSpouseName.Text);
                    _param.Add("FatherName", textInfo.ToTitleCase(txtFatherName.Text));
                    _param.Add("FatherNameBangla", txtFatherBanglaName.Text);
                    _param.Add("MotherName", textInfo.ToTitleCase(txtMotherName.Text));
                    _param.Add("MotherNameBangla", txtMotherBanglaName.Text);
                    _param.Add("PresentAddress", txtPresentAddress.Text);
                    _param.Add("PermanentAddress", txtPermanentAdress.Text);
                    _param.Add("ContactNo", txtContactNo.Text);
                    _param.Add("Gender", (rbFemale.Checked == true ? rbFemale.Text : rbMale.Text));
                    _param.Add("BirthDate", txtDateOfBirth.Text);
                    _param.Add("ProfilePhoto", hdImagePath.Value.ToString());
                    _param.Add("EmployeeType", ddlEmployeeType.SelectedValue.ToString());
                    _param.Add("UserGroupId", ddlUserGroup.SelectedValue.ToString());
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "Update");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_EMPLOYEE_INFO", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Update, "SP_EMPLOYEE_INFO", "Update", iAffRow.ToString());
                        DisplayMessage("Employee has been updated successful");
                        
                        LoadEmployeeList();
                        btnClear_OnClick(sender, e);
                    }
                    else
                    {
                        DisplayMessage("Employee has been updated failed. Please try again");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnUpdate_Click", ex);
                DisplayMessage("Emmployee has been updated failed. " + ex.Message);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ClearControls();
        }

        #endregion

        #region Grid Button and Search Click Events
        protected void gvTeacherList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdTeacherId.Value = gvRow.Cells[0].Text.Replace("&nbsp;", "");
                    LoadSelectedEmployee(hdTeacherId.Value);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvTeacherList_OnRowCommand", ex);
                DisplayMessage("Teacher profile has loaded fail. " + ex.Message);
            }
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

        #region Other Methods / Clear Methods

        public void ClearControls()
        {
            hdTeacherId.Value = String.Empty;
            txtEmployeeName.Text = String.Empty;
            txtIndexNo.Text = String.Empty;
            txtEmployeeBanglaName.Text = String.Empty;
            txtEmployeeNID.Text = String.Empty;
            txtContactNo.Text = String.Empty;
            txtDateOfBirth.Text = String.Empty;
            txtSpouseName.Text = String.Empty;

            rbFemale.Checked = false;
            rbMale.Checked = false;

            txtFatherName.Text = String.Empty;
            txtFatherBanglaName.Text = String.Empty;
            txtMotherName.Text = String.Empty;
            txtMotherBanglaName.Text = String.Empty;
            txtPresentAddress.Text = String.Empty;
            txtPermanentAdress.Text = String.Empty;
            imgProfilePhoto.ImageUrl = String.Empty;
            ddlEmployeeType.SelectedIndex = -1;
            ddlUserGroup.SelectedIndex = -1;

            btnSave.Visible = true;
            btnUpdate.Visible = false;
            txtLoginID.Enabled = true;
            txtPassword.Enabled = true;
        }

        protected bool UploadProfilePhoto()
        {
            Boolean isSuccess = false;
            String imgName = DateTime.Now.Ticks.ToString();

            if (uploaderProfilePhoto.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(uploaderProfilePhoto.FileName);

                if (fileExtension.ToLower().Contains("jpg") || fileExtension.ToLower().Contains("jpeg") || fileExtension.ToLower().Contains("bmp") || fileExtension.ToLower().Contains("png"))
                {
                    string imgPath = "/ProfilePhoto/Employee/" + imgName + fileExtension.ToLower();
                    hdImagePath.Value = imgPath;

                    if (uploaderProfilePhoto.PostedFile != null && uploaderProfilePhoto.PostedFile.FileName != "")
                    {
                        uploaderProfilePhoto.SaveAs(Server.MapPath(imgPath));
                        imgProfilePhoto.ImageUrl = imgPath;
                        isSuccess = true;
                    }
                }
                else
                {
                    DisplayMessage(fileExtension.ToUpper() + " is not allowed! Please update image file and try again.");
                }
            }
            return isSuccess;
        }

        protected ValidationProcess IsValidationPassed()
        {
            ValidationProcess result = new ValidationProcess();
            if (String.IsNullOrEmpty(txtEmployeeName.Text))
            {
                txtEmployeeName.Focus();
                result.IsValid = false;
                result.Message = "Employee Name should only contain letters.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtEmployeeBanglaName.Text))
            {
                txtEmployeeBanglaName.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Employee Bangla Name";
                return result;
            }
            else if (String.IsNullOrEmpty(txtEmployeeNID.Text))
            {
                result.IsValid = false;
                result.Message = "Please Enter Employee National ID number";
                return result;
            }
            else if (String.IsNullOrEmpty(txtFatherName.Text))
            {
                txtFatherName.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Father";
                return result;
            }
            else if (String.IsNullOrEmpty(txtFatherBanglaName.Text))
            {
                txtFatherBanglaName.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Father Bangla Name";
                return result;
            }
            else if (String.IsNullOrEmpty(txtMotherName.Text))
            {
                txtMotherName.Focus();
                result.IsValid = false;
                result.Message = "Mother Name should only contain letters.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtMotherBanglaName.Text))
            {
                txtMotherBanglaName.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Mother Bangla Name";
                return result;
            }
            else if (String.IsNullOrEmpty(txtPresentAddress.Text))
            {
                txtPresentAddress.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Present Address";
                return result;
            }
            else if (String.IsNullOrEmpty(txtPermanentAdress.Text))
            {
                txtPermanentAdress.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Permanent Address";
                return result;
            }
            else if (!Validator.IsValidBDMobileNo(txtContactNo.Text))
            {
                txtContactNo.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Valid Contact Number";
                return result;
            }
            else if (String.IsNullOrEmpty(txtDateOfBirth.Text))
            {
                txtDateOfBirth.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Birth Date";
                return result;
            }
            else if (IsUserIdExist(txtLoginID.Text.ToString()))
            {
                txtLoginID.Focus();
                result.IsValid = false;
                result.Message = "Login ID Already Exist. Please Enter Different Login ID";
                return result;
            }

            return result;
        }

        protected bool IsUserIdExist(String sLoginID)
        {
            bool isUserExist = false;
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("LoginId", sLoginID);
                _param.Add("Action", "IsLoginExist");

                _reader = new DataReader();
                DataTable dtUser = _reader.GetDataTableByStoredProcedure("SP_EMPLOYEE_INFO", _param);
                if (dtUser.Rows.Count > 0)
                {
                    isUserExist = true;
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "IsUserIdExist", ex);
            }
            return isUserExist;
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