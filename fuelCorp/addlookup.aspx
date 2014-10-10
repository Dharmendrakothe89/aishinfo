<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="addlookup.aspx.cs" Inherits="addlookup" %>

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
            document.getElementById("<%=add.ClientID %>").style.display = "block";
            document.getElementById("<%=showmaster.ClientID %>").style.display = "none";
            document.getElementById("<%=edit.ClientID%>").style.display = "none";

        }
        function showlist() {
            document.getElementById("<%=add.ClientID %>").style.display = "none";
            document.getElementById("<%=showmaster.ClientID %>").style.display = "block";
            document.getElementById("<%=edit.ClientID %>").style.display = "none";
        }
        function editdiv() {
            document.getElementById("<%=add.ClientID %>").style.display = "none";
            document.getElementById("<%=showmaster.ClientID %>").style.display = "none";
            document.getElementById("<%=edit.ClientID %>").style.display = "block";
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
                            &nbsp;Master Entry
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>Category </strong>
                                    </p>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="false" CssClass="form-control validate[required]"
                                 >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Name </strong>
                                    </p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtname" runat="server" style="text-transform:uppercase;" CssClass="form-control validate[required]"
                                      ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Value </strong>
                                    </p>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtvalue" runat="server" CssClass="form-control validate[required]"
                                        onkeypress="return isNumberWthDot(event, this.value)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-danger" Text="Submit" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="edit" runat="server" class="box bordered-box blue-border" style="display: none">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp;Edit Master Entry
                        </div>
                    </div>
                    <div class="box-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>Category </strong>
                                            </p>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddleditcategory" runat="server" AutoPostBack="false" CssClass="form-control validate[required]"
                                               >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>Name </strong>
                                            </p>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txteditname" runat="server" style="text-transform:uppercase;" CssClass="form-control validate[required]"
                                                onkeypress=" return isCharacterWithSpace(event);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>Value </strong>
                                            </p>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txteditvalue" runat="server" CssClass="form-control validate[required]"
                                                onkeypress="return isNumberWthDot(event, this.value)" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>Status</strong>
                                            </p>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="false" CssClass="form-control validate[required]"
                                             >
                                                <asp:ListItem Text="ACTIVE" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="DE-ACTIVE" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="btnedit" runat="server" CssClass="btn btn-danger" Text="Update" OnClick="btnedit_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div id="showmaster" runat="server" class="row">
                    <div class="col-sm-12">
                        <div class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    Master
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                    <tr>
                                        <td align="right" valign="top">
                                            <input type="button" runat="server" id="lnkAddTrms" title="Add Lookup" value="Add Lookup"
                                                class="btn btn-danger" onclick="showdiv();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="uplist" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvlookup" runat="server" CellPadding="3" AllowPaging="true" PageSize="8"
                                                        AutoGenerateColumns="false" CssClass="Grid" OnPageIndexChanging="gvlookup_OnPageIndexChanging"
                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                        <HeaderStyle Height="50px" Font-Bold="true" />
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First"
                                                            LastPageText="Last" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true"
                                                                ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkdetails" runat="server" Text='Detail' ForeColor="#00acec" OnClick="lnkdetails_Click"
                                                                        CommandName="SRNO" CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CATEGORY" HeaderText="CATEGORY" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                            <asp:BoundField DataField="NAME" HeaderText="NAME" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                            <asp:BoundField DataField="VALUE" HeaderText="VALUE" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                            <asp:BoundField DataField="STATUS" HeaderText="STATUS" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="1" width="900px" align="center" style="border: lightslategrey">
                                                                <tr style="height: 40px; background-color: lightslategrey">
                                                                    <td align="center" valign="middle">
                                                                        SRNO
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        NAME
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        HEADING
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        STATUS
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 30px">
                                                                    <td align="center" valign="top" colspan="5">
                                                                        NO LOOKUP=MASTER RECORD PRESENT
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
