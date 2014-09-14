<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="register.aspx.cs" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/Commonvalidation.js" type="text/javascript"></script>
<style type="text/css">
    #GB_overlay {background-color: #fff;position: absolute;margin: auto;top: 0;left: 0;z-index: 100;}
.forgotpasswordwrapper{width: 470px;border: 9px solid #eb4800;border-radius: 10px;padding: 10px;background:#fff;text-align: left;position: fixed; z-index: 999; top:74px; left:29%; height:450px;}
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

    function ValidateRegister() {
        var txtname = document.getElementById("<%=txtname.ClientID%>");
        var txtsemid = document.getElementById("<%=txtsemid.ClientID%>");
        var txtsponsorname = document.getElementById("<%=txtsponsorname.ClientID%>");
        var txtsponsorid = document.getElementById("<%=txtsponsorid.ClientID%>");
        var txtmobileno = document.getElementById("<%=txtmobileno.ClientID%>");
        var txtemail = document.getElementById("<%=txtemail.ClientID%>");

        if (txtname.value == "") {
            alert("please enter your name");
            return false;
        }
        else if (txtsemid.value == "") {
            alert("please enter your SAMI-ID");
            return false;
        }
        else if (txtsponsorname.value == "") {
            alert("please enter Sponsor name");
            return false;
        }
        else if (txtsponsorid.value == "") {
            alert("please enter Sponsor SAMI-ID");
            return false;
        }
        else if (txtmobileno.value == "") {
            alert("please enter your mobile no");
            return false;
        }
        else if (txtemail.value == "") {
            alert("please enter your email id");
            return false;
        }
        else {
            return true;
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header_text sub" style="margin-top: 15px;">
        <img class="pageBanner" src="img/pageBanner.png" alt="New products" />
        <h4>
            <span>Login or Regsiter</span></h4>
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
                                <asp:TextBox ID="txtusername" runat="server" placeholder="Enter your username" CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Password</label>
                            <div class="controls">
                                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" placeholder="Enter your password" CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <asp:Button ID="btnsign" runat="server" Text="Sign into your account" type="submit"
                                value="Sign into your account" OnClick="btnsign_Click" OnClientClick="return ValidateLogin();" CssClass="btn btn-inverse large" />
                            <hr />
                            <p class="reset">
                                Recover your <a tabindex="4" href="#" onclick="ShowForgetpassword();" title="Recover your username or password">username
                                    or password</a></p>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div class="span7">
                <h4 class="title">
                    <span class="text"><strong>Register</strong> Form</span></h4>
                <div>
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label">
                                Full Name</label>
                            <div class="controls">
                                <asp:TextBox ID="txtname" runat="server" style="text-transform:capitalize;" placeholder="Enter Your Full Name" CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Sami ID:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtsemid" runat="server" placeholder="Enter Your SAMI-ID" CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Sponsor Name:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtsponsorname" runat="server" style="text-transform:capitalize;" placeholder="Enter Sponsor Name"
                                    CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Sponsor Sami ID:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtsponsorid" runat="server" placeholder="Enter Sponsor SAMI-ID"
                                    CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Mobile Number:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtmobileno" runat="server" MaxLength="10" placeholder="Enter Your Mobile Number" onkeypress="return validateMobileNo(event,this.id);"
                                    CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Email address:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtemail" runat="server" placeholder="Enter Your E-mail" onblur="return validateEmail(this.value, this.id)" CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <hr />
                        <div class="actions">
                            <asp:Button ID="btnaccount" runat="server" Text="Create your account" OnClick="btnaccount_Click"  OnClientClick="return ValidateRegister();"
                                CssClass="btn btn-inverse large" />
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <div id="divforget" class="forgotpasswordwrapper" style="display: none">
        <h4 class="title">
            <span class="text"><strong>Forgot </strong>Password ?</span> <span style="float:right;"><img src="img/close.png" alt="" onclick="CloseForgetpassword();"/></span></h4>
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
                        <asp:TextBox ID="txtforgetname" runat="server" style="text-transform:capitalize;" placeholder="Enter Your Name" CssClass="input-xlarge"></asp:TextBox>
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
                    <p style="font-size:15px; font-weight:bold;">
                        OR </p>
                </div>
                <div class="control-group">
                    <label class="control-label">
                        Email address:</label>
                    <div class="controls">
                        <asp:TextBox ID="txtforgetemailid" runat="server" placeholder="Enter Your E-mail" onblur="return validateEmail(this.value, this.id)" CssClass="input-xlarge"></asp:TextBox>
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
