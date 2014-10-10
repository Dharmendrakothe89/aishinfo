<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="depotdetails.aspx.cs" Inherits="depotdetails" %>

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
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Depot Details
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
                                        <strong>Depot Name</strong>
                                    </p>
                                    <asp:TextBox ID="txtgodownname" runat="server" Style="text-transform: capitalize;"
                                        CssClass="form-control  validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Phone </strong>
                                    </p>
                                    <asp:TextBox ID="txtphone" runat="server" CssClass="form-control  validate[required]"
                                        MaxLength="16"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Fax </strong>
                                    </p>
                                    <asp:TextBox ID="txtfax" runat="server" MaxLength="16" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>E-mail </strong>
                                    </p>
                                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control  validate[required]"
                                     ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Address</strong>
                                    </p>
                                    <asp:TextBox ID="txtaddress" runat="server" Style="text-transform: capitalize;" CssClass="form-control  validate[required]"
                                        TextMode="MultiLine" ></asp:TextBox>
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
                                <td>
                                    <p>
                                        <strong>Pin </strong>
                                    </p>
                                    <asp:TextBox ID="txtpin" runat="server" CssClass="form-control  validate[required,custom[integer]]"
                                        maxlenght="6"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <p>
                                        <strong>Status</strong>
                                    </p>
                                    <asp:DropDownList ID="ddlstatus" runat="server" Enabled="false" AutoPostBack="false"
                                        CssClass="form-control validate[required]">
                                        <asp:ListItem Text="ACTIVE" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="DE-ACTIVE" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Concern Person
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr runat="server" id="traddmore" style="display:none;">
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                                                <asp:TextBox ID="txtpersonname" runat="server" Style="text-transform: capitalize;"
                                                                    Text='<%#Eval("PERSONNAME") %>' CssClass="form-control validate[required]" onkeypress=" return isCharacterWithSpace(event);"
                                                                    ></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Department</strong>
                                                                </p>
                                                                <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control validate[required]"
                                                                  >
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Designation</strong>
                                                                </p>
                                                                <asp:DropDownList ID="ddldesignation" runat="server" CssClass="form-control validate[required]"
                                                                >
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Email ID</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtperemailid" runat="server" Style="text-transform: lowercase;"
                                                                    Text='<%#Eval("EMAILID") %>' CssClass="form-control validate[required]"
                                                                 ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <p>
                                                                    <strong>Phone</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtperphone" runat="server" Text='<%#Eval("PHONENO") %>' CssClass="form-control validate[required]"
                                                                    MaxLength="16" ></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Mobile</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtpermobile" runat="server" Text='<%#Eval("MOBILE") %>' CssClass="form-control validate[required]"
                                                                    MaxLength="16"></asp:TextBox>
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
                <div>
                    <asp:Button ID="btnupdate" runat="server" Text="Submit" CssClass="btn btn-danger"
                        Visible="false" OnClick="btnupdate_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
