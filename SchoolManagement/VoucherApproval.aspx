<%@ Page Title="Voucher Approval | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="VoucherApproval.aspx.cs" Inherits="SchoolManagement.VoucherApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-lg-8">
                    <div runat="server" id="divMasterPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Voucher - Master Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Voucher Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlVoucherType" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hdVoucherId" />
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2">Voucher No</div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtVoucherNo" runat="server" placeholder="Voucher No" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Voucher Date
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtVoucherDate" runat="server" Width="80%" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                    <asp:Image runat="server" ID="imgVoucherDate" ImageUrl="~/img/calender.png" Height="25px" />
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtVoucherDate" PopupButtonID="imgVoucherDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></asp:CalendarExtender>
                                </div>
                                <div class="col-sm-1"></div>
                            </div>
                            <div class="col-sm-12" style="padding-top: 5px;">
                                <div class="col-sm-2">
                                    Description
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDescription" runat="server" placeholder="Voucher Description" CssClass="TextBoxStyle" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divDetailsPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Voucher Entry - Details Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-5">
                                    Account Head
                                </div>
                                <div class="col-sm-2">
                                    Curr. Balance
                                </div>
                                <div class="col-sm-2">
                                    Debit Amount
                                </div>
                                <div class="col-sm-2">
                                    Credit Amount
                                </div>
                                <div class="col-sm-1">
                                    Action
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="ddlAccountHead" runat="server" OnSelectedIndexChanged="ddlAccountHead_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtCurrentBalance" runat="server" Enabled="false" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtDebitAmount" runat="server" placeholder="Debit Amount" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtCreditAmount" runat="server" placeholder="Credit Amount" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:Button runat="server" ID="btnAddDetails" Text="Add" OnClick="AddDetails_Click" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top: 10px; min-height: 300px">
                                <div class="col-sm-12">
                                    <div runat="server" id="divEntryPanel" style="padding: 5px;">
                                        <asp:GridView ID="gvVoucher" runat="server" OnRowCommand="gvVoucher_RowCommand" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false"
                                            EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="VoucherMasterID" HeaderText="MasterID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                                <asp:BoundField DataField="VoucherDetailID" HeaderText="DetailID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                                <asp:BoundField DataField="AccountID" HeaderText="AccountID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                                <asp:BoundField DataField="BankID" HeaderText="BankID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                                <asp:BoundField DataField="AccountHead" HeaderText="Accounts Head" />
                                                <asp:BoundField DataField="DebitAmount" HeaderText="Debit Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="200px" />
                                                <asp:BoundField DataField="CreditAmount" HeaderText="Credit Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="200px" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="lbtnDelete" CommandName="DeletePending" ImageUrl="~/img/delete.png" Width="20px" Height="20px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle BackColor="#f3fdfa" />
                                            <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle CssClass="NewGridDesign" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4"></div>
                                <div class="col-sm-2">Total Debit</div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtTotalDebit" runat="server" CssClass="TextBoxStyle" Enabled="false" Style="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Total Credit</div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtTotalCredit" runat="server" CssClass="TextBoxStyle" Enabled="false" Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divButtonPanel">
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="padding: 5px; text-align: center;">
                                <asp:Button runat="server" ID="btnApproved" Text="Approved" OnClick="btnApproved_Click" class="btn btn-raised btn-primary" />
                                <asp:Button runat="server" ID="btnDisapproved" Text="Disapproved" OnClick="btnDisapproved_Click" class="btn btn-raised btn-danger" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" class="btn btn-raised btn-default-dark" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div runat="server" id="divVoucherList">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Voucher Info - Awaiting for Approval (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <asp:GridView ID="gvVoucherList" runat="server" OnRowCommand="gvVoucherList_RowCommand" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="VoucherMasterID" HeaderText="MasterID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                    <asp:TemplateField HeaderText="Voucher No">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSelect" runat="server" Text='<%# Eval("VoucherNo").ToString() %>' CommandName="SelectRow"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VoucherDate" HeaderText="Voucher Date" />
                                    <asp:BoundField DataField="VoucherType" HeaderText="Voucher Type" />
                                </Columns>
                                <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle CssClass="NewGridDesign" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
