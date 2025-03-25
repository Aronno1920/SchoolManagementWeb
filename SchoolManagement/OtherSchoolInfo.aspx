<%@ Page Title="Other School Info | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OtherSchoolInfo.aspx.cs" Inherits="SchoolManagement.OtherSchoolInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-6">
                    <div runat="server" id="divEntryPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Others School Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    School Name
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtSchoolName" runat="server" placeholder="School Name" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdSchoolId" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Location
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtLoacation" runat="server" placeholder="School Location" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                           <div class="col-sm-12" style="padding: 5px; text-align: center;">
                                <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_OnClick" class="btn btn-raised btn-primary" />
                                <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" class="btn btn-raised btn-warning" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_OnClick" class="btn btn-raised btn-default-dark" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6">
                    <div runat="server" id="divDetailsPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Others School  List (<asp:Label runat="server" ID="lblRowCount" Text="0"></asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <asp:TextBox runat="server" ID="txtGridSearch" CssClass="TextBoxStyle" Width="78%"></asp:TextBox>
                                <asp:Button runat="server" ID="btnGridSearch" Text="Find" CssClass="btn-raised" Width="10%" align="Right" />
                                <asp:Button runat="server" ID="btnGridClear" Text="Clear" CssClass="btn-raised" Width="10%" align="Right" />
                            </div>
                            <div class="col-sm-12">
                                <asp:GridView runat="server" ID="gvOtherSchoolList" OnRowCommand="gvOtherSchoolList_RowCommand" OnPageIndexChanging="gvOtherSchoolList_PageIndexChanging" Width="100%" Class="NewGridDesingBody" AllowPaging="True" PageSize="15" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="SchoolID" HeaderText="SL." HeaderStyle-Width="40px" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right"/>
                                        <asp:BoundField DataField="SchoolName" HeaderText="School Name" />
                                        <asp:BoundField DataField="Location" HeaderText="Location" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click For Edit" ImageUrl="~/img/edit.png" Height="23px" />
                                                <asp:ImageButton runat="server" ID="lbtnDelete" CommandName="DeleteRow" ToolTip="Click For Delete" ImageUrl="~/img/delete.png" Width="20px" Height="20px"></asp:ImageButton>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
