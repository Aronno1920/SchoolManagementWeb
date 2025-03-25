<%@ Page Title="Session Fee Details | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SessionFeesDetails.aspx.cs" Inherits="SchoolManagement.SessionFeesDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript">
        function savevalidate() {
            if (document.getElementById("<%=ddlClass.ClientID%>").value == "-1") {
                alert("Please Select Class Name");
                document.getElementById("<%=ddlClass.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlHeadName.ClientID%>").value == "-1") {
                alert("Please Select Session Fees Head");
                document.getElementById("<%=ddlHeadName.ClientID%>").focus();
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Session Fees - Details Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Class
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlClass" runat="server" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle"></asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hdDetailsId" />
                                </div>
                                <div class="col-sm-3">
                                    Session Fee
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtSessionFee" runat="server" placeholder="Session Fee" Enabled="false" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Fees
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlHeadName" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    Rest Amount
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtRestAmount" runat="server" placeholder="Rest Amount" Enabled="false" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Amount
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtAmount" runat="server" placeholder="Fee Amount" CssClass="TextBoxStyle" Style="text-align: right"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-sm-12" style="padding-top: 10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return savevalidate();" class="btn btn-raised btn-primary" />
                                <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" class="btn btn-raised btn-warning"/>
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" class="btn btn-raised btn-default-dark" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Session Fees - Details Info (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="margin-bottom: 10px">
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlGridSearch" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" ID="txtGridSearch" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button runat="server" ID="btnGridSearch" OnClick="btnGridSearch_Click" Text="Search" CssClass="btn-raised" />
                                    <asp:Button runat="server" ID="btnGridSearchClear" Text="Clear" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12" style="overflow: auto;">
                                <asp:GridView ID="gvFeesList" runat="server" OnRowCommand="gvFeesList_RowCommand" OnPageIndexChanging="gvFeesList_PageIndexChanging" Width="100%" 
                                    Class="NewGridDesingBody" AllowPaging="true" PageSize="20" RowStyle-Wrap="false" EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="SessionDetailsId" HeaderText="SessionDetailsId" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="ClassId" HeaderText="ClassId" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn"/>
                                        <asp:BoundField DataField="SessionHeadId" HeaderText="SessionHeadId" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn"/>
                                        <asp:BoundField DataField="ClassName" HeaderText="Class Name" />
                                        <asp:BoundField DataField="SessionHead" HeaderText="Session Head" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click For Edit" ImageUrl="~/img/edit.png" Height="25px" />
                                                <asp:ImageButton runat="server" ID="ibtnRemove" CommandName="RemoveRow" OnClientClick="return confirm('Are you sure, you want to this Session Fee Head?');" ToolTip="Click For Delete" ImageUrl="~/img/delete.png" Height="25px" />
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
