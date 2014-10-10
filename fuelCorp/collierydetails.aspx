<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="collierydetails.aspx.cs" Inherits="collierydetails" %>

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
                            &nbsp; Colliery Details
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
                                        <strong>Colliery Name</strong>
                                    </p>
                                    <asp:TextBox ID="txtcollieryname" runat="server" Style="text-transform: capitalize;" onkeypress=" return isCharacterWithSpace(event);"
                                        CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Colliery Code</strong>
                                    </p>
                                    <asp:TextBox ID="txtcollierycode" runat="server" Style="text-transform:uppercase;" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Region</strong>
                                    </p>
                                    
                                        <asp:RadioButton ID="rdnagpur" runat="server" CssClass="radio radio-inline" Text="Nagpur Depot"
                                            GroupName="REGION" Checked="true" />
                                        <asp:RadioButton ID="rdwani" runat="server" CssClass="radio radio-inline" Text="Wani Depot"
                                            GroupName="REGION" />
                                    
                                </td>
                                 <td>
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
                            &nbsp; Coal Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td colspan="3">
                                    <asp:Repeater ID="coaltyperepeater" runat="server" OnItemDataBound="coaltyperepeater_ItemDataBound">
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
                                                        ROYALTY
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
                                                        <asp:TextBox ID="txtgrade" runat="server" style="text-transform:uppercase;" CssClass="form-control"
                                                            Width="50px" Height="25px" Text='<%#Eval("GRADE") %>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtnotifiedprice" runat="server" CssClass="form-control"
                                                            Width="50px" Height="25px" Text='<%#Eval("NOTIFIEDPRICE") %>' onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                                    </td>
                                                    <td align="left" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtcommbenifitchrg" runat="server" Text='<%#Eval("COMMBENIFITCHARGES") %>' CssClass="form-control"
                                                            onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px" ></asp:TextBox>
                                                    </td>
                                                    <td align="left" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtcrushingcharges" runat="server" Text='<%#Eval("CRUSHINGCHARGES") %>' CssClass="form-control"
                                                            onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtstc" runat="server" Text='<%#Eval("STC") %>' CssClass="form-control"
                                                            onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtsed" runat="server" Text='<%#Eval("SED") %>' CssClass="form-control"
                                                            onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtcec" runat="server" Text='<%#Eval("CEC") %>' CssClass="form-control"
                                                            onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtroyalty" runat="server" Text='<%#Eval("ROYALTY") %>' CssClass="form-control"
                                                            onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtmprdtax" runat="server" Text='<%#Eval("MPRDTAX") %>' CssClass="form-control"
                                                            onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txttransitfee" runat="server" Text='<%#Eval("TRANSITTAX") %>' CssClass="form-control"
                                                            onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"></asp:TextBox>
                                                    </td>
                                                    <td align="center" valign="middle" style="width: 80px; padding-top: 7px;">
                                                        <asp:TextBox ID="txtentryfee" runat="server" Text='<%#Eval("ENTRYFEE") %>' CssClass="form-control"
                                                            onkeypress="return isNumberWthDot(event, this.value)" Width="50px" Height="25px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </div>
                  
                </div>
            </div>
            <div>
                <asp:Button ID="btnupdate" runat="server" Text="Submit" CssClass="btn btn-danger" Visible="false" OnClick="btnupdate_Click" />
            </div>
        </div>
    </div>
</asp:Content>
