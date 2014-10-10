<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="coaltypemaster.aspx.cs" Inherits="coaltypemaster" %>

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
        function showaddcoaldiv() {
            document.getElementById("<%=showmaster.ClientID%>").style.display = "none";
            document.getElementById("<%=divaddcoal.ClientID%>").style.display = "block";
            document.getElementById("<%=divaddgrade.ClientID%>").style.display = "none";
            document.getElementById("<%=diveditcoal.ClientID%>").style.display = "none";
            document.getElementById("<%=diveditgrade.ClientID%>").style.display = "none";

        }
        function showaddgradediv() {
            document.getElementById("<%=showmaster.ClientID%>").style.display = "none";
            document.getElementById("<%=divaddcoal.ClientID%>").style.display = "none";
            document.getElementById("<%=diveditcoal.ClientID%>").style.display = "none";
            document.getElementById("<%=diveditgrade.ClientID%>").style.display = "none";
            document.getElementById("<%=divaddgrade.ClientID%>").style.display = "block";
        }
        function showmaster() {
            document.getElementById("<%=showmaster.ClientID%>").style.display = "block";
            document.getElementById("<%=divaddcoal.ClientID%>").style.display = "none";
            document.getElementById("<%=divaddgrade.ClientID%>").style.display = "none";
            document.getElementById("<%=diveditcoal.ClientID%>").style.display = "none";
            document.getElementById("<%=diveditgrade.ClientID%>").style.display = "none";
        }

        function showeditcoaldiv() {
            document.getElementById("<%=showmaster.ClientID%>").style.display = "none";
            document.getElementById("<%=divaddcoal.ClientID%>").style.display = "none";
            document.getElementById("<%=divaddgrade.ClientID%>").style.display = "none";
            document.getElementById("<%=diveditcoal.ClientID%>").style.display = "block";
            document.getElementById("<%=diveditgrade.ClientID%>").style.display = "none";

        }
        function showeditgradediv() {
            document.getElementById("<%=showmaster.ClientID%>").style.display = "none";
            document.getElementById("<%=divaddcoal.ClientID%>").style.display = "none";
            document.getElementById("<%=diveditcoal.ClientID%>").style.display = "none";
            document.getElementById("<%=diveditgrade.ClientID%>").style.display = "block";
            document.getElementById("<%=divaddgrade.ClientID%>").style.display = "none";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper" style="margin-top: 20px;">
        <div class="row">
            <div class="col-sm-12">
                <div id="showmaster" runat="server" class="row">
                    <div class="col-sm-12">
                        <div class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp;Coal Master
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                    <tr>
                                        <td align="right" valign="top">
                                            <input type="button" title="Add Coal Type" value="Add Coal Type" class="btn btn-danger"
                                                onclick="showaddcoaldiv();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="uplist" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvcoaltype" runat="server" CellPadding="3" Width="100%" AllowPaging="true"
                                                        PageSize="5" AutoGenerateColumns="false" CssClass="Grid" OnPageIndexChanging="gvcoaltype_OnPageIndexChanging"
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
                                                                    <asp:LinkButton ID="lnkedit" runat="server" ForeColor="#00acec" Text='Edit Coal' OnClick="lnkedit_Click"
                                                                        CommandName="SRNO" CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="COALTYPE" HeaderText="COAL TYPE" ItemStyle-VerticalAlign="Middle"
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
                                                                        COAL TYPE
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        STATUS
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 30px">
                                                                    <td align="center" valign="top" colspan="3">
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
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp;Coal Grade Master
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                    <tr>
                                        <td align="right" valign="top">
                                            <input type="button" title="Add Grade" value="Add Grade" class="btn btn-danger" onclick="showaddgradediv();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvcoalgrade" runat="server" CellPadding="3" Width="100%" AllowPaging="true"
                                                        PageSize="5" AutoGenerateColumns="false" CssClass="Grid" OnPageIndexChanging="gvcoalgrade_OnPageIndexChanging"
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
                                                                    <asp:LinkButton ID="lnkgradeedit" runat="server" ForeColor="#00acec" Text='Edit Grade' OnClick="lnkgradeedit_Click"
                                                                        CommandName="SRNO" CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="GRADE" HeaderText="COAL GRADE" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                            <asp:BoundField DataField="COALTYPE" HeaderText="COAL TYPE" ItemStyle-VerticalAlign="Middle"
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
                                                                        COAL TYPE
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        COAL GRADE
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        STATUS
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 30px">
                                                                    <td align="center" valign="top" colspan="4">
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
                <div id="divaddcoal" runat="server" style="display: none;" class="row">
                    <div class="col-sm-12">
                        <div class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    Coal Master
                                </div>
                            </div>
                            <div class="box-content">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td>
                                                    <p>
                                                        <strong>Coal Type </strong>
                                                    </p>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control validate[required]"
                                                        Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnsubmitcoaltype" runat="server" CssClass="btn btn-danger" Text="Submit"
                                                        OnClientClick="showmaster();" OnClick="btnsubmitcoaltype_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divaddgrade" runat="server" style="display: none;" class="row">
                    <div class="col-sm-12">
                        <div class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    Grade Master
                                </div>
                            </div>
                            <div class="box-content">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td>
                                                    <p>
                                                        <strong>Coal Type </strong>
                                                    </p>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlcoaltype" runat="server" Width="250px" CssClass="form-control validate[required]">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <p>
                                                        <strong>Coal Grade </strong>
                                                    </p>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtgrade" runat="server" CssClass="form-control validate[required]"
                                                        Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnsubmitgrade" runat="server" CssClass="btn btn-danger" Text="Submit" OnClientClick="showmaster();"
                                                        OnClick="btnsubmitgrade_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="diveditcoal" runat="server" style="display: none;" class="row">
                    <div class="col-sm-12">
                        <div class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    Edit Coal
                                </div>
                            </div>
                            <div class="box-content">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td>
                                                    <p>
                                                        <strong>Coal Type </strong>
                                                    </p>
                                                    <asp:TextBox ID="txteditcoaltype" runat="server" CssClass="form-control validate[required]"
                                                        Width="250px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <p>
                                                        <strong>Status</strong>
                                                    </p>
                                                    <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="false" CssClass="form-control validate[required]"
                                                        Width="250px">
                                                        <asp:ListItem Text="ACTIVE" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="DE-ACTIVE" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="txtbtnupdatecoal" runat="server" CssClass="btn btn-danger" Text="Submit" OnClick="txtbtnupdatecoal_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="diveditgrade" runat="server" style="display: none;" class="row">
                    <div class="col-sm-12">
                        <div class="box bordered-box blue-border">
                            <div class="box-header grey-background">
                                <div class="title">
                                    &nbsp;Edit Grade
                                </div>
                            </div>
                            <div class="box-content">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td>
                                                    <p>
                                                        <strong>Coal Type </strong>
                                                    </p>
                                                       <asp:DropDownList ID="ddleditcoaltype" runat="server" Width="250px" CssClass="form-control validate[required]">
                                                    </asp:DropDownList>
                                                </td>
                                              
                                            </tr>
                                            <tr>
                                                <td>
                                                    <p>
                                                        <strong>Coal Grade </strong>
                                                    </p>
                                                    <asp:TextBox ID="txteditgrade" runat="server" CssClass="form-control validate[required]"
                                                        Width="250px"></asp:TextBox>
                                                </td>
                                               
                                                <td>
                                                    <p style="display: inline;">
                                                        <strong>Status</strong>
                                                    </p>
                                                    <asp:DropDownList ID="ddlgradestatus" runat="server" AutoPostBack="false" CssClass="form-control validate[required]"
                                                        Width="250px">
                                                        <asp:ListItem Text="ACTIVE" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="DE-ACTIVE" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnupdategrade" runat="server" CssClass="btn btn-danger" Text="Submit" OnClientClick="showmaster();"
                                                        OnClick="btnupdategrade_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
