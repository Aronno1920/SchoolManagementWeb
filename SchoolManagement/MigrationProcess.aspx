<%@ Page Title="Migration Process | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MigrationProcess.aspx.cs" Inherits="SchoolManagement.MigrationProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript">
        function save_validate() {
            if (document.getElementById("<%=ddlClass.ClientID%>").value == "0") {
                alert('Please Select Class');
                document.getElementById("<%=ddlClass.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlSection.ClientID%>").value == "0") {
                alert('Please Select Section');
                document.getElementById("<%=ddlSection.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtCapacity.ClientID%>").value == "") {
                alert('Please Enter Student Capacity');
                document.getElementById("<%=txtCapacity.ClientID%>").focus();
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
                    <div runat="server" id="divSearchPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Search Criteria - Candidate</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Session
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlAdmissionYear" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Candidate ID
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtCandidateId" placeholder="Candidate Code" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Marks Range</div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtMarksFrom" CssClass="TextBoxStyle" placeholder="From" Style="text-align:right"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtMarksTo" CssClass="TextBoxStyle" placeholder="To" Style="text-align:right"></asp:TextBox>
                                </div>
                                <div class="col-sm-2" style="visibility:hidden">
                                    Marks Order
                                </div>
                                <div class="col-sm-4" style="visibility:hidden">
                                    <asp:DropDownList runat="server" ID="ddlMarksOrder" Enabled="false" CssClass="DropDownListStyle">
                                        <asp:ListItem Text="Descending" Value="DESC"></asp:ListItem>
                                        <asp:ListItem Text="Ascending" Value="ASC"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnLoad" OnClick="btnLoad_Click" Text="Load Candidate" class="btn btn-raised btn-accent"/>
                                <asp:Button runat="server" ID="btnCandidateList" Text="Candidate List" OnClick="btnCandidateList_Click" CssClass="btn btn-raised btn-info" />
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-sm-6">
                    <div runat="server" id="divAcademicPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Academic Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Class<span style="color: red"> *</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlClass" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle" />
                                </div>
                                <div class="col-sm-2">
                                    Section<span style="color: red"> *</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlSection" CssClass="DropDownListStyle" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Capacity<span style="color: red"> *</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtCapacity" CssClass="TextBoxStyle" placeholder="Total Student Capacity" Style="text-align:right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnProcess" Text="Process" OnClick="btnProcess_Click" OnClientClick="return save_validate" class="btn btn-raised btn-primary"/>
                                <asp:Button runat="server" ID="btnClear" class="btn btn-raised btn-default-dark" Text="Clear" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divCandidateListPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Candidate List (<asp:Label runat="server" ID="lblRowCount" Text="0"></asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright" style="overflow: auto;">
                            <div class="col-sm-12">
                                <asp:GridView ID="gvCandidate" runat="server" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL.">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Photo">
                                            <ItemTemplate>
                                                <asp:Image runat="server" ID="cImage" ImageUrl='<%# Eval("ProfilePhoto") %>' Height="50px" Width="44px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CandidateId" HeaderText="CandidateId" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="CandidateCode" HeaderText="Code" />
                                        <asp:BoundField DataField="CandidateName" HeaderText="Candidate Name" />
                                        <asp:BoundField DataField="FatherName" HeaderText="Father Name" />
                                        <asp:BoundField DataField="MotherName" HeaderText="Mother Name" />
                                        <asp:BoundField DataField="ContactNo" HeaderText="Contact" />
                                        <asp:BoundField DataField="SchoolName" HeaderText="Previous School" />
                                        <asp:BoundField DataField="LastClass" HeaderText="Last Class" />
                                        <asp:BoundField DataField="LastGPA" HeaderText="GPA" ItemStyle-HorizontalAlign="Right"/>
                                        <asp:BoundField DataField="ExamMarks" HeaderText="Exam Marks" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f3fdfa" />
                                    <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle CssClass="NewGridDesign" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
