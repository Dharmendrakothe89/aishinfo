<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true" CodeFile="dolist.aspx.cs" Inherits="dolist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper" style="margin-top: 20px;">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; DO List
                        </div>
                    </div>
                    <div class="box-content">
                        <asp:UpdatePanel ID="uplist" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvdolist" runat="server" CellPadding="3" AllowPaging="true"
                                    PageSize="8" AutoGenerateColumns="false" CssClass="Grid" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt">
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
                                                <asp:LinkButton ID="lnkdetails" runat="server" Text='Detail' ForeColor="#00acec" CommandArgument='<%#Eval("DOID") %>'
                                                    OnClick="lnkdetails_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DOFOR" HeaderText="DO FOR" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="DONO" HeaderText="DO NO" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />
                                        <asp:BoundField DataField="DODATE" HeaderText="DO DATE" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="MONTH" HeaderText="MONTH" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="DESTINATION" HeaderText="DESTINATION" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0" width="900px" style="border: lightslategrey"
                                            align="center">
                                            <tr style="height: 40px; background-color: lightslategrey">
                                                <td align="center" valign="middle">
                                                </td>
                                                <td align="center" valign="middle">
                                                    DO FOR
                                                </td>
                                                <td align="center" valign="middle">
                                                    DO NO
                                                </td>
                                                <td align="center" valign="middle">
                                                    DO DATE
                                                </td>
                                                <td align="center" valign="middle">
                                                    MONTH
                                                </td>
                                                <td align="center" valign="middle">
                                                    DESTINATION
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <td align="center" valign="top" colspan="6">
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
    </div>
</asp:Content>

