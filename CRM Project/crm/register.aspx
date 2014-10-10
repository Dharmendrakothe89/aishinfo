<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="register.aspx.cs" Inherits="register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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

        function validateSamiId(evt, id) {
            var id = document.getElementById(id).value;
            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (id.length < 2) {
                if ((charCode >= 65) && (charCode <= 122)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else if (id.length >= 2) {
                if ((charCode >= 48) && (charCode <= 57)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {

                return false;
            }
        }
    </script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {

            debugger;

            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "register.aspx/GetAutoCompleteData",
                        data: "{'username':'" + document.getElementById('<%=txtsponsorname.ClientID%>').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);

                        },
                        error: function (result) {
                            alert("Error");
                            document.getElementById('<%=txtsponsorname.ClientID%>').value = "";
                        }
                    });
                }
            });
        }
        function Check() {

            document.getElementById('<%=txtsponsorid.ClientID%>').value = document.getElementById('<%=txtsponsorname.ClientID%>').value.split('-')[1];
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div class="header_text sub" style="margin-top: 15px;">
        <img class="pageBanner" src="img/pageBanner.png" alt="New products" />
        <h4>
          
    </div>
    <div class="main-content">
        <div class="row">
            <div class="span5">
                <h4 class="title">
                    <span class="text"><strong>Register</strong> Form</span></h4>
                <div>
                    <fieldset>
                        <asp:UpdatePanel ID="up1" runat="server">
                            <ContentTemplate>
                                <div class="control-group">
                                    <label class="control-label">
                                        Full Name</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtname" runat="server" AutoComplete="off" Style="text-transform: capitalize;"
                                            placeholder="Enter Your Full Name" CssClass="input-xlarge"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">
                                        Sami ID:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtsemid" runat="server" AutoComplete="off" onkeypress="return validateSamiId(event,this.id);"
                                            MaxLength="10" placeholder="Enter Your SAMI-ID" CssClass="input-xlarge"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">
                                        Sponsor Name:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtsponsorname" runat="server" AutoComplete="off" Style="text-transform: capitalize;"
                                            placeholder="Enter Sponsor Name" CssClass="autosuggest" onblur="Check();"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">
                                        Sponsor Sami ID:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtsponsorid" runat="server" AutoComplete="off" Enabled="false"
                                            placeholder="Enter Sponsor SAMI-ID" CssClass="input-xlarge"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">
                                        Mobile Number:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtmobileno" runat="server" MaxLength="12" AutoComplete="off" placeholder="Enter Your Mobile Number"
                                            onkeypress="return validateMobileNo(event,this.id);" CssClass="input-xlarge"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">
                                        Email address:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtemail" runat="server" placeholder="Enter Your E-mail" AutoComplete="off"
                                            onblur="return validateEmail(this.value, this.id)" CssClass="input-xlarge"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="actions">
                                    <asp:Button ID="btnaccount" runat="server" Text="Create your account" OnClick="btnaccount_Click"
                                        OnClientClick="return ValidateRegister();" CssClass="btn btn-inverse large" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
