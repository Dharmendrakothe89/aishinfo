<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="creategeneralvoucher.aspx.cs" Inherits="creategeneralvoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ValidationEngine.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdn.ucb.org.br/Scripts/formValidator/js/languages/jquery.validationEngine-en.js"
        charset="utf-8"></script>
    <script type="text/javascript" src="http://cdn.ucb.org.br/Scripts/formValidator/js/jquery.validationEngine.js"
        charset="utf-8"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SearchFirstText();
            SearchText();
        }); 
        function SearchFirstText() {

            debugger;

            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "creategeneralvoucher.aspx/GetAutoCompleteData",
                        data: "{'username':'" + document.getElementById('<%=txtfirstledger.ClientID%>').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);

                        },
                        error: function (result) {
                            alert("Error");
                            document.getElementById('<%=txtfirstledger.ClientID%>').value = "";
                        }
                    });
                }
            });
        }
        function SearchText() {

            debugger;

            $(".autosuggest1").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "creategeneralvoucher.aspx/GetAutoCompleteData",
                        data: "{'username':'" + document.getElementById('<%=txtsecondledger.ClientID%>').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);

                        },
                        error: function (result) {
                            alert("Error");
                            document.getElementById('<%=txtsecondledger.ClientID%>').value = "";
                        }
                    });
                }
            });
        }
        function FirstCheck() {
            document.getElementById('<%=hdnfirstledger.ClientID%>').value = document.getElementById('<%=txtfirstledger.ClientID%>').value.split('~')[1];

        }
        function Check() {
            document.getElementById('<%=hdnsecondledger.ClientID%>').value = document.getElementById('<%=txtsecondledger.ClientID%>').value.split('~')[1];

        }
    </script>
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
            <div runat="server" id="voucher" class="col-sm-12">
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
                                            <asp:DropDownList ID="ddlvouchertype" runat="server" AutoPostBack="true" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlvouchertype_SelectedIndexChanged">
                                                <asp:ListItem Text="General voucher" Value="5" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="center" colspan="2">
                                    <p>
                                        <strong>Date</strong>
                                    </p>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                        onkeyup="DateFormatValidation(this.id,this.value);" onkeypress="return isNumberWthOutDot(event);"
                                    ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <p>
                                        <strong>First Ledger</strong>
                                    </p>
                                    <asp:TextBox ID="txtfirstledger" runat="server" AutoComplete="off" Style="text-transform: capitalize;"
                                        placeholder="Enter Ledger Name" CssClass="autosuggest form-control" onblur="FirstCheck();"></asp:TextBox>
                                    <input type="hidden" id="hdnfirstledger" runat="server" />
                                </td>
                                <td colspan="2">
                                    <p>
                                        <strong>Second Ledger</strong>
                                    </p>
                                    <asp:TextBox ID="txtsecondledger" runat="server" AutoComplete="off" Style="text-transform: capitalize;"
                                        placeholder="Enter Ledger Name" CssClass="autosuggest1 form-control" onblur="Check();"></asp:TextBox>
                                    <input type="hidden" id="hdnsecondledger" runat="server" />
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
                                            <asp:DropDownList ID="ddlType" runat="server" Enabled="false" CssClass="form-control">
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
                                            <asp:TextBox ID="txtamount" runat="server" CssClass="form-control"
                                                AutoPostBack="true" OnTextChanged="txtamount_TextChanged"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                       
                                            <triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtamount" EventName="TextChanged" />
                                        </triggers>
                                         <ContentTemplate>
                                            <p>
                                                <strong>Amount (In Word)</strong>
                                            </p>
                                            <asp:TextBox ID="txtamtword" Style="text-transform: uppercase;" runat="server" CssClass="form-control"
                                                Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <p>
                                        <strong>Naraation</strong>
                                    </p>
                                    <asp:TextBox ID="txtnarration" runat="server" Style="text-transform: uppercase;"
                                        CssClass="form-control validate[required]" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-danger"
                            OnClick="btnsubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
