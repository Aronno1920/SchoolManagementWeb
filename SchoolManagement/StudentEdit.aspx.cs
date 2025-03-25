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
    public partial class StudentEdit : System.Web.UI.Page
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
                divAddress.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divExpertise.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divButton.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadNationalityInfo();
                LoadReligionsInfo();
                LoadRelationsInfo();
                LoadSchoolListInfo();
                LoadDistrictsInfo();
                LoadPassingYearInfo();
                IsHasQueryString();
            }
        }

        #region Load Primary Data

        protected void IsHasQueryString()
        {
            if (Session["SCode"] != null) 
            {
                txtStudentCode.Text = Session["SCode"].ToString();
                btnLoadStudent_Click(new object(), new EventArgs());
                Session["SCode"] = null;
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

        #region Other Methods / Clear Methods
        
        public void ClearControls()
        {
            hdStudentId.Value = String.Empty;
            txtStudentCode.Text = String.Empty;
            txtStudentName.Text = String.Empty;
            txtBanglaName.Text = String.Empty;
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

            cbDance.Checked = false;
            cbSong.Checked = false;
            cbPoetry.Checked = false;
            cbRecitation.Checked = false;
            cbDrawing.Checked = false;
            cbSports.Checked = false;
        }

        protected ValidationProcess IsValidationPassed()
        {
            ValidationProcess result = new ValidationProcess();

            
if (String.IsNullOrEmpty(txtStudentCode.Text))
            {
                txtStudentCode.Focus();
                result.IsValid = false;
                result.Message = "Search by Student Code first..";
                return result;
            }
            else if (String.IsNullOrEmpty(txtStudentName.Text))
            {
                txtStudentName.Focus();
                result.IsValid = false;
                result.Message = "Student Name should only contain letters.";
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

        #region Button Click and Selected Index Change Events

        protected void btnLoadStudent_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable _param = new Hashtable();
                _param.Add("StudentCode", txtStudentCode.Text.ToString());
                _param.Add("Action", "CodeWiseStudent");

                _reader = new DataReader();
                DataTable dtStudentInfo = _reader.GetDataTableByStoredProcedure("SP_STUDENT_UPDATE", _param);
                if (dtStudentInfo.Rows.Count > 0)
                {
                    hdStudentId.Value= dtStudentInfo.Rows[0]["StudentId"].ToString();
                    txtStudentName.Text = dtStudentInfo.Rows[0]["StudentName"].ToString();
                    txtBanglaName.Text = dtStudentInfo.Rows[0]["StudentNameBangla"].ToString();
                    txtBirthCerId.Text = dtStudentInfo.Rows[0]["BirthCertificateNo"].ToString();
                    txtBirthDate.Text = dtStudentInfo.Rows[0]["BirthDate"].ToString();
                    ddlNationality.SelectedIndex = ddlNationality.Items.IndexOf(ddlNationality.Items.FindByValue(dtStudentInfo.Rows[0]["NationalityId"].ToString()));
                    ddlReligion.SelectedIndex = ddlReligion.Items.IndexOf(ddlReligion.Items.FindByValue(dtStudentInfo.Rows[0]["ReligionId"].ToString()));

                    txtFatherName.Text = dtStudentInfo.Rows[0]["FatherName"].ToString();
                    txtFatherNameBangla.Text = dtStudentInfo.Rows[0]["FatherNameBangla"].ToString();
                    txtFatherNID.Text = dtStudentInfo.Rows[0]["FatherNID"].ToString();

                    txtMotherName.Text = dtStudentInfo.Rows[0]["MotherName"].ToString();
                    txtMotherNameBangla.Text = dtStudentInfo.Rows[0]["MotherNameBangla"].ToString();
                    txtMotherNID.Text = dtStudentInfo.Rows[0]["MotherNID"].ToString();

                    txtGuardianName.Text = dtStudentInfo.Rows[0]["GuardianName"].ToString();
                    ddlRelation.SelectedIndex = ddlRelation.Items.IndexOf(ddlRelation.Items.FindByValue(dtStudentInfo.Rows[0]["RelationId"].ToString()));
                    txtOccupation.Text = dtStudentInfo.Rows[0]["Occupation"].ToString();
                    txtIncome.Text = dtStudentInfo.Rows[0]["YearlyIncome"].ToString();
                    txtContactNo.Text = dtStudentInfo.Rows[0]["ContactNo"].ToString();

                    ddlSchool.SelectedIndex = ddlSchool.Items.IndexOf(ddlSchool.Items.FindByValue(dtStudentInfo.Rows[0]["SchoolId"].ToString()));
                    txtLastClass.Text = dtStudentInfo.Rows[0]["LastClass"].ToString();
                    ddlPassingYear.SelectedIndex = ddlPassingYear.Items.IndexOf(ddlPassingYear.Items.FindByValue(dtStudentInfo.Rows[0]["PassingYear"].ToString()));
                    txtRollNo.Text = dtStudentInfo.Rows[0]["LastRollNo"].ToString();
                    txtGPA.Text = dtStudentInfo.Rows[0]["LastGPA"].ToString();

                    txtAddressPre.Text = dtStudentInfo.Rows[0]["PreAddress"].ToString();
                    ddlDistrictPre.SelectedIndex = ddlDistrictPre.Items.IndexOf(ddlDistrictPre.Items.FindByValue(dtStudentInfo.Rows[0]["PreDistrictId"].ToString()));
                    ddlDistrictPre_SelectedIndexChanged(sender, e);
                    ddlUpazillaPre.SelectedIndex = ddlUpazillaPre.Items.IndexOf(ddlUpazillaPre.Items.FindByValue(dtStudentInfo.Rows[0]["PreUpazillaId"].ToString()));
                    ddlUpazillaPre_SelectedIndexChanged(sender, e);
                    ddlPostOfficePre.SelectedIndex = ddlPostOfficePre.Items.IndexOf(ddlPostOfficePre.Items.FindByValue(dtStudentInfo.Rows[0]["PrePostOfficeId"].ToString()));

                    txtAddressPer.Text = dtStudentInfo.Rows[0]["PerAddress"].ToString();
                    ddlDistrictPer.SelectedIndex = ddlDistrictPer.Items.IndexOf(ddlDistrictPer.Items.FindByValue(dtStudentInfo.Rows[0]["PerDistrictId"].ToString()));
                    ddlDistrictPer_SelectedIndexChanged(sender, e);
                    ddlUpazillaPer.SelectedIndex = ddlUpazillaPer.Items.IndexOf(ddlUpazillaPer.Items.FindByValue(dtStudentInfo.Rows[0]["PerUpazillaId"].ToString()));
                    ddlUpazillaPer_SelectedIndexChanged(sender, e);
                    ddlPostOfficePer.SelectedIndex = ddlPostOfficePer.Items.IndexOf(ddlPostOfficePer.Items.FindByValue(dtStudentInfo.Rows[0]["PerPostOfficeId"].ToString()));

                    cbDance.Checked = Convert.ToBoolean(dtStudentInfo.Rows[0]["Dance"]);
                    cbSong.Checked = Convert.ToBoolean(dtStudentInfo.Rows[0]["Song"]);
                    cbPoetry.Checked = Convert.ToBoolean(dtStudentInfo.Rows[0]["Poetry"]);
                    cbRecitation.Checked = Convert.ToBoolean(dtStudentInfo.Rows[0]["Recitation"]);
                    cbDrawing.Checked = Convert.ToBoolean(dtStudentInfo.Rows[0]["Drawing"]);
                    cbSports.Checked = Convert.ToBoolean(dtStudentInfo.Rows[0]["Sports"]);
                }
                else
                {
                    DisplayMessage("No student found with the code");
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnLoadStudent_Click", ex);
                DisplayMessage("Student profile has been failed to load. " + ex.Message);
            }
        }

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidationProcess result = IsValidationPassed();
                if (result.IsValid == true)
                {
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                    Hashtable _param = new Hashtable();
                    _param.Add("StudentId", hdStudentId.Value);
                    _param.Add("StudentName", textInfo.ToTitleCase(txtStudentName.Text));
                    _param.Add("StudentNameBangla", txtBanglaName.Text);
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
                    _param.Add("PreAddress", txtAddressPre.Text);
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
                    _param.Add("Dance", (cbDance.Checked?"1":"0"));
                    _param.Add("Song", (cbSong.Checked ? "1" : "0"));
                    _param.Add("Poetry", (cbPoetry.Checked ? "1" : "0"));
                    _param.Add("Recitation", (cbRecitation.Checked ? "1" : "0"));
                    _param.Add("Drawing", (cbDrawing.Checked ? "1" : "0"));
                    _param.Add("Sports", (cbSports.Checked ? "1" : "0"));
                    _param.Add("EntryBy", user.UserID.ToString());
                    _param.Add("Action", "UpdateProfile");

                    _writer = new DataWriter();
                    long iAffRow = _writer.ExecuteStoredProcedure("SP_STUDENT_UPDATE", _param, "AffRow");
                    if (iAffRow > 0)
                    {
                        ActivityLog.SaveProcess(ActionType.Insert, "SP_STUDENT_UPDATE", "UpdateProfile", iAffRow.ToString());
                        DisplayMessage("Student profile has been updated successful");
                        btnClear_OnClick(sender, e);
                    }
                    else
                    {
                        DisplayMessage("Student profile has been failed to update. Please try again");
                    }
                }
                else
                {
                    DisplayMessage(result.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnUpdate_OnClick", ex);
                DisplayMessage("Student profile has been failed to update. " + ex.Message);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ClearControls();
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
        #endregion
    }
}