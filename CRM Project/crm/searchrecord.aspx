<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="searchrecord.aspx.cs" Inherits="searchrecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/Commonvalidation.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtfromdate.ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
        $(document).ready(function () {
            $("#<%=txttodate.ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header_text sub" style="margin-top: 15px;">
        <img class="pageBanner" src="img/pageBanner.png" alt="New products" />
        <h4>
            <span>Search Prospect</span></h4>
    </div>
    <div class="main-content">
        <div class="row">
            <div class="span7" style="width: 96%;">
                <h4 class="title">
                    <span class="text"><strong>Record </strong>List</span></h4>
                <div>
                    <fieldset>
                        <div class="control-group">
                            <div class="controls">
                            <asp:TextBox ID="txtfromdate" runat="server" ReadOnly = "true"></asp:TextBox><img src="calender.png" />
                            <asp:TextBox ID="txttodate" runat="server" ReadOnly = "true"></asp:TextBox><img src="calender.png" />
                                <%--asp:TextBox ID="txtfromdate" runat="server" Style="text-transform: capitalize;"
                                    CssClass="form-control validate[required]" onkeyup="DateFormatValidation(this.id,this.value);"
                                    onblur="return checkdate(this.value,this.id);" onkeypress="return isNumberWthOutDot(event);"
                                    placeholder="dd/mm/yyyy"></asp:TextBox--%>
                                <%--asp:TextBox ID="txttodate" runat="server" Style="text-transform: capitalize;" CssClass="form-control validate[required]"
                                    onkeyup="DateFormatValidation(this.id,this.value);" onblur="return checkdate(this.value,this.id);"
                                    onkeypress="return isNumberWthOutDot(event);" placeholder="dd/mm/yyyy"></asp:TextBox--%>
                                <asp:TextBox ID="txtname" runat="server" Style="text-transform: capitalize;" CssClass="form-control validate[required]"
                                    placeholder="Name"></asp:TextBox>
                                <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                    CssClass="btn btn-inverse large" />
                            </div>
                        </div>
                        <div class="control-group">
                            <div class="controls">
                                <asp:GridView ID="grvExcelData" runat="server" Width="100%" CellPadding="3" AllowPaging="true"
                                    PageSize="10" AutoGenerateColumns="true" CssClass="Grid" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="gvlookup_OnPageIndexChanging">
                                    <HeaderStyle Height="50px" Font-Bold="true" BackColor="#D3CDCB" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First"
                                        LastPageText="Last" />
                                    <EmptyDataTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border: lightslategrey"
                                            align="center">
                                            <tr style="height: 40px; background-color:#D3CDCB">
                                                <td align="center" valign="middle">
                                                    SRNO
                                                </td>
                                                <td align="center" valign="middle">
                                                    NAME
                                                </td>
                                                <td align="center" valign="middle">
                                                    CONTACT NO
                                                </td>
                                                <td align="center" valign="middle">
                                                    ACTIVITY
                                                </td>
                                                <td align="center" valign="middle">
                                                    RESULT
                                                </td>
                                                <td align="center" valign="middle">
                                                    REMARK
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <td align="center" valign="top" colspan="6">
                                                    NO RECORD PRESENT
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                        <hr />
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
