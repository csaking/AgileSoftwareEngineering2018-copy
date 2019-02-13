<%@ Page Title="Projects" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllProjects.aspx.cs" Inherits="Agile_2018._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-top: 30px">
        <ul class="collection">
            <% if (Session["pID"] != null && Int32.Parse(Session["pID"].ToString()) == 0)
                { %>
            <li class="collection-item avatar">
                <form enctype="multipart/form-data">
                    <i class="material-icons circle">folder</i>
                    <span class="title truncate"><%# Eval("Title") %></span>
                    <div class="truncate">
                        <div class="input-field">
                            <input runat="server" id="projectName" class="validate" type="text" required />
                            <label for="projectName">Project Name</label>
                        </div>
                        <br />
                        <div class="input-field right-align">
                            <input runat="server" id="UploadFile" name="myFile" type="file" class="waves-effect waves-light btn btn-small darken-1 button-icon " />

                        </div>
                        <div class="input-field right-align">
                            <asp:LinkButton runat="server" ID="submit" type="Submit" OnClick="NewProject_Click" class="waves-effect waves-light btn btn-small blue darken-1 button-icon">
                            <i class="material-icons">add</i>
                                    Submit & Sign
                            </asp:LinkButton>
                        </div>
                    </div>
                </form>
            </li>
            <% } %>
            <% if (dt.Rows.Count != 0)
                { %>
            <asp:Repeater ID="Projects" runat="server">

                <ItemTemplate>
                    <li class="collection-item avatar">
                        <i class="material-icons circle">folder</i>
                        <span class="title truncate"><%# Eval("Title") %></span>
                        <p class="truncate">
                            <%# Eval("TimeAgo") %>
                            <br>
                            <div class="right-align">
                                <asp:LinkButton runat="server" class="waves-effect waves-light btn btn-small blue darken-1 button-icon" OnClick="ViewProject_Click"
                                    CommandArgument='<%# Eval("ProjectID") +" "+ Eval("Title") %>'>
                            <i class="material-icons">visibility</i>
                                    View
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" class="waves-effect waves-light btn btn-small amber darken-1 button-icon"
                                    OnClick="Sign_Click" CommandArgument='<%# Eval("ProjectID") %>'
                                    Visible='<%# Int32.Parse(Session["pID"].ToString()) == Int32.Parse(Eval("StatusCode").ToString()) || Int32.Parse(Eval("StatusCode").ToString()) == 5 %>'>
                            <i class="material-icons">create</i>
                                    Sign
                                </asp:LinkButton>
                                <% if ((Int32.Parse(Session["pID"].ToString()) == 0))
                                    { %>
                                <asp:LinkButton runat="server" class="waves-effect waves-light btn btn-small red darken-1 button-icon" OnClick="DeleteProject_Click"
                                    CommandArgument='<%# Eval("ProjectID") %>'>
                            <i class="material-icons">delete_forever</i>
                                    Delete
                                </asp:LinkButton>
                                <% } %>
                            </div>
                        </p>
                        <!--<div class="secondary-content"><span class="new badge" data-badge-caption="Files">4</span></div>-->
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <% }
            else
            { %>
        <p style="text-align: center">There is no project to display</p>
        <% } %>
    </div>
</asp:Content>
