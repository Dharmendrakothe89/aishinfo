<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.master" AutoEventWireup="true"
    CodeFile="dashboard.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="page-wrapper">
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">
                    Dashboard</h1>
            </div>
            <!-- /.row -->
            <div class="row box box-transparent">
            <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link purple-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-eye-open">
                                    <img src="images/party.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Party</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link blue-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-comments">
                                    <img src="images/purchase.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Purchase</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link green-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-star">
                                    <img src="images/report.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Reports</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link orange-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-magic">
                                    <img src="images/setting.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Setting</div>
                        </a>
                    </div>
                </div>
                
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link red-background">
                        <a href="orders.html">
                            <div class="header">
                                <div class="icon-inbox">
                                    <img src="images/bank.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Bank</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link muted-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-refresh">
                                    <img src="images/transport.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Transport</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link muted-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-comments">
                                    <img src="images/contract.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Contract</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link red-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-star">
                                    <img src="images/bills.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Bills</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link purple-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-magic">
                                    <img src="images/ledger.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Ledger</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link orange-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-eye-open">
                                    <img src="images/depot.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Depot</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link green-background">
                        <a href="orders.html">
                            <div class="header">
                                <div class="icon-inbox">
                                    <img src="images/sale.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Sales</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link blue-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-refresh">
                                    <img src="images/dispatch.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Dispatch</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link orange-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-eye-open">
                                    <img src="images/depot.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Depot</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link green-background">
                        <a href="orders.html">
                            <div class="header">
                                <div class="icon-inbox">
                                    <img src="images/sale.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Sales</div>
                        </a>
                    </div>
                </div>
                <div class="col-xs-4 col-sm-2">
                    <div class="box-quick-link blue-background">
                        <a href="#">
                            <div class="header">
                                <div class="icon-refresh">
                                    <img src="images/dispatch.png" alt="" style="margin-top: 20px;" /></div>
                            </div>
                            <div class="content">
                                Dispatch</div>
                        </a>
                    </div>
                </div>
            </div>
            <%--<div class="row" style="margin-top:20px;">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Area Chart Example
                            <div class="pull-right">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                        Actions
                                        <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu pull-right" role="menu">
                                        <li><a href="#">Action</a>
                                        </li>
                                        <li><a href="#">Another action</a>
                                        </li>
                                        <li><a href="#">Something else here</a>
                                        </li>
                                        <li class="divider"></li>
                                        <li><a href="#">Separated link</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div id="morris-area-chart"></div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                   
                </div>
                <!-- /.col-lg-8 -->
               
                   
                </div>--%>
        </div>
    </div>
</asp:Content>
