<%@ Page Title="Student List | SMS" Language="C#" MasterPageFile="~/MasterPageViewList.Master" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="SchoolManagement.StudentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SearchPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel1">
        <ContentTemplate>
            <div runat="server" id="divSearchPanel">
                <div class="card-head">
                    <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Search Criteria - Student</header>
                </div>
                <div class="card-body style-default-bright">
                    <div class="col-sm-12">
                        <div class="col-sm-4">
                            Class
                        </div>
                        <div class="col-sm-8">
                            <asp:DropDownList runat="server" ID="ddlClass" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-sm-4">
                            Section
                        </div>
                        <div class="col-sm-8">
                            <asp:DropDownList runat="server" ID="ddlSection" CssClass="DropDownListStyle"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-sm-4">
                            Search By
                        </div>
                        <div class="col-sm-8">
                            <asp:TextBox runat="server" ID="txtSearchBy" CssClass="TextBoxStyle" placeholder="Name / Code / Contact No."></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-12" style="padding-top: 10px; text-align: center;">
                        <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Search" class="btn btn-raised btn-primary" />
                        <asp:Button runat="server" ID="btnStudentEntry" Text="Add Student" OnClick="btnStudentEntry_Click" CssClass="btn btn-raised btn-info" />
                        <asp:Button runat="server" ID="btnClear" OnClick="btnClear_Click" Text="Clear" class="btn btn-raised btn-default-dark" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div runat="server" id="divDetails">
                <div class="card-head">
                    <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Student List (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                </div>
                <div class="card-body style-default-bright">
                    <asp:GridView ID="gvStudentList" runat="server" OnRowCommand="gvStudentList_RowCommand" OnPageIndexChanging="gvStudentList_PageIndexChanging" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                        EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="StudentId" HeaderText="StudentId" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                            <asp:TemplateField HeaderText="Photo" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Image runat="server" ID="gvImgPhoto" ImageUrl='<%# Eval("ProfilePhoto") %>' Height="50px" Width="44px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="StudentCode" HeaderText="Student Code" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="RollNo" HeaderText="Roll No" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                            <asp:BoundField DataField="StudentNameBangla" HeaderText="বাংলায় নাম" ItemStyle-Font-Size="18px" />
                            <asp:BoundField DataField="ClassName" HeaderText="Class" />
                            <asp:BoundField DataField="SectionName" HeaderText="Section" />
                            <asp:BoundField DataField="FatherName" HeaderText="Father Name" />
                            <asp:BoundField DataField="MotherName" HeaderText="Mother Name" />
                            <asp:BoundField DataField="ContactNo" HeaderText="Contact No" ItemStyle-HorizontalAlign="Right" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click For Edit" ImageUrl="~/img/edit.png" Height="25px" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
