<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--  page-wrapper -->
    <div id="page-wrapper">
        <div class="row">
            <!-- Page Header -->
            <div class="col-lg-12">
                <h1 class="page-header">
                    Dashboard</h1>
            </div>
            <!--End Page Header -->
        </div>
        <div class="row">
            <!-- Welcome -->
            <div class="col-lg-12">
                <div class="alert alert-info">
                    <i class="fa fa-folder-open"></i><b>&nbsp;Hello ! </b>Welcome TO <b>Global Freedom</b>
                </div>
            </div>
            <!--end  Welcome -->
        </div>
        <div class="row">
          
            <div class="col-lg-3">
                <a href="superadminmemberlist.aspx">
                    <div class="alert alert-success text-center">
                        <i class="fa  fa-key fa-3x"></i>&nbsp;<b></b>New User
                    </div>
                </a>
            </div>
          
        </div>
        <!--end quick info section -->
        </div>
        <div class="row">
            <div class="col-lg-8">
            </div>
        </div>
    </div>
    <!-- end page-wrapper -->
</asp:Content>
