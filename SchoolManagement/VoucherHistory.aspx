<%@ Page Title="Voucher History | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="VoucherHistory.aspx.cs" Inherits="SchoolManagement.VoucherHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-6">
                    <div runat="server" id="divSearchPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Search Criteria</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtGridSearch" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button runat="server" ID="btnGridSearch" Text="Search" CssClass="btn-raised" />
                                    <asp:Button runat="server" ID="btnGridSearchClear" Text="Clear" CssClass="btn-raised" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-6">
                    <div runat="server" id="divActionPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Action Panel</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="text-align: center;">
                                <asp:Button runat="server" ID="btnAddVoucher" Text="Voucher Entry" OnClick="btnAddVoucher_Click" class="btn btn-raised btn-primary" />
                                <asp:Button runat="server" ID="btnPrint" Text="Print" class="btn btn-raised btn-info" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divVoucherList">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Voucher List (<asp:Label runat="server" ID="lblTotalRecord">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <asp:GridView ID="gvVoucherList" runat="server" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="VoucherMasterID" HeaderText="MasterID"  HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                       <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No"/>
                                        <asp:BoundField DataField="VoucherType" HeaderText="Voucher Type"/>
                                        <asp:BoundField DataField="VoucherDate" HeaderText="Voucher Date"/>
                                        <asp:BoundField DataField="TotalDebitAmount" HeaderText="Debit Amount" ItemStyle-HorizontalAlign="Right"/>
                                        <asp:BoundField DataField="TotalCreditAmount" HeaderText="Credit Amount" ItemStyle-HorizontalAlign="Right"/>
                                        <asp:BoundField DataField="EntryRemarks" HeaderText="Remarks" />
                                        <asp:BoundField DataField="EntryBy" HeaderText="Entry By"/>
                                        <asp:BoundField DataField="ApprovalStatus" HeaderText="Status"/>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click For Edit" ImageUrl="~/img/edit.png" Height="25px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle CssClass="NewGridDesign" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
