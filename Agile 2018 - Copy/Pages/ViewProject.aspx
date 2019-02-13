<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" Inherits="Agile_2018.ViewProject" CodeBehind="~/Pages/ViewProject.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-top: 30px">
        <div runat="server" id="name"></div>
        <ul class="collection">
            <asp:Repeater ID="ProjectName" runat="server">
                <ItemTemplate>
                    <li class="collection-item avatar">
                        <i class="material-icons circle">folder</i>
                        <span class="title truncate"><%# Eval("Title") %></span>
                        <p class="truncate">
                            <%# Eval("StatusDesc") %>
                            <br />
                            <div class="right-align">
                                <% if ((Int32.Parse(Session["pID"].ToString()) == statusCode || 5 == statusCode) && statusCode != 2 && statusCode != 3 )
                                    { %>
                                <form enctype="multipart/form-data" id="form1">
                                    <input runat="server" id="UploadFile" name="myFile" type="file" class="waves-effect waves-light btn btn-small darken-1 button-icon " />
                                    <asp:LinkButton runat="server" ID="name" type="submit" class="waves-effect waves-light btn btn-small blue darken-1 button-icon" OnClick="Upload_Click">
                            <i class="material-icons">add</i>
                                    Upload
                                    </asp:LinkButton>
                                </form>
                                <% } %>
                                <asp:LinkButton runat="server" class="waves-effect waves-light btn btn-small amber darken-1 button-icon"
                                    OnClick="Sign_Click" CommandArgument='<%# Eval("ProjectID") %>'
                                    Visible='<%# Int32.Parse(Session["pID"].ToString()) == Int32.Parse(Eval("StatusCode").ToString()) || Int32.Parse(Eval("StatusCode").ToString()) == 5 %>'>
                            <i class="material-icons">create</i>
                                    Sign
                                </asp:LinkButton>
                                <% if (statusCode != 4)
                                    { %>
                                <asp:LinkButton runat="server" class="waves-effect waves-light btn btn-small red darken-1 button-icon"
                                    OnClick="Reject_Click" CommandArgument='<%# Eval("ProjectID") %>'
                                    Visible='<%# Int32.Parse(Session["pID"].ToString()) != 0 %>'>
                            <i class="material-icons">clear</i>
                                    Reject
                                </asp:LinkButton>
                                <% } %>
                                <% if ((Int32.Parse(Session["pID"].ToString()) == 0))
                                    { %>
                                <asp:LinkButton runat="server" class="waves-effect waves-light btn btn-small red darken-1 button-icon" OnClick="DeleteProject_Click" CommandArgument='<%# Eval("ProjectID") %>'>
                            <i class="material-icons">delete_forever</i>
                                    Delete
                                </asp:LinkButton>
                                <% } %>
                            </div>
                        </p>
                        <div class="secondary-content"><span class="new badge" data-badge-caption="Files">1</span></div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="Files" runat="server">
                <ItemTemplate>
                    <li class="collection-item avatar">
                        <span class="title truncate"><%# Eval("FileName") %></span>
                        <div class="right-align">
                            <asp:LinkButton runat="server" class="waves-effect waves-light btn btn-small blue darken-1 button-icon" OnClick="Download_Click" CommandArgument='<%# Eval("FileID") + "|" + Eval("FileName") %>'>
                            <i class="material-icons">file_download</i>
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" class="waves-effect waves-light btn btn-small red darken-1 button-icon" OnClick="DeleteFile_Click" CommandArgument='<%# Eval("FileID")%>'>
                            <i class="material-icons">delete_forever</i>
                            </asp:LinkButton>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="container" style="margin-top: 30px; "width: 800px">
            <asp:Repeater ID="comments" runat="server">
                <ItemTemplate>
                    <li style="list-style-type:none">
                        <span class="title truncate">Notes:</span>
                        <p><%# Eval("comments") %></p>
                        <br /><br/>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            <asp:TextBox id="inputComment" TextMode="multiline" runat="server"/>
            <br /><br />
            <div class="left-align">
                <asp:LinkButton runat="server" type="submit" class="waves-effect waves-light btn btn-small blue darken-1 button-icon" OnClick="Post_Comments">
                    <i class="material-icons">comment</i>
                        Submit Comment
                </asp:LinkButton>
            </div>
        </div>
</asp:Content>
