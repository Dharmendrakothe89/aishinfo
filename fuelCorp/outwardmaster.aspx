<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="outwardmaster.aspx.cs" Inherits="outwardmaster" %>

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
                    Outward Master</h2>
                <hr />
                <div class="box bordered-box blue-border">
                    <div class="box-header purple-background">
                        <div class="title">
                            &nbsp; Outward Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <p>
                                        <strong>Depot</strong>
                                    </p>
                                    <div>
                                        <asp:DropDownList ID="ddldepot" runat="server" AutoPostBack="false" CssClass="form-control  validate[required]"
                                           >
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>
                                    <p>
                                        <strong>DATE</strong>
                                    </p>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" onkeyup="DateFormatValidation(this.id,this.value);" MaxLength="10" onkeypress="return isNumberWthOutDot(event);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Coal Type</strong>
                                    </p>
                                    <asp:DropDownList ID="ddlcoaltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcoaltype_SelectedIndexChanged"
                                        CssClass="form-control  validate[required]">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <p>
                                        <strong>Coal Grade</strong>
                                    </p>
                                    <asp:DropDownList ID="ddlcoalgrade" runat="server" Enabled="false" AutoPostBack="false"
                                        CssClass="form-control  validate[required]">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <p>
                                        <strong>Party</strong>
                                    </p>
                                    <asp:DropDownList ID="ddlparty" runat="server" Enabled="true" AutoPostBack="false"
                                        CssClass="form-control  validate[required]">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <p>
                                        <strong>Destination</strong>
                                    </p>
                                    <asp:TextBox ID="txtdestination" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>Quantity</strong>
                                    </p>
                                    <asp:TextBox ID="txtquantity" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Transporter</strong>
                                    </p>
                                    <asp:DropDownList ID="ddltransporter" runat="server" AutoPostBack="true" CssClass="form-control  validate[required]"
                                        OnSelectedIndexChanged="ddltransporter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <p>
                                        <strong>Vehicle No</strong>
                                    </p>
                                    <asp:DropDownList ID="ddlvehicle" runat="server" Enabled="false" AutoPostBack="false"
                                        CssClass="form-control  validate[required]">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div>
                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
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
