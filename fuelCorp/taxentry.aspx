<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="taxentry.aspx.cs" Inherits="taxentry" %>

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
        function showdiv() {
            document.getElementById("<%=showterms.ClientID%>").style.display = "none";
            document.getElementById("<%=add.ClientID%>").style.display = "block";
            document.getElementById("<%=edit.ClientID%>").style.display = "none";

        }
        function FillState() {
            document.getElementById("<%=add.ClientID%>").style.display = "none";
            document.getElementById("<%=edit.ClientID%>").style.display = "none";
            document.getElementById("<%=showterms.ClientID%>").style.display = "block";

        }
        function EditState() {

            document.getElementById("<%=edit.ClientID%>").style.display = "block";
            document.getElementById("<%=add.ClientID%>").style.display = "none";
            document.getElementById("<%=showterms.ClientID%>").style.display = "none";

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper" style="margin-top: 20px;">
        
        <div class="row">
            <div class="col-sm-12">
                <div id="add" runat="server" class="box bordered-box blue-border" style="display: none">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp;Add Tax
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>Tax Name </strong>
                                    </p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttaxname" runat="server" CssClass="form-control validate[required]"
                                        ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Tax Value </strong>
                                    </p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttaxvalue" runat="server" CssClass="form-control validate[required]"
                                        onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Tax Unit </strong>
                                    </p>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlunit" runat="server" AutoPostBack="false"
                                        CssClass="form-control validate[required]">
                                        <asp:ListItem Text="rupees" Value="Rs"></asp:ListItem>
                                        <asp:ListItem Text="percentage" Value="%"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-danger" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div id="edit" runat="server" class="box bordered-box blue-border" style="display: none">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp;Edit Tax
                        </div>
                    </div>
                    <div class="box-content">
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>Tax Name </strong>
                                    </p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txteditname" runat="server" CssClass="form-control validate[required]"
                                   ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Tax Value </strong>
                                    </p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txteditvalue" runat="server" CssClass="form-control validate[required]"
                                        onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Tax Unit </strong>
                                    </p>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddleditunit" runat="server" AutoPostBack="false"
                                        CssClass="form-control validate[required]">
                                        <asp:ListItem Text="rupees" Value="Rs"></asp:ListItem>
                                        <asp:ListItem Text="percentage" Value="%"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <p>
                                        <strong>Status </strong>
                                    </p>
                                </td>
                                <td>
                                   <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="false"
                                        CssClass="form-control validate[required]">
                                        <asp:ListItem Text="ACTIVE" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="DE-ACTIVE" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnedit" runat="server" CssClass="btn btn-danger" Text="Submit" OnClick="btnedit_Click" />
                                </td>
                            </tr>
                        </table>
                        </ContentTemplate></asp:UpdatePanel>
                    </div>
                </div>
                <br />
                <div id="showterms" runat="server" class="row">
                    <div class="col-sm-12">
                        <div class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    Tax
                                </div>
                            </div>
                            <div class="box-content">
                                <asp:UpdatePanel ID="upbtn" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="0" border="0" cellspacing="0">
                                            <tr>
                                                <td align="right">
                                                  <input type="button" runat="server" id="Button1" title="Add Tax" value="Add Tax" class="btn btn-danger"
                                                        onclick="showdiv();" />
                                                   <%-- <asp:Button ID="lnkAddTrms" runat="server" CssClass="btn btn-danger" Text='Add Tax' OnClientClick="showdiv();">
                                                    </asp:Button>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:GridView ID="gvtax" runat="server" CellPadding="3" AllowPaging="true" PageSize="8"
                                                        AutoGenerateColumns="false" CssClass="Grid" OnPageIndexChanging="gvlookup_OnPageIndexChanging">
                                                        <HeaderStyle Height="50px" Font-Bold="true" />
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4"  FirstPageText="First" LastPageText="Last"/>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true"
                                                                ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkdetails" runat="server" Text='Edit Tax' ForeColor="#00acec" OnClick="lnkdetails_Click" CommandName="SRNO" CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="TAXNAME" HeaderText="TAXNAME" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                            <asp:BoundField DataField="TAXUNIT" HeaderText="TAXUNIT" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                            <asp:BoundField DataField="TAXVALUE" HeaderText="TAXVALUE" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                                <asp:BoundField DataField="STATUS" HeaderText="STATUS" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="1" width="900px" align="center" style="border: lightslategrey">
                                                                <tr style="height: 40px; background-color: lightslategrey">
                                                                    <td align="center" valign="middle">
                                                                        TAX NAME
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        TAX UNIT
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        TAX VALUE
                                                                    </td>
                                                                     <td align="center" valign="middle">
                                                                        TAX STATUS
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 30px">
                                                                    <td align="center" valign="top" colspan="5">
                                                                        NO TAX RECORD PRESENT
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <%-- <asp:UpdatePanel ID="uplist" runat="server">
                            <ContentTemplate>--%>
                                <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </div>
</asp:Content>
