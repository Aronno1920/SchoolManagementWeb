<%@ Page Title="Dashboard | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DashboardAccountant.aspx.cs" Inherits="SchoolManagement.DashboardAccountant" %>

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
            <div class="col-sm-12">
                <div class="col-sm-3">
                    <div class="card">
                        <div class="alert alert-callout alert-success no-margin" style="background: linear-gradient(to right, #80ff80 , #ffffff);">
                            <strong class="text-xl">TK.
                                        <asp:Label ID="lblSalesMonth" runat="server">0.00</asp:Label></strong><br>
                            <a href="#"><span style="color: darkblue; font-weight: bold; font-size: 15px">Total Income (Today)</span></a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="card">
                        <div class="alert alert-callout alert-warning no-margin" style="background: linear-gradient(to right, #ffc6b3 , #ffece6);">
                            <strong class="text-xl">TK.
                                        <asp:Label ID="lblPaymentsMonth" runat="server">0.00</asp:Label></strong><br>
                            <a href="#"><span style="color: darkred; font-weight: bold; font-size: 15px">Total Expense (Today)</span></a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="card">
                        <div class="alert alert-callout alert-info no-margin" style="background: linear-gradient(to right, #66b3ff , #ffffff);">
                            <strong class="text-xl">TK.
                                    <asp:Label ID="lblStockOutMonth" runat="server">0</asp:Label></strong><br>
                            <a href="#"><span style="color: #09116d; font-weight: bold; font-size: 15px">Total Income (Month)</span></a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="card">
                        <div class="alert alert-callout alert-danger no-margin" style="background: linear-gradient(to right, #b3b3ff, #ffffff);">
                            <strong class="text-xl">TK.
                                    <asp:Label ID="lblStockInMonth" runat="server">0</asp:Label></strong><br>
                            <a href="#"><span style="color: #09116d; font-weight: bold; font-size: 15px">Total Expense (Month)</span></a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="col-sm-4">
                    <div class="card" style="min-height: 350px">
                        <div class="col-sm-12" style="background-color: #10935f; color: white; text-align: center; font-weight: bold">Class Wise Action Student</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 2px; overflow: auto;">
                            <asp:GridView ID="gvWorkshopVehicle" Class="DashBoard" runat="server" Width="100%" RowStyle-Wrap="true" GridLines="None"
                                EmptyDataText="No data found for selected criteria." PageSize="10" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" ShowFooter="False">
                                <Columns>
                                    <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" ItemStyle-Width="80%" />
                                    <asp:BoundField DataField="TotalVehicle" HeaderText="Total Vehicle" ItemStyle-Width="20%" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#e1fff5" />
                                <HeaderStyle BackColor="#cadbdb" ForeColor="White" Font-Size="12px"></HeaderStyle>
                                <RowStyle Wrap="true" Font-Size="12px" Height="16px" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="card" style="min-height: 350px">
                        <div class="col-sm-12" style="background-color: #500451; color: white; text-align: center; font-weight: bold">Class Wise Present/Absent Student</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 2px; overflow: auto;">
                            <asp:GridView ID="gvServiceWiseVehicle" Class="DashBoard" runat="server" Width="100%" RowStyle-Wrap="true" GridLines="None"
                                EmptyDataText="No data found for selected criteria." PageSize="5" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" ShowFooter="False">
                                <Columns>
                                    <asp:BoundField DataField="ServiceTask" HeaderText="Service" ItemStyle-Width="80%" />
                                    <asp:BoundField DataField="TotalVehicle" HeaderText="Total Vehicle" ItemStyle-Width="20%" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#e1fff5" />
                                <HeaderStyle BackColor="#cadbdb" ForeColor="White" Font-Size="12px"></HeaderStyle>
                                <RowStyle Wrap="true" Font-Size="12px" Height="16px" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="card" style="min-height: 350px">
                        <div class="col-sm-12" style="background-color: #eb9800; color: white; text-align: center; font-weight: bold">Day Wise Income/Expense</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 2px; overflow: auto;">
                            <asp:GridView ID="gvCompanyWise" runat="server" Width="100%" RowStyle-Wrap="true" GridLines="Both" EmptyDataText="No data found for selected criteria."
                                PageSize="5" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company" ItemStyle-Width="80%" />
                                    <asp:BoundField DataField="TotalVehicle" HeaderText="Total Vehicle" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#e1fff5" />
                                <HeaderStyle BackColor="#feebdf" ForeColor="White" Font-Size="12px"></HeaderStyle>
                                <RowStyle Wrap="true" Font-Size="12px" Height="16px" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="col-sm-4">
                    <div class="card" style="min-height: 350px">
                        <div class="col-sm-12" style="background-color: #0c5f9d; color: white; text-align: center; font-weight: bold">Accidental Vehicle List</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 10px; overflow: auto;">
                            <asp:GridView ID="gvBCustomer" Class="DashBoard" runat="server" Width="100%" RowStyle-Wrap="true" GridLines="None"
                                EmptyDataText="No data found for selected criteria." PageSize="5" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" ShowFooter="true">
                                <Columns>
                                    <asp:BoundField DataField="CtmCompanyName" HeaderText="Customer Name" ItemStyle-Width="60%" />
                                    <asp:BoundField DataField="SALES" HeaderText="Sales" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Payment" HeaderText="Payment" ItemStyle-Width="20%" />
                                </Columns>
                                <AlternatingRowStyle BackColor="#e1fff5" />
                                <HeaderStyle BackColor="#cadbdb" ForeColor="White" Font-Size="12px"></HeaderStyle>
                                <RowStyle Wrap="true" Font-Size="12px" Height="16px" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="card" style="min-height: 350px">
                        <div class="col-sm-12" style="background-color: #133d19; color: white; text-align: center; font-weight: bold">Location Wise Vehicle</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 2px; overflow: auto;">
                            <asp:GridView ID="gvLocationWise" Class="DashBoard" runat="server" Width="100%" RowStyle-Wrap="true" GridLines="None" EmptyDataText="No data found for selected criteria."
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
                </div>

                <div class="col-sm-4">
                    <div class="card" style="min-height: 350px">
                        <div class="col-sm-12" style="background-color: #09116d; color: white; text-align: center; font-weight: bold">Type Wise Vehicle</div>
                        <div class="col-sm-12 tablaclass" style="margin-top: 2px; overflow: auto;">
                            <asp:GridView ID="gvVehicleType" Class="DashBoard" runat="server" Width="100%" RowStyle-Wrap="true" GridLines="None" EmptyDataText="No data found for selected criteria."
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
