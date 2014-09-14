<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="editprofile.aspx.cs" Inherits="editprofile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="Scripts/Commonvalidation.js" type="text/javascript"></script>
<script type="text/javascript">
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
            alert("please enter your sami id");
            return false;
        }
        else if (txtsponsorname.value == "") {
            alert("please enter Sponsor name");
            return false;
        }
        else if (txtsponsorid.value == "") {
            alert("please enter Sponsor sami id");
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
            <span>Edit Profile</span></h4>
    </div>
    <div class="main-content">
        <div class="row">
            <div class="span7">
                <h4 class="title">
                    <span class="text"><strong>Profile </strong> Details</span></h4>
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
                                <asp:TextBox ID="txtsemid" runat="server" Enabled="false" placeholder="Enter Your Semi-ID" CssClass="input-xlarge"></asp:TextBox>
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
                                <asp:TextBox ID="txtsponsorid" runat="server" placeholder="Enter Sponsor Semi-ID"
                                    CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Mobile Number:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtmobileno" runat="server" placeholder="Enter Your Mobile Number" onkeypress="return validateMobileNo(event,this.id);"
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
                            <asp:Button ID="btnaccount" runat="server" Text="Update Profile" OnClick="btnaccount_Click" OnClientClick="return ValidateRegister();"
                                CssClass="btn btn-inverse large" />
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

