<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="userdashboard.aspx.cs" Inherits="userdashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header_text sub" style="margin-top: 15px;">
        <img class="pageBanner" src="img/pageBanner.png" alt="New products" />
        <h4>
            <span>Dashboard</span></h4>
    </div>
    <div class="main-content">
        <div class="row">
            <div class="span7">
                <h4 class="title">
                    <span class="text"><strong>User </strong>Menu</span></h4>
                <div>
                    <fieldset>
                        <div class="control-group">
                            <div class="col-lg-3">
                                <a href="searchrecord.aspx">
                                    <div class="alert alert-danger text-center">
                                        <i class="fa fa-search fa-3x"></i>&nbsp;Search Prospect
                                    </div>
                                </a>
                            </div>
                            <div class="col-lg-3">
                                <a href="uploadexcel.aspx">
                                    <div class="alert alert-info text-center">
                                        <i class="fa fa-upload fa-3x"></i>&nbsp;<b></b> Prospect Excel Upload
                                    </div>
                                </a>
                            </div>
                            <div class="col-lg-3">
                                <a href="editprofile.aspx">
                                    <div class="alert alert-warning text-center">
                                        <i class="fa  fa-pencil fa-3x"></i>&nbsp;<b></b>Edit Profile
                                    </div>
                                </a>
                            </div>
                            <div class="col-lg-3">
                                <a href="#">
                                    <div class="alert alert-info text-center">
                                        <i class="fa fa-user fa-3x"></i>&nbsp;<b></b> Data Entry Part
                                    </div>
                                </a>
                            </div>
                        </div>
                        <hr />
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
