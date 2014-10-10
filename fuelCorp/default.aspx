<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="loginform" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: &nbsp;Welcome To Fuel Corporation &nbsp;::</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/sb-admin.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdn.ucb.org.br/Scripts/formValidator/js/languages/jquery.validationEngine-en.js"
        charset="utf-8"></script>
    <script type="text/javascript" src="http://cdn.ucb.org.br/Scripts/formValidator/js/jquery.validationEngine.js"
        charset="utf-8"></script>
         <script type="text/javascript">
             $(function () {
                 $("#form1").validationEngine('attach', { promptPosition: "topRight" });
             });
    </script>
    <script type="text/javascript">
        function ValidateUser() {
            __doPostBack("userid", "password");
        }
    </script>
</head>
<body style="background: #f8f8f8;">
    <form id="form1" runat="server">
    <h1 style="margin-top: -20px;">
        &nbsp;&nbsp;Fuel Corporation</h1>
    <hr />
    <div class="container" style="margin-top: 40px;">
        <div class="box bordered-box blue-border">
            <div class="box-header blue-background">
                <div class="title">
                    &nbsp; Login Details
                </div>
            </div>
            <div class="box-content">
                <div class="row">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" CssClass="col-sm-3 control-label" Text="User ID"></asp:Label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtUserid" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 control-label" Text="Password"></asp:Label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control validate[required]"
                                    TextMode="Password" AutoPostBack="true" onblur="ValidateUser();"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label3" runat="server" CssClass="col-sm-3 control-label" Text="Company"></asp:Label>
                            <div class="col-sm-5">
                                <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control validate[required]"
                                    Width="438px" Height="33px" Enabled="false" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-7 col-sm-5">
                                <asp:Button ID="btnlogin" runat="server" class="btn btn-danger" Text="Sign In" Enabled="false" OnClick="btnlogin_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <hr />
    <div class="footer" style="background: #bcbcbc;">
        <p style="font-size: 14px; line-height: 50px;">
            &nbsp;&nbsp;&copy; CopyRight 2014. All Right Reserverd.</p>
    </div>
    </form>
</body>
</html>
