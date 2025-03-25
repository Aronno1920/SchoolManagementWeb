<%@ Page Title="Holiday Entry | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HolidayEntry.aspx.cs" Inherits="SchoolManagement.HolidayEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript">
        function savevalidate() {
            if (document.getElementById("<%=txtStartDate.ClientID%>").value == "") {
                alert("Please Select Date From");
                document.getElementById("<%=txtStartDate.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtEndDate.ClientID%>").value == "") {
                alert("Please Select Date To");
                document.getElementById("<%=txtEndDate.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtRemarks.ClientID%>").value == "") {
                alert("Please Enter Holiday Remarks");
                document.getElementById("<%=txtRemarks.ClientID%>").focus();
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Holiday Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Start Date
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtStartDate" runat="server" Width="80%" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:Image runat="server" ID="imgStartDate" ImageUrl="~/img/calender.png" Height="25px" />
                                    <asp:CalendarExtender ID="CalendarExtenderStart" runat="server" TargetControlID="txtStartDate" PopupButtonID="imgStartDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></asp:CalendarExtender>
                                </div>

                                <div class="col-sm-2">
                                    End Date
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtEndDate" runat="server" Width="80%" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:Image runat="server" ID="imgEndDate" ImageUrl="~/img/calender.png" Height="25px" />
                                    <asp:CalendarExtender ID="CalendarExtenderEnd" runat="server" TargetControlID="txtEndDate" PopupButtonID="imgEndDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></asp:CalendarExtender>
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Description
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="4" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdHolidayId" />
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSubmit" class="btn btn-raised btn-primary" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return savevalidate();" />
                                <asp:Button runat="server" ID="btnUpdate" class="btn btn-raised btn-warning" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return savevalidate();" />
                                <asp:Button runat="server" ID="btnClear" class="btn btn-raised btn-default-dark" Text="Clear" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Holiday Info - Awaiting for Approval (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright" style="overflow: auto;">
                            <asp:GridView ID="gvHoliday" runat="server" OnRowCommand="gvHoliday_RowCommand" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="HolidayId" HeaderText="Holiday ID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                    <asp:BoundField DataField="DateFrom" HeaderText="Date From" />
                                    <asp:BoundField DataField="DateTo" HeaderText="Date To" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click For Edit" ImageUrl="~/img/edit.png" Height="25px" />
                                            <asp:ImageButton runat="server" ID="ibtnRemove" CommandName="RemoveRow" OnClientClick="return confirm('Are you sure, you want to holiday information?');" ToolTip="Click For Delete" ImageUrl="~/img/delete.png" Height="25px" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
