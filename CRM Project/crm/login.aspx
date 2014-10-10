<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/Commonvalidation.js" type="text/javascript"></script>
    <style type="text/css">
        .list
        {
            height: 150px;
            overflow: auto;
            list-style-type: square;
            font-family: Verdana;
            font-size: 11px;
            border: 1px solid #000000;
            padding-right: 2px;
            width: 410px !important;
        }
        
        #GB_overlay
        {
            background-color: #fff;
            position: absolute;
            margin: auto;
            top: 0;
            left: 0;
            z-index: 100;
        }
        .forgotpasswordwrapper
        {
            width: 470px;
            border: 9px solid #eb4800;
            border-radius: 10px;
            padding: 10px;
            background: #fff;
            text-align: left;
            position: fixed;
            z-index: 999;
            top: 74px;
            left: 29%;
            height: 450px;
        }
    </style>
    <script type="text/javascript">
        function ShowForgetpassword() {
            document.getElementById("divforget").style.display = "block";
        }
        function CloseForgetpassword() {
            document.getElementById("divforget").style.display = "none";
        }
        function ValidateGetPassword() {
            var name = document.getElementById("<%=txtforgetname.ClientID%>");
            var userid = document.getElementById("<%=txtforgetuserid.ClientID%>");
            var mail = document.getElementById("<%=txtforgetemailid.ClientID%>");
            if (name.value == "") {
                alert("please specify your name");
                return false;
            }
            else if (userid.value == "" && mail.value == "") {
                alert("please specify atleast one value");
                return false;
            }
            else {
                return true;
            }
        }
        function ValidateLogin() {
            var password = document.getElementById("<%=txtpassword.ClientID%>");
            var userid = document.getElementById("<%=txtusername.ClientID%>");

            if (userid.value == "") {
                alert("please enter your username");
                return false;
            }
            else if (password.value == "") {
                alert("please enter password");
                return false;
            }
            else {
                return true;
            }
        }

    </script>
   <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>  
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
    <div class="header_text sub" style="margin-top: 15px;">
        <img class="pageBanner" src="img/pageBanner.png" alt="New products" />
        <h4>
            <span>Login</span></h4>
    </div>
    <div class="main-content">
        <div class="row">
            <div class="span5">
                <h4 class="title">
                    <span class="text"><strong>Login</strong> Form</span></h4>
                <div>
                    <fieldset>
                    
                        <div class="control-group">
                            <label class="control-label">
                                User Names</label>
                            <div class="controls">
                                <asp:TextBox ID="txtusername" runat="server" placeholder="Enter your username" AutoComplete="off" CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Password</label>
                            <div class="controls">
                                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" AutoComplete="off" placeholder="Enter your password"
                                    CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <asp:Button ID="btnsign" runat="server" Text="Sign into your account" type="submit"
                                value="Sign into your account" OnClick="btnsign_Click" OnClientClick="return ValidateLogin();"
                                CssClass="btn btn-inverse large" />
                            <hr />
                            <p class="reset">
                                Recover your <a tabindex="4" href="#" onclick="ShowForgetpassword();" title="Recover your username or password">
                                    username or password</a></p>
                        </div>
                       
                    </fieldset>
                </div>
            </div>
            
        </div>
    </div>
    <div id="divforget" class="forgotpasswordwrapper" style="display: none">
        <h4 class="title">
            <span class="text"><strong>Forgot </strong>Password ?</span> <span style="float: right;">
                <img src="img/close.png" alt="" onclick="CloseForgetpassword();" /></span></h4>
        <div>
            <fieldset>
                <div class="control-group">
                    <p>
                        Enter the username and E-mail ID you provided when you registered. Your Account
                        Detail link will be sent on your registered email id.</p>
                </div>
                <div class="control-group">
                    <label class="control-label">
                        Your Name</label>
                    <div class="controls">
                        <asp:TextBox ID="txtforgetname" runat="server" Style="text-transform: capitalize;"
                            placeholder="Enter Your Name" CssClass="input-xlarge"></asp:TextBox>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">
                        User-ID</label>
                    <div class="controls">
                        <asp:TextBox ID="txtforgetuserid" runat="server" placeholder="Enter Your User ID"
                            CssClass="input-xlarge"></asp:TextBox>
                    </div>
                </div>
                <div class="control-group">
                    <p style="font-size: 15px; font-weight: bold;">
                        OR
                    </p>
                </div>
                <div class="control-group">
                    <label class="control-label">
                        Email address:</label>
                    <div class="controls">
                        <asp:TextBox ID="txtforgetemailid" runat="server" placeholder="Enter Your E-mail"
                            onblur="return validateEmail(this.value, this.id)" CssClass="input-xlarge"></asp:TextBox>
                    </div>
                </div>
                <hr />
                <div class="actions">
                    <asp:Button ID="btnforget" runat="server" Text="Send" OnClick="btnforget_Click" OnClientClick="return ValidateGetPassword();"
                        CssClass="btn btn-inverse large" />
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>

