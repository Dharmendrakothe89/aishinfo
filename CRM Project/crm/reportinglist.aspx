<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="reportinglist.aspx.cs" Inherits="reportinglist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="scripts/Commonvalidation.js" type="text/javascript"></script>
    
    <link href="css/gridstyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function DateFormat(field, rules, i, options) {
            var regex = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
            if (!regex.test(field.val())) {
                return "Please enter date in dd/MM/yyyy format."
            }
        }
        function ValidateAddRecord() {
            ShowDetailsPopup();
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
                HideDetailsPopup();
                return true;
            }
        }

        function ShowDetailsPopup() {
            document.getElementById("<%=divadd.ClientID%>").style.display = "block";
        }
        function HideDetailsPopup() {
            document.getElementById("<%=divadd.ClientID%>").style.display = "none";
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
                    Reporting
                </h1>
            </div>
            <div class="col-lg-10">
                <div class="panel panel-default">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtfromdate" runat="server" Style="text-transform: capitalize;"
                                onkeyup="DateFormatValidation(this.id,this.value);" onblur="return checkdate(this.value,this.id);"
                                onkeypress="return isNumberWthOutDot(event);" CssClass="form-control validate[required]"
                                Width="100px" placeholder="Date"></asp:TextBox>
                            <asp:TextBox ID="txttodate" runat="server" Style="text-transform: capitalize;" onkeyup="DateFormatValidation(this.id,this.value);"
                                onblur="return checkdate(this.value,this.id);" onkeypress="return isNumberWthOutDot(event);"
                                CssClass="form-control validate[required]" placeholder="Date" Width="100px"></asp:TextBox>
                            <asp:TextBox ID="txtname" runat="server" Style="text-transform: capitalize;" CssClass="form-control validate[required]"
                                placeholder="Name" Width="200px"></asp:TextBox>
                            <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary" Text="Submit"
                                OnClick="btnsubmit_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-lg-10">
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
                                    <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true"
                                        ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkdelete" runat="server" Text='Delete Record' OnClientClick="confirm('Are You Conferm To Delete Record?');" OnClick="lnkdelete_Click" CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-Font-Bold="true"
                                        ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkdetails" runat="server" Text='Edit Record' OnClientClick="ShowDetailsPopup();"
                                                OnClick="lnkdetails_Click" CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DATE" HeaderText="DATE" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="NAME" HeaderText="NAME" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="CONTACTNO" HeaderText="CONTACT NO" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="ACTIVITY" HeaderText="ACTIVITY" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="RESULT" HeaderText="RESULT" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="REMARK" HeaderText="REMARK" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
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
                                                DATE
                                            </td>
                                            <td align="center" valign="middle">
                                                NAME
                                            </td>
                                            <td align="center" valign="middle">
                                                CONTACTNO
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
                                            <td align="center" valign="top" colspan="8">
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
        <a href="#" class="close">×</a>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--POP UP END HERE--->
</asp:Content>
