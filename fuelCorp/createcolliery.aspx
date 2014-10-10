<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="createcolliery.aspx.cs" Inherits="createcolliery" %>

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
                    Colliery Form</h2>
                <hr />
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Colliery Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>Colliery Name</strong>
                                    </p>
                                    <asp:TextBox ID="txtcollieryname" runat="server" Style="text-transform: capitalize;" onkeypress=" return isCharacterWithSpace(event);"  CssClass="form-control validate[required]" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Colliery Code</strong>
                                    </p>
                                    <asp:TextBox ID="txtcollierycode" runat="server" Style="text-transform:uppercase;" CssClass="form-control validate[required]" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Region</strong>
                                    </p>
                                    <div>
                                        <asp:RadioButton ID="rdnagpur" runat="server" CssClass="radio radio-inline" Text="Nagpur Depot"
                                            GroupName="REGION" Checked="true" />
                                        <asp:RadioButton ID="rdwani" runat="server" CssClass="radio radio-inline" Text="Wani Depot"
                                            GroupName="REGION" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Coal Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td colspan="3">
                                    <asp:Repeater ID="coaltyperepeater" runat="server" OnItemCommand="coaltyperepeater_ItemCommand">
                                        <HeaderTemplate>
                                            <table style="width: 100%; border-bottom: 1px solid #e7e7e7; border-left: 1px solid #e7e7e7;
                                                border-right: 1px solid #e7e7e7; border-top: 1px solid #e7e7e7;" border="0" cellspacing="0px"
                                                cellpadding="0px">
                                                <tr>
                                                    <th align="left" valign="middle" style="width: 110px;">
                                                        COAL TYPE
                                                    </th>
                                                    <th align="left" valign="middle" style="width: 80px;">
                                                        Grade
                                                    </th>
                                                    <th align="left" valign="middle" style="width: 80px;">
                                                        Notified Price
                                                    </th>
                                                    <th align="left" valign="middle" style="width: 80px;">
                                                        Comm Benifit Charg
                                                    </th>
                                                    <th align="left" valign="middle" style="width: 80px;">
                                                        Crushing Chg
                                                    </th>
                                                    <th align="left" valign="middle" style="width: 80px;">
                                                        STC
                                                    </th>
                                                    <th align="left" valign="middle" style="width: 80px;">
                                                        SED
                                                    </th>
                                                    <th align="left" valign="middle" style="width: 80px;">
                                                        CEC
                                                    </th>
                                                    <th align="left" valign="middle" style="width: 80px;">
                                                        Royalty
                                                    </th>
                                                    <th align="left" valign="middle" style="width: 80px;">
                                                        MP Rd. Tax
                                                    </th>
                                                    <th align="left" valign="middle" style="width: 80px;">
                                                        Transit Fee
                                                    </th>
                                                    <th align="left" valign="middle" style="width: 80px;">
                                                        Entry Fee
                                                    </th>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%; border-left: 1px solid #e7e7e7; border-right: 1px solid #e7e7e7;
                                                border-top: 1px solid #e7e7e7;" border="0" cellspacing="0px" cellpadding="0px">
                                                <tr>
                                                    <td align="left" valign="middle" style="width: 110px; padding-left: 3px;">
                                                        <asp:CheckBox ID="chkcoaltype" runat="server" CssClass='<%#Eval("COALID") %>' Text='<%#Eval("COALTYPE") %>' />
                                                    </td>
                                                    <td align="left" valign="middle" style="width: 80px; padding-top: 7px;">

                                                        <asp:TextBox ID="txtgrade" runat="server" style="text-transform:uppercase;" CssClass="form-control" Width="50px" Height="25px"
                                                            ></asp:TextBox>
                                                    </td>
                                                    <td align="left" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtnotifiedprice" runat="server" CssClass="form-control" onkeypress="return isNumberWthDot(event, this.value)" Width="50px"
                                                            Height="25px" ></asp:TextBox>
                                                    </td>
                                                    <td align="left" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtcommbenifitchrg" runat="server" CssClass="form-control" onkeypress="return isNumberWthDot(event, this.value)" Width="50px"
                                                            Height="25px"></asp:TextBox>
                                                    </td>
                                                    <td align="left" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtcrushingchg" runat="server" CssClass="form-control" onkeypress="return isNumberWthDot(event, this.value)" Width="50px"
                                                            Height="25px"></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtstc" runat="server" CssClass="form-control" onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"
                                                            ></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtsed" runat="server" CssClass="form-control" onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"
                                                            ></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtcec" runat="server" CssClass="form-control" onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"
                                                            ></asp:TextBox>
                                                    </td>
                                                     <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtroyalty" runat="server" CssClass="form-control" onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"
                                                            ></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtmprdtax" runat="server" CssClass="form-control" onkeypress="return isNumberWthDot(event, this.value)" Width="50px"
                                                            Height="25px" ></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txttransitfee" runat="server" CssClass="form-control" onkeypress="return isNumberWthDot(event, this.value)" Width="50px"
                                                            Height="25px"></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtentryfee" runat="server" CssClass="form-control" onkeypress="return isNumberWthDot(event, this.value)" Width="50px"
                                                            Height="25px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-danger" Text="Submit" OnClick="btnsubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
        </div>
</asp:Content>
