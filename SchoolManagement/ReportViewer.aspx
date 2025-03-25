<%@ Page Language="C#" Title="Report Viewer | VMS" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="SchoolManagement.ReportViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer | VMS</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="crvReportViewer" runat="server" AutoDataBind="true" GroupTreeStyle-ShowLines="False" DisplayStatusbar="False"
                EnableDatabaseLogonPrompt="False" EnableDrillDown="False" EnableParameterPrompt="False" EnableTheming="False" EnableToolTips="False"
                HasCrystalLogo="False" HasDrilldownTabs="False" HasDrillUpButton="False" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False"
                ToolPanelView="None" PrintMode="Pdf" Width="350px" />
        </div>
    </form>
</body>
</html>
