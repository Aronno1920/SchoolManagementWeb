<%@ Page Title="Location Information | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LocationInfo.aspx.cs" Inherits="SchoolManagement.LocationInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-3">
                    <div runat="server" id="divDivision">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Division Information (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvDivision" runat="server" OnRowCommand="gvDivision_RowCommand" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="DivisionId" HeaderText="Division Id" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:TemplateField HeaderText="Division">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" Text='<%# Eval("Division").ToString() %>' CommandName="SelectRow"></asp:LinkButton>
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

                <div class="col-sm-3">
                    <div runat="server" id="divDistrict">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>District Information (<asp:Label runat="server" ID="lblTotalDistrict">0</asp:Label>)<asp:Label runat="server" ID="lblDivisionName"/></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <asp:GridView ID="gvDistrict" runat="server" OnRowCommand="gvDistrict_RowCommand" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="DistrictId" HeaderText="District Id" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:TemplateField HeaderText="District">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" Text='<%# Eval("District").ToString() %>' CommandName="SelectRow"></asp:LinkButton>
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

                <div class="col-sm-3">
                    <div runat="server" id="divUpazilla">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Upazilla Information (<asp:Label runat="server" ID="lblTotalUpazilla">0</asp:Label>)<asp:Label runat="server" ID="lblDistrictName"/></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <asp:GridView ID="gvUpazilla" runat="server" OnRowCommand="gvUpazilla_RowCommand" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="UpazillaId" HeaderText="Upazilla Id" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:TemplateField HeaderText="Upazilla">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" Text='<%# Eval("Upazilla").ToString() %>' CommandName="SelectRow"></asp:LinkButton>
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

                <div class="col-sm-3">
                    <div runat="server" id="divPostOffice">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Post Office Information (<asp:Label runat="server" ID="lblTotalPostOffice">0</asp:Label>)<asp:Label runat="server" ID="lblUpazillaName"/></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvPostOffice" runat="server" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="PostOfficeId" HeaderText="ID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="PostOffice" HeaderText="Post Office" />
                                        <asp:BoundField DataField="PostCode" HeaderText="Post Code" />
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
