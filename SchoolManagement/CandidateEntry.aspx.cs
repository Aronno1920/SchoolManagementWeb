using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreLibrary;

namespace SchoolManagement
{
    public partial class CandidateEntry : System.Web.UI.Page
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
                divStudent.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divGuardian.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divAcademic.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divProfilePhoto.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divCurrentAcademic.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divAddress.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divButton.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadClassInfo();
                LoadNationalityInfo();
                LoadReligionsInfo();
                LoadRelationsInfo();
                LoadSchoolListInfo();
                LoadDistrictsInfo();
                LoadAdmissionYearInfo();
                LoadFeesForCandidate();
                LoadPassingYearInfo();

                btnUpdate.Visible = false;
            }
        }

        #region Load Primary Data
        private void LoadClassInfo()
        {
            DataTable dtClasss = PopulateLists.GetClassForCandidate();
            if (dtClasss.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtClasss, ddlClassId, "ClassName", "ClassId");
            }
            else
            {
                ddlClassId.DataSource = null;
                ddlClassId.DataBind();
            }
        }

        protected void LoadNationalityInfo()
        {
            DataTable dbNationality = PopulateLists.GetNationalityList();
            if (dbNationality.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dbNationality, ddlNationality, "NationalityName", "NationalityId", true, "Select Nationality", "0");
            }
            else
            {
                ddlNationality.DataSource = null;
                ddlNationality.DataBind();
            }
        }

        protected void LoadReligionsInfo()
        {
            DataTable dtReligion = PopulateLists.GetReligionList();
            if (dtReligion.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtReligion, ddlReligion, "ReligionName", "ReligionId", true, "Select Religion", "0");
            }
            else
            {
                ddlReligion.DataSource = null;
                ddlReligion.DataBind();
            }
        }

        protected void LoadRelationsInfo()
        {
            DataTable dtRelation = PopulateLists.GetRelationshipList();
            if (dtRelation.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtRelation, ddlRelation, "RelationName", "RelationId", true, "Select Relation", "0");
            }
            else
            {
                ddlRelation.DataSource = null;
                ddlRelation.DataBind();
            }
        }

        protected void LoadSchoolListInfo()
        {
            DataTable dtSchool = PopulateLists.GetSchoolList();
            if (dtSchool.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtSchool, ddlSchool, "SchoolName", "SchoolId", true, "Select School", "0");
            }
            else
            {
                ddlSchool.DataSource = null;
                ddlSchool.DataBind();
            }
        }

        protected void LoadDistrictsInfo()
        {
            DataTable dtDistrict = PopulateLists.GetDistrictList();
            if (dtDistrict.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtDistrict, ddlDistrictPre, "District", "DistrictId", true, "Select District", "0");
                FillList.PopulateDropDownList(dtDistrict, ddlDistrictPer, "District", "DistrictId", true, "Select District", "0");
            }
            else
            {
                ddlDistrictPre.DataSource = null;
                ddlDistrictPre.DataBind();

                ddlDistrictPer.DataSource = null;
                ddlDistrictPer.DataBind();
            }
        }

        protected void LoadAdmissionYearInfo()
        {
            DataTable dtAdmissionYearFees = PopulateLists.GetCurrentAdmissionYearFees();
            if (dtAdmissionYearFees.Rows.Count > 0)
            {
                txtAdmissionYear.Text = dtAdmissionYearFees.Rows[0]["SessionYear"].ToString();
                txtSessionTitle.Text = dtAdmissionYearFees.Rows[0]["SessionTitle"].ToString(); 
                txtAmount.Text = dtAdmissionYearFees.Rows[0]["AdmissionFees"].ToString();
                txtPaidAmount.Text = dtAdmissionYearFees.Rows[0]["AdmissionFees"].ToString();
            }
        }

        protected void LoadFeesForCandidate()
        {
            DataTable dtCollectionType = PopulateLists.GetFeesForCandidate();
            if (dtCollectionType.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtCollectionType, ddlPaymentType, "AccountName", "AccountId");
                ddlPaymentType.SelectedIndex = 0;
            }
            else
            {
                ddlPaymentType.DataSource = null;
                ddlPaymentType.DataBind();
            }
        }

        protected void LoadPassingYearInfo()
        {
            DataTable dtYear = new DataTable();
            dtYear.Columns.Add("Value", typeof(Int32));
            dtYear.Columns.Add("Text", typeof(Int32));

            for (Int32 year = DateTime.Now.Year; year >= 2015; year--)
            {
                dtYear.Rows.Add(year, year);
            }

            FillList.PopulateDropDownList(dtYear, ddlPassingYear, "Text", "Value", true, "Select Year", "0");
        }

        #endregion

        #region Button Click and Selected Index Change Events
        protected void btnLoadCandidate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtFormNo.Text))
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("FormNo", txtFormNo.Text.ToString());
                    _param.Add("Action", "SearchByFormNo");

                    _reader = new DataReader();
                    DataTable dtInfo = _reader.GetDataTableByStoredProcedure("SP_CANDIDATE", _param);
                    if (dtInfo.Rows.Count > 0)
                    {
                        hdCandidateId.Value = dtInfo.Rows[0]["CandidateId"].ToString();
                        txtCandidateName.Text = dtInfo.Rows[0]["CandidateName"].ToString();
                        txtContactNo.Text = dtInfo.Rows[0]["ContactNo"].ToString();
                        txtAdmissionYear.Text = dtInfo.Rows[0]["AdmissionYear"].ToString();
                        
                        btnUpdate.Visible = true;
                        btnSave.Visible = false;
                    }
                    else
                    {
                        DisplayMessage("No information found");
                        btnUpdate.Visible = false;
                        btnSave.Visible = true;
                    }
                }
                else
                {
                    DisplayMessage("Please enter form no first. Then try again.");
                    txtFormNo.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
                DisplayMessage("Candidate profile has created fail. " + ex.Message);
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    UploadCandidatePhoto();
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                    Hashtable _param = new Hashtable();
                    _param.Add("AdmissionYear", txtAdmissionYear.Text);
                    _param.Add("CandidateName", textInfo.ToTitleCase(txtCandidateName.Text));
                    _param.Add("CandidateNameBangla", txtBanglaName.Text);
                    _param.Add("ProfilePhoto", hdImagePath.Value.ToString());
                    _param.Add("BirthCertificateNo", txtBirthCerId.Text);
                    _param.Add("BirthDate", txtBirthDate.Text);
                    _param.Add("NationalityId", ddlNationality.SelectedValue);
                    _param.Add("ReligionId", ddlReligion.SelectedValue);
                    _param.Add("FatherName", textInfo.ToTitleCase(txtFatherName.Text));
                    _param.Add("FatherNameBangla", txtFatherNameBangla.Text);
                    _param.Add("FatherNID", txtFatherNID.Text);
                    _param.Add("MotherName", textInfo.ToTitleCase(txtMotherName.Text));
                    _param.Add("MotherNameBangla", txtMotherNameBangla.Text);
                    _param.Add("MotherNID", txtMotherNID.Text);
                    _param.Add("GuardianName", textInfo.ToTitleCase(txtGuardianName.Text));
                    _param.Add("RelationId", ddlRelation.SelectedValue);
                    _param.Add("Occupation", txtOccupation.Text);
                    _param.Add("YearlyIncome", txtIncome.Text);
                    _param.Add("ContactNo", txtContactNo.Text);
                    _param.Add("SchoolId", ddlSchool.SelectedValue);
                    _param.Add("LastClass", textInfo.ToTitleCase(txtLastClass.Text));
                    _param.Add("PassingYear", ddlPassingYear.SelectedValue);
                    _param.Add("RollNo", txtRollNo.Text);
                    _param.Add("GPA", txtGPA.Text);
                    _param.Add("PreAddress", textInfo.ToTitleCase(txtAddressPre.Text));
                    _param.Add("PreDistrictId", ddlDistrictPre.SelectedValue);
                    _param.Add("PreUpazillaId", ddlUpazillaPre.SelectedValue);
                    _param.Add("PrePostOfficeId", ddlPostOfficePre.SelectedValue);
                    if (cbSameAsPresent.Checked == true)
                    {
                        _param.Add("PerAddress", textInfo.ToTitleCase(txtAddressPre.Text));
                        _param.Add("PerDistrictId", ddlDistrictPre.SelectedValue);
                        _param.Add("PerUpazillaId", ddlUpazillaPre.SelectedValue);
                        _param.Add("PerPostOfficeId", ddlPostOfficePre.SelectedValue);
                    }
                    else
                    {
                        _param.Add("PerAddress", textInfo.ToTitleCase(txtAddressPer.Text));
                        _param.Add("PerDistrictId", ddlDistrictPer.SelectedValue);
                        _param.Add("PerUpazillaId", ddlUpazillaPer.SelectedValue);
                        _param.Add("PerPostOfficeId", ddlPostOfficePer.SelectedValue);
                    }
                    _param.Add("ClassId", ddlClassId.SelectedValue);
                    _param.Add("Dance", cbExpertise.Items[0].Selected);
                    _param.Add("Song", cbExpertise.Items[1].Selected);
                    _param.Add("Poetry", cbExpertise.Items[2].Selected);
                    _param.Add("Recitation", cbExpertise.Items[3].Selected);
                    _param.Add("Drawing", cbExpertise.Items[4].Selected);
                    _param.Add("Sports", cbExpertise.Items[5].Selected);
                    _param.Add("FeesId", ddlPaymentType.SelectedValue);
                    _param.Add("FeeTitle", ddlPaymentType.SelectedItem.Text);
                    _param.Add("Amount", txtAmount.Text.ToString());
                    _param.Add("PaidAmount", txtPaidAmount.Text.ToString());
                    _param.Add("Remarks", txtRemarks.Text.ToString());
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "Create");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_CANDIDATE", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_CANDIDATE", "Create", iAffRow.ToString());
                        DisplayMessage("Candidate profile has created successfully");

                        btnClear_OnClick(sender, e);
                    }
                    else
                    {
                        DisplayMessage("Candidate profile has created successfully. Please try again");
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
                DisplayMessage("Candidate profile has created fail. " + ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    UploadCandidatePhoto();
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                    Hashtable _param = new Hashtable();
                    _param.Add("CandidateId", hdCandidateId.Value.ToString());
                    _param.Add("AdmissionYear", txtAdmissionYear.Text);
                    _param.Add("CandidateName", textInfo.ToTitleCase(txtCandidateName.Text));
                    _param.Add("CandidateNameBangla", txtBanglaName.Text);
                    _param.Add("ProfilePhoto", hdImagePath.Value.ToString());
                    _param.Add("BirthCertificateNo", txtBirthCerId.Text);
                    _param.Add("BirthDate", txtBirthDate.Text);
                    _param.Add("NationalityId", ddlNationality.SelectedValue);
                    _param.Add("ReligionId", ddlReligion.SelectedValue);
                    _param.Add("FatherName", textInfo.ToTitleCase(txtFatherName.Text));
                    _param.Add("FatherNameBangla", txtFatherNameBangla.Text);
                    _param.Add("FatherNID", txtFatherNID.Text);
                    _param.Add("MotherName", textInfo.ToTitleCase(txtMotherName.Text));
                    _param.Add("MotherNameBangla", txtMotherNameBangla.Text);
                    _param.Add("MotherNID", txtMotherNID.Text);
                    _param.Add("GuardianName", textInfo.ToTitleCase(txtGuardianName.Text));
                    _param.Add("RelationId", ddlRelation.SelectedValue);
                    _param.Add("Occupation", txtOccupation.Text);
                    _param.Add("YearlyIncome", txtIncome.Text);
                    _param.Add("ContactNo", txtContactNo.Text);
                    _param.Add("SchoolId", ddlSchool.SelectedValue);
                    _param.Add("LastClass", textInfo.ToTitleCase(txtLastClass.Text));
                    _param.Add("PassingYear", ddlPassingYear.SelectedValue);
                    _param.Add("RollNo", txtRollNo.Text);
                    _param.Add("GPA", txtGPA.Text);
                    _param.Add("PreAddress", textInfo.ToTitleCase(txtAddressPre.Text));
                    _param.Add("PreDistrictId", ddlDistrictPre.SelectedValue);
                    _param.Add("PreUpazillaId", ddlUpazillaPre.SelectedValue);
                    _param.Add("PrePostOfficeId", ddlPostOfficePre.SelectedValue);
                    if (cbSameAsPresent.Checked == true)
                    {
                        _param.Add("PerAddress", textInfo.ToTitleCase(txtAddressPre.Text));
                        _param.Add("PerDistrictId", ddlDistrictPre.SelectedValue);
                        _param.Add("PerUpazillaId", ddlUpazillaPre.SelectedValue);
                        _param.Add("PerPostOfficeId", ddlPostOfficePre.SelectedValue);
                    }
                    else
                    {
                        _param.Add("PerAddress", textInfo.ToTitleCase(txtAddressPer.Text));
                        _param.Add("PerDistrictId", ddlDistrictPer.SelectedValue);
                        _param.Add("PerUpazillaId", ddlUpazillaPer.SelectedValue);
                        _param.Add("PerPostOfficeId", ddlPostOfficePer.SelectedValue);
                    }
                    _param.Add("ClassId", ddlClassId.SelectedValue);
                    _param.Add("Dance", cbExpertise.Items[0].Selected);
                    _param.Add("Song", cbExpertise.Items[1].Selected);
                    _param.Add("Poetry", cbExpertise.Items[2].Selected);
                    _param.Add("Recitation", cbExpertise.Items[3].Selected);
                    _param.Add("Drawing", cbExpertise.Items[4].Selected);
                    _param.Add("Sports", cbExpertise.Items[5].Selected);
                    _param.Add("Remarks", txtRemarks.Text.ToString());
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "Update");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_CANDIDATE", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_CANDIDATE", "Update", iAffRow.ToString());
                        DisplayMessage("Candidate profile has created successfully");

                        btnClear_OnClick(sender, e);
                    }
                    else
                    {
                        DisplayMessage("Candidate profile has created successfully. Please try again");
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
                DisplayMessage("Candidate profile has created fail. " + ex.Message);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ClearControls();
            LoadClassInfo();
            LoadNationalityInfo();
            LoadReligionsInfo();
            LoadRelationsInfo();
            LoadSchoolListInfo();
            LoadDistrictsInfo();
            LoadAdmissionYearInfo();
            LoadFeesForCandidate();
            LoadPassingYearInfo();

            txtFormNo.Text = String.Empty;
            btnUpdate.Visible = false;
            btnSave.Visible = true;
        }

        protected void ddlDistrictPre_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtDistrict = PopulateLists.GetUpazillaListByDistrict(ddlDistrictPre.SelectedValue.ToString());
            if (dtDistrict.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtDistrict, ddlUpazillaPre, "Upazilla", "UpazillaId", true, "Select Upazilla", "0");
            }
            else
            {
                ddlDistrictPre.DataSource = null;
                ddlDistrictPre.DataBind();
            }

            ddlPostOfficePre.DataSource = null;
            ddlPostOfficePre.DataBind();
        }

        protected void ddlUpazillaPre_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtPostOffice = PopulateLists.GetPostOfficeListByUpazilla(ddlUpazillaPre.SelectedValue.ToString());
            if (dtPostOffice.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtPostOffice, ddlPostOfficePre, "PostOffice", "PostOfficeId", true, "Select Post Office", "0");
            }
            else
            {
                ddlUpazillaPre.DataSource = null;
                ddlUpazillaPre.DataBind();
            }
        }

        protected void ddlDistrictPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtDistrict = PopulateLists.GetUpazillaListByDistrict(ddlDistrictPer.SelectedValue.ToString());
            if (dtDistrict.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtDistrict, ddlUpazillaPer, "Upazilla", "UpazillaId", true, "Select Upazilla", "0");
            }
            else
            {
                ddlDistrictPer.DataSource = null;
                ddlDistrictPer.DataBind();
            }


            ddlPostOfficePer.DataSource = null;
            ddlPostOfficePer.DataBind();
        }

        protected void ddlUpazillaPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtPostOffice = PopulateLists.GetPostOfficeListByUpazilla(ddlUpazillaPer.SelectedValue.ToString());
            if (dtPostOffice.Rows.Count > 0)
            {
                FillList.PopulateDropDownList(dtPostOffice, ddlPostOfficePer, "PostOffice", "PostOfficeId", true, "Select Post Office", "0");
            }
            else
            {
                ddlUpazillaPer.DataSource = null;
                ddlUpazillaPer.DataBind();
            }
        }

        protected void btnCandidateList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CandidateList.aspx", false);
        }
        #endregion

        #region Other Methods / Clear Methods

        public void ClearControls()
        {
            txtAmount.Text = String.Empty;
            txtPaidAmount.Text = String.Empty;
            txtRemarks.Text = String.Empty;
            txtCandidateName.Text = String.Empty;
            txtBanglaName.Text = String.Empty;
            hdImagePath.Value = String.Empty;
            txtBirthCerId.Text = String.Empty;
            txtBirthDate.Text = String.Empty;
            ddlNationality.SelectedIndex = -1;
            ddlReligion.SelectedIndex = -1;
            txtFatherName.Text = String.Empty;
            txtFatherNameBangla.Text = String.Empty;
            txtFatherNID.Text = String.Empty;
            txtMotherName.Text = String.Empty;
            txtMotherNameBangla.Text = String.Empty;
            txtMotherNID.Text = String.Empty;
            txtGuardianName.Text = String.Empty;
            ddlRelation.SelectedIndex = -1;
            txtOccupation.Text = String.Empty;
            txtIncome.Text = String.Empty;
            txtContactNo.Text = String.Empty;
            ddlSchool.SelectedIndex = -1;
            txtLastClass.Text = String.Empty; ;
            ddlPassingYear.SelectedIndex = -1;
            txtRollNo.Text = String.Empty;
            txtGPA.Text = String.Empty; ;
            txtAddressPre.Text = String.Empty;
            ddlDistrictPre.SelectedIndex = -1;
            ddlUpazillaPre.SelectedIndex = -1;
            ddlPostOfficePre.SelectedIndex = -1;
            txtAddressPer.Text = String.Empty;
            ddlDistrictPer.SelectedIndex = -1;
            ddlUpazillaPer.SelectedIndex = -1;
            ddlPostOfficePer.SelectedIndex = -1;
            ddlClassId.SelectedIndex = -1;
            cbExpertise.Items[0].Selected = false;
            cbExpertise.Items[1].Selected = false;
            cbExpertise.Items[2].Selected = false;
            cbExpertise.Items[3].Selected = false;
            cbExpertise.Items[4].Selected = false;
            cbExpertise.Items[5].Selected = false;
            ddlPaymentType.SelectedIndex = -1;
            imgProfilePhoto.ImageUrl = String.Empty;
        }

        protected bool UploadCandidatePhoto()
        {
            Boolean isSuccess = false;
            String imgName = DateTime.Now.Ticks.ToString();

            if (uploaderProfilePhoto.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(uploaderProfilePhoto.FileName);

                if (fileExtension.ToLower().Contains("jpg") || fileExtension.ToLower().Contains("jpeg") || fileExtension.ToLower().Contains("bmp") || fileExtension.ToLower().Contains("png"))
                {
                    string imgPath = "/ProfilePhoto/Candidate/" + imgName + fileExtension.ToLower();
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
            if (String.IsNullOrEmpty(txtCandidateName.Text))
            {
                txtCandidateName.Focus();
                result.IsValid = false;
                result.Message = "Candidate Name should only contain letters.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtBanglaName.Text))
            {
                txtBanglaName.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Candidate Bangla Name";
                return result;
            }
            else if (String.IsNullOrEmpty(txtBirthDate.Text))
            {
                result.IsValid = false;
                result.Message = "Please Select Date of Birth";
                return result;
            }
            else if (String.IsNullOrEmpty(txtFatherName.Text))
            {
                txtFatherName.Focus();
                result.IsValid = false;
                result.Message = "Father Name should only contain letters.";
                return result;
            }
            else if (String.IsNullOrEmpty(txtFatherNameBangla.Text))
            {
                txtFatherNameBangla.Focus();
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
            else if (String.IsNullOrEmpty(txtMotherNameBangla.Text))
            {
                txtMotherNameBangla.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Mother Bangla Name";
                return result;
            }
            else if (String.IsNullOrEmpty(txtGuardianName.Text))
            {
                txtGuardianName.Focus();
                result.IsValid = false;
                result.Message = "Guardian Name should only contain letters.";
                return result;
            }
            //else if (ddlSchool.SelectedValue == "0")
            //{
            //    result.IsValid = false;
            //    result.Message = "Please Select Previous School";
            //    return result;
            //}
            //else if (String.IsNullOrEmpty(txtLastClass.Text))
            //{
            //    txtLastClass.Focus();
            //    result.IsValid = false;
            //    result.Message = "Last Class should only contain letters.";
            //    return result;
            //}
            //else if (ddlPassingYear.SelectedValue == "0")
            //{
            //    result.IsValid = false;
            //    result.Message = "Please Select Passing Year";
            //    return result;
            //}
            //else if (!Validator.IsIntNumberOnly(txtRollNo.Text))
            //{
            //    txtRollNo.Focus();
            //    result.IsValid = false;
            //    result.Message = "PSC/GSC Roll Number should only contain numbers.";
            //    return result;
            //}
            //else if (!Validator.IsNumericOnly(txtGPA.Text))
            //{
            //    txtGPA.Focus();
            //    result.IsValid = false;
            //    result.Message = "PSC/GSC GPA should only contain numbers.";
            //    return result;
            //}
            //else if (Convert.ToDecimal(txtGPA.Text) > 5 || Convert.ToDecimal(txtGPA.Text) < 1)
            //{
            //    txtGPA.Focus();
            //    result.IsValid = false;
            //    result.Message = "PSC/GSC GPA should minimum 1 to 5";
            //    return result;
            //}
            else if (ddlNationality.SelectedValue == "0")
            {
                result.IsValid = false;
                result.Message = "Please Select Nationality";
                return result;
            }
            else if (ddlReligion.SelectedValue == "0")
            {
                result.IsValid = false;
                result.Message = "Please Select Religion";
                return result;
            }
            else if (ddlClassId.SelectedValue == "0")
            {
                result.IsValid = false;
                result.Message = "Please Select Admission Class";
                return result;
            }
            else if (ddlReligion.SelectedValue == "0")
            {
                result.IsValid = false;
                result.Message = "Please Select Payment Type";
                return result;
            }
            else if (!Validator.IsIntNumberOnly(txtPaidAmount.Text))
            {
                txtPaidAmount.Focus();
                result.IsValid = false;
                result.Message = "Admission Fee should only contain numbers.";
                return result;
            }
            else if (!txtAmount.Text.Equals(txtPaidAmount.Text) && String.IsNullOrEmpty(txtRemarks.Text))
            {
                txtRemarks.Focus();
                result.IsValid = false;
                result.Message = "Please Enter Remarks.";
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