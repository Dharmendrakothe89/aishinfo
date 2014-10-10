<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="ledgerdetails.aspx.cs" Inherits="ledgerdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Styles/popupstyle.css" rel="stylesheet" type="text/css" />
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {

            debugger;

            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "createvoucher.aspx/GetAutoCompleteData",
                        data: "{'username':'" + document.getElementById('<%=txtsecondledger.ClientID%>').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);

                        },
                        error: function (result) {
                            alert("Error");
                            document.getElementById('<%=txtsecondledger.ClientID%>').value = "";
                        }
                    });
                }
            });
        }
        function Check() {
            document.getElementById('<%=hdnsecondledger.ClientID%>').value = document.getElementById('<%=txtsecondledger.ClientID%>').value.split('~')[1];

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper" style="margin-top: 20px;">
        <div class="row">
            <div class="col-sm-12">
                <div id="add" runat="server" class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp;Ledger List
                        </div>
                    </div>
                    <div class="box-content">
                        <table border="0" cellpadding="5" cellspacing="5" width="90%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtsecondledger" runat="server" AutoComplete="off" Style="text-transform: capitalize;"
                                        placeholder="Ledger Name" CssClass="autosuggest  form-control" onblur="Check();"></asp:TextBox>
                                    <input type="hidden" id="hdnsecondledger" runat="server" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnsubmit" runat="server" Text="Search" CssClass="btn btn-danger"
                                        OnClick="btnsubmit_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="uplist" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvledgerlist" runat="server" CellPadding="3" AllowPaging="true"
                                                PageSize="12" AutoGenerateColumns="false" CssClass="Grid" PagerStyle-CssClass="pgr"
                                                OnPageIndexChanging="gvlookup_OnPageIndexChanging" AlternatingRowStyle-CssClass="alt">
                                                <HeaderStyle Height="50px" Font-Bold="true" />
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First"
                                                    LastPageText="Last" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true"
                                                        ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkdetails" runat="server" Text='Detail' ForeColor="#00acec"
                                                                CommandArgument='<%#Eval("RELATIONSHIPID") %>' OnClick="lnkdetails_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NAME" HeaderText="NAME" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                    <asp:BoundField DataField="ASSOSIATEDFEILD" HeaderText="ASSOSIATED FEILD" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" />
                                                    <asp:BoundField DataField="BRANCHNAME" HeaderText="BRANCH NAME" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0" width="900px" style="border: lightslategrey"
                                                        align="center">
                                                        <tr style="height: 40px; background-color: lightslategrey">
                                                            <td align="center" valign="middle">
                                                                SR NO
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                DETAIL
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                NAME
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                ASSOSIATED FEILD
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                BRANCH NAME
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
            </div>
        </div>
    </div>
</asp:Content>
