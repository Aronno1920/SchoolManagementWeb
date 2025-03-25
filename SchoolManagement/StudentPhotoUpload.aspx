<%@ Page Title="Student Photo Upload | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StudentPhotoUpload.aspx.cs" Inherits="SchoolManagement.StudentPhotoUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <%--<script type="text/javascript">
        function savevalidate() {
            if (document.getElementById("<%=txtSectionName.ClientID%>").value == "") {
                alert("Please Enter Section Name");
                document.getElementById("<%=txtSectionName.ClientID%>").focus();
                return false;
            }
        }
    </script>--%>

    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgProfilePhoto.ClientID%>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
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
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Upload Photo - Student Profile</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-2">Code</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtStudentCode" placeholder="Student Code" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:HiddenField runat="server" ID="hdStudentId" />
                                </div>
                                <div class="col-sm-4"></div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Name</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtStudentName" placeholder="Student Name" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Contact No</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtContactNo" placeholder="Contact Number" CssClass="TextBoxStyle" Enabled="false"  Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">Class</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtClassName" placeholder="Class Name" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">Section</div>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="txtSectionName" placeholder="Section Name" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-12" style="margin-top: 10px;">
                                <div class="col-sm-2">
                                    Select Photo
                                </div>
                                <div class="col-sm-10">
                                    <asp:FileUpload runat="server" ID="uploaderProfilePhoto" onchange="ImagePreview(this);" CssClass="TextBoxStyle" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Preview
                                </div>
                                <div class="col-sm-10">
                                    <asp:Image runat="server" ID="imgProfilePhoto" Height="200px" />
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top:10px; text-align: center;">
                                <asp:Button runat="server" ID="btnSave" class="btn btn-raised btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return savevalidate();" />
                                <asp:Button runat="server" ID="btnStudentList" Text="Student List" OnClick="btnStudentList_Click" CssClass="btn btn-raised btn-info" />
                                <asp:Button runat="server" ID="btnClear" class="btn btn-raised btn-default-dark" Text="Clear" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div runat="server" id="divDetails">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-book"></i>Student List (<asp:Label runat="server" ID="lblRecordCount">0</asp:Label>)</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="margin-bottom: 10px">
                                <div class="col-sm-2">Session</div>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="ddlAdmissionYear" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                </div>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" ID="txtGridSearch" CssClass="TextBoxStyle"></asp:TextBox>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button runat="server" ID="btnGridSearch" OnClick="btnGridSearch_Click" Text="Search" CssClass="btn-raised" />
                                    <asp:Button runat="server" ID="btnGridSearchClear" OnClick="btnGridSearchClear_Click" Text="Clear" CssClass="btn-raised" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <asp:GridView ID="gvStudent" runat="server" OnRowCommand="gvStudent_RowCommand" OnPageIndexChanging="gvStudent_PageIndexChanging" Width="100%" Class="NewGridDesingBody" AllowPaging="true" PageSize="10" RowStyle-Wrap="false"
                                    EmptyDataText="No data found for selected criteria." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Photo">
                                            <ItemTemplate>
                                                <asp:Image runat="server" ID="cImage" ImageUrl='<%# Eval("ProfilePhoto") %>' Height="50px" Width="44px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="StudentId" HeaderText="Section ID" ItemStyle-CssClass="HideGridColumn" HeaderStyle-CssClass="HideGridColumn" />
                                        <asp:BoundField DataField="StudentCode" HeaderText="Code" ItemStyle-HorizontalAlign="Right"/>
                                        <asp:BoundField DataField="RollNo" HeaderText="Roll No." ItemStyle-HorizontalAlign="Right"/>
                                        <asp:BoundField DataField="StudentName" HeaderText="Name" />
                                        <asp:BoundField DataField="ClassName" HeaderText="Class" />
                                        <asp:BoundField DataField="SectionName" HeaderText="Section" />
                                        <asp:BoundField DataField="ContactNo" HeaderText="Contact" ItemStyle-HorizontalAlign="Right"/>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Height="20px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ibtnEdit" CommandName="EditRow" ToolTip="Click For Edit" ImageUrl="~/img/edit.png" Height="25px" /></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#f3fdfa" />
                                    <PagerSettings PageButtonCount="5" Mode="NumericFirstLast" FirstPageText="First Page" LastPageText="Last Page" />
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle CssClass="NewGridDesign" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
