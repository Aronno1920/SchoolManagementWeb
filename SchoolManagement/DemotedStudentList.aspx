<%@ Page Title="Demoted Student | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DemotedStudentList.aspx.cs" Inherits="SchoolManagement.DemotedStudentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-lg-12">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Demoted Student List (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <asp:GridView ID="gvStudentList" runat="server" OnRowDataBound="gvStudentList_RowDataBound" OnRowCommand="gvStudentList_RowCommand" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Photo" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="gvImgPhoto" ImageUrl='<%# Eval("ProfilePhoto") %>' Height="50px" Width="44px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StudentId" HeaderText="StudentId" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                    <asp:BoundField DataField="StudentCode" HeaderText="Code" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px" HeaderStyle-Width="60px" />
                                    <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                    <asp:BoundField DataField="StudentNameBangla" HeaderText="বাংলায় নাম" ItemStyle-Font-Size="18px" />
                                    <asp:BoundField DataField="BirthDate" HeaderText="Birth Date" />
                                    <asp:BoundField DataField="FatherName" HeaderText="Father Name" />
                                    <asp:BoundField DataField="MotherName" HeaderText="Mother Name" />
                                    <asp:BoundField DataField="ContactNo" HeaderText="Contact No" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="SchoolName" HeaderText="School Name" />
                                    <asp:TemplateField HeaderText="Class Name" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlClass" CssClass="DropDownListStyle"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btnPromotion" Text="Apply" CommandName="StudentPromotion" class="btn btn-raised btn-primary"/>
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
