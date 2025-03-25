<%@ Page Title="Password Reset | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs" Inherits="SchoolManagement.PasswordReset" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-5">
                    <div runat="server" id="divEntryPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Profile - Selected Employee</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Employee Name
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtEmployeeName" runat="server" placeholder="Employee Name" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdEmployeeID" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">Employee NID</div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtEmployeeNID" runat="server" placeholder="Employee NID" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">Index No.</div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtIndexNo" runat="server" placeholder="Teacher Index No" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Father Name
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtFatherName" runat="server" placeholder="Father Name" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Mother Name
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtMotherName" runat="server" placeholder="Mother Name" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Contact No
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtContactNo" runat="server" placeholder="Contact No" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-12" style="padding-top: 15px;">
                                <div class="col-sm-4">
                                    Login ID
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtLoginID" runat="server" placeholder="Login ID" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    New Password
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="TextBoxStyle" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Confirm Password
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="TextBoxStyle" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-12" style="padding-top: 20px; text-align: center;">
                                <asp:Button runat="server" ID="btnSave" Text="Change Password" OnClick="btnSave_OnClick" class="btn btn-raised btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-7">
                    <div runat="server" id="divDetailsPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Employee List (<asp:Label runat="server" ID="lblRowCount" Text="0"></asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <asp:TextBox runat="server" ID="txtGridSearch" CssClass="TextBoxStyle" Width="78%"></asp:TextBox>
                                <asp:Button runat="server" ID="btnGridSearch" Text="Find" CssClass="btn-raised" Width="10%" align="Right" />
                                <asp:Button runat="server" ID="btnGridClear" Text="Clear" CssClass="btn-raised" Width="10%" align="Right" />
                            </div>
                            <div class="col-sm-12" style="overflow: auto; padding-top: 10px;">
                                <asp:GridView runat="server" ID="gvEmployeeList" OnRowCommand="gvEmployeeList_RowCommand" Width="100%" Class="NewGridDesingBody" PageSize="10" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="EmployeeID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="EmployeeType" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="UserGroupId" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:TemplateField HeaderText="Photo">
                                            <ItemTemplate>
                                                <asp:Image runat="server" ID="cImage" ImageUrl='<%# Eval("ProfilePhoto") %>' Height="50px" Width="44px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LoginId" HeaderText="Login ID" />
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="ContactNo" HeaderText="Contact No." />
                                        <asp:BoundField DataField="UserType" HeaderText="User Type" />
                                        <asp:BoundField DataField="GroupName" HeaderText="Group Name" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click For Edit" ImageUrl="~/img/edit.png" Height="25px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
