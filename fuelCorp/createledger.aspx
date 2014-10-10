<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true" CodeFile="createledger.aspx.cs" Inherits="createledger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper" style="margin-top: 20px;">
        <br />
        <div class="row">
         <div class="col-sm-12">
                <h2>
                    Ledger Form - Create/Modify</h2>
                <hr />
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Ledger Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>Ledger Name</strong>
                                    </p>
                                    <asp:TextBox ID="txtledgername" runat="server" style="text-transform:capitalize;" CssClass="form-control validate[required]" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Ledger Feild</strong>
                                    </p>
                                    <asp:TextBox ID="txtfeild" runat="server" style="text-transform:capitalize;" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Group </strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlgroup" runat="server" AutoPostBack="true" CssClass="form-control validate[required]"
                                                OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <p>
                                        <strong>Sub Group</strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlgroup" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlsubgroup" runat="server" AutoPostBack="false" Enabled="false"
                                                CssClass="form-control ">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                              
                            </tr>
                             <tr>
                                <td>
                                    <p>
                                        <strong>Opening Amount</strong>
                                    </p>
                                    <asp:TextBox ID="txtamt" runat="server" CssClass="form-control validate[required,custom[integer]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Opening Type</strong>
                                    </p>
                                   <asp:RadioButton ID="rdcredit" runat="server" CssClass="radio radio-inline" Text="Credit"
                                            GroupName="ADDON" Checked="true" />
                                        <asp:RadioButton ID="rddebit" runat="server" CssClass="radio radio-inline" Text="Debit"
                                            GroupName="ADDON" />
                                </td>
                                <td colspan="2">
                                  
                                </td>
                           
                            </tr>
                         
                         
                        </table>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-danger"
                        OnClick="btnsubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

