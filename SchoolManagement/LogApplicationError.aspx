<%@ Page Title="Application Error Log | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LogApplicationError.aspx.cs" Inherits="SchoolManagement.LogApplicationError" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divDetailsPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Application Error Log (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvLogInfo" runat="server" OnPageIndexChanging="gvLogInfo_PageIndexChanging" Width="100%" Class="NewGridDesingBody" AllowPaging="True" PageSize="25" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="SL." ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="ErrorDate" HeaderText="Error Date" />
                                        <asp:BoundField DataField="EmployeeName" HeaderText="User Name" />
                                        <asp:BoundField DataField="ComputerName" HeaderText="Computer Name" />
                                        <asp:BoundField DataField="ComputerIPAddress" HeaderText="IP Address" />
                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" />
                                        <asp:BoundField DataField="MethodName" HeaderText="Method Name" />
                                        <asp:BoundField DataField="ErrorSource" HeaderText="Error Source" />
                                        <asp:BoundField DataField="ErrorMessage" HeaderText="Error Message" />
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f3fdfa" />
                                    <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle CssClass="NewGridDesign" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
