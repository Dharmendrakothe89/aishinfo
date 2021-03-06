﻿<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="transactiondetails.aspx.cs" Inherits="transactiondetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
                            &nbsp;Transaction Details
                        </div>
                    </div>
                    <div class="box-content">
                        <asp:UpdatePanel ID="uplist" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center" style="border: lightslategrey">
                                    <tr style="height: 40px;">
                                        <td align="left" valign="middle">
                                            FROM DATE &nbsp &nbsp<asp:TextBox ID="txtfromdate" onkeyup="DateFormatValidation(this.id,this.value);" CssClass="form-control" onkeypress="return isNumberWthOutDot(event);" runat="server"></asp:TextBox>
                                           
                                        </td>
                                        <td align="left" valign="middle">
                                            TO DATE &nbsp &nbsp
                                            <asp:TextBox ID="txttodate" onkeyup="DateFormatValidation(this.id,this.value);" CssClass="form-control" onkeypress="return isNumberWthOutDot(event);" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="middle">
                                            <asp:Button ID="btnshow" runat="server" CssClass="btn btn-danger" Text="Show" OnClick="btnshow_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="gvcashbook" runat="server" CellPadding="3" AllowPaging="true" PageSize="7"
                                                AutoGenerateColumns="false" CssClass="Grid" PagerStyle-CssClass="pgr" OnPageIndexChanging="gvcashbook_OnPageIndexChanging"
                                                AlternatingRowStyle-CssClass="alt">
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
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
