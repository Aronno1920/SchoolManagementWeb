<%@ Page Title="Candidate Entry | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CandidateEntry.aspx.cs" Inherits="SchoolManagement.CandidateEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">

    <script type="text/javascript">
        function save_validate() {
            if (document.getElementById("<%=txtCandidateName.ClientID%>").value == "") {
                alert('Please Enter Candidate Name');
                document.getElementById("<%=txtCandidateName.ClientID%>").focus();
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
            <%--if (document.getElementById("<%=ddlSchool.ClientID%>").value == "0") {
                alert('Please Select Previous School');
                document.getElementById("<%=txtGuardianName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtLastClass.ClientID%>").value == "") {
                alert('Please Enter Last Class');
                document.getElementById("<%=txtLastClass.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlPassingYear.ClientID%>").value == "0") {
                alert('Please Select Passing Year');
                document.getElementById("<%=ddlPassingYear.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtRollNo.ClientID%>").value == "") {
                alert('Please Enter PSC/GSC Roll No');
                document.getElementById("<%=txtRollNo.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtGPA.ClientID%>").value == "") {
                alert('Please Enter PSC/GSC GPA');
                document.getElementById("<%=txtGPA.ClientID%>").focus();
                return false;
            }--%>
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
            if (document.getElementById("<%=ddlClassId.ClientID%>").value == "0") {
                alert('Please Select Admission Class');
                document.getElementById("<%=ddlClassId.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlPaymentType.ClientID%>").value == "0") {
                alert('Please Select Payment Type');
                document.getElementById("<%=ddlPaymentType.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPaidAmount.ClientID%>").value == "") {
                alert('Please Enter Admission Fee (Amount)');
                document.getElementById("<%=txtPaidAmount.ClientID%>").focus();
                return false;
            }
        }
    </script>

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
                    <div runat="server" id="divStudent">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Candidate Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Form No. <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtFormNo" runat="server" placeholder="Form Number" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdCandidateId" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnLoadCandidate" OnClick="btnLoadCandidate_Click" Text="Load" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Name<span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtCandidateName" runat="server" placeholder="Candidate Name" CssClass="TextBoxStyle"></asp:TextBox>
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
                                    <asp:TextBox ID="txtBanglaName" runat="server" placeholder="Bangla Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    Birth Date
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtBirthDate" runat="server" CssClass="TextBoxStyle" placeholder="DD-MM-YYYY" MaxLength="10"></asp:TextBox>
                                    <%--<asp:MaskedEditExtender ID="meeBirthDay" runat="server" TargetControlID="txtBirthDate" Mask="99-99-9999" UserDateFormat="DayMonthYear" MaskType="DateTime" MessageValidatorTip="true" />--%>
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
                                    F. Name<span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtFatherName" runat="server" placeholder="Father Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    M. Name<span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtMotherName" runat="server" placeholder="Mother Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">F. Name(bn)</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtFatherNameBangla" runat="server" placeholder="Father Bangla Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">M. Name(bn)</div>
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

                    <div runat="server" id="divGuardian" style="margin-top: 20px;">
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
                                    <asp:TextBox ID="txtIncome" runat="server" CssClass="TextBoxStyle" placeholder="Yearly Income" Style="text-align: right"></asp:TextBox>
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
                                    Roll No (PSC/JSC)
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
                <div class="col-sm-6">
                    <div runat="server" id="divCurrentAcademic">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Admission Informaton</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="margin-bottom: 10px">
                                <div class="col-sm-2">
                                    Class Name
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlClassId" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Session
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtAdmissionYear" Enabled="false" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtSessionTitle" Enabled="false" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Expertise</div>
                                <div class="col-sm-10">
                                    <asp:CheckBoxList runat="server" ID="cbExpertise" RepeatColumns="3" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Dance" Value="Dance"></asp:ListItem>
                                        <asp:ListItem Text="Song" Value="Song"></asp:ListItem>
                                        <asp:ListItem Text="Poetry" Value="Poetry"></asp:ListItem>
                                        <asp:ListItem Text="Recitation" Value="Recitation"></asp:ListItem>
                                        <asp:ListItem Text="Drawing" Value="Drawing"></asp:ListItem>
                                        <asp:ListItem Text="Sports" Value="Sports"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="col-sm-12" style="margin-top: 20px;">
                                <div class="col-sm-2">Payment</div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">Amount</div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxStyle" Enabled="false" Style="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtPaidAmount" runat="server" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Remarks</div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtRemarks" runat="server" placeholder="Enter Remarks, If amount is decreased" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="divProfilePhoto" style="margin-top: 20px;">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Profile Photo</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Select Photo
                                </div>
                                <div class="col-sm-10">
                                    <asp:FileUpload ID="uploaderProfilePhoto" runat="server" onchange="ImagePreview(this);" CssClass="TextBoxStyle" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Preview
                                </div>
                                <div class="col-sm-10">
                                    <asp:Image runat="server" ID="imgProfilePhoto" Height="200px" Width="180px" />
                                    <asp:HiddenField runat="server" ID="hdImagePath" />
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
                                <asp:Button runat="server" ID="btnSave" Text="Save Candidate" OnClick="btnSave_OnClick" OnClientClick="return save_validate();" class="btn btn-raised btn-primary" />
                                <asp:Button runat="server" ID="btnUpdate" class="btn btn-raised btn-warning" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return savevalidate();" />
                                <asp:Button runat="server" ID="btnCandidateList" Text="Candidate List" OnClick="btnCandidateList_Click" CssClass="btn btn-raised btn-info" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_OnClick" class="btn btn-raised btn-default-dark" />
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
