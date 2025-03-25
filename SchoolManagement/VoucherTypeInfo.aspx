<%@ Page Title="Voucher Type Information | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="VoucherTypeInfo.aspx.cs" Inherits="SchoolManagement.VoucherTypeInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divDetailsPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Voucher Type Information (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <asp:GridView ID="gvVoucherType" runat="server" Width="100%" Class="NewGridDesingBody" AllowPaging="True" PageSize="15"
                                    RowStyle-Wrap="True" EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="SL" HeaderText="SL." ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Prefix" HeaderText="Short Code"  ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="VoucherType" HeaderText="Voucher Type" HeaderStyle-Width="180px" />
                                        <asp:BoundField DataField="Description" HeaderText="Description"/>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f3fdfa" />
                                    <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
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
