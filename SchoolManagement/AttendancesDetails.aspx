<%@ Page Title="Attendance Details | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AttendancesDetails.aspx.cs" Inherits="SchoolManagement.AttendancesDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript">
        function load_validate() {
            if (document.getElementById("<%=txtStudentCode.ClientID%>").value == "") {
                alert("Please Select Student Code");
                document.getElementById("<%=txtStudentCode.ClientID%>").focus();
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Search Criteria - Student</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Code
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtStudentCode" placeholder="Student Code" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdStudentId" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnLoadStudent" OnClick="btnLoadStudent_Click" Text="Load" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Name</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtStudentName" placeholder="Student's Name" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Bangla Name</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtBanglaName" placeholder="Student's Bangla Name" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="txtContact" placeholder="Contact Number" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Start Date
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtStartDate" runat="server" Width="80%" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                     <asp:Image runat="server" ID="imgStartDate" ImageUrl="~/img/calender.png" Height="25px" />
                                    <asp:CalendarExtender ID="CalendarExtenderStart" runat="server" TargetControlID="txtStartDate" PopupButtonID="imgStartDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></asp:CalendarExtender>
                                </div>

                                <div class="col-sm-2">
                                    End Date
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtEndDate" runat="server" Width="80%" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                     <asp:Image runat="server" ID="imgEndDate" ImageUrl="~/img/calender.png" Height="25px" />
                                    <asp:CalendarExtender ID="CalendarExtenderEnd" runat="server" TargetControlID="txtEndDate" PopupButtonID="imgEndDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" OnClientClick="return load_validate();" class="btn btn-raised btn-accent"/>
                                <asp:Button runat="server" ID="btnClear" class="btn btn-raised btn-default-dark" Text="Clear" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Day Wise Attendance</header>
                        </div>
                        <div class="card-body style-default-bright" style="min-height:350px">
                            <div class="col-sm-12">
                                <div class="col-sm-2">Present</div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtPresent" Text="0" Enabled="false" CssClass="TextBoxStyle" style="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Leave</div>
                                <div class="col-sm-2">
                                     <asp:TextBox runat="server" ID="txtLeave" Text="0" Enabled="false" CssClass="TextBoxStyle" style="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Absent</div>
                                <div class="col-sm-2">
                                     <asp:TextBox runat="server" ID="txtAbsent" Text="0" Enabled="false" CssClass="TextBoxStyle" style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Weekend</div>
                                <div class="col-sm-2">
                                     <asp:TextBox runat="server" ID="txtWeekend" Text="0" Enabled="false" CssClass="TextBoxStyle" style="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Holiday</div>
                                <div class="col-sm-2">
                                     <asp:TextBox runat="server" ID="txtHoliday" Text="0" Enabled="false" CssClass="TextBoxStyle" style="text-align: right"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Total</div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtTotalDays" Text="0" Enabled="false" CssClass="TextBoxStyle" style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="margin-top:15px; overflow: auto;">
                            <asp:GridView ID="gvAttendance" runat="server" OnRowDataBound="gvAttendance_RowDataBound" Width="100%" Class="NewGridDesingBody" RowStyle-Wrap="false" EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="s_sl" HeaderText="SL." />
                                    <asp:BoundField DataField="s_date" HeaderText="Date" />
                                    <asp:BoundField DataField="s_datename" HeaderText="Day" />
                                    <asp:BoundField DataField="Type" HeaderText="Type" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                </Columns>
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
