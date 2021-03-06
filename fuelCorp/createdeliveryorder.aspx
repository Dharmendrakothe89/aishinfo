﻿<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="createdeliveryorder.aspx.cs" Inherits="createdeliveryorder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function CoalDetails() {
            __doPostBack("coal", "password");
        }
    </script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper" style="margin-top: 20px;">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <asp:UpdatePanel ID="up1" runat="server">
                    <ContentTemplate>
                        <div class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp; DO Details
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <p>
                                                <strong>DO For </strong>
                                            </p>
                                            <asp:DropDownList ID="ddldotype" runat="server" CssClass="form-control validate[required]"
                                                Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddldotype_SelectedIndexChanged">
                                             
                                                <asp:ListItem Text="Auction" Value="AUCTION"></asp:ListItem>
                                                <asp:ListItem Text="Linked Customer" Value="LINKEDCUSTOMER"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>For Month </strong>
                                            </p>
                                            <asp:DropDownList ID="ddlmonth" runat="server" CssClass="form-control validate[required]"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Destination</strong>
                                            </p>
                                            <asp:TextBox ID="txtdestination" runat="server" CssClass="form-control validate[required]"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>Source</strong>
                                            </p>
                                            <div>
                                                <asp:DropDownList ID="ddlcolliery" runat="server" CssClass="form-control validate[required]"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlcolliery_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Colliery Code</strong>
                                            </p>
                                            <asp:TextBox ID="txtcollierycode" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>DO No</strong>
                                            </p>
                                            <asp:TextBox ID="txtdono" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthOutDot(event);"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>DO Date</strong>
                                            </p>
                                            <asp:TextBox ID="txtdodate" runat="server"  CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                               onkeyup="DateFormatValidation(this.id,this.value);" MaxLength="10" onkeypress="return isNumberWthOutDot(event);"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr id="trlinkedcustomer" runat="server" style="display: none;">
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <p>
                                                        <strong>Party Name</strong>
                                                    </p>
                                                    <asp:DropDownList ID="ddlparty" runat="server" CssClass="form-control validate[required]"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlparty_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlparty" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <strong>Party Code</strong>
                                                    </p>
                                                    <asp:TextBox ID="txtcode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td colspan="2">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlparty" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <strong>Address</strong>
                                                    </p>
                                                    <asp:TextBox ID="txtaddress" runat="server" Enabled="false" TextMode="MultiLine"
                                                        CssClass="form-control validate[required]"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr id="trauction" runat="server">
                                        <td>
                                            <p>
                                                <strong>Auction </strong>
                                            </p>
                                            <asp:DropDownList ID="ddlauction" runat="server" CssClass="form-control validate[required]"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlauction_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Auction Date</strong>
                                            </p>
                                            <asp:TextBox ID="txtauctiondate" runat="server" type="date" Enabled="false" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeyup="DateFormatValidation(this.id,this.value);" onkeypress="return isNumberWthOutDot(event);"></asp:TextBox>&nbsp;
                                        </td>
                                        <td>
                                        </td>
                                        <td>
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
                                        <td>
                                            <p>
                                                <strong>Coal Type </strong>
                                            </p>
                                            <asp:DropDownList ID="ddlcoaltype" runat="server" CssClass="form-control validate[required]"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlcoaltype_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        
                                        <td>
                                            <p>
                                                <strong>Coal Grade</strong>
                                            </p>
                                            <asp:DropDownList ID="ddlcoalgrade" Enabled="false" runat="server" CssClass="form-control validate[required]">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Quantity</strong>
                                            </p>
                                            <asp:TextBox ID="txtquantity" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Basic Charges</strong>
                                            </p>
                                            <asp:TextBox ID="txtbasiccharges" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Sizing Charges</strong>
                                            </p>
                                            <asp:TextBox ID="txtsizingcharges" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>STC</strong>
                                            </p>
                                            <asp:TextBox ID="txtstc" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Royalty</strong>
                                            </p>
                                            <asp:TextBox ID="txtroyalty" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>SED</strong>
                                            </p>
                                            <asp:TextBox ID="txtsed" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                Width="150px" onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Central Excise</strong>
                                            </p>
                                            <asp:TextBox ID="txtexcise" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Clean Eneryness</strong>
                                            </p>
                                            <asp:TextBox ID="txtcleanenergyness" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>MPGATSAVA Tax</strong>
                                            </p>
                                            <asp:TextBox ID="txtmpgatsava" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Transit Fee</strong>
                                            </p>
                                            <asp:TextBox ID="txttransit" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Entry Fee</strong>
                                            </p>
                                            <asp:TextBox ID="txtentryfee" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>TCS(1%)</strong>
                                            </p>
                                            <asp:TextBox ID="txttcs" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>VAT/CST</strong>
                                            </p>
                                            <asp:TextBox ID="txtvatcst" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>Total</strong>
                                            </p>
                                            <asp:TextBox ID="txttotal" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp; Payment Details
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
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lnkaddmember" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:Repeater ID="paymentrepeater" runat="server" OnItemDataBound="paymentrepeater_ItemDataBound">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 100%" border="0" cellspacing="5px" cellpadding="5px">
                                                                <tr>
                                                                    <td>
                                                                        <p>
                                                                            <strong>Payment Type</strong>
                                                                        </p>
                                                                        <asp:DropDownList ID="ddlpaymenttype" runat="server" CssClass="form-control validate[required]">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <p>
                                                                            <strong>Payment No</strong>
                                                                        </p>
                                                                        <asp:TextBox ID="txtpmtno" runat="server" Style="text-transform: lowercase;" Text='<%#Eval("PMTNO") %>'
                                                                            CssClass="form-control validate[required,custom[email]]"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <p>
                                                                            <strong>Payment Date</strong>
                                                                        </p>
                                                                        <asp:TextBox ID="txtpmtdate" runat="server" type="date" Style="text-transform: lowercase;" Text='<%#Eval("PMTDATE") %>'
                                                                          onkeyup="DateFormatValidation(this.id,this.value);" MaxLength="10" onkeypress="return isNumberWthOutDot(event);" CssClass="form-control validate[required,custom[email]]"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <p>
                                                                            <strong>Bank</strong>
                                                                        </p>
                                                                        <asp:DropDownList ID="ddlbank" runat="server" CssClass="form-control validate[required]">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <p>
                                                                            <strong>Amount</strong>
                                                                        </p>
                                                                        <asp:TextBox ID="txtamount" runat="server" Style="text-transform: lowercase;" Text='<%#Eval("PMTAMOUNT") %>'
                                                                            onkeypress="return isNumberWthDot(event, this.value)" CssClass="form-control validate[required,custom[email]]"></asp:TextBox>
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
                                    &nbsp; Terms & condition
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="chktermslist" runat="server" RepeatDirection="Vertical">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="box bordered-box blue-border">
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-danger"
                                OnClick="btnsubmit_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
