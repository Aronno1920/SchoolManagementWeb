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
    public partial class MyProfile : System.Web.UI.Page
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
            #endregion

            if (!IsPostBack)
            {
                divPicturePanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divEntryPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                sPageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                txtOldPassword.Attributes["value"] = txtOldPassword.Text;
                txtPassword.Attributes["value"] = txtPassword.Text;
                txtConfirmPassword.Attributes["value"] = txtConfirmPassword.Text;

                LoadEmployeeProfile();
            }
        }

        #region Load Primary Data
        protected void LoadEmployeeProfile()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("EmployeeId", user.UserID.ToString());
                _param.Add("Action", "LoadProfile");

                _reader = new DataReader();
                DataTable dtProfile = _reader.GetDataTableByStoredProcedure("SP_MANAGE_PASSWORD", _param);
                if (dtProfile.Rows.Count > 0)
                {
                    imgCurrentPhoto.ImageUrl = dtProfile.Rows[0]["ProfilePhoto"].ToString();
                    txtEmployeeName.Text = dtProfile.Rows[0]["EmployeeName"].ToString();
                    txtEmployeeNID.Text = dtProfile.Rows[0]["EmployeeNID"].ToString();
                    txtIndexNo.Text = dtProfile.Rows[0]["IndexNumber"].ToString();
                    txtFatherName.Text = dtProfile.Rows[0]["FatherName"].ToString();
                    txtMotherName.Text = dtProfile.Rows[0]["MotherName"].ToString();
                    txtContactNo.Text = dtProfile.Rows[0]["ContactNo"].ToString();
                    txtLoginID.Text = dtProfile.Rows[0]["LoginId"].ToString();
                    hdOldPassword.Value = dtProfile.Rows[0]["Password"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadEmployeeProfile", ex);
                DisplayMessage("Emmployee profile load failed. " + ex.Message);
            }
            // 
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
                    String ePassword = UtilityClass.Encrypt(txtPassword.Text.ToString(), true);
                    
                    Hashtable _param = new Hashtable();
                    _param.Add("EmployeeId", user.UserID.ToString());
                    _param.Add("Password", ePassword);
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "ChangePassword");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_MANAGE_PASSWORD", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Update, "SP_MANAGE_PASSWORD", "ChangePassword", user.UserID.ToString());
                        hdOldPassword.Value = ePassword;
                        DisplayMessage("Login Password has been saved successful.");
                    }
                    else
                    {
                        DisplayMessage("Login Password has been save failed. Please try again.");
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

        protected void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                UploadProfilePhoto();

                Hashtable _param = new Hashtable();
                _param.Add("EmployeeId", user.UserID.ToString());
                _param.Add("PhotoPath", hdImagePath.Value.ToString());
                _param.Add("EntryBy", user.UserID.ToString());
                _param.Add("Action", "UploadPhoto");

                _writer = new DataWriter();
                long iAffRow = _writer.ExecuteStoredProcedure("SP_MANAGE_PASSWORD", _param, "AffRow");
                if (iAffRow > 0)
                {
                    ActivityLog.SaveProcess(ActionType.Update, "SP_MANAGE_PASSWORD", "UploadPhoto", user.UserID.ToString());
                    DisplayMessage("Profile Photo has been saved successful");
                }
                else
                {
                    DisplayMessage("Profile Photo has been save failed. Please try again");
                }
            }
            catch(Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnUploadPhoto_Click", ex);
                DisplayMessage("Profile photo upload failed. " + ex.Message);
            }
        }
        #endregion

        #region Other Methods / Clear Methods

        public void ClearControls()
        {
            txtEmployeeName.Text = String.Empty;
            txtEmployeeNID.Text = String.Empty;
            txtFatherName.Text = String.Empty;
            txtContactNo.Text = String.Empty;
            hdImagePath.Value = String.Empty;
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
            if (String.IsNullOrEmpty(txtOldPassword.Text.Trim()))
            {
                txtOldPassword.Focus();
                result.IsValid = false;
                result.Message = "Old password can not be empty.";
                return result;
            }
            else if(String.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                txtPassword.Focus();
                result.IsValid = false;
                result.Message = "Password can not be empty.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                txtConfirmPassword.Focus();
                result.IsValid = false;
                result.Message = "Confirm password can not be empty.";
                return result;
            }
            else if (!hdOldPassword.Value.Equals(UtilityClass.Encrypt(txtOldPassword.Text.Trim(), true)))
            {
                txtOldPassword.Focus();
                result.IsValid = false;
                result.Message = "Old password dose not match. Enter correct old password.";
                return result;
            }
            else if (!txtPassword.Text.Equals(txtConfirmPassword.Text.Trim()))
            {
                txtConfirmPassword.Focus();
                result.IsValid = false;
                result.Message = "Confirm password dose not match with new password. Enter correct confirm password.";
                return result;
            }
            else if (hdOldPassword.Value.Equals(UtilityClass.Encrypt(txtConfirmPassword.Text.Trim(), true)))
            {
                txtPassword.Focus();
                result.IsValid = false;
                result.Message = "The new password you entered is the same as your old password. Enter a different password.";
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