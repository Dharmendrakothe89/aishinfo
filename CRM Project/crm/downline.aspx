<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="downline.aspx.cs" Inherits="downline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/Commonvalidation.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div class="header_text sub" style="margin-top: 15px;">
        <img class="pageBanner" src="img/pageBanner.png" alt="New products" />
        <h4>
            <span>Downline</span></h4>
    </div>
    <div class="main-content">
        <div class="row">
            <div class="span5">
                <h4 class="title">
                    <span class="text"><strong>Downline</strong> Form</span></h4>
                <div>
                    <fieldset>
                        <asp:UpdatePanel ID="up1" runat="server">
                            <ContentTemplate>
                                <asp:TreeView ID="downlinetree" runat="server" Font-Bold="False" Font-Italic="False"
                                    Font-Names="Times New Roman" Font-Size="12px" Font-Strikeout="False" ForeColor="Blue"
                                    ShowLines="True">
                               
                                    <RootNodeStyle ChildNodesPadding="2px" />
                                    <NodeStyle Font-Names="Times New Roman" NodeSpacing="2px" />
                                </asp:TreeView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
