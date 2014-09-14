<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="uploadexcel.aspx.cs" Inherits="uploadexcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript">

    function DisableUploadButton() {
        var password = document.getElementById("<%=btnImport.ClientID%>");
       // password.disabled = true;
    }
    function DisableSubmitButton() {
        var password = document.getElementById("<%=btnsubmit.ClientID%>");
       // password.disabled = true;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header_text sub" style="margin-top: 15px;">
        <img class="pageBanner" src="img/pageBanner.png" alt="New products" />
        <h4>
            <span>Upload Excel File</span></h4>
    </div>
    <div class="main-content">
        <div class="row">
            <div class="span5">
                <h4 class="title">
                    <span class="text"><strong>Upload</strong> Excel</span></h4>
                <div>
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label">
                                Date</label>
                            <div class="controls">
                                <asp:TextBox ID="txtdate" runat="server" Enabled="false" Style="text-transform: capitalize;"
                                    CssClass="form-control validate[required]" placeholder="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Select File</label>
                            <div class="controls">
                                <asp:FileUpload ID="fileuploadExcel" runat="server" />
                            </div>
                        </div>
                        <div class="control-group">
                            <asp:Button ID="btnImport" runat="server" Text="Upload Excel File" type="submit"
                                value="Sign into your account" OnClick="btnImport_Click" OnClientClick="DisableUploadButton();" CssClass="btn btn-inverse large" />
                            <hr />
                        </div>
                    </fieldset>
                </div>
            </div>
            <div class="span7">
                <h4 class="title">
                    <span class="text"><strong>Record </strong>List</span></h4>
                <div>
                    <fieldset>
                        <div class="control-group">
                            <div class="controls">
                                <asp:GridView ID="grvExcelData" runat="server" Width="100%" CellPadding="3" AllowPaging="true"
                                    PageSize="15" AutoGenerateColumns="true" CssClass="Grid" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="gvlookup_OnPageIndexChanging">
                                    <HeaderStyle Height="50px" Font-Bold="true" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First"
                                        LastPageText="Last" />
                                    
                                    <EmptyDataTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border: lightslategrey"
                                            align="center">
                                            <tr style="height: 40px; background-color: lightslategrey">
                                                <td align="center" valign="middle">
                                                    Sr. No.
                                                </td>
                                                <td align="center" valign="middle">
                                                    Name of Prospect
                                                </td>
                                                <td align="center" valign="middle">
                                                    Contact Number
                                                </td>
                                                <td align="center" valign="middle">
                                                    Activity
                                                </td>
                                                <td align="center" valign="middle">
                                                    Result
                                                </td>
                                                <td align="center" valign="middle">
                                                    Remarks
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
                        <div class="actions">
                            <asp:Button ID="btnsubmit" runat="server" Text="Save Record" OnClick="btnsubmit_Click" OnClientClick="DisableSubmitButton();"
                                CssClass="btn btn-inverse large" />
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
