<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ScrollDataFetchControl.ascx.cs"
    Inherits="UserControls_ScrollDataFetchControl" %>

<script src="../JS/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>

<script src="../JS/jquery-1.4.1.min.js" type="text/javascript"></script>

<script src="../JS/jquery-1.4.1.js" type="text/javascript"></script>

<script src="../JS/freezegridviewHeader.js" type="text/javascript"></script>

<link href="../CSS/modelpop.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .loading
    {
        background-color: Red;
        color: White;
        padding: 7px;
        position: absolute;
        left: 350px;
        top: 230px;
        z-index: 1000;
        display: block;
    }
    .labal_border
    {
        border-bottom: 1px solid #000000;
    }
    .scroll2
    {
        scrollbar-track-color: #ffffff;
        scrollbar-base-color: #000000;
        scrollbar-face-color: #D8D8D8;
        scrollbar-3dlight-color: #ffffff;
        scrollbar-darkshadow-color: #ffffff;
        scrollbar-shadow-color: #ffffff;
        scrollbar-highlight-color: #ffffff;
        scrollbar-arrow-color: #A4A4A4;
    }
    .hideen
    {
        visibility: hidden;
    }
    .bo_header
    {
        border-top: 1px solid #000000;
        border-bottom: 1px solid #000000;
    }
    .submit2
    {
        background: #d8d8d8;
        border: 1px solid #000000;
        border-color: #bdbdbd;
        font-size: x-small;
        font-weight: bold;
        height: 25px;
    }
    .disablebutton
    {
        display: none;
    }
</style>

<script type="text/javascript">
function HideDiv(id)
{
alert(id);
var div=document.getElementById(id);
div.style.display="none";
}


</script>

<script type="text/javascript">
function PrintGridVeiw()
{
var firstgv=document.getElementById("UserControlGridVeiw").html();

}



//function ChangeColumn
</script>

<div id="divProducts1" style="height: 300px; overflow-y: scroll; overflow-x: hidden;
    border: solid 0px #000000" class="scroll2">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center">
        <tr>
            <td align="right" style="width: 10%;">
                <asp:Label ID="FromDateLabel" runat="server" Width="70px" Font-Italic="true"> </asp:Label>
                <b>TO</b>
            </td>
            <td align="left" style="width: 10%">
                <asp:Label ID="ToDateLabel" runat="server" Width="70px" Font-Italic="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:UpdatePanel ID="ButtonUpdatePanal" runat="server">
                    <ContentTemplate>
                        <table align="center" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left">
                                    <asp:Button ID="ChangeLedgerButton" runat="server" Text="ChangeLedger" OnClick="ChangeLedgerButton_Click"
                                        CssClass="submit2" />
                                </td>
                               <%-- <td align="left">
                                    <asp:Button ID="ChartButton" runat="server" Text="Chart" CssClass="submit2 disablebutton" />
                                </td>--%>
                                <td align="left">
                                    <asp:Button ID="MonthVeiwButton" runat="server" Text="MonthVeiw" OnClick="MonthVeiwButton_Click"
                                        CssClass="submit2" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="ChangeRangeButton" runat="server" Text="ChangeRange" OnClick="ChangeRangeButton_Click"
                                        CssClass="submit2" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="PrintButton" runat="server" Text="Print" CssClass="submit2" OnClick="PrintButton_Click" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="ChangeColumnButton" runat="server" Text="ChangeColumn" CssClass="submit2"
                                        OnClick="ChangeColumnButton_Click" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="ChangeFeildButton" runat="server" Text="ChangeFeild" CssClass="submit2"
                                        OnClick="ChangeFeildButton_click" />
                                </td>
                                <%--<td align="left">
                                    <asp:Button ID="BtnVoucher" runat="server" Text="Voucher" CssClass="submit2" OnClick="BtnVoucher_click" />
                                </td>--%>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <div id="divProducts" runat="server" onclick="FreezeHeader();">
                    <input type="hidden" id="hdnscroll" runat="server" />
                    <input type="hidden" id="hdnCheckFreezeHeader" />
                    <asp:GridView ID="UserControlGridVeiw" runat="server" CellPadding="4" ForeColor="#333333"
                        EnableViewState="false" GridLines="None" OnRowDataBound="UserControlGridVeiw_OnBound"
                        AllowPaging="false" AutoGenerateColumns="false" OnRowCommand="UserControlGridVeiw_OnRowCommand"
                        Width="99%">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="Black" CssClass="bo_header" />
                        <PagerStyle HorizontalAlign="Center" />
                        <RowStyle />
                        <EmptyDataTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center">
                                <tr style="height: 20px; border: 1px solid #000000;">
                                    <td align="left" valign="top">
                                        Date
                                    </td>
                                    <td align="left" valign="top">
                                        Particulars
                                    </td>
                                    <td align="left" valign="top">
                                        Vch Type
                                    </td>
                                    <td align="left" valign="top">
                                        Vch No.
                                    </td>
                                    <td align="left" valign="top">
                                        Debit
                                    </td>
                                    <td align="left" valign="top">
                                        Credit
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top" colspan="6">
                                        No records Found
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</div>
<div id="divTotal" runat="server">
    <table cellpadding="3" border="0" width="90%">
        <tr>
            <td align="left" colspan="3" class="labal_border">
                <asp:Label ID="label2" runat="server" BorderWidth="0px" Text="Total Of Record :"
                    Font-Bold="true" Font-Size="Medium"></asp:Label>
                &nbsp;
                <asp:Label ID="labelRecordFetch" runat="server" BorderWidth="0px" Font-Size="Medium"
                    Font-Italic="true"></asp:Label>&nbsp;Of&nbsp;
                <asp:Label ID="lblTotalLedgers" runat="server" BorderWidth="0px" Font-Size="Medium"
                    Font-Italic="true"></asp:Label>
                &nbsp;
            </td>
            <td style="width: 68%;">
                &nbsp;
            </td>
            <td align="right" valign="top" style="border-bottom: 1px solid #000000;">
                <asp:Label ID="lblTotalCredit" runat="server"></asp:Label>
            </td>
            <td align="right" valign="top" style="border-bottom: 1px solid #000000;">
                <asp:Label ID="lblTotalDebit" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" colspan="3" class="labal_border">
                To <b>Closing Balance</b>
            </td>
            <td align="right" valign="top" colspan="2" style="border-bottom: 1px solid #000000;">
                <asp:Label ID="lblClosingTotalCredit" Font-Bold="true" runat="server"></asp:Label>
            </td>
            <td align="right" valign="top" style="border-bottom: 1px solid #000000;">
                &nbsp;&nbsp;<asp:Label ID="lblClosingTotalDebit" Font-Bold="true" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<div id="divProgress" style="margin-top: -50px; margin-left: 150px; z-index: -999;
    display: none;">
    <%--<span id="loading" class="loading">Loading...</span>--%>
</div>
<asp:Button ID="btnpostback" runat="server" Text="Get More Records" OnClick="btnpostback_Click"
    CssClass="submit2" />
<asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
        <div id="divmonthveiw" runat="server" style="display: none; width: 400px; overflow: scroll;
            height: 400px;">
            <div class="modalBackground" style="position: absolute; z-index: 750;">
            </div>
            <div class="modalContainer">
                <div class="modal" style="position: relative; left: 80px; top: -100px">
                    <div class="modalTop">
                        <table cellpadding="0" cellspacing="0" border="0" width="250px">
                            <tr>
                                <td style="height: 40px; font-weight: bolder; padding-left: 10px" align="center">
                                    <h3 style="text-align: left; vertical-align: baseline;">
                                        Duration
                                    </h3>
                                </td>
                                <td align="right" valign="middle">
                                    <a href="#" id="a2" onclick="hideModal1('LedgerGridVeiw_divmonthveiw');">
                                        <img src="../../Images/close.jpg" style="border: 0px;" alt="" /></a>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalBody" id="DivContainer" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="DivPrint" runat="server" style="display: none; width: 400px; overflow: scroll;
            height: 400px;">
            <div class="modalBackground" style="position: absolute; z-index: 750;">
            </div>
            <div class="modalContainer">
                <div class="modal" style="position: relative; left: 80px; top: -100px">
                    <div class="modalTop">
                        <table cellpadding="0" cellspacing="0" border="0" width="250px">
                            <tr>
                                <td style="height: 40px; font-weight: bolder; padding-left: 10px" align="center">
                                    <h3 style="text-align: left; vertical-align: baseline;">
                                        Print
                                    </h3>
                                </td>
                                <td align="right" valign="middle">
                                    <a href="#" id="a1" onclick="hideModal1('LedgerGridVeiw_divmonthveiw');">
                                        <img src="../../Images/close.jpg" style="border: 0px;" alt="" /></a>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalBody" id="Div2" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
        <div id="DivColumnChange" runat="server" style="display: none; width: 400px; overflow: scroll;
            height: 400px;">
            <div class="modalBackground" style="position: absolute; z-index: 750;">
            </div>
            <div class="modalContainer">
                <div class="modal" style="position: relative; left: 80px; top: -100px">
                    <div class="modalTop">
                        <table cellpadding="0" cellspacing="0" border="0" width="250px">
                            <tr>
                                <td style="height: 40px; font-weight: bolder; padding-left: 10px" align="center">
                                    <h3 style="text-align: left; vertical-align: baseline;">
                                        Change Column
                                    </h3>
                                </td>
                                <td align="right" valign="middle">
                                    <a href="#" id="a3" onclick="hideModal1('LedgerGridVeiw_divmonthveiw');">
                                        <img src="../../Images/close.jpg" style="border: 0px;" alt="" /></a>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalBody" id="DivChangeColContainer" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
        var previousProductId = 0;
        
        $(document).ready(function () {
       
            $("#divProgress").hide();

            $("#btnGetMoreRecords").hide();
            
        });
        
        function Scrolled()
        {
                var scrolltop = $('#divProducts').attr('scrollTop');
                var scrollheight = $('#divProducts').attr('scrollHeight');
                var windowheight = $('#divProducts').attr('clientHeight');
                var scrolloffset = 20;
               
                document.getElementById("<%=hdnscroll.ClientID %>").value = scrolltop;
                
                if (scrolltop >= (scrollheight - (windowheight + scrolloffset)))
                {   
                    //FreezeHeader();      
                }
               
        }
        
        function ClickToFix()
        {
            document.getElementById("LedgerGridVeiw_divProducts").click();
        }
        function FreezeHeader()
        {
            
            var d = document.getElementById('LedgerGridVeiw_UserControlGridVeiw');
            if( d != null && document.getElementById("hdnCheckFreezeHeader").value == "")
            {
                FixHeader(d,200);                
                document.getElementById("hdnCheckFreezeHeader").value = "1";
                
            }                
        }
        
        function ccc()
        {
            document.getElementById("LedgerGridVeiw_divProducts").scrollTop = document.getElementById("<%=hdnscroll.ClientID %>").value;            
        }
        
       
        

        function BindNewData()
         {
            PageMethods.GetRecordFromDatabase(lastProductId,GetData);

         }
         function BindDataSecondGridVeiw()
         {
             var button=document.getElementById("<%=btnpostback.ClientID %>");
             button.click();
           
         }
        function GetData(ResultString)
        {
            var button=document.getElementById("<%=btnpostback.ClientID %>");       
            button.click();         
            
        }  
        function GetRowsCount() 
        {
           
            var rowCount = $('#UserControlGridVeiw tr').length-1;
            return rowCount;

        }

        function revealModal1(divID)
        {      
          document.getElementById(divID).style.display = "block";
            
        }
        function hideModal1(divID)
        {    
          document.getElementById(divID).style.display = "none";
        }

</script>

