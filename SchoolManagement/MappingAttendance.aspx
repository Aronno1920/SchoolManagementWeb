<%@ Page Title="Attendance Officer | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MappingAttendance.aspx.cs" Inherits="SchoolManagement.MappingAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-lg-6">
                    <div runat="server" id="divEntry">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Attendance Officer Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Class
                                </div>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlClass" runat="server" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Section
                                </div>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Officer
                                </div>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlOfficer" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top: 10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSave" class="btn btn-raised btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return savevalidate();" />
                                <asp:Button runat="server" ID="btnClear" class="btn btn-raised btn-default-dark" Text="Clear" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Attendance Officer List (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="margin-bottom: 10px">
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlGridSearch" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" ID="txtGridSearch" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:Button runat="server" ID="btnGridSearch" Text="Search" CssClass="btn-raised" />
                                </div>
                                <div class="col-sm-1">
                                    <asp:Button runat="server" ID="btnGridSearchClear" Text="Clear" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvMapping" runat="server" OnRowCommand="gvMapping_RowCommand" OnPageIndexChanging="gvMapping_OnPageIndexChanging" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="RuleId" HeaderText="ID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="ClassName" HeaderText="Class" />
                                        <asp:BoundField DataField="SectionName" HeaderText="Section" />
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnRemove" CommandName="RemoveRow" OnClientClick="return confirm('Are you sure, you want to delete?');" ToolTip="Delete??" ImageUrl="~/img/delete.png" Height="25px" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
