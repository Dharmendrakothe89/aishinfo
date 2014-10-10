<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="vehicledetails.aspx.cs" Inherits="vehicledetails" %>

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
                <div runat="server" id="divadd" class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Edit Vehicle
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td align="right" colspan="4" valign="top">
                                    <asp:Button ID="btnedit" runat="server" CssClass="btn btn-danger" Text="Edit" OnClick="btnedit_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Transporter</strong>
                                    </p>
                                    <asp:DropDownList ID="ddltransporter" runat="server" Enabled="false" AutoPostBack="false" CssClass="form-control validate[required]"
                                        onkeypress=" return isCharacterWithSpace(event);" >
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <p>
                                        <strong>Vehicle Name</strong>
                                    </p>
                                    <asp:TextBox ID="txtvehiclename" runat="server" style="text-transform:uppercase;" Enabled="false" CssClass="form-control validate[required]"
                                     ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>vehicle No</strong>
                                    </p>
                                    <asp:TextBox ID="txtvehicleno" runat="server" style="text-transform:uppercase;" Enabled="false" CssClass="form-control validate[required]"
                                       ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Capacity</strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlcapacity" Enabled="false" runat="server" AutoPostBack="true" CssClass="form-control"
                                            >
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <p>
                                        <strong>State</strong>
                                    </p>
                                    <asp:DropDownList ID="ddlstatus" runat="server" Enabled="false" AutoPostBack="false"
                                        CssClass="form-control validate[required]" >
                                        <asp:ListItem Text="ACTIVE" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="DE-ACTIVE" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnsubmit" Visible="false" runat="server" Text="Submit" CssClass="btn btn-danger"
                        OnClick="btnsubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
