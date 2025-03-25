using System;
using System.Data;
using CoreLibrary;
using CrystalDecisions.Shared;
using SchoolManagement.Reports;

namespace SchoolManagement
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        Cookie user = new Cookie();
        String rptName = "";
        String SearchCriteria = "";
        DataTable dtReportTable = new DataTable();
        DataSet dsReportSet = new DataSet();
        String sPageName = String.Empty;

        #region Crystal Document Initializer
        rptInvoice invoiceReport = new rptInvoice();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            sPageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            
            if (Session["SearchCriteria"] != null)
            {
                SearchCriteria = Session["SearchCriteria"].ToString();
            }
            LoadReportData();
        }

        private void LoadReportData()
        {
            try
            {
                rptName = Request.QueryString["ReportName"].ToString();

                if (Session["dtReportData"] != null)
                {
                    dtReportTable = new DataTable();
                    dtReportTable = (DataTable)Session["dtReportData"];
                }
                else if (Session["dsReportSet"] != null)
                {
                    dsReportSet = new DataSet();
                    dsReportSet = (DataSet)Session["dsReportSet"];
                }
                else
                {
                    Response.Write("No Data Found.");
                }

                switch (rptName)
                {
                    case "InvoicePrint":
                        if (dsReportSet.Tables[0].Rows.Count > 0)
                        {
                            //dsReportSet.WriteXmlSchema("D:\\Project\\SchoolManagement\\SchoolManagement\\Reports\\XMLFiles\\Invoice.xsd");

                            invoiceReport.SetDataSource(dsReportSet);
                            invoiceReport.SetParameterValue("sSearchCriteria", SearchCriteria);
                            invoiceReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Invoice");
                        }
                        break;
                    default:
                        Response.Write("No Data Found.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                ErrorTracking.SaveError(user.UserID, sPageName, "LoadReportData", ex);
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            PollClear.ClearReport(invoiceReport);
            PollClear.ClearViewer(crvReportViewer);
        }
    }
}