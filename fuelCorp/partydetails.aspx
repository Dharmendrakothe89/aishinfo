<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="partydetails.aspx.cs" Inherits="partydetails" %>

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
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Party Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td colspan="4" align="right">
                                    <asp:Button ID="btnedit" runat="server" Text="Edit" CssClass="btn btn-danger" OnClick="btnedit_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Party Name</strong>
                                    </p>
                                    <asp:TextBox ID="txtpartyname" runat="server" CssClass="form-control validate[required]"
                                        onkeypress=" return isCharacterWithSpace(event);"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Party Code</strong>
                                    </p>
                                    <asp:TextBox ID="txtpartycode" runat="server" CssClass="form-control valiadate[required,custom[onlyLetterNumber]]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Party Type</strong>
                                    </p>
                                    <div>
                                        <asp:DropDownList ID="ddlpartytype" runat="server" CssClass="form-control validate[required]">
                                            <asp:ListItem Selected="True" Text="--Party Type--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Linked Customer" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="E-Auction" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Open Market" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Direct Customer" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>
                                    <p>
                                        <strong>Add on Party</strong>
                                    </p>
                                    <div>
                                        <asp:RadioButton ID="rdaddonyes" runat="server" CssClass="radio radio-inline" Text="Yes"
                                            GroupName="ADDON" Checked="true" />
                                        <asp:RadioButton ID="rdaddonno" runat="server" CssClass="radio radio-inline" Text="No"
                                            GroupName="ADDON" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Phone </strong>
                                    </p>
                                    <asp:TextBox ID="txtphone" runat="server" onkeypress="return landline(event);" MaxLength="16"
                                        CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Fax </strong>
                                    </p>
                                    <asp:TextBox ID="txtfax" runat="server" CssClass="form-control" MaxLength="16"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>E-mail </strong>
                                    </p>
                                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Website </strong>
                                    </p>
                                    <asp:TextBox ID="txtwebsite" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Address</strong>
                                    </p>
                                    <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control validate[required]"
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>State</strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="true" CssClass="form-control validate[required]"
                                                OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <p>
                                        <strong>City</strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlstate" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlcity" runat="server" AutoPostBack="false" Enabled="false"
                                                CssClass="form-control validate[required]">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <p>
                                        <strong>Pin </strong>
                                    </p>
                                    <asp:TextBox ID="txtpin" runat="server" MaxLength="6" CssClass="form-control validate[required,custom[integer]]"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Work Address</strong>
                                    </p>
                                    <asp:TextBox ID="txtworkaddress" runat="server" CssClass="form-control validate[required]"
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Work State</strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlworkstate" runat="server" CssClass="form-control validate[required]"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlworkstate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <p>
                                        <strong>Work City</strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlworkstate" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlworkcity" runat="server" AutoPostBack="false" Enabled="false"
                                                CssClass="form-control validate[required]">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <p>
                                        <strong>Work Pin </strong>
                                    </p>
                                    <asp:TextBox ID="txtworkpin" runat="server" MaxLength="6" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>PAN NO</strong>
                                    </p>
                                    <asp:TextBox ID="txtpanno" runat="server" MaxLength="15" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>CST NO </strong>
                                    </p>
                                    <asp:TextBox ID="txtcstno" runat="server" MaxLength="15" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>VAT NO </strong>
                                    </p>
                                    <asp:TextBox ID="txtvatno" runat="server" MaxLength="15" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>SERVICE TAX NO </strong>
                                    </p>
                                    <asp:TextBox ID="txtservicetaxno" runat="server" MaxLength="15" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>EXCISE NO</strong>
                                    </p>
                                    <asp:TextBox ID="txtexciseno" runat="server" MaxLength="15" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>EXCISE RANGE</strong>
                                    </p>
                                    <asp:TextBox ID="txtexciserange" runat="server" MaxLength="15" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>EXCISE DIVISION</strong>
                                    </p>
                                    <asp:TextBox ID="txtexcisedivision" runat="server" MaxLength="15" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>EXCISE COLLECTRATE </strong>
                                    </p>
                                    <asp:TextBox ID="txtexcisecollectrate" runat="server" Style="text-transform: uppercase;"
                                        CssClass="form-control" MaxLength="15"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Party Member
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr id="rowaddmore" runat="server" style="display: none;">
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
                                            <asp:Repeater ID="memberrepeater" runat="server" OnItemDataBound="memberrepeater_ItemDataBound">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 100%" border="0" cellspacing="5px" cellpadding="5px">
                                                        <tr>
                                                            <td>
                                                                <p>
                                                                    <strong>Person Name</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtpersonname" runat="server" Text='<%#Eval("PERSONNAME") %>' onkeypress=" return isCharacterWithSpace(event);"
                                                                    CssClass="form-control validate[required , custom[char]]"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Department</strong>
                                                                </p>
                                                                <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Designation</strong>
                                                                </p>
                                                                <asp:DropDownList ID="ddldesignation" runat="server" CssClass="form-control validate[required]">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Email ID</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtperemailid" runat="server" Text='<%#Eval("EMAILID") %>' CssClass="form-control validate[required]"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <p>
                                                                    <strong>Phone</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtperphone" runat="server" Text='<%#Eval("PHONENO") %>' MaxLength="16"
                                                                    CssClass="form-control validate[required,min[8]]"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Mobile</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtpermobile" runat="server" Text='<%#Eval("MOBILE") %>' MaxLength="16"
                                                                    CssClass="form-control validate[required]"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                               
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
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Billing Parameter
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>VAT/CST</strong>
                                    </p>
                                   <asp:RadioButton ID="rdvat" runat="server" GroupName="VATEXCISE" Checked="true" Text="VAT"
                                        CssClass="radio radio-inline" />
                                    <asp:RadioButton ID="rdexcise" runat="server" GroupName="VATEXCISE" Text="CST" CssClass="radio radio-inline" />
                                </td>
                                <td>
                                    <p>
                                        <strong>Service Tax</strong>
                                    </p>
                                    <asp:RadioButton ID="rdserviceapplicable" runat="server" GroupName="SERVICETAX" Checked="true"
                                        CssClass="radio radio-inline" Text="Applicable" />
                                    <asp:RadioButton ID="rdservicenotapplicable" runat="server" GroupName="SERVICETAX"
                                        CssClass="radio radio-inline" Text="Not Applicable" />
                                </td>
                                <td>
                                    <p>
                                        <strong>Excise</strong>
                                    </p>
                                    <asp:RadioButton ID="rdexciseapplicable" runat="server" GroupName="EXCISETAX" Checked="true"
                                        CssClass="radio radio-inline" Text="Applicable" />
                                    <asp:RadioButton ID="rdexcisenotapplicable" runat="server" GroupName="EXCISETAX"
                                        CssClass="radio radio-inline" Text="Not Applicable" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnupdate" runat="server" Text="Submit" CssClass="btn btn-danger"
                        Visible="false" OnClick="btnupdate_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
