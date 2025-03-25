<%@ Page Title="Queryboard | VMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Queryboard.aspx.cs" Inherits="SchoolManagement.Queryboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-3">
                    <div runat="server" id="divButton">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-automobile"></i>Query Type</header>
                        </div>
                        <div class="style-default-bright">
                            <div class="nav nav-pills nav-stacked">
                                <asp:Button runat="server" ID="btnDocument" Width="100%" Style="text-align: left;" Text="Documents" OnClick="NavigateToDesireDiv" />
                                <asp:Button runat="server" ID="btnGeneral" Width="100%" Style="text-align: left;" Text="General Service" OnClick="NavigateToDesireDiv" />
                                <asp:Button runat="server" ID="btnBill" Width="100%" Style="text-align: left;" Text="Vendor Bill" OnClick="NavigateToDesireDiv" />
                                <asp:Button runat="server" ID="btnOthers" Width="100%" Style="text-align: left;" Text="Others Service" OnClick="NavigateToDesireDiv" />
                                <asp:Button runat="server" ID="btnParts" Width="100%" Style="text-align: left;" Text="Parts Service" OnClick="NavigateToDesireDiv" />
                            </div>
                        </div>
                        <asp:HiddenField runat="server" ID="hdButtonSession" />
                    </div>
                </div>
                <div class="col-sm-9">
                    <div runat="server" id="divDocument">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-automobile"></i>Document Renewal Information</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Forecast
                                </div>
                                <div class="col-sm-4">
                                    <asp:CheckBox runat="server" ID="cbDocumentForecast" AutoPostBack="true" OnCheckedChanged="cbDocumentForecast_CheckedChanged" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Document Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlDocumentType" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Vehicle Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlDocumntVehicleType" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Report Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlDocumentReportRange" CssClass="DropDownListStyle">
                                        <asp:ListItem Text="Date Wise" Value="Day"></asp:ListItem>
                                        <asp:ListItem Text="Month Wise" Value="Month"></asp:ListItem>
                                        <asp:ListItem Text="Year Wise" Value="Year"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">Vehicle</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDocumentLicencePlate" placeholder="License Plate" runat="server" OnTextChanged="txtDocumentLicencePlate_OnTextChanged" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="SearchVehicleLicence" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtDocumentLicencePlate" FirstRowSelected="false" CompletionListCssClass="autoCompleteList" CompletionListItemCssClass="autoCompleteListItem" CompletionListHighlightedItemCssClass="autoCompleteListItemHighlight">
                                    </ajax:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <asp:Label runat="server" ID="lblDocumentFromDate" Text="Date From"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtDocumentFromDate" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:CalendarExtender ID="Calendar1" runat="server" TargetControlID="txtDocumentFromDate" PopupButtonID="txtDocumentFromDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></ajax:CalendarExtender>
                                </div>
                                <div class="col-sm-2"></div>
                                <div runat="server" id="divDocumentToDate">
                                    <div class="col-sm-2">
                                        Date To
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox runat="server" ID="txtDocumentToDate" CssClass="TextBoxStyle"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDocumentToDate" PopupButtonID="txtDocumentToDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></ajax:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top: 15px; text-align: center;">
                                <asp:Button runat="server" ID="btnDocumentView" OnClick="btnDocumentView_Click" class="btn btn-raised btn-primary" Text="View" />
                                <asp:Button runat="server" ID="btnDocumentClear" class="btn btn-raised btn-warning" Text="Clear" OnClick="btnDocumentClear_Click" />
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divGeneral">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-automobile"></i>Vehicle General Service</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Forecast
                                </div>
                                <div class="col-sm-4">
                                    <asp:CheckBox runat="server" ID="cbGeneralForecast" OnCheckedChanged="cbGeneralForecast_CheckedChanged" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Workshop
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlGeneralWorkshop" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Workshop Based
                                </div>
                                <div class="col-sm-4">
                                    <asp:CheckBox runat="server" ID="cbGeneralWorkshopBased" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    General Service
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlGeneralService" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Vehicle Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlGeneralVehicleType" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Report Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlGeneralReportRange" CssClass="DropDownListStyle">
                                        <asp:ListItem Text="Date Wise" Value="Day"></asp:ListItem>
                                        <asp:ListItem Text="Month Wise" Value="Month"></asp:ListItem>
                                        <asp:ListItem Text="Year Wise" Value="Year"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">Vehicle</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtGeneralLicencePlate" placeholder="License Plate" runat="server" OnTextChanged="txtGeneralLicencePlate_OnTextChanged" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" ServiceMethod="SearchVehicleLicence" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtGeneralLicencePlate" FirstRowSelected="false" CompletionListCssClass="autoCompleteList" CompletionListItemCssClass="autoCompleteListItem" CompletionListHighlightedItemCssClass="autoCompleteListItemHighlight">
                                    </ajax:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    <asp:Label runat="server" ID="lblGeneralFromDate" Text="Date From"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtGeneralFromDate" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtGeneralFromDate" PopupButtonID="txtGeneralFromDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></ajax:CalendarExtender>
                                </div>
                                <div class="col-sm-2"></div>
                                <div runat="server" id="divGeneralToDate">
                                    <div class="col-sm-2">
                                        Date To
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox runat="server" ID="txtGeneralToDate" CssClass="TextBoxStyle"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtGeneralToDate" PopupButtonID="txtGeneralToDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></ajax:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding: 5px; text-align: center;">
                                <asp:Button runat="server" ID="btnGeneralView" OnClick="btnGeneralView_Click" class="btn btn-raised btn-primary" Text="View" />
                                <asp:Button runat="server" ID="btnGeneralClear" class="btn btn-raised btn-warning" Text="Clear" OnClick="btnGeneralClear_Click" />
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divOthers">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-automobile"></i>Vehicle Others Service</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Workshop
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlOtherWorkshop" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Vehicle Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlOtherVehicleType" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Other Service
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlOtherService" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">Vehicle</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtOtherLicencePlate" placeholder="License Plate" runat="server" OnTextChanged="txtOtherLicencePlate_OnTextChanged" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtenderVehicle" runat="server" ServiceMethod="SearchVehicleLicence" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtOtherLicencePlate" FirstRowSelected="false" CompletionListCssClass="autoCompleteList" CompletionListItemCssClass="autoCompleteListItem" CompletionListHighlightedItemCssClass="autoCompleteListItemHighlight">
                                    </ajax:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Report Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlOtherReportRange" CssClass="DropDownListStyle">
                                        <asp:ListItem Text="Date Wise" Value="Day"></asp:ListItem>
                                        <asp:ListItem Text="Month Wise" Value="Month"></asp:ListItem>
                                        <asp:ListItem Text="Year Wise" Value="Year"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Workshop Based
                                </div>
                                <div class="col-sm-4">
                                    <asp:CheckBox runat="server" ID="cbOtherWorkshopBased" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Date From
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtOtherFromDate" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtOtherFromDate" PopupButtonID="txtOtherFromDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></ajax:CalendarExtender>
                                </div>
                                <div class="col-sm-2"></div>
                                <div class="col-sm-2">
                                    Date To
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtOtherToDate" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtOtherToDate" PopupButtonID="txtOtherToDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></ajax:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding: 5px; text-align: center;">
                                <asp:Button runat="server" ID="btnOtherView" OnClick="btnOtherView_Click" class="btn btn-raised btn-primary" Text="View" />
                                <asp:Button runat="server" ID="btnOtherClear" class="btn btn-raised btn-warning" Text="Clear" OnClick="btnOtherClear_Click" />
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divParts">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-automobile"></i>Parts Related Service</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Workshop
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlPartsWorkshop" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Parts Name
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtPartsName" placeholder="Select Parts" runat="server" OnTextChanged="txtPartsName_OnTextChanged" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" ServiceMethod="SearchPartsList" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtPartsName" FirstRowSelected="false" CompletionListCssClass="autoCompleteList" CompletionListItemCssClass="autoCompleteListItem" CompletionListHighlightedItemCssClass="autoCompleteListItemHighlight">
                                    </ajax:AutoCompleteExtender>
                                    <asp:HiddenField runat="server" ID="hdPartsId" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Vehicle Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlPartsVehicleType" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">Vehicle</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtPartsLicencePlate" placeholder="License Plate" runat="server" OnTextChanged="txtPartsLicencePlate_OnTextChanged" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="SearchVehicleLicence" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtPartsLicencePlate" FirstRowSelected="false" CompletionListCssClass="autoCompleteList" CompletionListItemCssClass="autoCompleteListItem" CompletionListHighlightedItemCssClass="autoCompleteListItemHighlight">
                                    </ajax:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Report Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlPartsReportRange" CssClass="DropDownListStyle">
                                        <asp:ListItem Text="Date Wise" Value="Day"></asp:ListItem>
                                        <asp:ListItem Text="Month Wise" Value="Month"></asp:ListItem>
                                        <asp:ListItem Text="Year Wise" Value="Year"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Workshop Based
                                </div>
                                <div class="col-sm-4">
                                    <asp:CheckBox runat="server" ID="cbPartsWorkshopBased" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Date From
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtPartsFromDate" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPartsFromDate" PopupButtonID="txtPartsFromDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></ajax:CalendarExtender>
                                </div>
                                <div class="col-sm-2"></div>
                                <div class="col-sm-2">
                                    Date To
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtPartsToDate" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPartsToDate" PopupButtonID="txtPartsToDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></ajax:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding: 5px; text-align: center;">
                                <asp:Button runat="server" ID="btnPartsView" OnClick="btnPartsView_Click" class="btn btn-raised btn-primary" Text="View" />
                                <asp:Button runat="server" ID="btnPartsClear" class="btn btn-raised btn-warning" Text="Clear" OnClick="btnPartsClear_Click" />
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divBill">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-automobile"></i>Vendor Bill</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Workshop
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlBillWorkshop" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Vehicle Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlBillVehicleType" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Report Type
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlBillReportRange" CssClass="DropDownListStyle">
                                        <asp:ListItem Text="Date Wise" Value="Day"></asp:ListItem>
                                        <asp:ListItem Text="Month Wise" Value="Month"></asp:ListItem>
                                        <asp:ListItem Text="Year Wise" Value="Year"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">Vehicle</div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtBillLicencePlate" placeholder="License Plate" runat="server" OnTextChanged="txtBillLicencePlate_OnTextChanged" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" ServiceMethod="SearchVehicleLicence" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtBillLicencePlate" FirstRowSelected="false" CompletionListCssClass="autoCompleteList" CompletionListItemCssClass="autoCompleteListItem" CompletionListHighlightedItemCssClass="autoCompleteListItemHighlight">
                                    </ajax:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Date From
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtBillFromDate" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtBillFromDate" PopupButtonID="txtBillFromDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></ajax:CalendarExtender>
                                </div>
                                <div class="col-sm-2"></div>
                                <div class="col-sm-2">
                                    Date To
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" ID="txtBillToDate" CssClass="TextBoxStyle"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtBillToDate" PopupButtonID="txtBillToDate" Format="dd-MMM-yyyy" CssClass="CalenderTheme"></ajax:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top: 15px; text-align: center;">
                                <asp:Button runat="server" ID="btnBillView" class="btn btn-raised btn-primary" Text="View" OnClick="btnBillView_Click" />
                                <asp:Button runat="server" ID="btnBillClear" class="btn btn-raised btn-warning" Text="Clear" OnClick="btnBillClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField runat="server" ID="hdVehicleId" />
            </div>

            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Queryboard Result (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="style-default-bright" style="min-height: 150px;">
                            <asp:GridView ID="gvReportData" runat="server" Width="100%" Class="NewGridDesingBody" AllowPaging="False" RowStyle-Wrap="false"
                                EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="True">
                                <PagerSettings PageButtonCount="10" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
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
