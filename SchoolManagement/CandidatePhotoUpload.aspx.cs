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
    public partial class CandidatePhotoUpload : System.Web.UI.Page
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
                LoadCandidateInfo();
            }
        }

        #region Load Primary Data
        private void LoadCandidateInfo()
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("AdmissionYear", ddlAdmissionYear.SelectedValue);
                _param.Add("Action", "ReadAll");

                _reader = new DataReader();
                DataTable dtCandidate = _reader.GetDataTableByStoredProcedure("SP_CANDIDATE", _param);
                if (dtCandidate.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtCandidate.Rows.Count.ToString();
                    gvCandidate.DataSource = dtCandidate;
                    gvCandidate.DataBind();
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvCandidate.DataSource = null;
                    gvCandidate.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadCandidateInfo", ex);
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
                            String imgPath = "/ProfilePhoto/Candidate/" + txtCandidateCode.Text + fileExtension.ToLower();
                            uploaderProfilePhoto.SaveAs(Server.MapPath(imgPath));

                            Hashtable _param = new Hashtable();
                            _param.Add("CandidateId", hdCandidateId.Value);
                            _param.Add("ProfilePhoto", imgPath);
                            _param.Add("EntryBy", user.UserID.ToString());
                            _param.Add("Action", "UploadPhoto");

                            _writer = new DataWriter();
                            long affRow = _writer.ExecuteStoredProcedure("SP_CANDIDATE", _param, "AffRow");
                            if (affRow > 0)
                            {
                                ActivityLog.SaveProcess(ActionType.Update, "SP_CANDIDATE", "UploadPhoto", hdCandidateId.Value.ToString());
                                DisplayMessage("Candidate profile photo has been uploaded successfully");

                                ClearControls();
                                LoadCandidateInfo();
                            }
                            else
                            {
                                DisplayMessage("Candidate profile photo update failed. Please try again");
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
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvCandidate_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditRow")
            {
                GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdCandidateId.Value = gvRow.Cells[1].Text;
                txtCandidateCode.Text = gvRow.Cells[2].Text.Replace("&nbsp;", "");
                txtCandidateName.Text = gvRow.Cells[3].Text.Replace("&nbsp;", "");
                txtFatherName.Text = gvRow.Cells[4].Text.Replace("&nbsp;", "");
                txtMotherName.Text = gvRow.Cells[5].Text.Replace("&nbsp;", "");
                txtContactNo.Text = gvRow.Cells[6].Text.Replace("&nbsp;", "");
            }
        }

        #endregion

        #region Others Code
        public void ClearControls()
        {
            hdCandidateId.Value = String.Empty;
            txtCandidateCode.Text = String.Empty;
            txtCandidateName.Text = String.Empty;
            txtFatherName.Text = String.Empty;
            txtMotherName.Text = String.Empty;
            txtContactNo.Text = String.Empty;
            hdCandidateId.Value = String.Empty;
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
                _param.Add("AdmissionYear", ddlAdmissionYear.SelectedValue);
                _param.Add("SearchBy", txtGridSearch.Text.Trim());
                _param.Add("Action", "AdvanceSearch");

                _reader = new DataReader();
                DataTable dtSubject = _reader.GetDataTableByStoredProcedure("SP_CANDIDATE", _param);
                if (dtSubject.Rows.Count > 0)
                {
                    gvCandidate.DataSource = dtSubject;
                    gvCandidate.DataBind();
                }
                else
                {
                    gvCandidate.DataSource = null;
                    gvCandidate.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnGridSearch_Click", ex);
            }
        }

        protected void btnGridSearchClear_Click(object sender, EventArgs e)
        {
            txtGridSearch.Text = String.Empty;
            LoadCandidateInfo();
        }
        #endregion

        protected void btnCandidateList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CollectionHistory.aspx", false);
        }
    }
}