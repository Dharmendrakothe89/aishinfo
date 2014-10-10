<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="createuser.aspx.cs" Inherits="createuser" %>

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
        function AddUser() {
            document.getElementById("<%=divedit.ClientID%>").style.display = "none";
            document.getElementById("<%=divadd.ClientID%>").style.display = "block";
            document.getElementById("<%=divlist.ClientID%>").style.display = "none";

        }
        function EditUser() {
            document.getElementById("<%=divedit.ClientID%>").style.display = "block";
            document.getElementById("<%=divadd.ClientID%>").style.display = "none";
            document.getElementById("<%=divlist.ClientID%>").style.display = "none";

        }
        function ShowUser() {
            document.getElementById("<%=divedit.ClientID%>").style.display = "none";
            document.getElementById("<%=divadd.ClientID%>").style.display = "none";
            document.getElementById("<%=divlist.ClientID%>").style.display = "block";

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
                <h2>
                    User Form - Create</h2>
                <hr />
                <div runat="server" id="divlist" class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; User List
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="2" cellspacing="2" border="0" width="100%">
                            <tr>
                                <td align="right" valign="top">
                                    <input type="button" title="Add User" value="Add User" class="btn btn-danger" onclick="AddUser();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="uplist" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvuserlist" runat="server" CellPadding="3" AllowPaging="true" PageSize="8"
                                                AutoGenerateColumns="false" CssClass="Grid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                <HeaderStyle Height="50px" Font-Bold="true" />
                                                <PagerSettings Mode="NextPrevious" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true"
                                                        ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkdetails" runat="server" Text='Edit Tax' OnClick="lnkdetails_Click"
                                                                CommandName="SRNO" CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="USERNAME" HeaderText="USER NAME" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                                    <asp:BoundField DataField="USERID" HeaderText="USER ID" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                                    <asp:BoundField DataField="DEPARTMENT" HeaderText="DEPARTMENT" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />
                                                    <asp:BoundField DataField="DESIGNATION" HeaderText="DESIGNATION" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                                    <asp:BoundField DataField="PASSWORD" HeaderText="PASSWORD" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0" width="900px" style="border: lightslategrey"
                                                        align="center">
                                                        <tr style="height: 40px; background-color: lightslategrey">
                                                            <td align="center" valign="middle">
                                                                USER NAME
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                USERID
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                DEPARTMENT
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                DESIGNATION
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                PASSWORD
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
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div runat="server" id="divadd" style="display: none;">
                    <div class="box bordered-box blue-border">
                        <div class="box-header blue-background">
                            <div class="title">
                                &nbsp; User Details
                            </div>
                        </div>
                        <div class="box-content">
                            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                <tr>
                                    <td>
                                        <p>
                                            <strong>User Name</strong>
                                        </p>
                                        <asp:TextBox ID="txtname" runat="server" Style="text-transform: capitalize;" CssClass="form-control validate[required]"
                                            onkeypress=" return isCharacterWithSpace(event);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p>
                                            <strong>Designation</strong>
                                        </p>
                                        <asp:DropDownList ID="ddldesignation" runat="server" CssClass="form-control validate[required]">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <p>
                                            <strong>Department</strong>
                                        </p>
                                        <div>
                                            <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control validate[required]">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td>
                                        <p>
                                            <strong>Gender</strong>
                                        </p>
                                        <div>
                                            <asp:RadioButton ID="rdfemale" runat="server" CssClass="radio radio-inline" Text="Female"
                                                GroupName="gender" Checked="true" />
                                            <asp:RadioButton ID="rdmale" runat="server" CssClass="radio radio-inline" Text="Male"
                                                GroupName="gender" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            <strong>Phone </strong>
                                        </p>
                                        <asp:TextBox ID="txtphone" runat="server" onkeypress="return landline(event);" CssClass="form-control validate[required]"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p>
                                            <strong>E-mail </strong>
                                        </p>
                                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control validate[required,custom[email]]"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p>
                                            <strong>User ID </strong>
                                        </p>
                                        <asp:TextBox ID="txtuserid" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="box bordered-box blue-border">
                        <div class="box-header blue-background">
                            <div class="title">
                                &nbsp; Company Access
                            </div>
                        </div>
                        <div class="box-content">
                            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                <tr>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <td>
                                                <asp:CheckBoxList ID="chkcmplist" runat="server" RepeatColumns="4" RepeatDirection="Vertical"
                                                    AutoPostBack="true" Width="100%" OnSelectedIndexChanged="chkcmplist_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                            </td>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="box bordered-box blue-border">
                        <div class="box-header blue-background">
                            <div class="title">
                                &nbsp; Branch Access
                            </div>
                        </div>
                        <div class="box-content">
                            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                <tr>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkcmplist" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <td>
                                                <asp:CheckBoxList ID="chkbranchlist" Enabled="false" runat="server" RepeatColumns="4"
                                                    RepeatDirection="Vertical" Width="100%">
                                                </asp:CheckBoxList>
                                            </td>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div>
                        <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" /></div>
                </div>
                <div runat="server" id="divedit" style="display: none;">
                    <div class="box bordered-box blue-border">
                        <div class="box-header blue-background">
                            <div class="title">
                                &nbsp; User Details
                            </div>
                        </div>
                        <div class="box-content">
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                <tr>
                                    <td>
                                        <p>
                                            <strong>User Name</strong>
                                        </p>
                                        <asp:TextBox ID="txteditname" runat="server" Style="text-transform: capitalize;"
                                            CssClass="form-control validate[required]" onkeypress=" return isCharacterWithSpace(event);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p>
                                            <strong>Designation</strong>
                                        </p>
                                        <asp:DropDownList ID="ddleditdesignation" runat="server" CssClass="form-control validate[required]">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <p>
                                            <strong>Department</strong>
                                        </p>
                                        <div>
                                            <asp:DropDownList ID="ddleditdepartment" runat="server" CssClass="form-control validate[required]">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td>
                                        <p>
                                            <strong>Gender</strong>
                                        </p>
                                        <div>
                                            <asp:RadioButton ID="rdeditmale" runat="server" CssClass="radio radio-inline" Text="Female"
                                                GroupName="gender" Checked="true" />
                                            <asp:RadioButton ID="rdeditfemale" runat="server" CssClass="radio radio-inline" Text="Male"
                                                GroupName="gender" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            <strong>Phone </strong>
                                        </p>
                                        <asp:TextBox ID="txteditphone" runat="server" onkeypress="return landline(event);"
                                            CssClass="form-control validate[required]"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p>
                                            <strong>E-mail </strong>
                                        </p>
                                        <asp:TextBox ID="txteditemail" runat="server" CssClass="form-control validate[required,custom[email]]"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p>
                                            <strong>User ID </strong>
                                        </p>
                                        <asp:TextBox ID="txtedituserid" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p>
                                            <strong>Password </strong>
                                        </p>
                                        <asp:TextBox ID="txteditpassword" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr> <td>
                                        <p>
                                            <strong>Status </strong>
                                        </p>
                                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="false" CssClass="form-control validate[required]"
                                        Width="250px">
                                        <asp:ListItem Text="ACTIVE" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="DE-ACTIVE" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    </td></tr>
                            </table>
                            </ContentTemplate></asp:UpdatePanel>
                        </div>
                    </div>
                    <div>
                        <asp:Button ID="btnedit" runat="server" Text="Update" CssClass="btn btn-danger" OnClick="btnedit_Click" /></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
