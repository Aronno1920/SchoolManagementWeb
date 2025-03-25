<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageViewList.Master" AutoEventWireup="true" CodeBehind="ExamList.aspx.cs" Inherits="SchoolManagement.ExamList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SearchPlaceHolder" runat="server">
    <div class="col-sm-12">
        <div runat="server" id="divEntry">
            <div class="card-head">
                <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Student Search</header>
            </div>
            <div class="card-body style-default-bright">
                <div class="col-sm-12">
                    <div class="col-sm-5">
                        Class
                    </div>
                    <div class="col-sm-7">
                        <asp:DropDownList runat="server" ID="ddlClass" AutoPostBack="true" CssClass="DropDownListStyle"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-5">
                        Section
                    </div>
                    <div class="col-sm-7">
                        <asp:DropDownList runat="server" ID="ddlSection" CssClass="DropDownListStyle"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="col-sm-5">
                        Search By
                    </div>
                    <div class="col-sm-7">
                        <asp:TextBox runat="server" ID="txtSearchBy" CssClass="TextBoxStyle" placeholder="Name / Code / Contact No."></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12" style="padding-top: 10px; text-align: center;">
                    <asp:Button runat="server" ID="btnSearch" Text="Search" class="btn btn-raised btn-primary" />
                    <asp:Button runat="server" ID="btnStudentEntry" Text="Add Student" CssClass="btn btn-raised btn-info" />
                    <asp:Button runat="server" ID="btnClear" Text="Clear" class="btn btn-raised btn-default-dark" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="col-sm-12">
        <div runat="server" id="divDetails">
            <div class="card-head">
                <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Brand Details (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
            </div>
            <div class="card-body style-default-bright">
                <div class="col-sm-12" style="margin-bottom: 10px">
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlGridSearch" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                    </div>
                    <div class="col-sm-5">
                        <asp:TextBox runat="server" ID="txtGridSearch" CssClass="TextBoxStyle"></asp:TextBox>
                    </div>
                    <div class="col-sm-2" align="right">
                        <asp:Button runat="server" ID="btnGridSearch" Text="Search" CssClass="btn-raised" />
                    </div>
                    <div class="col-sm-2" align="right">
                        <asp:Button runat="server" ID="btnGridSearchClear" Text="Clear" CssClass="btn-raised" />
                    </div>
                </div>
                <div class="col-sm-12" style="overflow: auto;">
                    <asp:GridView ID="gvBrand" runat="server" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                        EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="Brand ID" />
                            <asp:BoundField DataField="Name" HeaderText="Brand Name" />
                            <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click For Edit" ImageUrl="~/img/edit.png" Height="25px" />
                                    <asp:ImageButton runat="server" ID="ibtnRemove" CommandName="RemoveRow" OnClientClick="return confirm('Are you sure, you want to brand name?');" ToolTip="Click For Delete" ImageUrl="~/img/delete.png" Height="25px" />
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
</asp:Content>
