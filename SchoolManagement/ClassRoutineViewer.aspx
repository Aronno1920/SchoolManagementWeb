<%@ Page Title="Class Routine Viewer | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ClassRoutineViewer.aspx.cs" Inherits="SchoolManagement.ClassRoutineViewer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <script type="text/javascript">
        function loadvalidate() {
            if (document.getElementById("<%=ddlClass.ClientID%>").value == "0") {
                alert("Please Select Any Class");
                document.getElementById("<%=ddlClass.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlSection.ClientID%>").value == "0") {
                alert("Please Select Any Section");
                document.getElementById("<%=ddlSection.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-6">
                <div runat="server" id="divEntry">
                    <div class="card-head">
                        <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Search Criteria - Class/Section Wise Routine</header>
                    </div>
                    <div class="card-body style-default-bright">
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                Class Name
                                    <span style="color: red">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlClass" runat="server" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true" CssClass="DropDownListStyle"></asp:DropDownList>
                            </div>
                            <div class="col-sm-2">
                                Section Name
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-12" style="padding-top: 10px; text-align: center;">
                            <asp:Button runat="server" ID="btnLoad" Text="Load Routine" OnClick="btnLoad_Click" CssClass="btn btn-raised btn-accent" OnClientClick="return loadvalidate();"/>
                            <asp:Button runat="server" ID="btnLoadBangla" Text="Load Routine Bangla" OnClick="btnLoadBangla_Click" CssClass="btn btn-raised btn-accent" OnClientClick="return loadvalidate();"/>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">
                <div runat="server" id="divDetails">
                    <div class="card-head">
                        <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Class Routine Viewer<asp:Label runat="server" ID="lblRecordCount"></asp:Label></header>
                    </div>
                    <div class="card-body style-default-bright">
                        <div class="col-sm-12" style="overflow: auto;">
                            <asp:GridView ID="gvRoutine" runat="server" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="15" RowStyle-Wrap="false"
                                EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="DayName" HeaderText="Day" />
                                    <asp:TemplateField HeaderText="First" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <b><%# Server.HtmlDecode(Eval("Subject1").ToString())%></b><br />
                                            <%# Server.HtmlDecode(Eval("Teacher1").ToString())%><br />
                                            <%# Server.HtmlDecode(Eval("Time1").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Second" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <b><%# Server.HtmlDecode(Eval("Subject2").ToString())%></b><br />
                                            <%# Server.HtmlDecode(Eval("Teacher2").ToString())%><br />
                                            <%# Server.HtmlDecode(Eval("Time2").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Third" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <b><%# Server.HtmlDecode(Eval("Subject3").ToString())%></b><br />
                                            <%# Server.HtmlDecode(Eval("Teacher3").ToString())%><br />
                                            <%# Server.HtmlDecode(Eval("Time3").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fourth" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <b><%# Server.HtmlDecode(Eval("Subject4").ToString())%></b><br />
                                            <%# Server.HtmlDecode(Eval("Teacher4").ToString())%><br />
                                            <%# Server.HtmlDecode(Eval("Time4").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Break Time" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Server.HtmlDecode(Eval("BreakTime").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fifth" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <b><%# Server.HtmlDecode(Eval("Subject5").ToString())%></b><br />
                                            <%# Server.HtmlDecode(Eval("Teacher5").ToString())%><br />
                                            <%# Server.HtmlDecode(Eval("Time5").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sixth" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <b><%# Server.HtmlDecode(Eval("Subject6").ToString())%></b><br />
                                            <%# Server.HtmlDecode(Eval("Teacher6").ToString())%><br />
                                            <%# Server.HtmlDecode(Eval("Time6").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Seventh" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <b><%# Server.HtmlDecode(Eval("Subject7").ToString())%></b><br />
                                            <%# Server.HtmlDecode(Eval("Teacher7").ToString())%><br />
                                            <%# Server.HtmlDecode(Eval("Time7").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Eighth" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <b><%# Server.HtmlDecode(Eval("Subject8").ToString())%></b><br />
                                            <%# Server.HtmlDecode(Eval("Teacher8").ToString())%><br />
                                            <%# Server.HtmlDecode(Eval("Time8").ToString())%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <%--<AlternatingRowStyle BackColor="#f0fdf9" />--%>
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
