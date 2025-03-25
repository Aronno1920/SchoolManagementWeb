<%@ Page Title="Dashboard | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DashboardAdmin.aspx.cs" Inherits="SchoolManagement.DashboardAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript"> 
        $(document).ready(function () {
            $('.TextAnimation').each(function (_, self) {
                jQuery({
                    Counter: 0
                }).animate({
                    Counter: $(self).text()
                }, {
                    duration: (Math.floor(Math.random() * 2500) + 1000),
                    easing: 'swing',
                    step: function () {
                        $(self).text(Math.ceil(this.Counter));
                    }
                });
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <%--Student Realted Information--%>
            <div class="col-sm-12">
                <div class="col-sm-2">
                    <div class="card">
                        <div class="alert alert-callout alert-success no-margin" style="background: linear-gradient(to right, #99ffe6 , #e6fff9);">
                            <strong class="text-xl">
                                <asp:Label ID="lblActiveStudent" runat="server" class="TextAnimation">0</asp:Label></strong><br>
                            <a href="#"><span style="color: #0c6818; font-weight: bold; font-size: 15px">Total Students</span></a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2">
                    <div class="card">
                        <div class="alert alert-callout alert-warning no-margin" style="background: linear-gradient(to right, #ffe6b3 , #fff7e6);">
                            <strong class="text-xl">
                                <asp:Label ID="lblNewStudent" runat="server" class="TextAnimation">0</asp:Label></strong><br>
                            <a href="#"><span style="color: #ff6a00; font-weight: bold; font-size: 15px">Active Students</span></a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2">
                    <div class="card">
                        <div class="alert alert-callout alert-info no-margin" style="background: linear-gradient(to right, #b3f0ff , #e6faff);">
                            <strong class="text-xl">
                                <asp:Label ID="lblTotalCandidate" runat="server" class="TextAnimation">0</asp:Label></strong><br>
                            <a href="#"><span style="color: #09116d; font-weight: bold; font-size: 15px">SSC Candidates</span></a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2">
                    <div class="card">
                        <div class="alert alert-callout alert-danger no-margin" style="background: linear-gradient(to right, #e6ccff, #fff6ff);">
                            <strong class="text-xl">
                                <asp:Label ID="lblTotalPresent" runat="server" class="TextAnimation">0</asp:Label></strong><br>
                            <a href="#"><span style="color: #09116d; font-weight: bold; font-size: 15px">Today's Presents</span></a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2">
                    <div class="card">
                        <div class="alert alert-callout alert-danger no-margin" style="background: linear-gradient(to right, #66b3ff , #ffffff);">
                            <strong class="text-xl">
                                <asp:Label ID="lblTotalAbsent" runat="server" class="TextAnimation">0</asp:Label></strong><br>
                            <a href="#"><span style="color: #09116d; font-weight: bold; font-size: 15px">Today's Absents</span></a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2">
                    <div class="card">
                        <div class="alert alert-callout alert-danger no-margin" style="background: linear-gradient(to right, #80ff80 , #ffffff);">
                            <strong class="text-xl">
                                <asp:Label ID="lblSSCCandidate" runat="server" class="TextAnimation">0</asp:Label></strong><br>
                            <a href="#"><span style="color: #09116d; font-weight: bold; font-size: 15px">Active Teachers</span></a>
                        </div>
                    </div>
                </div>
            </div>

            <%--Income / Expense Realated Information--%>
            <div class="col-sm-12">
                <div class="col-sm-3">
                    <div class="card">
                        <div class="alert alert-callout alert-success no-margin" style="background: linear-gradient(to right, #80ff80 , #ffffff);">
                            <strong class="text-xl">TK.
                                        <asp:Label ID="lblDailyIncome" runat="server" class="TextAnimation">0</asp:Label></strong><br>
                            <a href="#"><span style="color: darkblue; font-weight: bold; font-size: 15px">Today's Income</span></a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="card">
                        <div class="alert alert-callout alert-warning no-margin" style="background: linear-gradient(to right, #ffc6b3 , #ffece6);">
                            <strong class="text-xl">TK.
                                        <asp:Label ID="lblDailyExpense" runat="server" class="TextAnimation">0.00</asp:Label></strong><br>
                            <a href="#"><span style="color: darkred; font-weight: bold; font-size: 15px">Today's Expense</span></a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="card">
                        <div class="alert alert-callout alert-info no-margin" style="background: linear-gradient(to right, #66b3ff , #ffffff);">
                            <strong class="text-xl">TK.
                                    <asp:Label ID="lblMonthlyIncome" runat="server" class="TextAnimation">0</asp:Label></strong><br>
                            <a href="#"><span style="color: #09116d; font-weight: bold; font-size: 15px">Monthly Income</span></a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="card">
                        <div class="alert alert-callout alert-danger no-margin" style="background: linear-gradient(to right, #b3b3ff, #ffffff);">
                            <strong class="text-xl">TK.
                                    <asp:Label ID="lblMonthlyExpense" runat="server" class="TextAnimation">0</asp:Label></strong><br>
                            <a href="#"><span style="color: #09116d; font-weight: bold; font-size: 15px">Monthly Expense</span></a>
                        </div>
                    </div>
                </div>

            </div>

            <%--Income Expense Chart View--%>
            <%-- <div class="col-sm-12">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="col-sm-12" style="height: 30px; background-color: #0c5f9d; font-size: 16px; color: white; text-align: center; font-weight: bold">Monthly Income Expense</div>
                        <div class="col-sm-12" style="margin-top: 2px;">

                            <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
                            <script type="text/javascript">
                                google.charts.load('current', { 'packages': ['corechart'] });
                                google.charts.setOnLoadCallback(drawChart);

                                function drawChart() {
                                    var data = google.visualization.arrayToDataTable([
                                        ['Month', 'Income', 'Expenses'],
                                        ['Jan', 3000, 1170],
                                        ['Feb', 1170, 1660],
                                        ['March', 1660, 2130],
                                        ['April', 2130, 4000],
                                        ['May', 4000, 3570],
                                        ['Jun', 3570, 2660],
                                        ['July', 2660, 1930],
                                        ['Aug', 1930, 4000],
                                        ['Sep', 4000, 3170],
                                        ['Oct', 3170, 3660],
                                        ['Nov', 3660, 0],
                                        ['Dec', 0, 3000]
                                    ]);

                                    var options = {
                                        series: {
                                            1: { color: '#43459d' },
                                            2: { color: '#e2431e' },
                                        },
                                        curveType: 'function',
                                        legend: { position: 'bottom' }
                                    };

                                    var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));
                                    chart.draw(data, options);
                                }
                            </script>
                            <div id="curve_chart"></div>
                        </div>
                    </div>
                </div>
            </div>--%>

            <div class="col-sm-12">
                <%--Class Wise Active Student--%>
                <div class="col-sm-4">
                    <div class="card" style="min-height: 250px">
                        <div class="col-sm-12" style="background-color: #10935f; color: white; text-align: center; font-weight: bold">Class Wise Active Student</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 2px; overflow: auto;">
                            <asp:GridView ID="gvActiveStudent" Class="NewGridDesingBody" runat="server" Width="100%" RowStyle-Wrap="true" GridLines="None"
                                EmptyDataText="No data found for selected criteria." PageSize="10" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="ClassName" HeaderText="Class Name" ItemStyle-Width="70%" />
                                    <asp:BoundField DataField="TotalStudent" HeaderText="Total Student" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#e1fff5" />
                                <HeaderStyle BackColor="#cadbdb" ForeColor="White" Font-Size="12px"></HeaderStyle>
                                <RowStyle Wrap="true" Font-Size="12px" Height="16px" />
                            </asp:GridView>
                        </div>
                    </div>
                    <!--end .card -->
                </div>

                <%--Class Wise Present/Absent Student--%>
                <div class="col-sm-4">
                    <div class="card" style="min-height: 250px">
                        <div class="col-sm-12" style="background-color: #500451; color: white; text-align: center; font-weight: bold">Class Wise Present/Absent Student</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 2px; overflow: auto;">
                            <asp:GridView ID="gvTotalPresentAbsent" Class="NewGridDesingBody" runat="server" Width="100%" RowStyle-Wrap="true" GridLines="None"
                                EmptyDataText="No data found for selected criteria." PageSize="10" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="ClassName" HeaderText="Class Name" ItemStyle-Width="70%" />
                                    <asp:BoundField DataField="TotalPresent" HeaderText="Total Student" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#e1fff5" />
                                <HeaderStyle BackColor="#cadbdb" ForeColor="White" Font-Size="12px"></HeaderStyle>
                                <RowStyle Wrap="true" Font-Size="12px" Height="16px" />
                            </asp:GridView>
                        </div>

                    </div>
                    <!--end .card -->
                </div>

                <%--Day Wise Income/Expense Student--%>
                <div class="col-sm-4">
                    <div class="card" style="min-height: 250px">
                        <div class="col-sm-12" style="background-color: #eb9800; color: white; text-align: center; font-weight: bold">Day Wise Income/Expense</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 2px; overflow: auto;">

                            <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
                            <script type="text/javascript">
                                google.charts.load('current', { 'packages': ['corechart'] });
                                google.charts.setOnLoadCallback(drawChart);

                                function drawChart() {
                                    var data = google.visualization.arrayToDataTable([
                                        ['Month', 'Income', 'Expenses'],
                                        ['Sat', 3000, 1170],
                                        ['Sun', 1170, 1660],
                                        ['Mon', 1660, 2130],
                                        ['Tue', 2130, 4000],
                                        ['Thu', 4000, 3570],
                                        ['Wed', 3570, 2660]
                                    ]);

                                    var options = {
                                        series: {
                                            1: { color: '#FF0000' },
                                            2: { color: '#008000' },
                                        },
                                        curveType: 'function',
                                        legend: { position: 'bottom' }
                                    };

                                    var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));
                                    chart.draw(data, options);
                                }
                            </script>
                            <div id="curve_chart"></div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <%--Class Wise Action Student--%>
                <div class="col-sm-4">
                    <div class="card" style="min-height: 250px">
                        <div class="col-sm-12" style="background-color: #0c5f9d; color: white; text-align: center; font-weight: bold">Current Balance</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 2px; overflow: auto;">

                            <asp:GridView ID="gvCurrentBalance" Class="NewGridDesingBody" runat="server" Width="100%" RowStyle-Wrap="true" GridLines="None"
                                EmptyDataText="No data found for selected criteria." PageSize="5" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="AccountName" HeaderText="Account Name" ItemStyle-Width="70%" />
                                    <asp:BoundField DataField="TotalAmount" HeaderText="Sales" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#e1fff5" />
                                <HeaderStyle BackColor="#cadbdb" ForeColor="White" Font-Size="12px"></HeaderStyle>
                                <RowStyle Wrap="true" Font-Size="12px" Height="16px" />
                            </asp:GridView>
                        </div>
                    </div>
                    <!--end .card -->
                </div>

                <%--Class Wise Action Student--%>
                <div class="col-sm-4">
                    <div class="card" style="min-height: 250px">
                        <div class="col-sm-12" style="background-color: #133d19; color: white; text-align: center; font-weight: bold">Location Wise Vehicle</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 2px; overflow: auto;">
                            <asp:GridView ID="gvLocationWise" Class="NewGridDesingBody" runat="server" Width="100%" RowStyle-Wrap="true" GridLines="None" EmptyDataText="No data found for selected criteria."
                                PageSize="5" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="LocationName" HeaderText="Location" ItemStyle-Width="80%" />
                                    <asp:BoundField DataField="TotalVehicle" HeaderText="Total Vehicle" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#e1fff5" />
                                <HeaderStyle BackColor="#cadbdb" ForeColor="White" Font-Size="12px"></HeaderStyle>
                                <RowStyle Wrap="true" Font-Size="12px" Height="16px" />
                            </asp:GridView>
                        </div>
                    </div>
                    <!--end .card -->
                </div>

                <%--Class Wise Action Student--%>
                <div class="col-sm-4">
                    <div class="card" style="min-height: 250px">
                        <div class="col-sm-12" style="background-color: #09116d; color: white; text-align: center; font-weight: bold">Type Wise Vehicle</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 2px; overflow: auto;">
                            <asp:GridView ID="gvVehicleType" Class="NewGridDesingBody" runat="server" Width="100%" RowStyle-Wrap="true" GridLines="None" EmptyDataText="No data found for selected criteria."
                                PageSize="5" ShowHeaderWhenEmpty="True" ShowHeader="true" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="TypeName" HeaderText="Type Name" ItemStyle-Width="80%" />
                                    <asp:BoundField DataField="TotalVehicle" HeaderText="Total Vehicle" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#e1fff5" />
                                <HeaderStyle BackColor="#cadbdb" ForeColor="White" Font-Size="12px"></HeaderStyle>
                                <RowStyle Wrap="true" Font-Size="12px" Height="16px" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
