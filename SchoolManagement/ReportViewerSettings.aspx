<%@ Page Title="Report Viewer - Setup | VMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReportViewerSettings.aspx.cs" Inherits="SchoolManagement.ReportViewerSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-6">
                <div runat="server" id="divHeader">
                    <div class="card-head">
                        <div class="tools">
                            <div class="btn-group">
                                <a class="btn btn-icon-toggle btn-collapse"><i class="fa fa-angle-down"></i></a>
                            </div>
                        </div>
                        <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-fw fa-building"></i><asp:Label runat="server" ID="lblHeader"></asp:Label></header>
                    </div>
                    <div class="card-body style-default-bright">
                        <div class="col-sm-12">
                            <div class="col-sm-6">
                                Show All <asp:Label runat="server" ID="lblReportName"></asp:Label> with Inactive
                            </div>
                            <div class="col-sm-2">
                                <asp:CheckBox runat="server" ID="cbInactive" />
                            </div>
                        </div>
                        <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                            <asp:Button runat="server" ID="btnView" class="btn btn-raised btn-primary" Text="View" OnClick="btnView_OnClick" />
                            <asp:Button runat="server" ID="btnExport" class="btn btn-raised btn-accent" Text="Export" OnClick="btnExport_OnClick"/>
                            <asp:Button runat="server" ID="btnPrint" class="btn btn-raised btn-warning" Text="Print" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div runat="server" id="divGridView">
                    <div class="card-body style-default-bright">
                        <asp:GridView ID="gvReport" OnPageIndexChanging="gvReport_OnPageIndexChanging" runat="server" Width="100%" Class="NewGridDesingBody" PageSize="50" AllowPaging="true" RowStyle-Wrap="false"
                            EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True">
                            <AlternatingRowStyle BackColor="#f0fdf9" />
                            <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page"/>
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
