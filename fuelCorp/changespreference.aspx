<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="changespreference.aspx.cs" Inherits="changespreference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function showdiv() {

            document.getElementById("<%=divprefference.ClientID%>").style.display = "block";
            document.getElementById("<%=divlist.ClientID%>").style.display = "none";

        }
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
                    My Account
                </h2>
                <hr />
                <div runat="server" id="divlist" class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Login Details
                        </div>
                    </div>
                    <input type="button" id="btn" Value="Change Preferences" onclick="showdiv();"/>
                    <br />
                    <div class="box-content">
                        <table cellpadding="2" cellspacing="2" border="0">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="uplist" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvcompanylist" runat="server" CellPadding="3" AutoGenerateColumns="false"
                                                CssClass="Grid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                <HeaderStyle Height="50px" Font-Bold="true" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="COMPANYNAME" HeaderText="COMPANY NAME" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                    <asp:BoundField DataField="ABBRIVATION" HeaderText="COMPANY ABBRIVATION" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                    <asp:BoundField DataField="REGISTRATIONDATE" HeaderText="REGISTRATION DATE" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                    <asp:BoundField DataField="PREFFERENCE" HeaderText="PREFFERED" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="1" width="900px" align="center" style="border: lightslategrey">
                                                        <tr style="height: 40px; background-color: lightslategrey">
                                                            <td align="center" valign="middle">
                                                                SRNO
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                COMPANY NAME
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                COMPANY ABBRIVATION
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                REGISTRATION DATE
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                PREFFERED
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
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvbranchlist" runat="server" CellPadding="3" AllowPaging="true"
                                                PageSize="8" AutoGenerateColumns="false" CssClass="Grid" OnPageIndexChanging="gvbranchlist_OnPageIndexChanging"
                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                <HeaderStyle Height="50px" Font-Bold="true" />
                                                <PagerSettings Mode="NextPrevious" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BRANCHNAME" HeaderText="BRANCH NAME" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                    <asp:BoundField DataField="CMPNAME" HeaderText="COMPANY NAME" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                    <asp:BoundField DataField="CITYNAME" HeaderText="CITY NAME" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                    <asp:BoundField DataField="PREFFERENCE" HeaderText="PREFFRED" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="1" width="900px" align="center" style="border: lightslategrey">
                                                        <tr style="height: 40px; background-color: lightslategrey">
                                                            <td align="center" valign="middle">
                                                                SRNO
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                BRANCH NAME
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                COMPANY NAME
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                CITY NAME
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                PREFFRED
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 30px">
                                                            <td align="center" valign="top" colspan="5">
                                                                NO RECORD PRESENT
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
                <div runat="server" id="divprefference" style="display: none;" class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Change Prefference
                        </div>
                    </div>
                    <br />
                    <div class="box-content">
                        <table cellpadding="2" cellspacing="2" border="0">
                            <tr>
                                <td style="height: 50px">
                                    Company
                                </td>
                            </tr>
                            <tr>  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                <td>
                                    <asp:RadioButtonList ID="rdcmplist" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                        RepeatColumns="3" OnSelectedIndexChanged="rdcmplist_SelectedIndexChanged">
                                    </asp:RadioButtonList>
                                </td>
                                </ContentTemplate></asp:UpdatePanel>
                            </tr>
                            <tr>
                                <td style="height: 50px">
                                    Branch
                                </td>
                            </tr>
                            <tr>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rdcmplist" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                <td>
                                    <asp:RadioButtonList ID="rdbranchlist" runat="server" AutoPostBack="false" RepeatDirection="Horizontal"
                                        RepeatColumns="3">
                                    </asp:RadioButtonList>
                                </td>
                                </ContentTemplate></asp:UpdatePanel>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnsubmit" Text="Submit" OnClick="btnsubmit_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
