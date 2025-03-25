<%@ Page Title="Monthly Attendances | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AttendancesMonthly.aspx.cs" Inherits="SchoolManagement.AttendancesMonthly" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-6">
                    <div runat="server" id="divEntry">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Search Criteria - Monthly Attendances</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Class <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlClass" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Start Date
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtStartDate" runat="server" Width="80%" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                     <asp:Image runat="server" ID="imgStartDate" ImageUrl="~/img/calender.png" Height="25px" />
                                    <asp:CalendarExtender ID="CalendarExtenderStart" runat="server" TargetControlID="txtStartDate" PopupButtonID="imgStartDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Section
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">End Date</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtEndDate" runat="server" Width="80%" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                     <asp:Image runat="server" ID="imgEndDate" ImageUrl="~/img/calender.png" Height="25px" />
                                    <asp:CalendarExtender ID="CalendarExtenderEnd" runat="server" TargetControlID="txtEndDate" PopupButtonID="imgEndDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnLoadData" Text="Load" OnClick="btnLoadData_Click" OnClientClick="return savevalidate();" class="btn btn-raised btn-accent" />
                                <asp:Button runat="server" ID="btnExportData" Text="Export Excel" OnClick="btnExportData_Click" class="btn btn-raised btn-primary" />
                                <asp:Button runat="server" ID="btnDaily" Text="Daily Attendance" OnClick="btnDaily_Click" CssClass="btn btn-raised btn-info" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" class="btn btn-raised btn-default-dark" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Monthly Attendances Report (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright" style="overflow: auto;">
                            <asp:GridView runat="server" ID="gvAttendance" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false" EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True">
                                <AlternatingRowStyle BackColor="#f0fdf9" />
                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle CssClass="NewGridDesign" />
                                <FooterStyle BackColor="Silver" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportData" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
