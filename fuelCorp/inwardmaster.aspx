<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="inwardmaster.aspx.cs" Inherits="inwardmaster" %>

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
                    Inward Master</h2>
                <hr />
                <div class="box bordered-box blue-border">
                    <div class="box-header purple-background">
                        <div class="title">
                            &nbsp; Inward Details
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
                                    <asp:TextBox ID="txtdate" runat="server"  onkeyup="DateFormatValidation(this.id,this.value);" MaxLength="10" onkeypress="return isNumberWthOutDot(event);" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        <strong>DO NO</strong>
                                    </p>
                                    <asp:DropDownList ID="ddldo" runat="server" AutoPostBack="true" CssClass="form-control  validate[required]"
                                        OnSelectedIndexChanged="ddldo_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <p>
                                        <strong>DO DATE</strong>
                                    </p>
                                    <asp:TextBox ID="txtdodate" runat="server" Enabled="false" CssClass="form-control validate[required]"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Coal Type</strong>
                                    </p>
                                    <asp:DropDownList ID="ddlcoaltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcoaltype_SelectedIndexChanged"
                                        CssClass="form-control  validate[required]" Width="250px">
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
                                        <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-danger" Text="Submit" OnClick="btnsubmit_Click" />
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
