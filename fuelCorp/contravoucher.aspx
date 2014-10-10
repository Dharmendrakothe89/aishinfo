<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="contravoucher.aspx.cs" Inherits="contravoucher" %>

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
                    Voucher Form
                </h2>
                <hr />
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Voucher Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td align="left" colspan="2">
                                    <p>
                                        <strong>Voucher Type</strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlvouchertype" runat="server" AutoPostBack="true" CssClass="form-control validate[required]"
                                                OnSelectedIndexChanged="ddlvouchertype_SelectedIndexChanged">
                                                <asp:ListItem Text="ContraBank Payment" Value="4" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="ContraBank Receive" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="center" colspan="2">
                                    <p>
                                        <strong>Date</strong>
                                    </p>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                         onkeyup="DateFormatValidation(this.id,this.value);" onkeypress="return isNumberWthOutDot(event);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <p>
                                        <strong>Bank Ledger</strong>
                                    </p>
                                    <input type="hidden" id="hdnfirstledger" runat="server" />
                                    <asp:DropDownList ID="ddlbank" runat="server" AutoPostBack="true" CssClass="form-control validate[required]">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <p>
                                        <strong>Ledger</strong>
                                    </p>
                                    <input type="hidden" id="hdnsecondledger" runat="server" />
                                    <asp:TextBox ID="txtsecondledger" runat="server" Enabled="false" CssClass="form-control validate[required]"></asp:TextBox>
                                    <input type="hidden" id="hdnSRNO_REL2" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Type</strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlvouchertype" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlType" runat="server" Enabled="false" CssClass="form-control  validate[required] ">
                                                <asp:ListItem Value="1">Debit</asp:ListItem>
                                                <asp:ListItem Value="2">Credit</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <p>
                                                <strong>Amount</strong>
                                            </p>
                                            <asp:TextBox ID="txtamount" runat="server" CssClass="form-control  validate[required]"
                                                AutoPostBack="true" OnTextChanged="txtamount_TextChanged"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtamount" EventName="TextChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <p>
                                                <strong>Amount (In Word)</strong>
                                            </p>
                                            <asp:TextBox ID="txtamtword" runat="server" Style="text-transform: uppercase;" CssClass="form-control  validate[required]"
                                                Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <p>
                                        <strong>Narration</strong></p>
                                    <asp:TextBox ID="txtnarration" runat="server" Style="text-transform: uppercase;"
                                        CssClass="form-control  validate[required]" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-danger"
                            OnClick="btnsubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
