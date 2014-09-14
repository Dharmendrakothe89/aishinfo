<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="memberlist.aspx.cs" Inherits="memberlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/gridstyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:ScriptManager ID="SC1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper">
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">
                    Member List
                </h1>
            </div>
            <div class="col-lg-10">
                <div class="panel panel-default">
                    <asp:UpdatePanel ID="uplist" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvmemberlist" runat="server" CellPadding="3" AllowPaging="true"
                                PageSize="15" AutoGenerateColumns="false" CssClass="Grid" PagerStyle-CssClass="pgr"
                                AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="gvlookup_OnPageIndexChanging">
                                <HeaderStyle Height="50px" Font-Bold="true" />
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First"
                                    LastPageText="Last" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NAME" HeaderText="MEMBER NAME" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="SEMICODE" HeaderText="SEMI CODE" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="SPONSORNAME" HeaderText="SPONSOR NAME" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="SPONSORSEMICODE" HeaderText="SPONSOR CODE" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="EMAILID" HeaderText="EMAILID" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="PHONENO" HeaderText="PHONENO" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="900px" style="border: lightslategrey"
                                        align="center">
                                        <tr style="height: 40px; background-color: lightslategrey">
                                            <td align="center" valign="middle">
                                                Sr.No.
                                            </td>
                                            <td align="center" valign="middle">
                                                MEMBER NAME
                                            </td>
                                            <td align="center" valign="middle">
                                                SEMI CODE
                                            </td>
                                            <td align="center" valign="middle">
                                                SPONSOR NAME
                                            </td>
                                            <td align="center" valign="middle">
                                                SPONSOR CODE
                                            </td>
                                            <td align="center" valign="middle">
                                                EMAILID
                                            </td>
                                            <td align="center" valign="middle">
                                                PHONENO
                                            </td>
                                            <td align="center" valign="middle">
                                                STATUS
                                            </td>
                                        </tr>
                                        <tr style="height: 30px">
                                            <td align="center" valign="top" colspan="8">
                                                NO PARTY RECORD PRESENT
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
