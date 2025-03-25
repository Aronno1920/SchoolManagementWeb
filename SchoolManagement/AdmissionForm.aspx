<%@ Page Title="Admission Form | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdmissionForm.aspx.cs" Inherits="SchoolManagement.AdmissionForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript">
        function savevalidate() {
            if (document.getElementById("<%=txtFormNo.ClientID%>").value == "") {
                alert('Please Enter Form No');
                document.getElementById("<%=txtFormNo.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtName.ClientID%>").value == "") {
                alert('Please Enter Student Name');
                document.getElementById("<%=txtName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlClassName.ClientID%>").value == "0") {
                alert('Please Select Class Name');
                document.getElementById("<%=ddlClassName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlSessionYear.ClientID%>").value == "") {
                alert("Please Enter Section Year");
                document.getElementById("<%=ddlSessionYear.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtContactNo.ClientID%>").value == "") {
                alert('Please Enter Guardian Contact No');
                document.getElementById("<%=txtContactNo.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-lg-6">
                    <div runat="server" id="divEntry">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Admission Form</header>
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
                                    Class <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlClassName" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Name <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtName" runat="server" placeholder="Student Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    Session <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlSessionYear" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Contact
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtContactNo" runat="server" placeholder="Guardian Contact" MaxLength="11" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-12" style="margin-top: 20px;">
                                <div class="col-sm-2">Payment</div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">Amount <span style="color: red">*</span></div>
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
                            <div class="col-sm-12" style="padding-top: 10px; text-align: center;">
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Admission Form Details (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
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
                                    <asp:Button runat="server" ID="btnGridSearch" OnClick="btnGridSearch_Click" Text="Search" CssClass="btn-raised" />
                                </div>
                                <div class="col-sm-2" align="right">
                                    <asp:Button runat="server" ID="btnGridSearchClear" OnClick="btnGridSearchClear_Click" Text="Clear" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvPreviousForm" runat="server" OnRowCommand="gvPreviousForm_RowCommand" OnPageIndexChanging="gvPreviousForm_OnPageIndexChanging" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="CandidateId" HeaderText="CandidateId" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="ClassId" HeaderText="FeesId" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="FeesId" HeaderText="FeesId" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="FormNo" HeaderText="Form No" />
                                        <asp:BoundField DataField="CandidateName" HeaderText="Student Name" />
                                        <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                        <asp:BoundField DataField="AdmissionYear" HeaderText="Year" />
                                        <asp:BoundField DataField="ClassName" HeaderText="Class Name" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" />
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
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
