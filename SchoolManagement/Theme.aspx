<%@ Page Title="Theme | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Theme.aspx.cs" Inherits="SchoolManagement.Theme" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-lg-1"></div>
            <div class="col-lg-4">
                <div class="card-body style-default-bright">
                    <div class="col-sm-12">
                        <div class="col-sm-4">
                            Choose Theme
                        </div>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlTheme" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTheme_SelectedIndexChanged" CssClass="DropDownListStyle">
                                <asp:ListItem Text="Default Dark" Value="card card-bordered style-default-dark"></asp:ListItem>
                                <asp:ListItem Text="Default" Value="card card-bordered style-default"></asp:ListItem>
                                <asp:ListItem Text="Default Light" Value="card card-bordered style-default-light"></asp:ListItem>
                                <asp:ListItem Text="Default Bright" Value="card card-bordered style-default-bright"></asp:ListItem>
                                <asp:ListItem Text="Primary Dark" Value="card card-bordered style-primary-dark"></asp:ListItem>
                                <asp:ListItem Text="Primary" Value="card card-bordered style-primary"></asp:ListItem>
                                <asp:ListItem Text="Primary Light" Value="card card-bordered style-primary-light"></asp:ListItem>
                                <asp:ListItem Text="Primary Bright" Value="card card-bordered style-primary-bright"></asp:ListItem>
                                <asp:ListItem Text="Accent Dark" Value="card card-bordered style-accent-dark"></asp:ListItem>
                                <asp:ListItem Text="Accent" Value="card card-bordered style-accent"></asp:ListItem>
                                <asp:ListItem Text="Accent Light" Value="card card-bordered style-accent-light"></asp:ListItem>
                                <asp:ListItem Text="Accent Bright" Value="card card-bordered style-accent-bright"></asp:ListItem>
                                <asp:ListItem Text="Danger" Value="card card-bordered style-danger"></asp:ListItem>
                                <asp:ListItem Text="Warning" Value="card card-bordered style-warning"></asp:ListItem>
                                <asp:ListItem Text="Success" Value="card card-bordered style-success"></asp:ListItem>
                                <asp:ListItem Text="Info" Value="card card-bordered style-info"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                        <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Save My Theme" CssClass="btn btn-raised btn-primary" />
                    </div>
                </div>
            </div>

            <div class="col-lg-6">
                <div runat="server" id="divEntry">
                    <div class="card-head">
                        <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-fw fa-tag"></i>Theme Preview</header>
                    </div>
                    <div class="card-body style-default-bright">
                        <div class="card-body" style="min-height: 200px">
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="Button1" class="btn btn-raised btn-accent" Text="Load" />
                                <asp:Button runat="server" ID="btn1" class="btn btn-raised btn-primary" Text="Save" />
                                <asp:Button runat="server" ID="Button2" class="btn btn-raised btn-warning" Text="Update" />
                                <asp:Button runat="server" ID="Button3" class="btn btn-raised btn-danger" Text="Delete" />
                                <asp:Button runat="server" ID="Button5" class="btn btn-raised btn-info" Text="Print" />
                                <asp:Button runat="server" ID="Button4" class="btn btn-raised btn-default-dark" Text="Clear" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-1"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
