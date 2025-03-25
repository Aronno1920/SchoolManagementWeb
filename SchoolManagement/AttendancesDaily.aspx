<%@ Page Title="Daily Attendances | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AttendancesDaily.aspx.cs" Inherits="SchoolManagement.AttendancesDaily" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">

    <script type="text/javascript">
        function load_validate() {
            if (document.getElementById("<%=ddlClass.ClientID%>").value == "0") {
                alert('Please Enter Class Name');
                document.getElementById("<%=ddlClass.ClientID%>").focus();
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-9">
                    <div runat="server" id="divEntry">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Search Criteria - Daily Attendances</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-1">
                                    Class
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlClass" runat="server" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    Section
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    Date
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top: 10px; text-align: center;">
                                <asp:Button runat="server" ID="btnLoad" Text="Load Student" OnClick="btnLoad_Click" OnClientClick="return load_validate();" class="btn btn-raised btn-accent" />
                                <asp:Button runat="server" ID="btnSave" Text="Save Attendance" OnClick="btnSave_Click" OnClientClick="return savevalidate();" class="btn btn-raised btn-primary" />
                                <asp:Button runat="server" ID="btnMonthly" Text="Monthly Attendance" OnClick="btnMonthly_Click" CssClass="btn btn-raised btn-info" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" class="btn btn-raised btn-default-dark" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Student List (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright" style="overflow: auto;">
                            <asp:GridView ID="gvStudentList" runat="server" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false"
                                EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Photo">
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="gvImgPhoto" ImageUrl='<%# Eval("ProfilePhoto") %>' Height="50px" Width="44px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StudentId" HeaderText="StudentId" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                    <asp:BoundField DataField="StudentCode" HeaderText="Student Code" />
                                    <asp:BoundField DataField="RollNo" HeaderText="Roll No" />
                                    <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                    <asp:BoundField DataField="StudentNameBangla" HeaderText="বাংলায় নাম" />
                                    <asp:BoundField DataField="ClassName" HeaderText="Class" />
                                    <asp:BoundField DataField="SectionName" HeaderText="Section" />
                                    <asp:BoundField DataField="FatherName" HeaderText="Father Name" />
                                    <asp:BoundField DataField="MotherName" HeaderText="Mother Name" />
                                    <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                    <asp:TemplateField HeaderText="Is Present?">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="cbIsPresent" Checked="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#f0fdf9" />
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
