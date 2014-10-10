<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="branchlist.aspx.cs" Inherits="branchlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                            &nbsp; Branch List
                        </div>
                    </div>
                    <div class="box-content">
                        <asp:UpdatePanel ID="uplist" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvcontractlist" runat="server" CellPadding="3" AllowPaging="true" Width="100%"
                                    PageSize="15" AutoGenerateColumns="false" CssClass="Grid" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="gvlookup_OnPageIndexChanging">
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
                                                <asp:LinkButton ID="lnkdetails" runat="server" Text='Detail' ForeColor="#00acec" CommandArgument='<%#Eval("BRANCHID") %>'
                                                    OnClick="lnkdetails_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BRANCHNAME" HeaderText="BRANCH NAME" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="STATENAME" HeaderText="STATE NAME" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />
                                        <asp:BoundField DataField="CITYNAME" HeaderText="CITY NAME" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="STATUS" HeaderText="STATUS" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0" width="900px" style="border: lightslategrey"
                                            align="center">
                                            <tr style="height: 40px; background-color: lightslategrey">
                                                <td align="center" valign="middle">
                                                </td>
                                                <td align="center" valign="middle">
                                                   BRANCH NAME
                                                </td>
                                                <td align="center" valign="middle">
                                                    STATE NAME
                                                </td>
                                                <td align="center" valign="middle">
                                                    CITY NAME
                                                </td>
                                                <td align="center" valign="middle">
                                                    ADDRESS
                                                </td>
                                                <td align="center" valign="middle">
                                                    STATUS
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
