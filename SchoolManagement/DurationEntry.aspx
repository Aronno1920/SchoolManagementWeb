<%@ Page Title="Class Section Information | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DurationEntry.aspx.cs" Inherits="SchoolManagement.DurationEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript">
        function savevalidate() {
            if (document.getElementById("<%=ddlOrder.ClientID%>").value == "-1") {
                alert("Please Enter Section Name");
                document.getElementById("<%=ddlOrder.ClientID%>").focus();
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Class Duration Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Class Duration
                                    <span style="color: red">*</span>
                                </div>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlOrder" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    Start Time
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlStartHour" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlStartMinute" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlStartAMPM" runat="server" CssClass="DropDownListStyle">
                                        <asp:ListItem Text="AM" Value="am"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="pm"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    End Time
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlEndHour" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlEndMinute" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlEndAMPM" runat="server" CssClass="DropDownListStyle">
                                        <asp:ListItem Text="AM" Value="am"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="pm"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnUpdate" class="btn btn-raised btn-warning" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return savevalidate();" />
                                <asp:Button runat="server" ID="btnClear" class="btn btn-raised btn-default-dark" Text="Clear" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Class Duration Details (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="margin-bottom: 10px; overflow: auto;">
                                <asp:GridView ID="gvDuration" runat="server" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="DurationId" HeaderText="ID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn"/>
                                        <asp:BoundField DataField="DurationName" HeaderText="Duration Name" />
                                        <asp:BoundField DataField="DurationNameBangla" HeaderText="Duration (BN)" />
                                        <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
                                        <asp:BoundField DataField="EndTime" HeaderText="End Time" />
                                        <asp:BoundField DataField="TotalDuration" HeaderText="Class Duration" />
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
