<%@ Page Title="Collection Entry | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CollectionEntry.aspx.cs" Inherits="SchoolManagement.CollectionEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-lg-6">

                    <%-- Student Information --%>
                    <div runat="server" id="divStudentInfo">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Search Criteria - Student</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Code
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtStudentCode" placeholder="Student Code" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdScholarshipId" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnLoadStudent" OnClick="btnLoadStudent_Click" Text="Load" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Name</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtStudentName" placeholder="Student Name" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Bangla Name</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtBanglaName" placeholder="Bangla Name" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Class</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtClass" placeholder="Class" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Roll No.</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtRollNo" placeholder="Roll No." Enabled="false" CssClass="TextBoxStyle"  Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Section</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtSection" placeholder="Section" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Contact</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtContact" placeholder="Contact Number" Enabled="false" CssClass="TextBoxStyle"  Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnLoad" Text="Load Fees" OnClick="btnLoad_Click" class="btn btn-raised btn-accent" />
                                <asp:Button runat="server" ID="btnClear" class="btn btn-raised btn-default-dark" Text="Clear" OnClick="btnClear_Click" />
                                <asp:Button runat="server" ID="btnCollectionList" Text="Collection List" OnClick="btnCollectionList_Click" class="btn btn-raised btn-info" />
                            </div>
                        </div>
                    </div>

                    <%-- Unpaid Fees List --%>
                    <div runat="server" id="divUnpaid">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Unpaid Fees List</header>
                        </div>
                        <div class="card-body style-default-bright" style="overflow: auto;">
                            <asp:GridView ID="gvUnpaidFees" runat="server" Width="100%" Class="NewGridDesingBody"
                                RowStyle-Wrap="false" EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="cbSelect" OnCheckedChanged="cbSelect_CheckedChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ScheduleId" HeaderText="SL." HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn"/>
                                    <asp:BoundField DataField="Serial" HeaderText="SL." HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="FeesDetails" HeaderText="Fees Details" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount (Tk.)" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="WaiverAmount" HeaderText="Waiver Amount" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="PayableAmount" HeaderText="Payable (Tk.)" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="AccountID" HeaderText="ID"  HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn"/>
                                </Columns>
                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle CssClass="NewGridDesign" />
                            </asp:GridView>
                        </div>
                    </div>

                    <%-- Collected Fees List --%>
                    <div runat="server" id="divPaid">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Paid Fees List</header>
                        </div>
                        <div class="card-body style-default-bright" style="overflow: auto;">
                            <asp:GridView ID="gvPaidFees" runat="server" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false"
                                EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="CollectionCode" HeaderText="Code" />
                                    <asp:BoundField DataField="EntryDate" HeaderText="Date" />
                                    <asp:BoundField DataField="Serial" HeaderText="SL." HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="FeesTitle" HeaderText="Title" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount"  ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="WaiverAmount" HeaderText="Waiver(Tk)"  ItemStyle-HorizontalAlign="Right"/>
                                    <asp:BoundField DataField="PayableAmount" HeaderText="Paid Amount"  ItemStyle-HorizontalAlign="Right"/>
                                </Columns>
                                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle CssClass="NewGridDesign" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">

                    <%-- Selected Fees List for Collection --%>
                    <div runat="server" id="divFeesCollection">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Fees Collection</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-3">Collection Code</div>
                                <div class="col-sm-3">
                                    <asp:HiddenField runat="server" ID="hdCollectionId" />
                                    <asp:TextBox runat="server" ID="txtCollectionCode" Text="" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">Collection Date</div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtCollectionDate" Text="" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="min-height: 250px; margin-top: 10px;overflow: auto;">
                                <asp:GridView runat="server" ID="gvSelectedFees" OnRowCommand="gvSelectedFees_RowCommand" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false" EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="CollectionMasterId" HeaderText="MasterId" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="CollectionDetailsId" HeaderText="DetailsId" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="ScheduleId" HeaderText="ScheduleId" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="FeesTitle" HeaderText="Fees Title" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="WaiverAmount" HeaderText="Waiver (Tk.)" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="PaidAmount" HeaderText="Paid Amount" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="lbtnDelete" CommandName="DeletePending" ImageUrl="~/img/delete.png" Width="20px" Height="20px"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="AccountID" HeaderText="ID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn"/>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle CssClass="NewGridDesign" />
                                </asp:GridView>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-9" style="text-align: right;">Total Amount</div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtTotalAmount" Text="0" Enabled="false" CssClass="TextBoxStyle" Style="text-align:right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-9" style="text-align: right;">Total Discount</div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtTotalDiscount" Text="0" Enabled="false" CssClass="TextBoxStyle" Style="text-align:right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-9" style="text-align: right;">Total Paid Amount</div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtTotalPaidAmount" Text="0" Enabled="false" CssClass="TextBoxStyle" Style="text-align:right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnCollectionSubmit" OnClick="btnCollectionSubmit_Click" Text="Submit Collection" class="btn btn-raised btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
