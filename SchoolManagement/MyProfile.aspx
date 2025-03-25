<%@ Page Title="My Profile | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="SchoolManagement.MyProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgProfilePhoto.ClientID%>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-1"></div>
                <div class="col-sm-4">
                    <div runat="server" id="divPicturePanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Profile Photo</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-6">
                                    <asp:Image runat="server" ID="imgCurrentPhoto" Height="200px" />
  
                                </div>

                                <div class="col-sm-6">
                                    <asp:Image runat="server" ID="imgProfilePhoto" Height="200px" />
                                    <asp:HiddenField runat="server" ID="hdImagePath" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <asp:FileUpload runat="server" ID="uploaderProfilePhoto" onchange="ImagePreview(this);" CssClass="TextBoxStyle" />
                            </div>
                            <div class="col-sm-12" style="padding-top: 20px; text-align: center;">
                                <asp:Button runat="server" ID="btnUploadPhoto" OnClick="btnUploadPhoto_Click" Text="Update Photo" class="btn btn-raised btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6">
                    <div runat="server" id="divEntryPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>My Profile</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Employee Name
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdOldPassword" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">Employee NID</div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtEmployeeNID" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">Index No.</div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtIndexNo" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Father Name
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtFatherName" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Mother Name
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtMotherName" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Contact No
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-12" style="padding-top: 15px;">
                                <div class="col-sm-4">
                                    Login ID
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtLoginID" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Old Password
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtOldPassword" runat="server" CssClass="TextBoxStyle" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    New Password
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="TextBoxStyle" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    Confirm Password
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="TextBoxStyle" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-12" style="padding-top: 20px; text-align: center;">
                                <asp:Button runat="server" ID="btnSave" Text="Change Password" OnClick="btnSave_OnClick" class="btn btn-raised btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1"></div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUploadPhoto" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
