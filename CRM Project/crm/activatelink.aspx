<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="activatelink.aspx.cs" Inherits="activatelink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript">
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
                    <span class="text"><strong>Account</strong> Activation</span></h4>
                <div>
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label">
                                </label>
                            <div class="controls">
                                <p>
                                <asp:Label ID="lblactivate" runat="server" Text="Your Account is Activated Now. Login for further process using credential provide in E-mail"></asp:Label>
                                <asp:Label ID="lblnotactivate" runat="server" Visible="false" Text="Your Account Not Activated. Please Try again"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Username</label>
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
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
