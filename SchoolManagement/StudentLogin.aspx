<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentLogin.aspx.cs" Inherits="SchoolManagement.StudentLogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" lang="en">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />

    <title>Sign in | SMS</title>
    <link rel="shortcut icon" href="img/favicon.png" />
    <link rel="stylesheet" href="css/login-page.css" />

</head>
<body style="background-image: url(img/background.png); background-repeat: no-repeat; background-size: 100% 100%;">

    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtUserName.ClientID%>").value == "") {
                alert("Please Enter Login ID and Try Again");
                document.getElementById("<%=txtUserName.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtPassword.ClientID%>").value == "") {
                alert("Please Enter Password and Try Again");
                document.getElementById("<%=txtPassword.ClientID%>").focus();
                return false;
            }
        }
    </script>

    <form id="form1" runat="server">
        <div class="page-center">
            <div class="page-center-in" style="padding-top: 150px">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="sign-box">
                                <div class="sign-avatar">
                                    <img src="img/favicon.png" alt="" />
                                </div>
                                <header class="sign-title">
                                    School Management System<br />
                                    <br />
                                    <%--<span style="font-size: 14px; color: #062f87; font-family: Tahoma;">Shahzadpur Ibrahim Pilot Girls High School</span> 
                                    <span style="font-size: 13px; color: #062f87; font-family: Tahoma;">Shahzadpur, Sirajgonj</span>--%>

                                    <span style="font-size: 14px; color: #062f87; font-family: Tahoma;">XYZ Pilot High School</span>
                                    <span style="font-size: 13px; color: #062f87; font-family: Tahoma;">Shahzadpur, Sirajgonj</span>
                                </header>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtUserName" type="text" class="form-control" placeholder="Enter Your Login ID" />
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtPassword" type="password" class="form-control" placeholder="Enter Your Password" />
                                </div>
                                <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" OnClientClick="return validate();" class="btn btn-rounded" Text="Sign in" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
