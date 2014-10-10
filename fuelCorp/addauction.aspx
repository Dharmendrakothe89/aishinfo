<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="addauction.aspx.cs" Inherits="addauction" %>

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
                    Create Auction</h2>
                <hr />
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Auction Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>Auction Name</strong>
                                    </p>
                                    <asp:TextBox ID="txtauctionname" runat="server" Style="text-transform: capitalize;"
                                        CssClass="form-control  validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Auction Date</strong>
                                    </p>
                                    <asp:TextBox ID="txtauctiondate" runat="server" onkeyup="DateFormatValidation(this.id,this.value);"
                                        MaxLength="10" onkeypress="return isNumberWthOutDot(event);" CssClass="form-control  validate[required]"
                                ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Description</strong>
                                    </p>
                                    <asp:TextBox ID="txtdescription" runat="server" Style="text-transform: capitalize;"
                                        TextMode="MultiLine" CssClass="form-control  validate[required]"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Start Date</strong>
                                    </p>
                                    <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control  validate[required]"
                                        MaxLength="10"  onkeypress="return isNumberWthOutDot(event);" ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>End Date </strong>
                                    </p>
                                    <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control  validate[required]"
                                        MaxLength="10"  onkeypress="return isNumberWthOutDot(event);" ></asp:TextBox>
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
                            &nbsp; Coal Specification
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td colspan="4" align="right">
                                    <asp:UpdatePanel ID="upaddmore" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton CommandName="Addmore" ID="lnkaddmember" runat="server" Text="Add More"
                                                ForeColor="#00acec" OnClick="lnkaddmember_Click"></asp:LinkButton>
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
                                            <asp:Repeater ID="coaltyperepeater" runat="server" OnItemDataBound="coaltyperepeater_ItemDataBound">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 100%" border="0" cellspacing="5px" cellpadding="5px">
                                                        <tr>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                                    <ContentTemplate>
                                                                        <p>
                                                                            <strong>Coal Type</strong>
                                                                        </p>
                                                                        <asp:DropDownList ID="ddlcoaltype" runat="server" CssClass="form-control validate[required]">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlcoaltype" EventName="SelectedIndexChanged" />
                                                                    </Triggers>
                                                                    <ContentTemplate>
                                                                        <p>
                                                                            <strong>Coal Grade</strong>
                                                                        </p>
                                                                        <asp:DropDownList ID="ddlcoalgrade" runat="server" CssClass="form-control validate[required]">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Rate/PMT</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtcoalrate" runat="server" CssClass="form-control validate[required]" onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Quantity</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtquantity" runat="server" CssClass="form-control validate[required]" onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Unit</strong>
                                                                </p>
                                                                <asp:DropDownList ID="ddlquantityunit" runat="server" CssClass="form-control validate[required]">
                                                                    <asp:ListItem Text="Per Month" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Per Year" Value="1"></asp:ListItem>
                                                                </asp:DropDownList>
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
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-danger"
                        OnClick="btnsubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
