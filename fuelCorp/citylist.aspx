<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="citylist.aspx.cs" Inherits="citylist" %>

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
        function AddVehicle() {
            document.getElementById("<%=divedit.ClientID%>").style.display = "none";
            document.getElementById("<%=divadd.ClientID%>").style.display = "block";
            document.getElementById("<%=divlist.ClientID%>").style.display = "none";

        }
        function FillState() {
            document.getElementById("<%=divedit.ClientID%>").style.display = "none";
            document.getElementById("<%=divadd.ClientID%>").style.display = "none";
            document.getElementById("<%=divlist.ClientID%>").style.display = "block";

        }
        function EditState() {

            document.getElementById("<%=divedit.ClientID%>").style.display = "block";
            document.getElementById("<%=divadd.ClientID%>").style.display = "none";
            document.getElementById("<%=divlist.ClientID%>").style.display = "none";

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper" style="margin-top: 20px;">
        <div class="row">
            <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                    <div class="col-sm-12">
                        <div runat="server" id="divlist" class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp; City List
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                    <tr>
                                        <td align="right" valign="top">
                                            <input type="button" title="Add City" value="Add City" class="btn btn-danger" onclick="AddVehicle();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="uplist" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvstatelist" runat="server" CellPadding="3" AllowPaging="true"
                                                        PageSize="8" AutoGenerateColumns="false" CssClass="Grid" OnPageIndexChanging="gvlookup_OnPageIndexChanging"
                                                        AlternatingRowStyle-CssClass="alt">
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
                                                                    <asp:LinkButton ID="lnkedit" runat="server" Text='Edit City' ForeColor="#00acec" CommandArgument='<%#Eval("CITYID") %>'
                                                                        OnClick="lnkedit_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CITYNAME" HeaderText="CITY NAME" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                            <asp:BoundField DataField="STATENAME" HeaderText="STATE NAME" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" />
                                                            <asp:BoundField DataField="TYPE" HeaderText="TYPE" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" />
                                                            <asp:BoundField DataField="STATUS" HeaderText="STATUS" ItemStyle-VerticalAlign="Middle"
                                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0" width="900px" style="border: lightslategrey"
                                                                align="center">
                                                                <tr style="height: 40px; background-color: lightslategrey">
                                                                    <td>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        CITY NAME
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        STATE NAME
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        TYPE
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        STATUS
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 30px">
                                                                    <td align="center" valign="top" colspan="4">
                                                                        NO PARTY RECORD PRESENT
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
                        <div runat="server" id="divadd" class="box bordered-box blue-border" style="display: none;">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp; Add City
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>CITY NAME</strong>
                                            </p>
                                            <asp:TextBox ID="txtname" runat="server" Style="text-transform: uppercase;" CssClass="form-control validate[required]"
                                                ></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>STATE NAME</strong>
                                            </p>
                                            <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="false" CssClass="form-control validate[required]"
                                                >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <div>
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-danger" Text="Submit"
                                                    OnClientClick="AddVehicle();" OnClick="btnsubmit_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div runat="server" id="divedit" class="box bordered-box blue-border" style="display: none;">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp; Edit City
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>CITY NAME</strong>
                                            </p>
                                            <asp:TextBox ID="txteditcity" runat="server" Style="text-transform: uppercase;" CssClass="form-control validate[required]"
                                                ></asp:TextBox>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>STATE NAME</strong>
                                            </p>
                                            <asp:DropDownList ID="ddleditstate" runat="server" AutoPostBack="false" CssClass="form-control validate[required]"
                                                >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>State</strong>
                                            </p>
                                            <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="false" CssClass="form-control validate[required]"
                                                >
                                                <asp:ListItem Text="ACTIVE" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="DE-ACTIVE" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="left">
                                            <div>
                                                <asp:Button ID="btnedit" runat="server" Text="Submit" OnClientClick="EditState();"
                                                    CssClass="btn btn-danger" OnClick="btnedit_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
