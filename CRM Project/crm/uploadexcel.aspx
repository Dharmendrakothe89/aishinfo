<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="uploadexcel.aspx.cs" Inherits="uploadexcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/Commonvalidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        function DisableUploadButton() {
            var password = document.getElementById("<%=btnImport.ClientID%>");
            // password.disabled = true;
        }
        function DisableSubmitButton() {
            var password = document.getElementById("<%=btnsubmit.ClientID%>");
            // password.disabled = true;
        }

        function ShowAddPopup() {

            document.getElementById("<%=divadd.ClientID%>").style.display = "block";
            Clear();
        }
        function Clear() {
            document.getElementById("<%=txtprospect.ClientID%>").value = "";
            document.getElementById("<%=txtcontactno.ClientID%>").value = "";
            document.getElementById("<%=txtactivity.ClientID%>").value = "";
            document.getElementById("<%=txtresult.ClientID%>").value = "";
            document.getElementById("<%=txtremark.ClientID%>").value = "";
        }
        function HideAddPopup() {
            document.getElementById("<%=divadd.ClientID%>").style.display = "none";
        }

        function ValidateAddRecord() {
            var txtname = document.getElementById("<%=txtprospect.ClientID%>");
            var txtsemid = document.getElementById("<%=txtcontactno.ClientID%>");
            var txtsponsorname = document.getElementById("<%=txtactivity.ClientID%>");
            var txtsponsorid = document.getElementById("<%=txtresult.ClientID%>");
            var txtmobileno = document.getElementById("<%=txtremark.ClientID%>");

            if (txtname.value == "") {
                alert("please enter your name");
                return false;
            }
            else if (txtsemid.value == "") {
                alert("please enter your contact no");
                return false;
            }
            else if (txtsponsorname.value == "") {
                alert("please enter Activity");
                return false;
            }
            else if (txtsponsorid.value == "") {
                alert("please enter Sponsor Result");
                return false;
            }
            else if (txtmobileno.value == "") {
                alert("please enter your Remark");
                return false;
            }
            else {
                return true;
            }
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
                                value="Sign into your account" OnClick="btnImport_Click" OnClientClick="DisableUploadButton();"
                                CssClass="btn btn-inverse large" />
                            <hr />
                        </div>
                    </fieldset>
                </div>
            </div>
            <div class="span7">
                <h4 class="title">
                    <span class="text"><strong>Record </strong>List</span></h4>
                <div>
                    <input type="button" id="btnadd" title="ADD" value="ADD" class="btn btn-inverse large pull-right login-link"
                        onclick="ShowAddPopup();" />
                    <%--<asp:Button ID="btnadd" runat="server" Text="ADD" CssClass="btn btn-inverse large pull-right login-link" />--%><br />
                    <br />
                    <fieldset>
                        <div class="control-group">
                            <div class="controls">
                                <asp:GridView ID="grvExcelData" runat="server" Width="100%" CellPadding="3" AllowPaging="true"
                                    PageSize="15" AutoGenerateColumns="false" CssClass="Grid" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="gvlookup_OnPageIndexChanging">
                                    <HeaderStyle Height="50px" Font-Bold="true" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First"
                                        LastPageText="Last" />
                                    <Columns>
                                        <asp:BoundField DataField="SRNO" HeaderText="Sr. No." ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="NAME" HeaderText="Name of Prospect" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="CONTACTNO" HeaderText="Contact Number" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="ACTIVITY" HeaderText="Activity" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="RESULT" HeaderText="Result" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="REMARK" HeaderText="Remarks" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
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
                            <asp:Button ID="btnsubmit" runat="server" Text="Save Record" OnClick="btnsubmit_Click"
                                OnClientClick="DisableSubmitButton();" CssClass="btn btn-inverse large" />
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <!--POP UP START HERE--->
    <div class="overlay">
    </div>
    <div runat="server" id="divadd" style="background: none repeat scroll 0 0 #fff; height: 370px;
        display: none; margin-left: 28%; position: fixed; top: 10%; width: 550px; border: 2px solid #000;
        border-radius: 5px;">
        <h4>
            Add Record</h4>
        <a href="#" class="close" onclick="HideAddPopup();" >×</a>
        <div class="control-group">
            <div class="span2">
                <label class="control-label" style="float: left;">
                    Full Name :
                </label>
            </div>
            <div class="controls ">
                <asp:TextBox ID="txtprospect" runat="server" placeholder="Enter Full Name" Style="text-transform: capitalize;"
                    CssClass="input-xlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <div class="span2">
                <label class="control-label" style="float: left;">
                    Contact Number :
                </label>
            </div>
            <div class="controls ">
                <asp:TextBox ID="txtcontactno" runat="server" placeholder="Contact Number" MaxLength="12"
                    onkeypress="return validateMobileNo(event,this.id);" CssClass="input-xlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <div class="span2">
                <label class="control-label" style="float: left;">
                    Activity :
                </label>
            </div>
            <div class="controls ">
                <asp:TextBox ID="txtactivity" runat="server" placeholder="Activity" Style="text-transform: capitalize;"
                    CssClass="input-xlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <div class="span2">
                <label class="control-label" style="float: left;">
                    Result :
                </label>
            </div>
            <div class="controls ">
                <asp:TextBox ID="txtresult" runat="server" placeholder="Result" CssClass="input-xlarge"
                    Style="text-transform: capitalize;" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <div class="span2">
                <label class="control-label" style="float: left;">
                    Remark :
                </label>
            </div>
            <div class="controls ">
                <asp:TextBox ID="txtremark" runat="server" placeholder="Remark" TextMode="MultiLine"
                    Style="text-transform: capitalize;" CssClass="input-xlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <div class="controls ">
                <asp:Button ID="btnaddrecord" runat="server" Text="Submit" type="submit" value="Submit"
                    OnClick="btnaddrecord_Click" OnClientClick="return ValidateAddRecord();" Style="margin-left: 70%;"
                    CssClass="btn btn-inverse large" />
            </div>
        </div>
    </div>
    <!--POP UP END HERE--->
</asp:Content>
