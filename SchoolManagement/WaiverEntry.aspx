<%@ Page Title="Waiver Entry | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WaiverEntry.aspx.cs" Inherits="SchoolManagement.WaiverEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript">
        function savevalidate() {
            if (document.getElementById("<%=txtStudentCode.ClientID%>").value == "") {
                alert("Please Select Student Code");
                document.getElementById("<%=txtStudentCode.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlWaiverType.ClientID%>").value == "0") {
                alert("Please Select Waiver Reason");
                document.getElementById("<%=ddlWaiverType.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtAmount.ClientID%>").value == "") {
                alert("Please Enter Waiver Amount");
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Waiver Entry</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Code
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtStudentCode" placeholder="Student Code" CssClass="TextBoxStyle"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdStudentId" />
                                    <asp:HiddenField runat="server" ID="hdWaiverId" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnLoadStudent" OnClick="btnLoadStudent_Click" Text="Load" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Name</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtStudentName" Enabled="false" placeholder="Student Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Name (bn)</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtBanglaName" Enabled="false" placeholder="Student Bangla Name" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Class</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtClass" Enabled="false" placeholder="Class" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Roll No.</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtRollNo" Enabled="false" placeholder="Roll No." CssClass="TextBoxStyle"  Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Section</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtSection" Enabled="false" placeholder="Section" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Contact</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtContact" Enabled="false" placeholder="Contact Number" CssClass="TextBoxStyle"  Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12" style="margin-top: 15px">
                                <div class="col-sm-2">
                                    Waiver     
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" ID="ddlWaiverType" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Amount
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtAmount" CssClass="TextBoxStyle" placeholder="Waiver Amount" Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Remarks
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="2" placeholder="Remarks" CssClass="TextBoxStyle"></asp:TextBox>
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Waiver - Awaiting for Approval (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <asp:GridView ID="gvWaiver" runat="server" OnRowCommand="gvWaiver_RowCommand" Width="100%" Class="NewGridDesingBody" AllowPaging="True" PageSize="15" RowStyle-Wrap="false"
                                EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Photo">
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="cImage" ImageUrl='<%# Eval("ProfilePhoto") %>' Height="50px" Width="44px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="WaiverId" HeaderText="WaiverId" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                    <asp:BoundField DataField="ScheduleId" HeaderText="ScheduleId" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                    <asp:BoundField DataField="StudentCode" HeaderText="Code" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="RollNo" HeaderText="Roll" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="StudentName" HeaderText="Name" />
                                    <asp:BoundField DataField="ClassName" HeaderText="Class" />
                                    <asp:BoundField DataField="FeesName" HeaderText="Waiver Type" />
                                    <asp:BoundField DataField="WaiverAmount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
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
