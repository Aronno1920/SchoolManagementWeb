<%@ Page Title="Report | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="SchoolManagement.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="col-sm-12">
<div class="col-lg-6">
            <div runat="server" id="divServices">
                <div class="card-head">
                    <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-fw fa-wrench"></i>Services</header>
                </div>
                <div class="card-body style-default-bright">
                    <div class="list-group" style="margin: -9px;">
                        <a class="list-group-item" href="GeneralService.aspx">
                            <h5 class="list-group-item-heading">General Service</h5>
                            <p class="list-group-item-text">
                                Listing of upcomming general service for vehicles.
                            </p>
                        </a><a class="list-group-item" href="/064b580d7f/reports/service_entries">
                            <h5 class="list-group-item-heading">Service History by Vehicle</h5>
                            <p class="list-group-item-text">
                                Listing of all service by vehicle grouped by entry or task.
                            </p>
                        </a><a class="list-group-item" href="/064b580d7f/reports/service_tasks/summary">
                            <h5 class="list-group-item-heading">Service Task Summary
                            </h5>
                            <p class="list-group-item-text">
                                Aggregate service data grouped by Service Task
                            </p>
                        </a><a class="list-group-item" href="/064b580d7f/reports/service_reminders">
                            <h5 class="list-group-item-heading">Service Reminders</h5>
                            <p class="list-group-item-text">
                                Lists all service reminders.
                            </p>
                        </a><a class="list-group-item" href="/064b580d7f/reports/vehicles_without_service">
                            <h5 class="list-group-item-heading">Vehicles Without Service</h5>
                            <p class="list-group-item-text">
                                Lists all vehicles that haven't had a service task(s) performed.
                            </p>
                        </a>
                    </div>
                </div>
            </div>

            <div runat="server" id="divTransaction">
                <div class="card-head">
                    <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-fw fa-wrench"></i>Fees Report</header>
                </div>
                <div class="card-body style-default-bright">
                    <div class="list-group" style="margin: -9px;">
                        <a class="list-group-item" href="RequisitionList.aspx">
                            <h5 class="list-group-item-heading">Requistion Report</h5>
                            <p class="list-group-item-text">
                                Listing of all vehicle requistion.
                            </p>
                        </a>
                        <a class="list-group-item" href="PurchaseList.aspx">
                            <h5 class="list-group-item-heading">Purchase Report</h5>
                            <p class="list-group-item-text">
                                Listing of all types vehicle purchase.
                            </p>
                        </a>
                        <a class="list-group-item" href="DistributionList.aspx">
                            <h5 class="list-group-item-heading">Distribution Report</h5>
                            <p class="list-group-item-text">
                                Listing of all types vehicle distribution.
                            </p>
                        </a>
                        <a class="list-group-item" href="ServiceRequestList.aspx">
                            <h5 class="list-group-item-heading">Service Request Report</h5>
                            <p class="list-group-item-text">
                                Listing of all types vehicle Service Request.
                            </p>
                        </a>
                        <a class="list-group-item" href="ServiceInvoiceList.aspx">
                            <h5 class="list-group-item-heading">Service Invoice Report</h5>
                            <p class="list-group-item-text">
                                Listing of all types vehicle Service Invoice.
                            </p>
                        </a>
                        <a class="list-group-item" href="BillEntry.aspx">
                            <h5 class="list-group-item-heading">Bill Payments Report</h5>
                            <p class="list-group-item-text">
                                Listing of all types vehicle Bill Payments.
                            </p>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-6">
            <div runat="server" id="divVehicles">
                <div class="card-head">
                    <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-fw fa-tag"></i>Vehicles</header>
                </div>
                <div class="card-body style-default-bright">
                    <div class="list-group" style="margin: -9px;">
                        <a class="list-group-item" href="ReportViewerSettings.aspx?rpt=vehicle">
                            <h5 class="list-group-item-heading">Vehicle List</h5>
                            <p class="list-group-item-text">Listing of all basic vehicle information.</p>
                        </a>
                        <a class="list-group-item" href="ReportViewerSettings.aspx?rpt=allocation">
                            <h5 class="list-group-item-heading">Vehicle Allocation Details</h5>
                            <p class="list-group-item-text">Listing of full vehicle profiles &amp; details.</p>
                        </a>
                    </div>
                </div>
            </div>

            <div runat="server" id="divSetup">
                <div class="card-head">
                    <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-fw fa-tag"></i>Setup</header>
                </div>
                <div class="card-body style-default-bright">
                    <div class="list-group" style="margin: -9px;">
                        <a class="list-group-item" href="ReportViewerSettings.aspx?rpt=parts">
                            <h5 class="list-group-item-heading">Parts List</h5>
                            <p class="list-group-item-text">Analysis of total vehicle costs per meter (mile/kilometer/hour) over time.</p>
                        </a>
                        <a class="list-group-item" href="ReportViewerSettings.aspx?rpt=service">
                            <h5 class="list-group-item-heading">Services Task List</h5>
                            <p class="list-group-item-text">Analysis of total vehicle costs over time.</p>
                        </a>
                        <a class="list-group-item" href="ReportViewerSettings.aspx?rpt=brand">
                            <h5 class="list-group-item-heading">Brand List</h5>
                            <p class="list-group-item-text">Listing of all basic vehicle information.</p>
                        </a>
                        <a class="list-group-item" href="ReportViewerSettings.aspx?rpt=model">
                            <h5 class="list-group-item-heading">Model List</h5>
                            <p class="list-group-item-text">Listing of full vehicle profiles &amp; details.</p>
                        </a>
                        <a class="list-group-item" href="ReportViewerSettings.aspx?rpt=color">
                            <h5 class="list-group-item-heading">Color List</h5>
                            <p class="list-group-item-text">Summary of costs associated with vehicles.</p>
                        </a>
                        <a class="list-group-item" href="ReportViewerSettings.aspx?rpt=vendor">
                            <h5 class="list-group-item-heading">Vendor List</h5>
                            <p class="list-group-item-text">Shows usage (e.g. distance traveled) per vehicle based on meter entries.</p>
                        </a>
                        <a class="list-group-item" href="ReportViewerSettings.aspx?rpt=ptype">
                            <h5 class="list-group-item-heading">Parts Type List</h5>
                            <p class="list-group-item-text">Lists all date-based reminders for vehicles.</p>
                        </a>
                        <a class="list-group-item" href="ReportViewerSettings.aspx?rpt=vtype">
                            <h5 class="list-group-item-heading">Vehicle Type List</h5>
                            <p class="list-group-item-text">Lists updates to every vehicle's status.</p>
                        </a>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
