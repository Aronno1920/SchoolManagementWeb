<%@ Page Title="Student Entry | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StudentEdit.aspx.cs" Inherits="SchoolManagement.StudentEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript">
        function save_validate() {
            if (document.getElementById("<%=txtStudentName.ClientID%>").value == "") {
                alert('Please Enter Student Name');
                document.getElementById("<%=txtStudentName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtBanglaName.ClientID%>").value == "") {
                alert('Please Enter Candidate Bangla Name');
                document.getElementById("<%=txtBanglaName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtBirthDate.ClientID%>").value == "") {
                alert('Please Select Date of Birth');
                document.getElementById("<%=txtBirthDate.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtFatherName.ClientID%>").value == "") {
                alert('Please Enter Father Name');
                document.getElementById("<%=txtFatherName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtFatherNameBangla.ClientID%>").value == "") {
                alert('Please Enter Father Bangla Name');
                document.getElementById("<%=txtFatherNameBangla.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtMotherName.ClientID%>").value == "") {
                alert('Please Enter Mother Name');
                document.getElementById("<%=txtMotherName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtMotherNameBangla.ClientID%>").value == "") {
                alert('Please Enter Mother Bangla Name');
                document.getElementById("<%=txtMotherNameBangla.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtGuardianName.ClientID%>").value == "") {
                alert('Please Enter Guardian Name');
                document.getElementById("<%=txtGuardianName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlNationality.ClientID%>").value == "0") {
                alert('Please Select Nationality');
                document.getElementById("<%=ddlNationality.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlReligion.ClientID%>").value == "0") {
                alert('Please Select Religion');
                document.getElementById("<%=ddlReligion.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-6">
                    <div runat="server" id="divStudent">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Student Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Code
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtStudentCode" placeholder="Student Code" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdStudentId" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnLoadStudent" OnClick="btnLoadStudent_Click" Text="Load" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12" style="margin-top: 20px;">
                                <div class="col-sm-2">
                                    Name
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtStudentName" runat="server" placeholder="Student Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    Birth ID
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtBirthCerId" runat="server" placeholder="Birth Cer. ID" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Name (bn)<span style="color: red">*</span></div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtBanglaName" runat="server" placeholder="Student Bangla Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    Birth Date
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtBirthDate" runat="server" placeholder="DD-MM-YYYY" CssClass="TextBoxStyle" MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Nationality
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlNationality" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">Religion</div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlReligion" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top: 10px">
                                <div class="col-sm-2">
                                    F. Name
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtFatherName" runat="server" placeholder="Father Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    M. Name
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtMotherName" runat="server" placeholder="Mother Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">F. Name (bn)</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtFatherNameBangla" runat="server" placeholder="Father Bangla Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">M. Name (bn)</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtMotherNameBangla" runat="server" placeholder="Mother Bangla Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Father NID</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtFatherNID" runat="server" placeholder="Father NID" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Mother NID</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtMotherNID" runat="server" placeholder="Mother NID" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div runat="server" id="divGuardian">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Guardian Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">Name</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtGuardianName" runat="server" placeholder="Guardian Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Relation</div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlRelation" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Occupation</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtOccupation" runat="server" placeholder="Occupation" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Income</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtIncome" runat="server" placeholder="Yearly Income" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Contact No</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtContactNo" runat="server" placeholder="Contact Number" CssClass="TextBoxStyle" MaxLength="11"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div runat="server" id="divAcademic" style="margin-top: 20px;">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Previous Academic Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    School Name
                                </div>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlSchool" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Last Class
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtLastClass" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    Year of Passing
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" ID="ddlPassingYear" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    PSC/JSC Roll No
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtRollNo" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    G.P.A
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGPA" runat="server" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divExpertise">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Expertise Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">Expertise</div>
                                <div class="col-sm-1">
                                    <asp:CheckBox runat="server" ID="cbDance" Text="Dance" />
                                </div>
                                <div class="col-sm-1">
                                    <asp:CheckBox runat="server" ID="cbSong" Text="Song" />
                                </div>
                                <div class="col-sm-1">
                                    <asp:CheckBox runat="server" ID="cbPoetry" Text="Poetry" />
                                </div>
                                <div class="col-sm-1">
                                    <asp:CheckBox runat="server" ID="cbRecitation" Text="Recitation" />
                                </div>
                                <div class="col-sm-1">
                                    <asp:CheckBox runat="server" ID="cbDrawing" Text="Drawing" />
                                </div>
                                <div class="col-sm-1">
                                    <asp:CheckBox runat="server" ID="cbSports" Text="Sports" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divAddress">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Address Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-1">
                                    Present
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtAddressPre" runat="server" placeholder="Present Address" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:DropDownList runat="server" ID="ddlDistrictPre" OnSelectedIndexChanged="ddlDistrictPre_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:DropDownList runat="server" ID="ddlUpazillaPre" OnSelectedIndexChanged="ddlUpazillaPre_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:DropDownList runat="server" ID="ddlPostOfficePre" CssClass="DropDownListStyle" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-1">
                                    Permanent
                                </div>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtAddressPer" runat="server" placeholder="Permanent Address" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:DropDownList runat="server" ID="ddlDistrictPer" OnSelectedIndexChanged="ddlDistrictPer_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:DropDownList runat="server" ID="ddlUpazillaPer" OnSelectedIndexChanged="ddlUpazillaPer_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:DropDownList runat="server" ID="ddlPostOfficePer" CssClass="DropDownListStyle" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-1">
                                    <asp:CheckBox runat="server" ID="cbSameAsPresent" />
                                </div>
                                <div class="col-sm-5">
                                    Same as present address?
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divButton">
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="padding: 5px; text-align: center;">
                                <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_OnClick" OnClientClick="return validate();" class="btn btn-raised btn-warning" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_OnClick" class="btn btn-raised btn-default-dark" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
