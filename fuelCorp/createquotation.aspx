<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="createquotation.aspx.cs" Inherits="createquotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function PartyTypeChange(type) {
            if (type == "existing") {

                document.getElementById("<%=trnewparty.ClientID %>").style.display = "none";
                document.getElementById("<%=trcontactperson.ClientID %>").style.display = "none";
                document.getElementById("<%=trexistingparty.ClientID %>").style.display = "block";
            } else {

                document.getElementById("<%=trexistingparty.ClientID %>").style.display = "none";
                document.getElementById("<%=trcontactperson.ClientID %>").style.display = "block";
                document.getElementById("<%=trnewparty.ClientID %>").style.display = "block";
            }
        }

        function CalculateTotalCost() {
            var quantity = document.getElementById("<%=txtcoalrate.ClientID %>").value;
            var rate = document.getElementById("<%=txtcoalquantity.ClientID %>").value;
            if (quantity != "" && rate != "") {
                var coalcost = parseFloat(quantity) * parseFloat(rate);
                document.getElementById("<%=txtfinalcoalcost.ClientID %>").value = coalcost;
            }
            else {
                document.getElementById("<%=txtfinalcoalcost.ClientID %>").value = 0;
            }
            CalculateGrandTotal();
        }
        function addtax() {
            var tax = document.getElementById("<%=ddlcoaltax.ClientID %>");
            if (tax.selectedIndex > 0) {
                var strUser = tax.options[tax.selectedIndex].value;
                var coalcost = document.getElementById("<%=txtfinalcoalcost.ClientID %>").value;
                if (parseFloat(coalcost) > 0 && parseFloat(strUser) > 0) {
                    var amount = (parseFloat(coalcost) * parseFloat(coalcost)) / 100;
                    document.getElementById("<%=txtfinaltax.ClientID %>").value = amount;
                }
                CalculateGrandTotal();
            }
        }
        function CalculateGrandTotal() {

            var coalcost = document.getElementById("<%=txtfinalcoalcost.ClientID %>").value;
            var taxcost = document.getElementById("<%=txtfinaltax.ClientID %>").value;
            var transcost = document.getElementById("<%=txttransportationcost.ClientID %>").value;
            var grandtotal = document.getElementById("<%=txtgrandtotal.ClientID %>");
            var coal = 0;
            if (coalcost != "" && (parseFloat(coalcost) > 0)) {
                coal = parseFloat(coalcost);
            }
            var tax = 0;
            if (taxcost != "" && (parseFloat(taxcost) > 0)) {
                tax = parseFloat(taxcost);
            }
            var transpot = 0;
            if (transcost != "" && (parseFloat(transcost) > 0)) {
                transpot = parseFloat(transcost);
            }
            document.getElementById("<%=txtgrandtotal.ClientID %>").value = (coal + tax + transpot);
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
                    Quotation Form - Create/Modify</h2>
                <hr />
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Party Details
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                    <td style="width: 25%;">
                                    <p>
                                        <strong>Date</strong>
                                    </p>
                                    <asp:TextBox ID="txtdate" runat="server" name="input" placeholder="dd/MM/yyyy"  CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                     MaxLength="10" onkeyup="DateFormatValidation(this.id,this.value);" onkeypress="return isNumberWthOutDot(event);" onblur="ValidateDate(this.id,this.value);"
                                    ></asp:TextBox>&nbsp;
                                </td>
                                <td style="width: 25%;">
                                    <p>
                                        <strong>Expiry Date</strong>
                                    </p>
                                    <asp:TextBox ID="txtexpirydate" runat="server" CssClass="form-control validate[required,funcCall[DateFormat[]]"
                                        onkeyup="DateFormatValidation(this.id,this.value);" MaxLength="10" onkeypress="return isNumberWthOutDot(event);"
                                      ></asp:TextBox>&nbsp;
                                </td>
                                <td style="width: 25%;">
                                    <p>
                                        <strong>Ref. No.</strong>
                                    </p>
                                    <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control validate[required]"
                                       ></asp:TextBox>&nbsp;
                                </td>
                                <td style="width: 25%;">
                                    <p>
                                        <strong>Coal Source</strong>
                                    </p>
                                    <asp:DropDownList ID="ddlcoalsource" runat="server" CssClass="form-control validate[required]"
                                        AutoPostBack="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trexistingparty" runat="server">
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <p>
                                                <strong>Party Name</strong>
                                            </p>
                                            <asp:DropDownList ID="ddlparty" runat="server" CssClass="form-control validate[required]"
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
                                            <asp:TextBox ID="txtcode" runat="server" Enabled="false" CssClass="form-control"
                                              ></asp:TextBox>
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
                                            <asp:TextBox ID="txtaddress" runat="server" Enabled="false" TextMode="MultiLine"
                                                CssClass="form-control validate[required]"></asp:TextBox>
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
                                            <asp:DropDownList ID="ddlcontactperson" runat="server" CssClass="form-control validate[required,custom[integer]]"
                                       >
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr id="trnewparty" runat="server" style="display: none;">
                                <td>
                                    <p>
                                        <strong>Party Name </strong>
                                    </p>
                                    <asp:TextBox ID="txtnewpartyname" runat="server" CssClass="form-control validate[required]"
                                      ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Party Code </strong>
                                    </p>
                                    <asp:TextBox ID="txtnewpartycode" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Address</strong>
                                    </p>
                                    <asp:TextBox ID="txtnewpartyaddress" runat="server" TextMode="MultiLine" CssClass="form-control validate[required]"
                                    ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Contact No</strong>
                                    </p>
                                    <asp:TextBox ID="txtnewpartyno" runat="server" TextMode="MultiLine" CssClass="form-control validate[required,custom[integer]"
                                       ></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trcontactperson" runat="server" style="display: none;">
                                <td>
                                    <p>
                                        <strong>Contact Person </strong>
                                    </p>
                                    <asp:TextBox ID="txtpersonname" runat="server" CssClass="form-control validate[required,custom[integer]"
                                ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Designation</strong>
                                    </p>
                                    <asp:DropDownList ID="ddldesignation" runat="server" AutoPostBack="false"
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <p>
                                        <strong>E-mail</strong>
                                    </p>
                                    <asp:TextBox ID="txtemailid" runat="server" CssClass="form-control validate[required,custom[email]]"
                                  ></asp:TextBox>
                                </td>
                                <td>
                                    <p>
                                        <strong>Contact No</strong>
                                    </p>
                                    <asp:TextBox ID="txtpersonno" runat="server" TextMode="MultiLine" CssClass="form-control validate[required,custom[integer]]"
                                   ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <p>
                                        <strong>Coal Quantity</strong>
                                    </p>
                                    <asp:TextBox ID="txtcoalquantity" onkeypress="return isNumberWthDot(event, this.value)"
                                        runat="server" CssClass="form-control validate[required]" onblur="CalculateTotalCost();"></asp:TextBox>&nbsp;
                                    MT
                                </td>
                                <td style="width: 25%;">
                                    <p>
                                        <strong>Coal Rate</strong>
                                    </p>
                                    <asp:TextBox ID="txtcoalrate" runat="server" onkeypress="return isNumberWthDot(event, this.value)"
                                        CssClass="form-control validate[required]" onblur="CalculateTotalCost();"></asp:TextBox>&nbsp;
                                    per MT
                                </td>
                                <td style="width: 25%;">
                                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <p>
                                                <strong>Coal Type</strong>
                                            </p>
                                            <asp:DropDownList ID="ddlcoaltype" runat="server" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlcoaltype_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 25%;">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlcoaltype" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <p>
                                                <strong>Coal Grade</strong>
                                            </p>
                                            <asp:DropDownList ID="ddlcoalgrade" runat="server" CssClass="form-control"
                                                AutoPostBack="false">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Coal Specification
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td style="width: 30%;">
                                    <p>
                                        Coal Size
                                    </p>
                                    <div>
                                        <asp:TextBox ID="txtcoalsizemin" runat="server" onkeypress="return isNumberWthDot(event, this.value)"
                                            CssClass="form-control validate[required]" Width="50px"></asp:TextBox>&nbsp;mm
                                        To
                                        <asp:TextBox ID="txtcoalsizemax" runat="server" onkeypress="return isNumberWthDot(event, this.value)"
                                            CssClass="form-control  validate[required]" Width="50px"></asp:TextBox>&nbsp;mm
                                    </div>
                                </td>
                                <td style="width: 30%;">
                                    <p>
                                        <strong>GCV (Air Dry Basis)</strong>
                                    </p>
                                    <asp:TextBox ID="txtgcv" runat="server" onkeypress="return isNumberWthDot(event, this.value)"
                                        CssClass="form-control  validate[required]" Width="50px"></asp:TextBox>&nbsp;
                                    +-
                                    <asp:TextBox ID="txtgcverror" runat="server" onkeypress="return isNumberWthDot(event, this.value)"
                                        CssClass="form-control  validate[required]" Width="50px"></asp:TextBox>&nbsp;K.Cal/kg
                                </td>
                                <td style="width: 30%;">
                                    <p>
                                        <strong>Moisture</strong>
                                    </p>
                                    <asp:TextBox ID="txtmoisture" runat="server" onkeypress="return isNumberWthDot(event, this.value)"
                                        CssClass="form-control  validate[required]" Width="50px"></asp:TextBox>&nbsp;
                                    +-
                                    <asp:TextBox ID="txtmoistureerror" runat="server" onkeypress="return isNumberWthDot(event, this.value)"
                                        CssClass="form-control  validate[required]" Width="50px"></asp:TextBox>&nbsp;%
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%;">
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Tax
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="10" border="0" width="100%">
                            <tr>
                                <td style="width: 25%;">
                                    <p>
                                        <strong>Tax</strong>
                                    </p>
                                    <asp:DropDownList ID="ddlcoaltax" runat="server" AutoPostBack="false" CssClass="form-control"
                                        Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 20%;">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <p>
                                                <strong>Tax</strong>
                                            </p>
                                            <asp:Button ID="btntaxsubmit" runat="server" CssClass="btn btn-danger" Text="Submit"
                                                OnClick="btntaxsubmit_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 50%;">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btntaxsubmit" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:GridView ID="gvtax" runat="server" CellPadding="3" AutoGenerateColumns="false"
                                                CssClass="Grid">
                                                <HeaderStyle Height="50px" Font-Bold="true" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TAXNAME" HeaderText="TAXNAME" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                    <asp:BoundField DataField="TAXVALUE" HeaderText="TAXVALUE" ItemStyle-VerticalAlign="Middle"
                                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="1" width="400px" align="center" style="border: lightslategrey">
                                                        <tr style="height: 40px; background-color: lightslategrey">
                                                            <td align="center" valign="middle">
                                                                SR No
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                TAX NAME
                                                            </td>
                                                            <td align="center" valign="middle">
                                                                TAX NAME
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 30px">
                                                            <td align="center" valign="top" colspan="3">
                                                                NO TAX RECORD PRESENT
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Break Up Rates
                        </div>
                    </div>
                    <div class="box-content">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btntaxsubmit" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                    <tr>
                                        <td style="width: 20%;">
                                            Coal Cost
                                        </td>
                                        <td style="width: 80%;">
                                            <asp:TextBox ID="txtfinalcoalcost" runat="server" onkeypress="return isNumberWthDot(event, this.value)"
                                                Enabled="false" CssClass="form-control  validate[required]" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;">
                                            Tax Cost
                                        </td>
                                        <td style="width: 80%;">
                                            <asp:TextBox ID="txtfinaltax" runat="server" onkeypress="return isNumberWthDot(event, this.value)"
                                                Enabled="false" CssClass="form-control  validate[required]"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;">
                                            Transportation Cost
                                        </td>
                                        <td style="width: 80%;">
                                            <asp:TextBox ID="txttransportationcost" runat="server" onblur="CalculateGrandTotal();"
                                                CssClass="form-control  validate[required]" onkeypress="return isNumberWthDot(event, this.value)"
                                            ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%;">
                                            Grand Total
                                        </td>
                                        <td style="width: 80%;">
                                            <asp:TextBox ID="txtgrandtotal" runat="server" CssClass="form-control validate[required]"
                                                onkeypress="return isNumberWthDot(event, this.value)" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="box bordered-box blue-border">
                    <div class="box-header blue-background">
                        <div class="title">
                            &nbsp; Terms & condition
                        </div>
                    </div>
                    <div class="box-content">
                        <table cellpadding="5" cellspacing="5" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="chktermslist" runat="server" RepeatDirection="Vertical">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-danger" Text="Submit"
                        OnClick="btnsubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
