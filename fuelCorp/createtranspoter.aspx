<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="createtranspoter.aspx.cs" Inherits="createtranspoter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link href="ValidationEngine.css" rel="stylesheet" type="text/css" />
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
        function DateFormat(field, rules, i, options) {
            var regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
            if (!regex.test(field.val())) {
                return "Please enter date in dd/MM/yyyy format."
            }
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper" style="margin-top: 20px;">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <h2>
                    Transpoter Form</h2>
                <hr />
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Transpoter Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>Transporter Name</strong>
                                    </p>
                                    <asp:TextBox ID="txttransportername" runat="server" Style="text-transform: capitalize;" onkeypress=" return isCharacterWithSpace(event);" CssClass="form-control validate[required]" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Transporter Code</strong>
                                    </p>
                                    <asp:TextBox ID="txttransportercode" runat="server" Style="text-transform:uppercase;"  CssClass="form-control validate[required,custom[onlyLetterNumber]]" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Transporter Type</strong>
                                    </p>
                                    <div>
                                        <asp:RadioButton ID="rdlocal" runat="server" CssClass="radio radio-inline" Text="Local"
                                            GroupName="TRANSPORTERTYPE"  Checked="true" />
                                        <asp:RadioButton ID="rdremote" runat="server" CssClass="radio radio-inline" Text="Remote"
                                            GroupName="TRANSPORTERTYPE" />
                                    </div>
                                </td>
                                <td>
                                    <p>
                                        <strong>Transporter Address</strong>
                                    </p>
                                    <div>
                                        <asp:TextBox ID="txtaddress" runat="server" Style="text-transform: capitalize;" CssClass="form-control validate[required]" TextMode="MultiLine"
                                            ></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Mobile </strong>
                                    </p>
                                    <asp:TextBox ID="txtphone" runat="server" onkeypress="return mobile(event);" MaxLength="16" CssClass="form-control validate[required]" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Fax </strong>
                                    </p>
                                    <asp:TextBox ID="txtfax" runat="server" onkeypress="return mobile(event);" MaxLength="16" CssClass="form-control" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>E-mail </strong>
                                    </p>
                                    <asp:TextBox ID="txtemail" runat="server" style="text-transform:lowercase;" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Service Tax </strong>
                                    </p>
                                    <div style="margin-left: 20px;">
                                        <asp:RadioButton ID="rdserviceapplicable" runat="server" CssClass="radio radio-inline"
                                            Text="Applicable" GroupName="SERVICETAX" Checked="true" />
                                        <asp:RadioButton ID="rdservicenotapplicable" runat="server" CssClass="radio radio-inline"
                                            Text="Not Applicable" GroupName="SERVICETAX" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>PAN NO </strong>
                                    </p>
                                    <asp:TextBox ID="txtpano" runat="server" MaxLength="15" CssClass="form-control validate[required]" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>SERVICE TAX NO </strong>
                                    </p>
                                    <asp:TextBox ID="txtservicetaxno" runat="server" MaxLength="15" CssClass="form-control validate[required]" ></asp:TextBox>
                                </td>
                             
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Transporter Contact Person
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td colspan="4" align="right">
                                    <asp:UpdatePanel ID="upaddmore" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lnkaddmember" runat="server" Text="Add More" ForeColor="#00acec"
                                                OnClick="lnkaddmember_Click"></asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lnkaddmember" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Repeater ID="memberrepeater" runat="server">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 100%" border="0" cellspacing="5px" cellpadding="5px">
                                                        <tr>
                                                            <td>
                                                                <p>
                                                                    <strong>Person Name</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtpersonname" runat="server" Text='<%#Eval("PERSONNAME") %>' style=" text-transform:capitalize;" onkeypress=" return isCharacterWithSpace(event);" CssClass="form-control validate[required]" ></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Email ID</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtperemailid" runat="server" Text='<%#Eval("EMIALID") %>' CssClass="form-control" ></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Phone</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtperphone" MaxLength="16"  Text='<%#Eval("PHONE") %>' runat="server" onkeypress=" return landline(event);" CssClass="form-control validate[required]" ></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Mobile</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtpermobile" runat="server" Text='<%#Eval("MOBILE") %>' MaxLength="16" onkeypress=" return mobile(event);" CssClass="form-control validate[required]" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-danger" OnClick="btnsubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
