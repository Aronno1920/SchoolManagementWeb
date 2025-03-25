<%@ Page Title="Fees Schedule Entry | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FeesScheduleEntry.aspx.cs" Inherits="SchoolManagement.FeesScheduleEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript">
        function savevalidate() {
            if (document.getElementById("<%=ddlClass.ClientID%>").value == "-1") {
                alert("Please Select Class Name");
                document.getElementById("<%=ddlClass.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlFeesName.ClientID%>").value == "-1") {
                alert("Please Select Fees Name");
                document.getElementById("<%=ddlFeesName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtAmount.ClientID%>").value == "") {
                alert("Please Enter Amount");
                document.getElementById("<%=txtAmount.ClientID%>").focus();
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Fees Schedule Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Class Name
                                </div>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlClass" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Fee Name
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlFeesName" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Amount (BDT)
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtAmount" runat="server" placeholder="Fee Amount" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Month Name
                                </div>
                                <div class="col-sm-9">
                                    <asp:CheckBoxList runat="server" ID="cbMonthNo" RepeatColumns="4" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                </div>
                            </div>

                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSave" class="btn btn-raised btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return savevalidate();" />
                                <asp:Button runat="server" ID="btnClear" class="btn btn-raised btn-default-dark" Text="Clear" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Class Fees Details (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
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
                                    <asp:Button runat="server" ID="btnGridSearch" OnClick="btnGridSearch_Click" Text="Search" CssClass="btn-raised"/>
                                    </div><div class="col-sm-2" align="right">
                                    <asp:Button runat="server" ID="btnGridSearchClear" Text="Clear" CssClass="btn-raised"/>
                                </div>
                            </div>
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvFeesList" runat="server" OnRowCommand="gvFeesList_RowCommand" OnPageIndexChanging="gvFeesList_PageIndexChanging" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="20" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ScheduleId" HeaderText="ScheduleId" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="ClassId" HeaderText="ClassId" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="MonthNo" HeaderText="MonthNo" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="ClassName" HeaderText="Class Name" />
                                        <asp:BoundField DataField="FeesDetails" HeaderText="Fees Details" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnRemove" CommandName="RemoveRow" OnClientClick="return confirm('Are you sure, you want to this Fee Schedule?');" ToolTip="Click For Delete" ImageUrl="~/img/delete.png" Height="25px" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
