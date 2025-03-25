<%@ Page Title="Candidate List | SMS" Language="C#" MasterPageFile="~/MasterPageViewList.Master" AutoEventWireup="true" CodeBehind="CandidateList.aspx.cs" Inherits="SchoolManagement.CandidateList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <%--<script type="text/javascript">
        function savevalidate() {
            if (document.getElementById("<%=txtSectionName.ClientID%>").value == "") {
                alert("Please Enter Section Name");
                document.getElementById("<%=txtSectionName.ClientID%>").focus();
                return false;
            }
        }
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SearchPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel1">
        <ContentTemplate>
            <div runat="server" id="divSearchPanel">
                <div class="card-head">
                    <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Search Criteria - Candidate</header>
                </div>
                <div class="card-body style-default-bright">
                    <div class="col-sm-12">
                        <div class="col-sm-4">
                            Session
                        </div>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlAdmissionYear" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-sm-4">
                            School
                        </div>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlPreviousSchool" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-sm-4">
                            Search By
                        </div>
                        <div class="col-sm-8">
                            <asp:TextBox runat="server" ID="txtGridSearch" CssClass="TextBoxStyle"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-12" style="padding-top: 10px; text-align: center;">
                        <asp:Button runat="server" ID="btnGridSearch" OnClick="btnGridSearch_Click" Text="Search" CssClass="btn-raised" />
                        <asp:Button runat="server" ID="btnGridSearchClear" OnClick="btnGridSearchClear_Click" Text="Clear" CssClass="btn-raised" />
                        <asp:Button runat="server" ID="btnExportToExcel" OnClick="btnExportToExcel_Click" Text="Export" CssClass="btn-raised" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div runat="server" id="divDetailsPanel">
                <div class="card-head">
                    <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Candidate List (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                </div>
                <div class="card-body style-default-bright">
                    <div class="col-sm-12" style="margin-bottom: 10px; overflow: auto;">
                        <asp:GridView ID="gvCandidate" runat="server" OnPageIndexChanging="gvCandidate_PageIndexChanging" Width="100%" Class="NewGridDesingBody" AllowPaging="True" PageSize="15"
                            RowStyle-Wrap="False" EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="Photo" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="cImage" ImageUrl='<%# Eval("ProfilePhoto") %>' Height="50px" Width="44px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CandidateId" HeaderText="Section ID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                <asp:BoundField DataField="CandidateCode" HeaderText="Code" />
                                <asp:BoundField DataField="CandidateName" HeaderText="Name" />
                                <asp:BoundField DataField="CandidateNameBangla" HeaderText="বাংলায় নাম" HeaderStyle-Font-Size="16px" ItemStyle-Font-Size="18px" />
                                <asp:BoundField DataField="FatherName" HeaderText="Father Name" />
                                <asp:BoundField DataField="MotherName" HeaderText="Mother Name" />
                                <asp:BoundField DataField="ContactNo" HeaderText="Contact" />
                                <asp:BoundField DataField="LastClass" HeaderText="Completed" />
                                <asp:BoundField DataField="LastGPA" HeaderText="GPA" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="ExamMarks" HeaderText="Marks" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="SchoolName" HeaderText="Previous School" />
                            </Columns>
                            <AlternatingRowStyle BackColor="#f3fdfa" />
                            <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle CssClass="NewGridDesign" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
