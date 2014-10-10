<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="cashbook.aspx.cs" Inherits="cashbook" %>

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
                    <div class="box-header grey-background">
                        <div class="title">
                            &nbsp;Cash Book
                        </div>
                    </div>
                    <div class="box-content">
                        <asp:UpdatePanel ID="uplist" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" border="0" width="900px" align="center" style="border: lightslategrey">
                                    <tr style="height: 40px; background-color: lightslategrey">
                                        <td align="center" valign="middle">
                                            FROM DATE &nbsp &nbsp<asp:TextBox ID="txtfromdate" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="center" valign="middle">
                                            TO DATE &nbsp &nbsp
                                            <asp:TextBox ID="txttodate" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="center" valign="middle">
                                            <asp:Button ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvcashbook" runat="server" CellPadding="3" AllowPaging="true" PageSize="7"
                                    AutoGenerateColumns="false" CssClass="Grid" PagerStyle-CssClass="pgr" OnPageIndexChanging="gvcashbook_OnPageIndexChanging" AlternatingRowStyle-CssClass="alt">
                                    <HeaderStyle Height="50px" Font-Bold="true" />
                                    <PagerSettings Mode="NextPrevious" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TRANSDATE" HeaderText="DATE" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                        <asp:BoundField DataField="LEDGER" HeaderText="LEDGER" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                        <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                        <asp:BoundField DataField="LTRNTYPE" HeaderText="LTRN TYPE" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                        <asp:BoundField DataField="VOUCHERTYPE" HeaderText="VOUCHER TYPE" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                        <asp:BoundField DataField="NARRATION" HeaderText="NARRATION" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                        <asp:BoundField DataField="BALANCE" HeaderText="BALANCE" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table cellpadding="0" cellspacing="0" border="1" width="900px" align="center" style="border: lightslategrey">
                                            <tr style="height: 40px; background-color: lightslategrey">
                                                <td align="center" valign="middle">
                                                    SR NO
                                                </td>
                                                <td align="center" valign="middle">
                                                    DATE
                                                </td>
                                                <td align="center" valign="middle">
                                                    LEDGER
                                                </td>
                                                <td align="center" valign="middle">
                                                    AMOUNT
                                                </td>
                                                <td align="center" valign="middle">
                                                    LTRN TYPE
                                                </td>
                                                <td align="center" valign="middle">
                                                    VOUCHER TYPE
                                                </td>
                                                <td align="center" valign="middle">
                                                    NARRATION
                                                </td>
                                                <td align="center" valign="middle">
                                                    BALANCE
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
    </div>
</asp:Content>
