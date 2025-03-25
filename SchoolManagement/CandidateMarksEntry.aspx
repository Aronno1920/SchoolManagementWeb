<%@ Page Title="Marks Entry | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CandidateMarksEntry.aspx.cs" Inherits="SchoolManagement.CandidateMarksEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <%--    <script type="text/javascript">
        function savevalidate() {
            if (document.getElementById("<%=txtSectionName.ClientID%>").value == "") {
                alert("Please Enter Section Name");
                document.getElementById("<%=txtSectionName.ClientID%>").focus();
                return false;
            }
        }
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-9">
                    <div runat="server" id="divSearchPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Search Criteria - Candidate</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-1">
                                    Session
                                </div>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="ddlAdmissionYear" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlPreviousSchool" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtGridSearch" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnGridSearch" OnClick="btnGridSearch_Click" Text="Search" CssClass="btn-raised"/>
                                    <asp:Button runat="server" ID="btnGridSearchClear" OnClick="btnGridSearchClear_Click" Text="Clear" CssClass="btn-raised"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div runat="server" id="divActionPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Action Panel</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="text-align: center;">
                                <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Save All" class="btn btn-raised btn-primary"/>
                                <asp:Button runat="server" ID="btnCandidateList" Text="Candidate List" OnClick="btnCandidateList_Click" CssClass="btn btn-raised btn-info" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divCandidateListPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Candidate List (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">

                            <div class="col-sm-12" style="overflow:auto;">
                                <asp:GridView ID="gvCandidate" runat="server" OnRowCommand="gvCandidate_RowCommand" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Photo">
                                            <ItemTemplate>
                                                <asp:Image runat="server" ID="cImage" ImageUrl='<%# Eval("ProfilePhoto") %>' Height="50px" Width="44px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CandidateId" HeaderText="Section ID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="CandidateCode" HeaderText="Code" />
                                        <asp:BoundField DataField="CandidateName" HeaderText="Candidate Name" />
                                        <asp:BoundField DataField="FatherName" HeaderText="Father Name" />
                                        <asp:BoundField DataField="MotherName" HeaderText="Mother Name" />
                                        <asp:BoundField DataField="ContactNo" HeaderText="Contact" />
                                        <asp:BoundField DataField="SchoolName" HeaderText="Previous School" />
                                        <asp:BoundField DataField="LastClass" HeaderText="Last Class" />
                                        <asp:BoundField DataField="LastGPA" HeaderText="GPA" ItemStyle-HorizontalAlign="Right" />
                                        <asp:TemplateField HeaderText="Marks" HeaderStyle-Width="65px" ItemStyle-Width="65px">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtMarks" Text='<%# Eval("ExamMarks") %>' CssClass="TextBoxStyle"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="lbtnSave" CommandName="SaveRow" ImageUrl="~/img/save.png" Width="25px" Height="25px"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
