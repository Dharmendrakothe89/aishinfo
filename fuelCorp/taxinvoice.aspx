<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="taxinvoice.aspx.cs" Inherits="taxinvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper" style="margin-top: 20px;">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <h2>
                    Party Form - Create/Modify</h2>
                <hr />
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Tax Invoice
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>Party Name</strong>
                                    </p>
                                    <%-- <asp:TextBox ID="txtpartyname" runat="server" Style="text-transform: capitalize;"
                                        CssClass="form-control validate[required]" onkeypress=" return isCharacterWithSpace(event);"></asp:TextBox>--%>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="false" Enabled="true"
                                        CssClass="form-control validate[required]"></asp:DropDownList>
                                </td>
                                <td>
                                    <p>
                                        <strong>Website </strong>
                                    </p>
                                    <asp:TextBox ID="txtwebsite" runat="server" Style="text-transform: lowercase;" CssClass="form-control validate[custom[url]]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Debit Note/Bill no</strong>
                                    </p>
                                    <asp:TextBox ID="txtpartycode" runat="server" Style="text-transform: uppercase;"
                                        CssClass="form-control valiadate[required,custom[onlyLetterNumber]]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Date</strong>
                                    </p>
                                    <div>
                                        <asp:DropDownList ID="ddlpartytype" runat="server" CssClass="form-control validate[required]">
                                            <asp:ListItem Selected="True" Text="--Party Type--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Linked Customer" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="E-Auction" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Open Market" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Direct Customer" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>
                                    <p>
                                        <strong>Add on Party</strong>
                                    </p>
                                    <div>
                                        <asp:RadioButton ID="rdaddonyes" runat="server" CssClass="radio radio-inline" Text="Yes"
                                            GroupName="ADDON" Checked="true" />
                                        <asp:RadioButton ID="rdaddonno" runat="server" CssClass="radio radio-inline" Text="No"
                                            GroupName="ADDON" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Ref / your order no </strong>
                                    </p>
                                    <asp:TextBox ID="txtphone" runat="server" onkeypress="return landline(event);" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Date </strong>
                                    </p>
                                    <asp:TextBox ID="txtfax" runat="server" CssClass="form-control "></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>TIN No </strong>
                                    </p>
                                    <asp:TextBox ID="txtemail" runat="server" Style="text-transform: lowercase;" CssClass="form-control validate[required,custom[email]]"></asp:TextBox>
                                </td>
                                
                            </tr>
                          
                        <%--    <tr>
                                <td>
                                    <p>
                                        <strong>EXCISE NO</strong>
                                    </p>
                                    <asp:TextBox ID="txtexciseno" runat="server" CssClass="form-control validate[required,custom[onlyLetterNumber]]"
                                        MaxLength="11"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>EXCISE RANGE</strong>
                                    </p>
                                    <asp:TextBox ID="txtexciserange" runat="server" CssClass="form-control validate[required]"
                                        MaxLength="11"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>EXCISE DIVISION</strong>
                                    </p>
                                    <asp:TextBox ID="txtexcisedivision" runat="server" CssClass="form-control validate[required]"
                                        MaxLength="11"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>--%>
                        </table>
                    </div>
                </div>
                <div class="box bordered-box blue-border">
                    <div class="box-header green-background">
                        <div class="title">
                            &nbsp; Party Member
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td colspan="4" align="right">
                                  
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                  
                                        <ContentTemplate>
                                          
                                                
                                                    <table style="width: 100%" border="0" cellspacing="5px" cellpadding="5px">
                                                        <tr>
                                                            <td>
                                                                <p>
                                                                    <strong>Despt Dt</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtdesptdt" runat="server" Style="text-transform: capitalize;"
                                                                    onkeypress=" return isCharacterWithSpace(event);" CssClass="form-control validate[required , custom[char]]"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Recd Dt</strong>
                                                                </p>
                                                               <asp:TextBox ID="txtrecddt" runat="server" Style="text-transform: capitalize;"
                                                                    onkeypress=" return isCharacterWithSpace(event);" CssClass="form-control validate[required , custom[char]]"></asp:TextBox>
                                                        
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Bilty/RR No</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtbilty" runat="server" Style="text-transform: capitalize;"
                                                                    onkeypress=" return isCharacterWithSpace(event);" CssClass="form-control validate[required , custom[char]]"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Truck/Container No</strong>
                                                                </p>
                                                                <asp:DropDownList ID="ddltruckno" runat="server" CssClass="form-control validate[required]">
                                                               <asp:ListItem Text="MH40/AA 1098"></asp:ListItem>
                                                                <asp:ListItem Text="MH40/AA 9108"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Dispatch Wieght</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtweight" runat="server" Style="text-transform: lowercase;"
                                                                    CssClass="form-control validate[required,custom[email]]"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <p>
                                                                    <strong>Rate</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtrate" runat="server" MaxLength="16" CssClass="form-control validate[required,min[8]]"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <p>
                                                                    <strong>Amount</strong>
                                                                </p>
                                                                <asp:TextBox ID="txtamount" runat="server" MaxLength="10" CssClass="form-control validate[required]"></asp:TextBox>
                                                            </td>
                                                            <td colspan="2">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                
                                       
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                
                                            <asp:LinkButton ID="lnkadd" runat="server" Text="Add More" ForeColor="#00acec"
                                                OnClick="lnkadd_Click"></asp:LinkButton>
                                        
                                </td>
                              <asp:GridView ID="gvinvoicelist" runat="server" CellPadding="3" AllowPaging="true"
                                    PageSize="8" AutoGenerateColumns="false"  CssClass="Grid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <HeaderStyle Height="50px" Font-Bold="true" />
                                    <PagerSettings Mode="NextPrevious" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true"
                                            ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkdetails" runat="server" Text='Detail' 
                                                    CommandArgument='<%#Eval("PARTYID") %>' OnClick="lnkdetails_Click" ></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="DesptDt" HeaderText="PARTY NAME" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="RecdDt" HeaderText="PARTY CODE" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />
                                        <asp:BoundField DataField="Biltno" HeaderText="STATE NAME" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="truckno" HeaderText="CITY NAME" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="DispatchWeigth" HeaderText="PHONE NO" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="Rate" HeaderText="EMAIL ID" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="Amount" HeaderText="WEBSITE" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0" width="900px" style=" border:lightslategrey" align="center">
                                            <tr style="height: 40px; background-color:lightslategrey">
                                                <td align="center" valign="middle">
                                                </td>
                                                <td align="center" valign="middle">
                                                  Despt No
                                                </td>
                                                <td align="center" valign="middle">
                                                   Recd No
                                                </td>
                                                <td align="center" valign="middle">
                                                   Bilty/RR No
                                                </td>
                                                <td align="center" valign="middle">
                                                 Truck / Container No
                                                </td>
                                                <td align="center" valign="middle">
                                                    Dispatch Weight
                                                </td>
                                                <td align="center" valign="middle">
                                                    Rate
                                                </td>
                                                <td align="center" valign="middle">
                                                    Ammount
                                                </td>
                                                
                                            </tr>
                                            <tr style="height: 30px">
                                                <td align="center" valign="top" colspan="9">
                                                    NO Invoice Record Added
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="box bordered-box blue-border">
                    <div class="box-header muted-background">
                        <div class="title">
                            &nbsp; Billing Parameter
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>VAT/Excise</strong>
                                    </p>
                                    <asp:RadioButton ID="rdvat" runat="server" GroupName="VATEXCISE" Checked="true" Text="VAT"
                                        CssClass="radio radio-inline" />
                                    <asp:RadioButton ID="rdexcise" runat="server" GroupName="VATEXCISE" Text="Excise"
                                        CssClass="radio radio-inline" />
                                </td>
                                <td>
                                    <p>
                                        <strong>Service Tax</strong>
                                    </p>
                                    <asp:RadioButton ID="rdserviceapplicable" runat="server" GroupName="SERVICETAX" Checked="true"
                                        CssClass="radio radio-inline" Text="Applicable" />
                                    <asp:RadioButton ID="rdservicenotapplicable" runat="server" GroupName="SERVICETAX"
                                        CssClass="radio radio-inline" Text="Not Applicable" />
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
