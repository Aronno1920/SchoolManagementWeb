using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreLibrary;

namespace SchoolManagement
{
    public partial class ReportViewerSettings : System.Web.UI.Page
    {
        Cookie user = new Cookie();
        DataReader _reader = new DataReader();
        DataWriter _writer = new DataWriter();
        String sPageName = String.Empty;
        String sReportName = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divHeader.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                divGridView.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.AccessDenied, sPageName);
            }

            sReportName = Request.QueryString["rpt"];

            if (sReportName == "parts")
            {
                lblReportName.Text = "Parts";
                lblHeader.Text = "Parts Report";
            }
            else if (sReportName == "service")
            {
                lblReportName.Text = "Services";
                lblHeader.Text = "Services Report";
            }
            else if (sReportName == "brand")
            {
                lblReportName.Text = "Brand";
                lblHeader.Text = "Brand Report";
            }
            else if (sReportName == "model")
            {
                lblReportName.Text = "Model";
                lblHeader.Text = "Model Report";
            }
            else if (sReportName == "color")
            {
                lblReportName.Text = "Color";
                lblHeader.Text = "Color Report";
            }
            else if (sReportName == "vendor")
            {
                lblReportName.Text = "Vendor";
                lblHeader.Text = "Vendor Report";
            }
            else if (sReportName == "vehicle")
            {
                lblReportName.Text = "Vehicle";
                lblHeader.Text = "Vehicle Report";
            }
            else if (sReportName == "vtype")
            {
                lblReportName.Text = "Vehicle Type";
                lblHeader.Text = "Vehicle Type";
            }
            else if (sReportName == "availability")
            {
                lblReportName.Text = "Vehicle Availability";
                lblHeader.Text = "Vehicle Availability Report";
            }
            else if (sReportName == "allocation")
            {
                lblReportName.Text = "Vehicle Allocation Details";
                lblHeader.Text = "Vehicle Allocation Details Report";
            }

            LoadReportData();
        }

        protected void btnView_OnClick(object sender, EventArgs e)
        {
            LoadReportData();
        }

        protected void LoadReportData()
        {
            try
            {
                sReportName = Request.QueryString["rpt"];

                if (sReportName == "service")
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("WithInactive", cbInactive.Checked);
                    _param.Add("ReportType", sReportName);

                    _reader = new DataReader();
                    DataTable dtService = _reader.GetDataTableByStoredProcedure("SP_VMS_BASIC_REPORT", _param);
                    if (dtService.Rows.Count > 0)
                    {
                        gvReport.DataSource = dtService;
                        gvReport.DataBind();
                    }
                    else
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                    }
                }
                else if (sReportName == "brand")
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("WithInactive", cbInactive.Checked);
                    _param.Add("ReportType", sReportName);

                    _reader = new DataReader();
                    DataTable dtService = _reader.GetDataTableByStoredProcedure("SP_VMS_BASIC_REPORT", _param);
                    if (dtService.Rows.Count > 0)
                    {
                        gvReport.DataSource = dtService;
                        gvReport.DataBind();
                    }
                    else
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                    }
                }
                else if (sReportName == "model")
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("WithInactive", cbInactive.Checked);
                    _param.Add("ReportType", sReportName);

                    _reader = new DataReader();
                    DataTable dtService = _reader.GetDataTableByStoredProcedure("SP_VMS_BASIC_REPORT", _param);
                    if (dtService.Rows.Count > 0)
                    {
                        gvReport.DataSource = dtService;
                        gvReport.DataBind();
                    }
                    else
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                    }
                }
                else if (sReportName == "color")
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("WithInactive", cbInactive.Checked);
                    _param.Add("ReportType", sReportName);

                    _reader = new DataReader();
                    DataTable dtService = _reader.GetDataTableByStoredProcedure("SP_VMS_BASIC_REPORT", _param);
                    if (dtService.Rows.Count > 0)
                    {
                        gvReport.DataSource = dtService;
                        gvReport.DataBind();
                    }
                    else
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                    }
                }
                else if (sReportName == "vendor")
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("WithInactive", cbInactive.Checked);
                    _param.Add("ReportType", sReportName);

                    _reader = new DataReader();
                    DataTable dtService = _reader.GetDataTableByStoredProcedure("SP_VMS_BASIC_REPORT", _param);
                    if (dtService.Rows.Count > 0)
                    {
                        gvReport.DataSource = dtService;
                        gvReport.DataBind();
                    }
                    else
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                    }
                }
                else if (sReportName == "vehicle")
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("WithInactive", cbInactive.Checked);
                    _param.Add("ReportType", sReportName);

                    _reader = new DataReader();
                    DataTable dtService = _reader.GetDataTableByStoredProcedure("SP_VMS_BASIC_REPORT", _param);
                    if (dtService.Rows.Count > 0)
                    {
                        gvReport.DataSource = dtService;
                        gvReport.DataBind();
                    }
                    else
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                    }
                }
                else if (sReportName == "availability")
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("WithInactive", cbInactive.Checked);
                    _param.Add("ReportType", sReportName);

                    _reader = new DataReader();
                    DataTable dtService = _reader.GetDataTableByStoredProcedure("SP_VMS_BASIC_REPORT", _param);
                    if (dtService.Rows.Count > 0)
                    {
                        gvReport.DataSource = dtService;
                        gvReport.DataBind();
                    }
                    else
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                    }
                }
                else if (sReportName == "allocation")
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("WithInactive", cbInactive.Checked);
                    _param.Add("ReportType", sReportName);

                    _reader = new DataReader();
                    DataTable dtService = _reader.GetDataTableByStoredProcedure("SP_VMS_BASIC_REPORT", _param);
                    if (dtService.Rows.Count > 0)
                    {
                        gvReport.DataSource = dtService;
                        gvReport.DataBind();
                    }
                    else
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                    }
                }
                else if (sReportName == "vtype")
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("WithInactive", cbInactive.Checked);
                    _param.Add("ReportType", "vehicletype");

                    _reader = new DataReader();
                    DataTable dtService = _reader.GetDataTableByStoredProcedure("SP_VMS_BASIC_REPORT", _param);
                    if (dtService.Rows.Count > 0)
                    {
                        gvReport.DataSource = dtService;
                        gvReport.DataBind();
                    }
                    else
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                    }
                }
                else if (sReportName == "parts")
                {
                    Hashtable _param = new Hashtable();
                    _param.Add("WithInactive", cbInactive.Checked);
                    _param.Add("ReportType", sReportName);

                    _reader = new DataReader();
                    DataTable dtService = _reader.GetDataTableByStoredProcedure("SP_VMS_BASIC_REPORT", _param);
                    if (dtService.Rows.Count > 0)
                    {
                        gvReport.DataSource = dtService;
                        gvReport.DataBind();
                    }
                    else
                    {
                        gvReport.DataSource = null;
                        gvReport.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, "ReportViewerSettings.aspx", "btnSearch_OnClick", ex);
            }
        }

        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            try
            {
                ExportSalesSummaryReport();
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, "ReportViewerSettings.aspx", "btnSearch_OnClick", ex);
            }
        }

        #region Export to Excel/PDF/Word method

        public void ExportSalesSummaryReport()
        {

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=SalesReport" + "_" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            divGridView.RenderControl(htw);

            string imagepath = string.Format("<img src='{0}' width='{1}' height='{2}' style='{3}' />", "", 100, 40, " display:block; margin-left:auto; margin-right:auto;");
            String headerTable = @"<div style='width: 100%; text-align:center; position:absolute;'><a>" + imagepath + "</a></div>";
            Response.Write(headerTable);
            string style = @"<style> .textmode { mso-number-format:\@;font-family:Cambria;} </style>";
            Response.Write(style);
            Response.Write("<div  style='width: 100%;text-align:center;font-family:Cambria;'><a style='font-size:20px; font-weight:bold'>" + "asdfasdfadsf" + "</a></div>");

            Response.Write("<div  style='width: 100%;text-align:center;font-family:Cambria;'><h1> </br></h1></div>");
            Response.Write(sw.ToString());
            Response.Flush();
            Response.Write("<div  style='width: 100%;text-align:center;font-family:Cambria;'><br/><a>Power By : Orion IT , OrionPOS </a> </div> ");
            Response.End();

        }
        protected string GetUrl(string imagepath)
        {
            imagepath = imagepath.Replace("~/", "");
            string[] splits = Request.Url.AbsoluteUri.Split('/');
            if (splits.Length >= 2)
            {
                string url = splits[0] + "//";
                for (int i = 2; i < splits.Length - 1; i++)
                {
                    url += splits[i];
                    url += "/";
                }
                return url + imagepath;
            }
            return imagepath;
        }

        #endregion

        protected void gvReport_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            btnView_OnClick(sender, e);
            gvReport.PageIndex = e.NewPageIndex;
            gvReport.DataBind();
        }
    }
}