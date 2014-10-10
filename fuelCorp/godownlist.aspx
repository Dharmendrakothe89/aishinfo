<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="godownlist.aspx.cs" Inherits="godownlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link href="Styles/gridstyle.css" rel="stylesheet" type="text/css" />
<style type="text/css">
</style>
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
                            &nbsp; Depot List
                        </div>
                    </div>
                    <div class="box-content">
                        <asp:UpdatePanel ID="uplist" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvgodownlist" runat="server" AllowPaging="True"
                                    PageSize="15" AutoGenerateColumns="False"   CssClass="Grid" AlternatingRowStyle-CssClass="alt">
                                    <HeaderStyle Height="50px"/>
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4"  FirstPageText="First" LastPageText="Last"/>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true"
                                            ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkdetails" runat="server" Text='Detail' CommandName="Ledger" ForeColor="#00acec"
                                                    CommandArgument='<%#Eval("GODOWNID") %>' OnClick="lnkdetails_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Width="100px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="GODOWNNAME" HeaderText="GODOWNNAME" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" >
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CITYNAME" HeaderText="CITYNAME" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" >
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STATENAME" HeaderText="STATENAME" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" >
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PHONE" HeaderText="PHONE NO" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" >
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" >
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" >
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0" width="900px" style=" border:lightslategrey" align="center">
                                            <tr style="height: 40px; background-color:lightslategrey">
                                                <td align="center" valign="middle">
                                                </td>
                                                <td align="center" valign="middle">
                                                    GODOWN NAME
                                                </td>
                                                <td align="center" valign="middle">
                                                    CITY NAME
                                                </td>
                                                <td align="center" valign="middle">
                                                    STATE NAME
                                                </td>
                                                <td align="center" valign="middle">
                                                    PHONE NO
                                                </td>
                                                <td align="center" valign="middle">
                                                    EMAIL
                                                </td>
                                                <td align="center" valign="middle">
                                                    ADDRESS
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <td align="center" valign="top" colspan="7">
                                                    NO PARTY RECORD PRESENT
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                   <%-- <HeaderStyle BackColor="#999999" BorderStyle="None" Font-Bold="false" ForeColor="#ffffff" />--%>
                                    <PagerStyle CssClass="pgr" />
                                    <AlternatingRowStyle CssClass="alt" />
                                 </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
