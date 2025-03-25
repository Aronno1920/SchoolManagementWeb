<%@ Page Title="Student Promotion Process | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StudentPromotion.aspx.cs" Inherits="SchoolManagement.StudentPromotion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-lg-8">
                    <div runat="server" id="divEntry">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-binoculars"></i>Promote Student</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Current Session Year
                                        <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Next Session Year
                                        <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Promote From Class
                                        <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Promote To Class
                                        <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top: 10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSearch" class="btn btn-raised btn-primary" Text="Search" OnClick="btnSearch_OnClick" />
                                <asp:Button runat="server" ID="btnClear" class="btn btn-raised btn-default-dark" Text="Clear" OnClick="btnClear_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Student List (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <asp:GridView ID="gvVehicleList" OnRowCommand="gvVehicleList_OnRowCommand" OnPageIndexChanging="gvVehicleList_OnPageIndexChanging" runat="server" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="50" RowStyle-Wrap="false"
                                EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="VehicleId" HeaderText="Id" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                    <asp:BoundField DataField="LicencePlate" HeaderText="Licence Plate" />
                                    <asp:BoundField DataField="EngineNo" HeaderText="Engine No" />
                                    <asp:BoundField DataField="ChassisNo" HeaderText="Chassis No" />
                                    <asp:BoundField DataField="TypeName" HeaderText="Vehicle Type" />
                                    <asp:BoundField DataField="BrandName" HeaderText="Brand" />
                                    <asp:BoundField DataField="ModelName" HeaderText="Model" />
                                    <asp:BoundField DataField="StatusType" HeaderText="Status" />
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
