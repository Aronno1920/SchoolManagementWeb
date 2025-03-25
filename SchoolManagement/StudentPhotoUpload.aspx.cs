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
    public partial class StudentPhotoUpload : System.Web.UI.Page
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

                LoadAdmissionYear();
                LoadStudentInfo();
            }
        }

        #region Load Primary Data
        private void LoadStudentInfo()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("Action", "ReadAll");

                _reader = new DataReader();
                DataTable dtStudent = _reader.GetDataTableByStoredProcedure("SP_STUDENT", _param);
                if (dtStudent.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtStudent.Rows.Count.ToString();
                    gvStudent.DataSource = dtStudent;
                    gvStudent.DataBind();
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvStudent.DataSource = null;
                    gvStudent.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSearch_OnClick", ex);
                DisplayMessage("Candidate profile has created fail. " + ex.Message);
            }
        }

        private void LoadAdmissionYear()
        {
            DataTable dtAdmissionYear = PopulateLists.GetSessionList();
            if (dtAdmissionYear.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtAdmissionYear, ddlAdmissionYear, "SessionTitle", "SessionYear");
                ddlAdmissionYear.SelectedIndex = 0;
            }
            else
            {
                ddlAdmissionYear.DataSource = null;
                ddlAdmissionYear.DataBind();
            }
        }
        #endregion

        #region Button Click Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (uploaderProfilePhoto.HasFile)
                {
                    String fileExtension = System.IO.Path.GetExtension(uploaderProfilePhoto.FileName);
                    if (fileExtension.ToLower().Contains("jpg") || fileExtension.ToLower().Contains("jpeg") || fileExtension.ToLower().Contains("bmp") || fileExtension.ToLower().Contains("png"))
                    {
                        if (uploaderProfilePhoto.PostedFile != null && uploaderProfilePhoto.PostedFile.FileName != "")
                        {
                            String imgPath = "/ProfilePhoto/Student/" + txtStudentCode.Text + fileExtension.ToLower();
                            uploaderProfilePhoto.SaveAs(Server.MapPath(imgPath));

                            Hashtable _param = new Hashtable();
                            _param.Add("StudentId", hdStudentId.Value);
                            _param.Add("ProfilePhoto", imgPath);
                            _param.Add("EntryBy", user.UserID.ToString());
                            _param.Add("Action", "UploadPhoto");

                            _writer = new DataWriter();
                            long affRow = _writer.ExecuteStoredProcedure("SP_STUDENT", _param, "AffRow");
                            if (affRow > 0)
                            {
                                ActivityLog.SaveProcess(ActionType.Update, "SP_STUDENT", "UploadPhoto", affRow.ToString());
                                ClearControls();
                                LoadStudentInfo();
                                DisplayMessage("Student profile photo has been uploaded successful");
                            }
                            else
                            {
                                DisplayMessage("Student profile photo update failed. Please try again");
                            }
                        }
                    }
                    else
                    {
                        DisplayMessage(fileExtension.ToUpper() + " is not allowed! Please update image file and try again.");
                    }
                }
                else
                {
                    DisplayMessage("Please select an image to upload");
                }

            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSearch_OnClick", ex);
                DisplayMessage("Candidate profile has created fail. " + ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditRow")
                {
                    GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    hdStudentId.Value = gvRow.Cells[1].Text;
                    txtStudentCode.Text = gvRow.Cells[2].Text.Replace("&nbsp;", "");
                    txtStudentName.Text = gvRow.Cells[4].Text.Replace("&nbsp;", "");
                    txtClassName.Text = gvRow.Cells[5].Text.Replace("&nbsp;", "");
                    txtSectionName.Text = gvRow.Cells[6].Text.Replace("&nbsp;", "");
                    txtContactNo.Text = gvRow.Cells[7].Text.Replace("&nbsp;", "");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "gvStudent_RowCommand", ex);
                DisplayMessage(ex.Message.ToString(), "Contact to Software Administrator");
            }
        }

        #endregion

        #region Others Code
        public void ClearControls()
        {
            hdStudentId.Value = String.Empty;
            txtStudentCode.Text = String.Empty;
            txtStudentName.Text = String.Empty;
            txtClassName.Text = String.Empty;
            txtSectionName.Text = String.Empty;
            txtContactNo.Text = String.Empty;
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

        #region Search Box Related Methods
        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("AdmissionYear", ddlAdmissionYear.SelectedValue.ToString());
                _param.Add("SearchBy", txtGridSearch.Text.Trim());
                _param.Add("Action", "AdvanceSearch");

                _reader = new DataReader();
                DataTable dtSubject = _reader.GetDataTableByStoredProcedure("SP_STUDENT", _param);
                if (dtSubject.Rows.Count > 0)
                {
                    gvStudent.DataSource = dtSubject;
                    gvStudent.DataBind();
                }
                else
                {
                    gvStudent.DataSource = null;
                    gvStudent.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSearch_OnClick", ex);
                DisplayMessage("Candidate profile has created fail. " + ex.Message);
            }
        }

        protected void btnGridSearchClear_Click(object sender, EventArgs e)
        {
            txtGridSearch.Text = String.Empty;
            LoadStudentInfo();
        }
        #endregion

        protected void gvStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStudent.PageIndex = e.NewPageIndex;
            LoadStudentInfo();
        }

        protected void btnStudentList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StudentList.aspx", false);
        }
    }
}