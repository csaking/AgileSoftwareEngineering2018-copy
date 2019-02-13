<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Agile_2018.Pages.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h3>Edit Profile</h3>
        <hr>
        <div class="row">
            <!-- left column -->


            <!-- edit form column -->
            <div class="col-md-9 personal-info">

                <form class="form-horizontal" role="form">
                    <div class="form-group">
                        <label class="col-lg-3 control-label">Username:</label>
                        <div class="col-lg-8">
                            <input id="username" runat="server" class="form-control" type="text" value="" readonly>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Email:</label>
                        <div class="col-md-8">
                            <input id="email" runat="server" class="form-control" type="email">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 control-label">First Name:</label>
                        <div class="col-lg-8">
                            <input id="firstname" runat="server" class="form-control" type="text" value="">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 control-label">Last Name:</label>
                        <div class="col-lg-8">
                            <input id="lastname" runat="server" class="form-control" type="text" value="">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 control-label">Current Password: (Must be filled for changes to take effect)</label>
                        <div class="col-lg-8">
                            <input id="currentpassword" runat="server" class="form-control" type="password" value="">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 control-label">New Password:</label>
                        <div class="col-lg-8">
                            <input id="newpassword" runat="server" class="form-control" type="password" value="">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 control-label">Confirm Password:</label>
                        <div class="col-lg-8">
                            <input id="confirmpassword" runat="server" class="form-control" type="password" value="">
                        </div>
                    </div>
                    <div class="row center-align">
                        <div class="input-field col s12" style="color: red">
                            <asp:Label runat="server" ID="errorLabel"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label"></label>
                        <div class="col-md-8">
                            <asp:LinkButton runat="server" type="submit" class="btn btn-primary" OnClick="Save_Click">
                    Save Changes 
                            </asp:LinkButton>
                            <span></span>
                            <input type="reset" class="btn btn-default" value="Cancel">
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

</asp:Content>





