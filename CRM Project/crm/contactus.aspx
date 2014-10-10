<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="contactus.aspx.cs" Inherits="contactus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header_text sub" style="margin-top: 15px;">
        <img class="pageBanner" src="img/pageBanner.png" alt="New products" />
        <h4>
            <span>Contact US</span></h4>
    </div>
    <div class="main-content">
        <div class="row">
            <div class="span5">
                <h4 class="title">
                    <span class="text"><strong>Contact</strong> Details</span></h4>
                <div>
                    
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label">
                                Corporate Office</label>
                            <div class="controls">
                              <p>
                              Marketing Company Pvt Ltd,<br />
                              Sadar, nagpur.<br />
                              company@company.com
                              232332
                              </p>
                            </div>
                        </div>
                       
                    </fieldset>
                </div>
            </div>
            <%--<div class="span7">
                <h4 class="title">
                    <span class="text"><strong>Enquiry </strong> Form</span></h4>
                <div>
                    <fieldset>
                        <div class="control-group">
                            <label class="control-label">
                                Full Name</label>
                            <div class="controls">
                                <asp:TextBox ID="txtname" runat="server" style="text-transform:capitalize;" placeholder="Enter Your Full Name" CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Mobile Number:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtmobileno" runat="server" MaxLength="10" placeholder="Enter Your Mobile Number" CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                Email address:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtemail" runat="server" placeholder="Enter Your E-mail"  CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                      <div class="control-group">
                            <label class="control-label">
                                Description:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtdescription" runat="server" TextMode="MultiLine" placeholder="Enter Description" CssClass="input-xlarge"></asp:TextBox>
                            </div>
                        </div>
                        <hr />
                        <div class="actions">
                            <asp:Button ID="btnaccount" runat="server" Text="Send Enquiry" CssClass="btn btn-inverse large" />
                        </div>
                    </fieldset>
                </div>
            </div>--%>
        </div>
    </div>
    
</asp:Content>

