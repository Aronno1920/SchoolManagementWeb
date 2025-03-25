<%@ Page Title="Activity Log | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LogApplicationActivity.aspx.cs" Inherits="SchoolManagement.LogApplicationActivity" %>

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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Activity Log (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvLogInfo" runat="server" OnPageIndexChanging="gvLogInfo_PageIndexChanging" Width="100%" Class="NewGridDesingBody" AllowPaging="True" PageSize="25" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="AuditID" HeaderText="SL." ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="AuditDateTime" HeaderText="Audit Date Time" />
                                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                        <asp:BoundField DataField="LoginID" HeaderText="Login ID" />
                                        <asp:BoundField DataField="ActionType" HeaderText="Action Type" />
                                        <asp:BoundField DataField="ActionDetails" HeaderText="Action Details" />
                                        <asp:BoundField DataField="ProcedureName" HeaderText="Procedure Name (DB)" />
                                        <asp:BoundField DataField="ActionName" HeaderText="Action Name (DB)" />
                                        <asp:BoundField DataField="PrimaryID" HeaderText="Record ID" />
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f3fdfa" />
                                    <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle CssClass="NewGridDesign" />
                                    <RowStyle Wrap="true" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
