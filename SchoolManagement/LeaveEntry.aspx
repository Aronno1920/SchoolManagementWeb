<%@ Page Title="Leave Entry | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LeaveEntry.aspx.cs" Inherits="SchoolManagement.LeaveEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript">
        function savevalidate() {
            if (document.getElementById("<%=txtStudentCode.ClientID%>").value == "") {
                alert("Please Select Student Code");
                document.getElementById("<%=txtStudentCode.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlReasonType.ClientID%>").value == "0") {
                alert("Please Select Leave Reason");
                document.getElementById("<%=ddlReasonType.ClientID%>").focus();
                return false;
            }
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Leave Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Code
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtStudentCode" placeholder="Student Code" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdStudentId" />
                                    <asp:HiddenField runat="server" ID="hdLeaveId" />
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
                                    <asp:TextBox runat="server" ID="txtRollNo" placeholder="Roll No." Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Section</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtSection" placeholder="Section" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Contact</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtContact" placeholder="Contact No." Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="margin-top: 10px">
                                <div class="col-sm-2">
                                    Reason
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlReasonType" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
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
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return savevalidate();" class="btn btn-raised btn-primary" />
                                <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return savevalidate();" class="btn btn-raised btn-warning" />
                                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" class="btn btn-raised btn-default-dark" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Leave Application - Awaiting for Approval (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright" style="overflow: auto;">
                            <asp:GridView ID="gvLeave" runat="server" OnRowCommand="gvLeave_RowCommand" Width="100%" Class="NewGridDesingBody" AllowPaging="True" PageSize="15" RowStyle-Wrap="false"
                                EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Photo">
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="cImage" ImageUrl='<%# Eval("ProfilePhoto") %>' Height="50px" Width="44px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LeaveId" HeaderText="Brand ID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                    <asp:BoundField DataField="StudentCode" HeaderText="Code" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="RollNo" HeaderText="Roll" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="StudentName" HeaderText="Name" />
                                    <asp:BoundField DataField="ClassName" HeaderText="Class" />
                                    <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                    <asp:BoundField DataField="DateFrom" HeaderText="Date From" />
                                    <asp:BoundField DataField="DateTo" HeaderText="Date To" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ImageUrl="~/img/edit.png" Height="25px" />
                                            <asp:ImageButton runat="server" ID="ibtnRemove" CommandName="RemoveRow" ImageUrl="~/img/delete.png" Height="25px" />
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
