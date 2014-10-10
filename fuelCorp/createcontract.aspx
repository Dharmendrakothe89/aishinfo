<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="createcontract.aspx.cs" Inherits="createcontract" %>

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
        function ShowDiv1() {
            HideDiv();
            document.getElementById("<%=div1.ClientID%>").style.display = "block";
        }
        function ShowDiv2() {
            HideDiv();
            document.getElementById("<%=div2.ClientID%>").style.display = "block";
        }
        function ShowDiv3() {
            HideDiv();
            document.getElementById("<%=div3.ClientID%>").style.display = "block";
        }
        function ShowDiv4() {
            HideDiv();
            document.getElementById("<%=div4.ClientID%>").style.display = "block";
        }
        function ShowDiv5() {
            HideDiv();
            document.getElementById("<%=div5.ClientID%>").style.display = "block";
        }
        function ShowDiv6() {
            HideDiv();
            document.getElementById("<%=div6.ClientID%>").style.display = "block";
        }

        function HideDiv() {
            document.getElementById("<%=div1.ClientID%>").style.display = "none";
            document.getElementById("<%=div2.ClientID%>").style.display = "none";
            document.getElementById("<%=div3.ClientID%>").style.display = "none";
            document.getElementById("<%=div4.ClientID%>").style.display = "none";
            document.getElementById("<%=div5.ClientID%>").style.display = "none";
            document.getElementById("<%=div6.ClientID%>").style.display = "none";


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
                    Contract Form - Create</h2>
                <hr />
                <asp:UpdatePanel ID="up1" runat="server">
                    <ContentTemplate>
                        <div runat="server" id="div1" class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp; Contract Details
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>Contract Type</strong>
                                            </p>
                                            <div>
                                                <asp:RadioButton ID="rdservicecontract" runat="server" CssClass="radio radio-inline"
                                                    Text="Service" GroupName="ADDON" Checked="true" AutoPostBack="true" OnCheckedChanged="ContractType_CheckedChanged" />
                                                <asp:RadioButton ID="rdtransportcontract" runat="server" CssClass="radio radio-inline"
                                                    Text="Transport" GroupName="ADDON" AutoPostBack="true" OnCheckedChanged="ContractType_CheckedChanged" />
                                            </div>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Date</strong>
                                            </p>
                                            <asp:TextBox ID="txtdate" runat="server" type="date" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeyup="DateFormatValidation(this.id,this.value);" onkeypress="return isNumberWthOutDot(event);"
                                         ></asp:TextBox>&nbsp;
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Ref No</strong>
                                            </p>
                                            <asp:TextBox ID="txtrefno" runat="server" style="text-transform:uppercase;" CssClass="form-control validate[required]"
                                           ></asp:TextBox>&nbsp;
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Ref Date</strong>
                                            </p>
                                            <asp:TextBox ID="txtrefdate" runat="server" type="date" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeyup="DateFormatValidation(this.id,this.value);" onkeypress="return isNumberWthOutDot(event);"
                                               ></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <p>
                                                        <strong>Party Name</strong>
                                                    </p>
                                                    <asp:DropDownList ID="ddlparty" type="date" runat="server" CssClass="form-control validate[required]"
                                                  AutoPostBack="true" OnSelectedIndexChanged="ddlparty_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlparty" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <strong>Party Code</strong>
                                                    </p>
                                                    <asp:TextBox ID="txtcode" runat="server" style="text-transform:uppercase;" Enabled="false" CssClass="form-control"
                                                     ></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlparty" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <strong>Contact Person</strong>
                                                    </p>
                                                    <asp:DropDownList ID="ddlcontactperson" runat="server" style="text-transform:capitalize;"  CssClass="form-control validate[required]"
                                               >
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlparty" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <strong>Address</strong>
                                                    </p>
                                                    <asp:TextBox ID="txtaddress" runat="server" style="text-transform:capitalize;" Enabled="false" TextMode="MultiLine"
                                                        CssClass="form-control validate[required]"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>Contract Start Date</strong>
                                            </p>
                                            <asp:TextBox ID="txtcontractdate" runat="server" type="date" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeyup="DateFormatValidation(this.id,this.value);" onkeypress="return isNumberWthOutDot(event);"
                                        ></asp:TextBox>&nbsp;
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Expiry Date</strong>
                                            </p>
                                            <asp:TextBox ID="txtexpirydate" runat="server" type="date" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                                onkeyup="DateFormatValidation(this.id,this.value);" onkeypress="return isNumberWthOutDot(event);"
                                  ></asp:TextBox>&nbsp;
                                        </td>
                                        <td align="right">
                                            <p>
                                                <strong>Quantity</strong>
                                            </p>
                                            <asp:TextBox ID="txtquantity" runat="server" CssClass="form-control validate[required,custom[integer]]"
                                                onkeypress="return isNumberWthOutDot(event);"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                        <p>
                                                <strong></strong>
                                            </p>
                                            <asp:DropDownList ID="ddlquantitytype" runat="server" CssClass="form-control validate[required]"
                                                AutoPostBack="false">
                                                <asp:ListItem Text="Per Month" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Per Year" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <strong>Rate</strong>
                                            </p>
                                            <div>
                                            <asp:TextBox ID="txtrate" Enabled="false" runat="server" CssClass="form-control"
                                              ></asp:TextBox>&nbsp;/PMT
                                                </div>
                                        </td>
                                        <td>
                                            <p>
                                                <strong>Service Charge</strong>
                                            </p>
                                            <div>
                                            <asp:TextBox ID="txtchargservice" CssClass="form-control" runat="server"
                                               ></asp:TextBox>&nbsp;/PMT
                                                </div>
                                        </td>
                                        <td colspan="2">
                                            <p>
                                                <strong>Service Tax</strong>
                                            </p>
                                            <div>
                                                <asp:RadioButton ID="rdservicetaxapplicable" runat="server" CssClass="radio radio-inline"
                                                    Text="Applicable" GroupName="SERVICETAX" Checked="true" />
                                                <asp:RadioButton ID="rdservicetaxnotapplicable" runat="server" CssClass="radio radio-inline"
                                                    Text="Not Applicable" GroupName="SERVICETAX" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="bottom" colspan="4">
                                            <div>
                                                <asp:Button ID="btnsubmit1" runat="server" CssClass="btn btn-danger" Text="Continue" OnClick="btnsubmit1_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        
                        <div runat="server" id="div2" style="display:none;"  class="box bordered-box blue-border">
                            <div class="box-content">
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                              <h4 style="color:#0072bc; font-size:24px; font-weight:700;"> Work Details</h4>
                                            </p>
                                            <asp:TextBox ID="txtworkdetails" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                               <h4 style="color:#0072bc; font-size:24px; font-weight:700;"> Delivery</h4>
                                            </p>
                                            <asp:TextBox ID="txtdeliverydetails" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                                 <h4 style="color:#0072bc; font-size:24px; font-weight:700;"> Quality Guarantee</h4>
                                            </p>
                                            <asp:CheckBoxList ID="chkqualityguarantity" runat="server" Width="1024px" Font-Bold="false" RepeatDirection="Vertical">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                            <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Analysis Of Quality</h4>
                                            </p>
                                            <asp:CheckBoxList ID="chkqualityanalysis" runat="server" Width="1024px" Font-Bold="false" RepeatDirection="Vertical">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                              <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Payment Terms</h4> 
                                            </p>
                                            <asp:CheckBoxList ID="chkpaymentterms" runat="server" Width="1024px" Font-Bold="false" RepeatDirection="Vertical">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="bottom" colspan="4">
                                            <div>
                                                <asp:Button ID="btnsubmit2" runat="server" CssClass="btn btn-danger" Text="Continue"
                                                    OnClick="btnsubmit2_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div runat="server" id="div3" style="display: none;" class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp; Contract Details
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                                 <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Bonus/Penalty for lower BTU/lb</h4>
                                            </p>
                                            <asp:CheckBoxList ID="chklowerbtu" runat="server" Width="1024px" RepeatDirection="Vertical">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                             <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Penalty in Ash</h4>
                                            </p>
                                            <asp:CheckBoxList ID="chkashpenalty" runat="server" Width="1024px" RepeatDirection="Vertical">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                             <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Delay/non-Delivery of Supply</h4>
                                            </p>
                                            <asp:TextBox ID="txtdeleysupply" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                   <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Excess Supply</h4>  
                                            </p>
                                            <asp:TextBox ID="txtexcesssupply" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                                <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Weighment</h4>  
                                            </p>
                                            <asp:TextBox ID="txtweighment" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                                <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Security Deposit</h4>  
                                            </p>
                                            <asp:TextBox ID="txtsecuritydeposit" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="4">
                                            <div>
                                                <asp:Button ID="btnsubmit4" runat="server" CssClass="btn btn-danger" Text="Continue" OnClick="btnsubmit3_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div runat="server" id="div4" style="display: none;" class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp; Contract Details
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                        <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Jurisdiction</h4>
                                            </p>
                                            <asp:TextBox ID="txtjurisdiction" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                        <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Compliance Of Laws</h4>  
                                            </p>
                                            <asp:TextBox ID="txtcompliancelaw" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                             <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Confidentiality</h4>  
                                              
                                            </p>
                                            <asp:TextBox ID="txtconfidential" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                      <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Indenmnity</h4> 
                                            </p>
                                            <asp:TextBox ID="txtindenmnity" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="bottom" colspan="4">
                                            <div>
                                                <asp:Button ID="btnsubmit5" runat="server" CssClass="btn btn-danger" Text="Continue" OnClick="btnsubmit4_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div runat="server" id="div5" style="display: none;" class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp; Contract Details
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                        <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Dispute Resolution & Arbitration</h4>
                                            </p>
                                            <asp:TextBox ID="txtarbitration" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                         <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Termination Of Contract</h4>
                                            </p>
                                            <asp:TextBox ID="txttermination" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                         <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Force Majeure</h4>  
                                            </p>
                                            <asp:TextBox ID="txtforcemajeure" runat="server" TextMode="MultiLine" Width="1024px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                         <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Other Terms & Condition</h4>  
                                            </p>
                                            <asp:CheckBoxList ID="chkotherterms" runat="server" RepeatDirection="Vertical" Width="1024px">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="bottom" colspan="4">
                                            <div>
                                                <asp:Button ID="btnsubmit6" runat="server" Text="Continue" CssClass="btn btn-danger" OnClick="btnsubmit5_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div runat="server" id="div6" style="display: none;" class="box bordered-box blue-border">
                            <div class="box-header blue-background">
                                <div class="title">
                                    &nbsp; Contract Details
                                </div>
                            </div>
                            <div class="box-content">
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td colspan="4">
                                            <p>
                                                <h4 style="color:#0072bc; font-size:24px; font-weight:700;">Party Values</h4>  
                                            </p>
                                            <asp:TextBox ID="txtpartyvalue" runat="server" TextMode="MultiLine" Width="1024px" Height="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="bottom" colspan="4">
                                            <div>
                                                <asp:Button ID="btnsubmit7" runat="server" CssClass="btn btn-danger" Text="Finish" OnClick="btnsubmit6_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
