<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="termsconditions.aspx.cs" Inherits="termsconditions" %>

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
            document.getElementById("<%=add.ClientID%>").style.display = "block";
            document.getElementById("<%=showterms.ClientID%>").style.display = "none";
           
        }
        function EditState() {
            document.getElementById("<%=add.ClientID%>").style.display = "none";
            document.getElementById("<%=showterms.ClientID %>").style.display = "none";
           
        }
        function Showlist() {
            document.getElementById("<%=add.ClientID %>").style.display = "none";
            document.getElementById("<%=showterms.ClientID %>").style.display = "block";
            
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
                        <div id="add" runat="server" class="box bordered-box blue-border" style="display: none">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp;Add Terms & Condition
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
                                                <strong>Sub Category </strong>
                                            </p>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlsubcategory" runat="server" AutoPostBack="false" CssClass="form-control validate[required]"
                                                >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>Terms </strong>
                                            </p>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtterms" runat="server" Style="text-transform: uppercase;" CssClass="form-control validate[required]"
                                                ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>Description </strong>
                                            </p>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtdesc" runat="server" Style="text-transform: capitalize;" TextMode="MultiLine"
                                                Height="150px" CssClass="form-control validate[required]"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-danger" Text="Submit" OnClientClick="showdiv();"
                                                OnClick="btnSave_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        
                        <div id="showterms" runat="server" class="row">
                            <div class="col-sm-12">
                                <div class="box bordered-box blue-border">
                                    <div class="box-header blue-background">
                                        <div class="title">
                                            &nbsp;Terms List
                                        </div>
                                    </div>
                                    <div class="box-content">
                                        <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                            <tr>
                                                <td align="right" valign="top">
                                                    <input type="button" runat="server" id="lnkAddTrms" title="Add" value="Add" class="btn btn-danger"
                                                        onclick="showdiv();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upbtwwwwwn" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvtermscondition" runat="server" CellPadding="3" AllowPaging="true"
                                                                PageSize="10" AutoGenerateColumns="false" CssClass="Grid" OnPageIndexChanging="gvtermscondition_OnPageIndexChanging"
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
                                                                            <asp:LinkButton ID="lnkedit" runat="server" Text='Edit Terms' ForeColor="#00acec" OnClick="lnkedit_Click"
                                                                                CommandName="SRNO" CommandArgument='<%#Eval("SRNO") %>' OnClientClick="EditState();"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="CATEGORY" HeaderText="CATEGORY" ItemStyle-VerticalAlign="Middle"
                                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                                    <asp:BoundField DataField="SUBCATEGORY" HeaderText="SUB CATEGORY" ItemStyle-VerticalAlign="Middle"
                                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                                    <asp:BoundField DataField="TERMS" HeaderText="TERMS" ItemStyle-VerticalAlign="Middle"
                                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                                    <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" ItemStyle-VerticalAlign="Middle"
                                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <table cellpadding="0" cellspacing="0" border="1" width="900px" align="center" style="border: lightslategrey">
                                                                        <tr style="height: 40px; background-color: lightslategrey">
                                                                            <td align="center" valign="middle">
                                                                                CATEGORY
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                TERMS
                                                                            </td>
                                                                            <td align="center" valign="middle">
                                                                                DESCRIPTION
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="height: 30px">
                                                                            <td align="center" valign="top" colspan="3">
                                                                                NO TERMS-CONDITION RECORD PRESENT
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
    </div>
</asp:Content>
