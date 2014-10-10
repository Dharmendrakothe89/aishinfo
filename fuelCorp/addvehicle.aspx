<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="addvehicle.aspx.cs" Inherits="addvehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ValidationEngine.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdn.ucb.org.br/Scripts/formValidator/js/languages/jquery.validationEngine-en.js"
        charset="utf-8"></script>
    <script type="text/javascript" src="http://cdn.ucb.org.br/Scripts/formValidator/js/jquery.validationEngine.js"
        charset="utf-8"></script>
    <script type="text/javascript">
        $(function () {
            $("#form1").validationEngine('attach', { promptPosition: "topRight" });
        });
    </script>
    <script type="text/javascript">
        function AddVehicle() {

            document.getElementById("<%=divadd.ClientID%>").style.display = "block";
            document.getElementById("<%=divlist.ClientID%>").style.display = "none";

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div id="page-wrapper" style="margin-top: 20px;">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <div runat="server" id="divlist" class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Vehicle List
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="2" cellspacing="2" border="0" width="100%">
                            <tr>
                                <td align="right" valign="top">
                                    <input type="button" title="Add Vehicle" class="btn btn-danger" value="Add Vehicle"
                                        onclick="AddVehicle();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="uplist" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvvehiclelist" runat="server" CellPadding="3" AllowPaging="true"
                                                PageSize="15" Width="90%" AutoGenerateColumns="false" CssClass="Grid" PagerStyle-CssClass="pgr"
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
                                                    <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true"
                                                        ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkedit" runat="server" Text='Edit Vehicle' ForeColor="#00acec"
                                                                CommandArgument='<%#Eval("VEHICLEID") %>' OnClick="lnkedit_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TRANSPORTERNAME" HeaderText="TRANSPORTER NAME" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                                    <asp:BoundField DataField="VEHICLENAME" HeaderText="VEHICLE NAME" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />
                                                    <asp:BoundField DataField="VEHICLENO" HeaderText="VEHICLE NO" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                                    <asp:BoundField DataField="CAPACITY" HeaderText="CAPACITY" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0" width="900px" style="border: lightslategrey"
                                                        align="center">
                                                        <tr style="height: 40px; background-color: lightslategrey">
                                                            <td align="center" valign="middle">
                                                                TRANSPORTER NAME
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                VEHICLE NAME
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                VEHICLE NO
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                CAPACITY
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                STATUS
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 30px">
                                                            <td align="center" valign="top" colspan="5">
                                                                NO PARTY RECORD PRESENT
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
                <div runat="server" id="divadd" class="box bordered-box blue-border" style="display: none;">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Add Vehicle
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>Transporter</strong>
                                    </p>
                                    <asp:DropDownList ID="ddltransporter" runat="server" AutoPostBack="false" CssClass="form-control validate[required]"
                                        onkeypress=" return isCharacterWithSpace(event);">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <p>
                                        <strong>Vehicle Name</strong>
                                    </p>
                                    <asp:TextBox ID="txtvehiclename" runat="server" CssClass="form-control validate[required]"
                                        Style="text-transform: uppercase;"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>vehicle No</strong>
                                    </p>
                                    <asp:TextBox ID="txtvehicleno" runat="server" CssClass="form-control validate[required]"
                                        Style="text-transform: uppercase;"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Capacity</strong>
                                    </p>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlcapacity" runat="server" AutoPostBack="true" CssClass="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="right">
                                    <div>
                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-danger"
                                            OnClick="btnsubmit_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
