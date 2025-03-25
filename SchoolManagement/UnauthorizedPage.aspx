<%@ Page Title="Unauthorized Page | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UnauthorizedPage.aspx.cs" Inherits="SchoolManagement.UnauthorizedPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divButton">
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12" style="padding: 10px; text-align: center;">
                                <img src="img/road-barrier.png" style="border: 5px solid #A1DBB2; border-radius: 100px; width: 150px; height: 150px;" />
                                <h3>Oops, Sorry!</h3>
                                <p style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: 14px;">
                                    Sorry, your access is refused due to security reasons of our server and also our sensitive data.<br />
                                    Please go back to the previous page to continue browsing.
                                </p>
                                <a class="btn btn-danger" href="javascript:history.back()">Go Back</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
