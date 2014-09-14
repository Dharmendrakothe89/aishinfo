<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Welcome To | Login</title>
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

    <!-- Core CSS - Include with every page -->
    <link href="plugins/bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="plugins/pace/pace-theme-big-counter.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/main-style.css" rel="stylesheet" />
</head>
<body class="body-Login-back">
    <form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4 text-center logo-margin ">
                <h3 style="color: #fff;">
                    Multilevel Marketing</h3>
            </div>
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Please Sign In</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control validate[required]" placeholder="User ID"
                       ></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="form-control validate[required]" placeholder="Password"
                                type="password"></asp:TextBox>
                        </div>
                        <div class="checkbox">
                           
                        </div>

                        <!-- Change this to a button or input when using this as a form -->
                        <asp:Button ID="btnlogin" runat="server" Text="Login" CssClass="btn btn-lg btn-success btn-block" OnClick="btnlogin_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    
    <!-- Core Scripts - Include with every page -->
    <script type="text/javascript" src="plugins/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="plugins/bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript" src="plugins/metisMenu/jquery.metisMenu.js"></script>
    </form>
</body>
</html>
