<%@ Page Title="Book Information | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BookEntry.aspx.cs" Inherits="SchoolManagement.BookEntry" %>

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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Book Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Book ID
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtBrandID" runat="server" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Book Name
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtBrandName" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Author Name
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    ISBN Code
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="margin-top: 20px">
                                <div class="col-sm-3">
                                    Price (BDT)
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Description
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Rack Information
                                </div>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="TextBox7" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12" style="margin-top: 20px">
                                <div class="col-sm-3">
                                    Class Name
                                </div>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Image Upload
                                </div>
                                <div class="col-sm-9">
                                    <asp:FileUpload ID="TextBox6" runat="server" CssClass="TextBoxStyle"></asp:FileUpload>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSave" class="btn btn-raised btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return savevalidate();" />
                                <asp:Button runat="server" ID="btnUpdate" class="btn btn-raised btn-warning" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return savevalidate();" />
                                <asp:Button runat="server" ID="btnClear" class="btn btn-raised btn-default-dark" Text="Clear" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
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
                                    <asp:Button runat="server" ID="btnGridSearch" Text="Search" CssClass="btn-raised"/>
                                </div>
                                <div class="col-sm-2" align="right">
                                    <asp:Button runat="server" ID="btnGridSearchClear" Text="Clear" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvBrand" runat="server" OnRowCommand="gvBrand_RowCommand" OnPageIndexChanging="gvBrand_OnPageIndexChanging" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
