<%@ Page Title="Admission Fees Receive | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdmissionFeeReceive.aspx.cs" Inherits="SchoolManagement.AdmissionFeeReceive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-6">
                    <div runat="server" id="divSearchPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Search Criteria</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Candidate By
                                </div>
                                <div class="col-sm-7">
                                    <asp:DropDownList ID="ddlEntryBy" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnLoad" OnClick="btnLoad_Click" Text="Load" CssClass="btn-raised" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div runat="server" id="divActionPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Action Panel</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Total Receivable
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtTotalAmount" Enabled="false" CssClass="TextBoxStyle" align="right"></asp:TextBox>
                                </div>
                                <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Collect" class="btn btn-raised btn-primary" />
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

                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvCandidate" runat="server" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="CandidateId" HeaderText="Candidate Id" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="CandidateCode" HeaderText="Code" />
                                        <asp:BoundField DataField="CandidateName" HeaderText="Name" />
                                        <asp:BoundField DataField="FatherName" HeaderText="Father Name" />
                                        <asp:BoundField DataField="MotherName" HeaderText="Mother Name" />
                                        <asp:BoundField DataField="ContactNo" HeaderText="Contact" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="PaidAmount" HeaderText="Paid Amount" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f3fdfa" />
                                    <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle CssClass="NewGridDesign" />
                                </asp:GridView>
                            </div>
                            <div class="col-sm-12">
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
