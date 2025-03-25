using CoreLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class Queryboard : System.Web.UI.Page
    {
        Cookie user = new Cookie();
        DataReader _reader = new DataReader();
        DataWriter _writer = new DataWriter();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialDivStyle();
                InitialButtonStyle();

                LoadDateControls();
                //LoadVendorList();
                //LoadVehicleType();
                //LoadGeneralServiceList();
                //LoadOtherServiceList();
                //LoadDocumentType();
            }

            if (!String.IsNullOrEmpty(hdButtonSession.Value))
            {
                ChangeDivVisibility(hdButtonSession.Value);
            }

        }

        #region Load Primary Data

        //private void LoadVehicleType()
        //{
        //    try
        //    {
        //        DataTable dtType = PopulateLists.GetAllVehicleType();
        //        if (dtType.Rows.Count > 0)
        //        {
        //            FillList.PopulateDDL(dtType, ddlGeneralVehicleType, "Id", "Name", true, "Select Vehicle Type", "0");
        //            FillList.PopulateDDL(dtType, ddlOtherVehicleType, "Id", "Name", true, "Select Vehicle Type", "0");
        //            FillList.PopulateDDL(dtType, ddlDocumntVehicleType, "Id", "Name", true, "Select Vehicle Type", "0");
        //            FillList.PopulateDDL(dtType, ddlPartsVehicleType, "Id", "Name", true, "Select Vehicle Type", "0");
        //            FillList.PopulateDDL(dtType, ddlBillVehicleType, "Id", "Name", true, "Select Vehicle Type", "0");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void LoadDocumentType()
        //{
        //    try
        //    {
        //        DataTable dtDocument = PopulateLists.GetAllDocumentType();
        //        if (dtDocument.Rows.Count > 0)
        //        {
        //            FillList.PopulateDDL(dtDocument, ddlDocumentType, "Id", "Name", true, "Select Document Type", "0");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void LoadVendorList()
        //{
        //    try
        //    {
        //        DataTable dtVendor = PopulateLists.GetAllServiceProvider();
        //        if (dtVendor.Rows.Count > 0)
        //        {
        //            FillList.PopulateDDL(dtVendor, ddlGeneralWorkshop, "VendorId", "VendorName", true, "Select Workshop", "0");
        //            FillList.PopulateDDL(dtVendor, ddlOtherWorkshop, "VendorId", "VendorName", true, "Select Workshop", "0");
        //            FillList.PopulateDDL(dtVendor, ddlPartsWorkshop, "VendorId", "VendorName", true, "Select Workshop", "0");
        //            FillList.PopulateDDL(dtVendor, ddlBillWorkshop, "VendorId", "VendorName", true, "Select Workshop", "0");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void LoadGeneralServiceList()
        //{
        //    try
        //    {
        //        DataTable dtGeneral = PopulateLists.GetAllGeneralService();
        //        if (dtGeneral.Rows.Count > 0)
        //        {
        //            FillList.PopulateDDL(dtGeneral, ddlGeneralService, "Id", "Name", true, "Select Service", "0");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void LoadOtherServiceList()
        //{
        //    try
        //    {
        //        DataTable dtGeneral = PopulateLists.GetAllOtherService();
        //        if (dtGeneral.Rows.Count > 0)
        //        {
        //            FillList.PopulateDDL(dtGeneral, ddlOtherService, "Id", "Name", true, "Select Service", "0");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        private void LoadDateControls()
        {
            txtGeneralFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
            txtGeneralToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            txtOtherFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
            txtOtherToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            txtDocumentFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
            txtDocumentToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            txtPartsFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
            txtPartsToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            txtBillFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
            txtBillToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }

        public static void MergeRows(GridView gridView, Int32 MargeRowTo)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = 0; i < MargeRowTo; i++)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                            previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }

        #endregion

        #region Button & Div Navigation Style

        private void InitialDivStyle()
        {
            divButton.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
            divGeneral.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
            divOthers.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
            divParts.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
            divDocument.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
            divBill.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
            divDetails.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());

            divDocument.Visible = true;
            divGeneral.Visible = false;
            divBill.Visible = false;
            divOthers.Visible = false;
            divParts.Visible = false;
        }

        private void InitialButtonStyle()
        {
            btnDocument.Attributes.Add("class", "list-group-item active");
            btnGeneral.Attributes.Add("class", "list-group-item");
            btnBill.Attributes.Add("class", "list-group-item");
            btnOthers.Attributes.Add("class", "list-group-item");
            btnParts.Attributes.Add("class", "list-group-item");
        }

        private void ChangeButtonStyle(Button ActiveButton)
        {
            btnGeneral.Attributes.Add("class", "list-group-item");
            btnOthers.Attributes.Add("class", "list-group-item");
            btnParts.Attributes.Add("class", "list-group-item");
            btnDocument.Attributes.Add("class", "list-group-item");
            btnBill.Attributes.Add("class", "list-group-item");
            ActiveButton.Attributes.Add("class", "list-group-item active");
        }

        private void ChangeDivVisibility(String ActiveDiv)
        {
            divGeneral.Visible = false;
            divOthers.Visible = false;
            divParts.Visible = false;
            divDocument.Visible = false;
            divBill.Visible = false;

            if (ActiveDiv == "General")
            {
                divGeneral.Visible = true;
            }
            else if (ActiveDiv == "Others")
            {
                divOthers.Visible = true;
            }
            else if (ActiveDiv == "Parts")
            {
                divParts.Visible = true;
            }
            else if (ActiveDiv == "Document")
            {
                divDocument.Visible = true;
            }
            else if (ActiveDiv == "Bill")
            {
                divBill.Visible = true;
            }
        }

        protected void NavigateToDesireDiv(object sender, EventArgs e)
        {
            Button activeButton = (Button)sender;
            ChangeButtonStyle(activeButton);

            String sDivName = activeButton.ID.Replace("btn", "");
            ChangeDivVisibility(sDivName);

            if (!String.IsNullOrEmpty(hdButtonSession.Value))
            {
                if (hdButtonSession.Value != sDivName)
                {
                    lblRecordCount.Text = "0";
                    gvReportData.DataSource = null;
                    gvReportData.DataBind();
                    hdButtonSession.Value = sDivName;
                }
            }
            else
            {
                hdButtonSession.Value = sDivName;
            }
        }

        protected void cbDocumentForecast_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDocumentForecast.Checked)
            {
                lblDocumentFromDate.Text = "As On Date";
                divDocumentToDate.Visible = false;
                txtGeneralToDate.Text = DateTime.Now.AddMonths(1).ToString("dd-MMM-yyyy");
            }
            else
            {
                lblDocumentFromDate.Text = "Date From";
                divDocumentToDate.Visible = true;
                LoadDateControls();
            }
        }

        protected void cbGeneralForecast_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGeneralForecast.Checked)
            {
                lblGeneralFromDate.Text = "As On Date";
                divGeneralToDate.Visible = false;
                txtGeneralToDate.Text = DateTime.Now.AddMonths(1).ToString("dd-MMM-yyyy");
            }
            else
            {
                lblGeneralFromDate.Text = "Date From";
                divGeneralToDate.Visible = true;
                LoadDateControls();
            }
        }
        #endregion

        #region Common Mathod for Display Message
        private void DisplayMessage(String sMessage)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('" + sMessage.ToString() + "');", true);
            return;
        }
        private void DisplayErrorMessage(String sTypeofData, String exMessage)
        {
            String strMessage = String.Format(sTypeofData + " \nError: {0}", exMessage);
            strMessage = strMessage.Replace("\r\n", "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('" + strMessage.ToString() + "');", true);
            return;
        }
        #endregion

        #region Auto Complete Vehicle List and OnTextChange Events 

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchVehicleLicence(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigReader.AppConnString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText =
                        "SELECT VehicleId, LicencePlate FROM vwVehicleMaster WHERE LicencePlate like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0} - {1}", sdr["VehicleId"], sdr["LicencePlate"]));
                        }
                    }

                    conn.Close();
                    return customers;
                }
            }
        }

        protected void txtGeneralLicencePlate_OnTextChanged(object sender, EventArgs e)
        {
            String sTotalText = txtGeneralLicencePlate.Text.ToString();
            hdVehicleId.Value = sTotalText.Split('-')[0].Trim();
        }

        protected void txtOtherLicencePlate_OnTextChanged(object sender, EventArgs e)
        {
            String sTotalText = txtOtherLicencePlate.Text.ToString();
            hdVehicleId.Value = sTotalText.Split('-')[0].Trim();
        }

        protected void txtDocumentLicencePlate_OnTextChanged(object sender, EventArgs e)
        {
            String sTotalText = txtDocumentLicencePlate.Text.ToString();
            hdVehicleId.Value = sTotalText.Split('-')[0].Trim();
        }

        protected void txtPartsLicencePlate_OnTextChanged(object sender, EventArgs e)
        {
            String sTotalText = txtPartsLicencePlate.Text.ToString();
            hdVehicleId.Value = sTotalText.Split('-')[0].Trim();
        }

        protected void txtBillLicencePlate_OnTextChanged(object sender, EventArgs e)
        {
            String sTotalText = txtBillLicencePlate.Text.ToString();
            hdVehicleId.Value = sTotalText.Split('-')[0].Trim();
        }

        #endregion

        #region Auto Complete Parts List and OnTextChange Events 

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchPartsList(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigReader.AppConnString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Parts_Id, Parts_Name FROM Vms_Sys_Part_List  WHERE Parts_Name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> parts = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            parts.Add(string.Format("{0} - {1}", sdr["Parts_Id"], sdr["Parts_Name"]));
                        }
                    }

                    conn.Close();
                    return parts;
                }
            }
        }

        protected void txtPartsName_OnTextChanged(object sender, EventArgs e)
        {
            String sTotalText = txtPartsName.Text.ToString();
            hdPartsId.Value = sTotalText.Split('-')[0].Trim();
        }

        #endregion

        #region View Button Click Event

        protected void btnDocumentView_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iMargeColumn = 0;

                Hashtable htParam = new Hashtable();
                htParam.Add("ForecastReport", cbDocumentForecast.Checked);
                htParam.Add("DocumentType", ddlDocumentType.SelectedValue);
                htParam.Add("VehicleType", ddlDocumntVehicleType.SelectedValue);
                htParam.Add("VehicleId", hdVehicleId.Value);
                htParam.Add("RangeType", ddlDocumentReportRange.SelectedValue);
                htParam.Add("FromDate", txtOtherFromDate.Text);
                htParam.Add("ToDate", txtOtherToDate.Text);
                if (ddlDocumentReportRange.SelectedValue == "Day")
                {
                    iMargeColumn = 4;
                }
                else if (ddlDocumentReportRange.SelectedValue == "Month")
                {
                    iMargeColumn = 1;
                }
                else if (ddlDocumentReportRange.SelectedValue == "Year")
                {
                    iMargeColumn = 1;
                }

                _reader = new DataReader();
                DataTable dtGeneralInfo = _reader.GetDataTableByStoredProcedure("SP_VMS_QUERYBOARD_DOCUMENT", htParam);
                if (dtGeneralInfo.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtGeneralInfo.Rows.Count.ToString();
                    gvReportData.DataSource = dtGeneralInfo;
                    gvReportData.DataBind();

                    //MergeRows(gvReportData, iMargeColumn);
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvReportData.DataSource = dtGeneralInfo;
                    gvReportData.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnGeneralView_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iMargeColumn = 0;

                Hashtable htParam = new Hashtable();
                htParam.Add("ForecastReport", cbGeneralForecast.Checked);
                htParam.Add("VendorId", ddlGeneralWorkshop.SelectedValue);
                htParam.Add("ServiceId", ddlGeneralService.SelectedValue);
                htParam.Add("VehicleType", ddlGeneralVehicleType.SelectedValue);
                htParam.Add("VehicleId", hdVehicleId.Value);
                htParam.Add("RangeType", ddlGeneralReportRange.SelectedValue);
                htParam.Add("WorkshopBased", (cbGeneralWorkshopBased.Checked == true ? "1" : "0"));
                htParam.Add("FromDate", txtGeneralFromDate.Text);
                htParam.Add("ToDate", txtGeneralToDate.Text);

                if (ddlGeneralReportRange.SelectedValue == "Day")
                {
                    iMargeColumn = 5;
                }
                else if (ddlGeneralReportRange.SelectedValue == "Month")
                {
                    iMargeColumn = 1;
                }
                else if (ddlGeneralReportRange.SelectedValue == "Year")
                {
                    iMargeColumn = 1;
                }

                _reader = new DataReader();
                DataTable dtGeneralInfo = _reader.GetDataTableByStoredProcedure("SP_VMS_QUERYBOARD_GENERAL", htParam);
                if (dtGeneralInfo.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtGeneralInfo.Rows.Count.ToString();
                    gvReportData.DataSource = dtGeneralInfo;
                    gvReportData.DataBind();

                    //MergeRows(gvReportData, iMargeColumn);
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvReportData.DataSource = dtGeneralInfo;
                    gvReportData.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnOtherView_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iMargeColumn = 0;

                Hashtable htParam = new Hashtable();
                htParam.Add("VendorId", ddlOtherWorkshop.SelectedValue);
                htParam.Add("ServiceId", ddlOtherService.SelectedValue);
                htParam.Add("VehicleType", ddlOtherVehicleType.SelectedValue);
                htParam.Add("VehicleId", hdVehicleId.Value);
                htParam.Add("RangeType", ddlOtherReportRange.SelectedValue);
                htParam.Add("WorkshopBased", (cbGeneralWorkshopBased.Checked == true ? "1" : "0"));
                htParam.Add("FromDate", txtOtherFromDate.Text);
                htParam.Add("ToDate", txtOtherToDate.Text);
                if (ddlOtherReportRange.SelectedValue == "Day")
                {
                    iMargeColumn = 5;
                }
                else if (ddlOtherReportRange.SelectedValue == "Month")
                {
                    iMargeColumn = 1;
                }
                else if (ddlOtherReportRange.SelectedValue == "Year")
                {
                    iMargeColumn = 1;
                }

                _reader = new DataReader();
                DataTable dtGeneralInfo = _reader.GetDataTableByStoredProcedure("SP_VMS_QUERYBOARD_OTHER", htParam);
                if (dtGeneralInfo.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtGeneralInfo.Rows.Count.ToString();
                    gvReportData.DataSource = dtGeneralInfo;
                    gvReportData.DataBind();

                    //MergeRows(gvReportData, iMargeColumn);
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvReportData.DataSource = dtGeneralInfo;
                    gvReportData.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnPartsView_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable htParam = new Hashtable();
                htParam.Add("VendorId", ddlPartsWorkshop.SelectedValue);
                htParam.Add("VehicleType", ddlPartsVehicleType.SelectedValue);
                htParam.Add("VehicleId", hdVehicleId.Value);
                htParam.Add("PartsId", hdPartsId.Value);
                htParam.Add("RangeType", ddlPartsReportRange.SelectedValue);
                htParam.Add("FromDate", txtPartsFromDate.Text);
                htParam.Add("ToDate", txtPartsToDate.Text);

                _reader = new DataReader();
                DataTable dtGeneralInfo = _reader.GetDataTableByStoredProcedure("SP_VMS_QUERYBOARD_PARTS", htParam);
                if (dtGeneralInfo.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtGeneralInfo.Rows.Count.ToString();
                    gvReportData.DataSource = dtGeneralInfo;
                    gvReportData.DataBind();

                    //MergeRows(gvReportData, 2);
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvReportData.DataSource = dtGeneralInfo;
                    gvReportData.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnBillView_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable htParam = new Hashtable();
                htParam.Add("VendorId", ddlBillWorkshop.SelectedValue);
                htParam.Add("VehicleType", ddlBillVehicleType.SelectedValue);
                htParam.Add("VehicleId", hdVehicleId.Value);
                htParam.Add("RangeType", ddlBillReportRange.SelectedValue);
                htParam.Add("FromDate", txtBillFromDate.Text);
                htParam.Add("ToDate", txtBillToDate.Text);

                _reader = new DataReader();
                DataTable dtGeneralInfo = _reader.GetDataTableByStoredProcedure("SP_VMS_QUERYBOARD_VENDOR_BILL", htParam);
                if (dtGeneralInfo.Rows.Count > 0)
                {
                    lblRecordCount.Text = dtGeneralInfo.Rows.Count.ToString();
                    gvReportData.DataSource = dtGeneralInfo;
                    gvReportData.DataBind();

                    //MergeRows(gvReportData, 2);
                }
                else
                {
                    lblRecordCount.Text = "0";
                    gvReportData.DataSource = dtGeneralInfo;
                    gvReportData.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Clear Button Click Events
        protected void btnDocumentClear_Click(object sender, EventArgs e)
        {
            cbDocumentForecast.Checked = false;
            ddlDocumentType.SelectedIndex = -1;
            ddlDocumntVehicleType.SelectedIndex = -1;
            ddlDocumentReportRange.SelectedIndex = -1;

            hdVehicleId.Value = String.Empty;
            LoadDateControls();
            ClearReportGrid();

        }

        protected void btnGeneralClear_Click(object sender, EventArgs e)
        {
            cbGeneralForecast.Checked=false;
            ddlGeneralWorkshop.SelectedIndex = -1;
            ddlGeneralService.SelectedIndex = -1;
            ddlGeneralVehicleType.SelectedIndex = -1;
            ddlGeneralReportRange.SelectedIndex = -1;
            cbGeneralWorkshopBased.Checked = false;

            hdVehicleId.Value = String.Empty;
            LoadDateControls();
            ClearReportGrid();
        }

        protected void btnOtherClear_Click(object sender, EventArgs e)
        {
            ddlOtherWorkshop.SelectedIndex = -1;
            ddlOtherService.SelectedIndex = -1;
            ddlOtherVehicleType.SelectedIndex = -1;
            ddlOtherReportRange.SelectedIndex = -1;
            cbGeneralWorkshopBased.Checked = false;

            hdVehicleId.Value = String.Empty;
            LoadDateControls();
            ClearReportGrid();
        }

        protected void btnPartsClear_Click(object sender, EventArgs e)
        {
            ddlPartsWorkshop.SelectedIndex = -1;
            ddlPartsVehicleType.SelectedIndex = -1;
            hdPartsId.Value = String.Empty;
            ddlPartsReportRange.SelectedIndex = -1;

            hdVehicleId.Value = String.Empty;
            LoadDateControls();
            ClearReportGrid();
        }

        protected void btnBillClear_Click(object sender, EventArgs e)
        {
            ddlBillWorkshop.SelectedIndex = -1;
            ddlBillVehicleType.SelectedIndex = -1;
            ddlBillReportRange.SelectedIndex = -1;

            hdVehicleId.Value = String.Empty;
            LoadDateControls();
            ClearReportGrid();
        }

        private void ClearReportGrid()
        {
            gvReportData.DataSource = null;
            gvReportData.DataBind();
        }

        #endregion
    }
}