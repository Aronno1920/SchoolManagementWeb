using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagement
{
    public class PollClear
    {
        public static void ClearReport(ReportDocument rptName)
        {
            try
            {
                CloseReports(rptName);
            }
            catch { }

            try
            {
                rptName.Dispose();
                rptName.Close();
            }
            catch { }
        }

        private static void CloseReports(ReportDocument reportDocument)
        {
            Sections sections = reportDocument.ReportDefinition.Sections;
            foreach (Section section in sections)
            {
                ReportObjects reportObjects = section.ReportObjects;
                foreach (ReportObject reportObject in reportObjects)
                {
                    if (reportObject.Kind == ReportObjectKind.SubreportObject)
                    {
                        SubreportObject subreportObject = (SubreportObject)reportObject;
                        ReportDocument subReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName);
                        subReportDocument.Dispose();
                        subReportDocument.Close();
                    }
                }
            }
            reportDocument.Dispose();
            reportDocument.Close();
        }

        public static void ClearViewer(CrystalReportViewer viewerName)
        {
            try
            {
                viewerName.Controls.Clear();
            }
            catch { }
            try
            {
                viewerName.Dispose();
            }
            catch { }
        }
    }
}