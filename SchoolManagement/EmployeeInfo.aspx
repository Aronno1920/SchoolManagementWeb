<%@ Page Title="Employee Information | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmployeeInfo.aspx.cs" Inherits="SchoolManagement.EmployeeInfo" %>

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
                <div class="col-sm-6">
                    <div runat="server" id="divEntryPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Employee Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Name
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtEmployeeName" runat="server" placeholder="Employee Name" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdTeacherId" />
                                </div>
                                <div class="col-sm-2">
                                    Index No.
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtIndexNo" runat="server" placeholder="Teacher Index No." CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Bangla Name
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtEmployeeBanglaName" runat="server" placeholder="Employee Bangla Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">NID No.</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtEmployeeNID" runat="server" placeholder="Employee NID" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Contact No.
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtContactNo" runat="server" placeholder="Contact No" MaxLength="11" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    DOB.
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDateOfBirth" runat="server" placeholder="DD-MM-YYYY" CssClass="TextBoxStyle" MaxLength="10"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-12" style="padding-top: 20px">
                                <div class="col-sm-2">
                                    S.Name
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtSpouseName" runat="server" placeholder="Spouse Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    Gender
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:RadioButton ID="rbFemale" runat="server" Checked="true" Text="Female" GroupName="gender" CssClass="radio-inline" />
                                    <asp:RadioButton ID="rbMale" runat="server" Text="Male" GroupName="gender" CssClass="radio-inline" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    F.Name
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtFatherName" runat="server" placeholder="Father Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    M.Name
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtMotherName" runat="server" placeholder="Mother Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">F.Name (bn)</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtFatherBanglaName" runat="server" placeholder="Father Bangla Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">M.Name (bn)</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtMotherBanglaName" runat="server" placeholder="Mother Bangla Name " CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top: 20px">
                                <div class="col-sm-2">
                                    Present
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtPresentAddress" runat="server" placeholder="Present Address" CssClass="TextBoxStyle" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    Permanent
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtPermanentAdress" runat="server" placeholder="Permanent Address" CssClass="TextBoxStyle" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="margin-top: 10px;">
                                <div class="col-sm-2">
                                    Profile Photo
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-10">
                                    <asp:FileUpload runat="server" ID="uploaderProfilePhoto" onchange="ImagePreview(this);" CssClass="TextBoxStyle" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Preview Photo
                                </div>
                                <div class="col-sm-10">
                                    <asp:Image runat="server" ID="imgProfilePhoto" Height="200px" />
                                    <asp:HiddenField runat="server" ID="hdImagePath" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divUserPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>User Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Employee Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlEmployeeType" CssClass="DropDownListStyle">
                                        <asp:ListItem Value="-1" Text="Select Employee Type"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Teacher"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Staff"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="General"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Access Group
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlUserGroup" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Login ID
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtLoginID" runat="server" placeholder="Login ID" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    Password
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" CssClass="TextBoxStyle" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divButtonPanel">
                        <div class="card-body style-default-bright">
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Employee List (<asp:Label runat="server" ID="lblRowCount" Text="0"></asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <asp:TextBox runat="server" ID="txtGridSearch" CssClass="TextBoxStyle" Width="78%"></asp:TextBox>
                                <asp:Button runat="server" ID="btnGridSearch" Text="Find" CssClass="btn-raised" Width="10%" align="Right" />
                                <asp:Button runat="server" ID="btnGridClear" Text="Clear" CssClass="btn-raised" Width="10%" align="Right" />
                            </div>
                            <div class="col-sm-12" style="overflow: auto; padding-top: 10px;">
                                <asp:GridView runat="server" ID="gvTeacherList" OnRowCommand="gvTeacherList_RowCommand" Width="100%" Class="NewGridDesingBody" PageSize="10" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="EmployeeID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="EmployeeType" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="UserGroupId" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:TemplateField HeaderText="Photo">
                                            <ItemTemplate>
                                                <asp:Image runat="server" ID="cImage" ImageUrl='<%# Eval("ProfilePhoto") %>' Height="50px" Width="44px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LoginId" HeaderText="Login ID" />
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="ContactNo" HeaderText="Contact No." />
                                        <asp:BoundField DataField="UserType" HeaderText="User Type" />
                                        <asp:BoundField DataField="GroupName" HeaderText="Group Name" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="SelectRow" ToolTip="Click For Edit" ImageUrl="~/img/edit.png" Height="25px" />
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
