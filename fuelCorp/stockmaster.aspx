<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="stockmaster.aspx.cs" Inherits="stockmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="page-wrapper" style="margin-top: 20px;">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <div class="box bordered-box blue-border">
                    <div class="box-header purple-background">
                        <div class="title">
                            &nbsp; Stock Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>FROM DATE</strong>
                                    </p>
                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>TO DATE</strong>
                                    </p>
                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Depot</strong>
                                    </p>
                                    <div>
                                        <asp:DropDownList ID="ddldepot" runat="server"  AutoPostBack="false" CssClass="form-control validate[required]"
                                        >
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-danger" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnback" runat="server" Text="Search" CssClass="btn btn-danger" Visible="false" OnClick="btnback_Click" />

                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvstock" runat="server" AutoGenerateColumns="false" CellPadding="5"
                                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvstock_PageIndexChanging"
                                        Width="100%">
                                        <HeaderStyle BackColor="Silver" ForeColor="White" Height="10px" />
                                        <Columns>
                                        <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkselect" runat="server" Text='Select' CommandName="ITEMCLICK"
                                                        CommandArgument='<%# String.Format("{0} - {1}",Eval("COALID"),Eval("GRADEID"))%>' OnClick="lnkselect_Click"  CssClass="ITEM"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="COAL TYPE">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkcoaltype1" runat="server" Text='<%#Eval("COALTYPE") %>' CommandName="ITEMCLICK"
                                                        CommandArgument='<%#Eval("COALTYPE") %>'  CssClass="ITEM"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="COAL GRADE">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk2" runat="server" Text='<%#Eval("GRADE") %>' CommandName="ITEMCLICK"
                                                        CommandArgument='<%#Eval("GRADE") %>'  CssClass="ITEM"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OPENING" HeaderText="OPENING STOCK" />
                                            <asp:BoundField DataField="INWARD" HeaderText="INWARD QTY" />
                                            <asp:BoundField DataField="OUTWARD" HeaderText="OUTWARD QTY" />
                                            <asp:BoundField DataField="BALANCE" HeaderText="CLOSING STOCK" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center">
                                                <tr style="background-color: Silver; height: 20px;">
                                                    <td align="left" valign="top">
                                                        COAL TYPE
                                                    </td>
                                                    <td align="left" valign="top">
                                                        COAL GRADE
                                                    </td>
                                                   
                                                    <td align="left" valign="top">
                                                        OPENING STOCK
                                                    </td>
                                                    <td align="left" valign="top">
                                                        INWARDQTY
                                                    </td>
                                                    <td align="left" valign="top">
                                                        OUTWARDQTY
                                                    </td>
                                                    <td align="left" valign="top">
                                                        STOCK
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" valign="top" colspan="6">
                                                        No records Found
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:GridView ID="gvcoaldetailsstock" runat="server" AutoGenerateColumns="false" CellPadding="5"
                                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvcoaldetailsstock_PageIndexChanging"
                                       Visible="false"  Width="100%">
                                        <HeaderStyle BackColor="Silver" ForeColor="White" Height="10px" />
                                        <Columns>
                                        <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkselect" runat="server" Text='Detail' CommandName="ITEMCLICK"
                                                        CommandArgument='<%#Eval("SRNO") %>'  CssClass="ITEM"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="COAL TYPE">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkcoaltype" runat="server" Text='<%#Eval("COALTYPE") %>' CommandName="ITEMCLICK"
                                                        CommandArgument='<%#Eval("COALTYPE") %>'  CssClass="ITEM"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="COAL GRADE">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkgrade" runat="server" Text='<%#Eval("GRADE") %>' CommandName="ITEMCLICK"
                                                        CommandArgument='<%#Eval("GRADE") %>'  CssClass="ITEM"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DATE" HeaderText="DATE" />
                                            <asp:BoundField DataField="INWARD" HeaderText="INWARD QTY" />
                                            <asp:BoundField DataField="OUTWARD" HeaderText="OUTWARD QTY" />
                                            <asp:BoundField DataField="TRANSPORTERNAME" HeaderText="TRANSPORTER NAME" />
                                            <asp:BoundField DataField="VEHICLENAME" HeaderText="VEHICLE NAME" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center">
                                                <tr style="background-color: Silver; height: 20px;">
                                                    <td align="left" valign="top">
                                                        
                                                    </td>
                                                    <td align="left" valign="top">
                                                        COAL TYPE
                                                    </td>
                                                    <td align="left" valign="top">
                                                        COAL GRADE
                                                    </td>
                                                   
                                                    <td align="left" valign="top">
                                                       DATE
                                                    </td>
                                                    <td align="left" valign="top">
                                                        INWARD QTY
                                                    </td>
                                                    <td align="left" valign="top">
                                                        OUTWARD QTY
                                                    </td>
                                                    <td align="left" valign="top">
                                                        TRANSPORTER NAME
                                                    </td>
                                                    <td align="left" valign="top">
                                                        VEHICLE NAME
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" valign="top" colspan="8">
                                                        No records Found
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
