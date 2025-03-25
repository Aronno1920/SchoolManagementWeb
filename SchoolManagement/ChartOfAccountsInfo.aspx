<%@ Page Title="Chart Of Accounts Information | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChartOfAccountsInfo.aspx.cs" Inherits="SchoolManagement.ChartOfAccountsInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-3">
                <div runat="server" id="divEntryPanel" style="padding: 5px;">
                    <div runat="server" id="divAccountTypeEntryPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Account Type Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <asp:TextBox ID="txtTypeName" runat="server" CssClass="TextBoxStyle" placeholder="Type Name"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdTypeId" />
                            </div>
                            <div class="col-sm-12">
                                <asp:CheckBox runat="server" ID="cbActiveType" Text=" Is Active?" />
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSaveType" OnClick="btnSaveType_Click" class="btn btn-raised btn-primary" Text="Save" />
                                <asp:Button runat="server" ID="btnUpdateType" OnClick="btnUpdateType_Click" class="btn btn-raised btn-warning" Text="Update" />
                                <asp:Button runat="server" ID="btnClearType" OnClick="btnClearType_Click" class="btn btn-raised btn-default-dark" Text="Clear" />
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divAccountGroupEntryPanel" style="margin-top: 30px">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Account Group Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <asp:DropDownList ID="ddlTypeNameGroup" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                <asp:HiddenField runat="server" ID="hdGroupId" />
                            </div>
                            <div class="col-sm-12">
                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="TextBoxStyle" placeholder="Group Name"></asp:TextBox>
                            </div>
                            <div class="col-sm-12">
                                <asp:CheckBox runat="server" ID="cbActiveGroup" Text=" Is Active?" />
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSaveGroup" OnClick="btnSaveGroup_Click" class="btn btn-raised btn-primary" Text="Save" />
                                <asp:Button runat="server" ID="btnUpdateGroup" OnClick="btnUpdateGroup_Click" class="btn btn-raised btn-warning" Text="Update" />
                                <asp:Button runat="server" ID="btnClearGroup" OnClick="btnClearGroup_Click" class="btn btn-raised btn-default-dark" Text="Clear" />
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divAccountHeadEntryPanel" style="margin-top: 30px">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Account Head Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <asp:DropDownList ID="ddlTypeNameAccount" runat="server" OnSelectedIndexChanged="ddlTypeNameAccount_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle"></asp:DropDownList>
                                <asp:HiddenField runat="server" ID="hdAccountID" />
                            </div>
                            <div class="col-sm-12">
                                <asp:DropDownList ID="ddlGroupNameAccount" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                            </div>
                            <div class="col-sm-12">
                                <asp:TextBox ID="txtAccountName" runat="server" CssClass="TextBoxStyle" placeholder="Account Name"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdAccountHead" />
                            </div>
                            <div class="col-sm-12">
                                <asp:CheckBox runat="server" ID="cbActiveAccount" Text=" Is Active?" />
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSaveAccount" OnClick="btnSaveAccount_Click" class="btn btn-raised btn-primary" Text="Save" />
                                <asp:Button runat="server" ID="btnUpdateAccount" OnClick="btnUpdateAccount_Click" class="btn btn-raised btn-warning" Text="Update" />
                                <asp:Button runat="server" ID="btnClearAccount" OnClick="btnClearAccount_Click" class="btn btn-raised btn-default-dark" Text="Clear" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-9">
                <div class="col-sm-3">
                    <div runat="server" id="divTypePanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Type Info (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvType" runat="server" OnRowCommand="gvType_RowCommand" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="AccountID" HeaderText="AccountID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="Code" HeaderText="Code" />
                                        <asp:TemplateField HeaderText="Type Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" Text='<%# Eval("AccountName").ToString() %>' CommandName="SelectRow"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IsActive" HeaderText="Active?" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click for edit" ImageUrl="~/img/edit.png" Height="25px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f0fdf9" />
                                    <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle CssClass="NewGridDesign" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div runat="server" id="divGroupPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Group Info (<asp:Label runat="server" ID="lblTotalGroup">0</asp:Label>)<asp:Label runat="server" ID="lblTypeName" /></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvGroup" runat="server" OnRowCommand="gvGroup_RowCommand" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="AccountID" HeaderText="AccountID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="ParentID" HeaderText="ParentID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="Code" HeaderText="Code" />
                                        <asp:TemplateField HeaderText="Group Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" Text='<%# Eval("AccountName").ToString() %>' CommandName="SelectRow"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IsActive" HeaderText="Active?" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click for edit" ImageUrl="~/img/edit.png" Height="25px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f0fdf9" />
                                    <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle CssClass="NewGridDesign" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-5">
                    <div runat="server" id="divAccountsPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Accounts Info (<asp:Label runat="server" ID="lblTotalAccounts">0</asp:Label>)<asp:Label runat="server" ID="lblGroupName" /></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvAccounts" runat="server" OnRowCommand="gvAccounts_RowCommand" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="AccountID" HeaderText="AccountID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn"/>
                                        <asp:BoundField DataField="TypeID" HeaderText="TypeID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn"/>
                                        <asp:BoundField DataField="ParentID" HeaderText="GroupID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn"/>
                                        <asp:BoundField DataField="Code" HeaderText="Code" />
                                        <asp:BoundField DataField="AccountName" HeaderText="Accounts Name" />
                                        <asp:BoundField DataField="IsActive" HeaderText="Active?" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click for edit" ImageUrl="~/img/edit.png" Height="25px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f0fdf9" />
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
