<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="createbank.aspx.cs" Inherits="createbank" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper" style="margin-top: 20px;">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <h2>
                    CREATE BANK ACCOUNT</h2>
                <hr />
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Bank Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>Account Name</strong>
                                    </p>
                                    <asp:TextBox ID="txtaccountname" style="text-transform:capitalize;" runat="server" CssClass="form-control  validate[required,onlyLetterSp[],minSize[6]]" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Account No</strong>
                                    </p>
                                    <asp:TextBox ID="txtactno" runat="server" MaxLength="20" onkeypress="return landline(event);" CssClass="form-control  validate[required,custom[integer]]" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>State</strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="true" CssClass="form-control  validate[required]"
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
                                                CssClass="form-control  validate[required]">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Bank Name</strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlbank" runat="server" AutoPostBack="false" CssClass="form-control  validate[required]"
                                                >
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <p>
                                        <strong>Branch Name </strong>
                                    </p>
                                    <asp:TextBox ID="txtbranchname" style="text-transform:capitalize;" runat="server" CssClass="form-control  validate[required]" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>IFSC Code</strong>
                                    </p>
                                    <asp:TextBox ID="txtifsccode" style="text-transform:uppercase;" MaxLength="20" runat="server" CssClass="form-control  validate[required]" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Address</strong>
                                    </p>
                                    <asp:TextBox ID="txtaddress" style="text-transform:capitalize;" runat="server" CssClass="form-control  validate[required]" TextMode="MultiLine"
                                        Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>MICR Code</strong>
                                    </p>
                                    <asp:TextBox ID="txtmicrcode" MaxLength="20" style="text-transform:uppercase;" runat="server" CssClass="form-control  validate[required]"></asp:TextBox>
                                </td>
                                <td colspan="3">
                                   
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-danger" Text="Submit" OnClick="btnsubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
