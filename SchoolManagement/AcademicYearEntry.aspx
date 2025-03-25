<%@ Page Title="Session Entry | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AcademicYearEntry.aspx.cs" Inherits="SchoolManagement.AcademicYearEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">

    <script type="text/javascript">
        function save_validate() {
            if (document.getElementById("<%=txtSessionYear.ClientID%>").value == "") {
                alert("Please Enter Academic Year");
                document.getElementById("<%=txtSessionYear.ClientID%>").focus();
                return false;
            }
            else if (document.getElementById("<%=txtSessionTitle.ClientID%>").value == "") {
                alert("Please Enter Academic Year Title");
                document.getElementById("<%=txtSessionTitle.ClientID%>").focus();
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Session Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Year<span style="color: red"> *</span>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtSessionYear" runat="server" placeholder="Academic Year" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdYearId" />
                                </div>
                                <div class="col-sm-3">
                                    Admission Fees
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtAdmissionFees" runat="server" placeholder="Admission Fees" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Title<span style="color: red"> *</span>
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtSessionTitle" runat="server" placeholder="Session Title" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top: 10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return save_validate();" class="btn btn-raised btn-primary" />
                                <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return save_validate();" class="btn btn-raised btn-warning" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" class="btn btn-raised btn-default-dark" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Session Details (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="margin-bottom: 10px">
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="txtGridSearch" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2" align="right">
                                    <asp:Button runat="server" ID="btnGridSearch" Text="Search" CssClass="btn-raised" />
                                </div>
                                <div class="col-sm-2" align="right">
                                    <asp:Button runat="server" ID="btnGridSearchClear" Text="Clear" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12" style="overflow: auto">
                                <asp:GridView ID="gvYear" runat="server" OnRowCommand="gvYear_RowCommand" OnPageIndexChanging="gvYear_OnPageIndexChanging" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="YearId" HeaderText="Year ID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="SessionYear" HeaderText="Year" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="SessionTitle" HeaderText="Title" />
                                        <asp:BoundField DataField="AdmissionFees" HeaderText="Fees" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Entry By" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click For Edit" ImageUrl="~/img/edit.png" Height="25px" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
