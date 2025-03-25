<%@ Page Title="Collection History | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CollectionHistory.aspx.cs" Inherits="SchoolManagement.CollectionHistory" %>

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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Search Criteria</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Code
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtStudentCode" placeholder="Student Code" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Class
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlClass" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Section
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlSection" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    From Date
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtFromDate" Width="85%" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                    <asp:Image runat="server" ID="imgFromDate" ImageUrl="~/img/calender.png" Height="25px" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" PopupButtonID="imgFromDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></asp:CalendarExtender>
                                </div>
                                <div class="col-sm-2">
                                    To Date
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtToDate" Width="85%" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                    <asp:Image runat="server" ID="imgToDate" ImageUrl="~/img/calender.png" Height="25px" />
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" PopupButtonID="imgToDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-12" style="text-align: center; margin-top: 20px;">
                                <asp:Button runat="server" ID="btnLoadSummary" OnClick="btnLoadSummary_Click" Text="Load Collection" class="btn btn-raised btn-accent" />
                                <asp:Button runat="server" ID="btnPrintSummary" Text="Print Summary" class="btn btn-raised btn-info" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" class="btn btn-raised btn-default-dark" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="col-sm-6" style="min-height:150px">
                    <div runat="server" id="divCollectionList">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Collection List (<asp:Label runat="server" ID="lblTotalRecord">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvCollectionList" runat="server" OnRowCommand="gvCollectionList_RowCommand" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="CollectionMasterId" HeaderText="Id" />
                                        <asp:BoundField DataField="CollectionCode" HeaderText="Code" />
                                        <asp:BoundField DataField="CollectionDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="StudentCode" HeaderText="S. Code" />
                                        <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                        <asp:BoundField DataField="ClassName" HeaderText="Class" />
                                        <asp:BoundField DataField="SectionName" HeaderText="Section" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnDetails" CommandName="DetailsRow" ToolTip="Click here for Details" ImageUrl="~/img/details.png" Height="25px" />
                                                <asp:ImageButton runat="server" ID="ibtnPrint" CommandName="PrintRow" ToolTip="Click here for Print" ImageUrl="~/img/print.png" Height="25px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle CssClass="NewGridDesign" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6">
                    <div runat="server" id="divCollectionDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Collection Details <asp:Label runat="server" ID="lblCollectionCode"></asp:Label></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvCollectionDetails" runat="server" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="AccountID" HeaderText="AccountID" />
                                        <asp:BoundField DataField="FeesTitle" HeaderText="Fees Title"/>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount"  ItemStyle-HorizontalAlign="Right"/>
                                        <asp:BoundField DataField="WaiverAmount" HeaderText="Waiver Amount" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="PaidAmount" HeaderText="Paid Amount"  ItemStyle-HorizontalAlign="Right"/>
                                    </Columns>
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
