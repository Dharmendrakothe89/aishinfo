<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="superadminmemberlist.aspx.cs" Inherits="superadminmemberlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/gridstyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ShowApproval() {
            document.getElementById("<%=divapproval.ClientID%>").style.display = "block";
            document.getElementById("<%=divlist.ClientID%>").style.display = "none";
        }
        function Validatepassword() {
            var password = document.getElementById("<%=txtpassword.ClientID%>");
            if (password.value == "") {
                alert("Please generate password");
                return false;
            }
            else {
                return true;
            }
        }
    </script>
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
            <div runat="server" id="divlist" class="col-lg-10">
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
                                    <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true"
                                        ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkdetails" runat="server" Text='apporve' Width="63px" Height="21px" BackColor="#84B899" ForeColor="White" Font-Bold="true" OnClientClick="ShowApproval();"
                                                OnClick="lnkdetails_Click" CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="900px" style="border: lightslategrey"
                                        align="center">
                                        <tr style="height: 40px; background-color: lightslategrey">
                                            <td align="center" valign="middle">
                                            </td>
                                            <td align="center" valign="middle">
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
                                            <td align="center" valign="top" colspan="9">
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
            <div id="divapproval" style="display: none;" runat="server">
                <div class="col-lg-10">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="panel panel-default">
                                <div class="form-group form-group-sm" style="margin-top: 10px;">
                                    <label class="col-sm-2 control-label" for="formGroupInputSmall">
                                        Name</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtmemname" runat="server" Enabled="false" Style="text-transform: capitalize;"
                                            CssClass="form-control validate[required]" placeholder="Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-2 control-label" for="formGroupInputSmall">
                                        Email-ID</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtmememail" runat="server" Enabled="false" style="text-transform:lowercase;" CssClass="form-control validate[required]"
                                            placeholder="Email ID"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-2 control-label" for="formGroupInputSmall">
                                        Password</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtpassword" runat="server" Enabled="false" CssClass="form-control validate[required]"
                                            placeholder="Password"></asp:TextBox>&nbsp;<asp:LinkButton ID="lnkpassword" runat="server"
                                                Text="Generate Password" OnClick="lnkpassword_Click"></asp:LinkButton>
                                    </div>
                                </div>
                                <asp:Button ID="btnsubmit" runat="server" Style="margin-left: 90%; margin-bottom: 2%;"
                                    CssClass="btn btn-primary" Text="Submit" OnClick="btnsubmit_Click" OnClientClick="return Validatepassword();" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
