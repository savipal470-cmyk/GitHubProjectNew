<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddNewUser.aspx.cs" Inherits="GitHubTest.AddNewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h2>Admin Dashboard</h2>

    <div class="container">

        <h2>Create New User</h2>

        <div class="form-group">
            <label>User Name</label>

            <asp:TextBox ID="txtUserName"
                runat="server"
                CssClass="form-control">
            </asp:TextBox>
        </div>

        <div class="form-group">
            <label>Full Name</label>

            <asp:TextBox ID="txtFullName"
                runat="server"
                CssClass="form-control">
            </asp:TextBox>
        </div>

        <div class="form-group">
            <label>Email</label>

            <asp:TextBox ID="txtEmail"
                runat="server"
                CssClass="form-control">
            </asp:TextBox>
        </div>

        <div class="form-group">
            <label>Mobile</label>

            <asp:TextBox ID="txtMobile"
                runat="server"
                CssClass="form-control">
            </asp:TextBox>
        </div>

        <div class="form-group">
            <label>Password</label>

            <asp:TextBox ID="txtPassword"
                runat="server"
                TextMode="Password"
                CssClass="form-control">
            </asp:TextBox>
        </div>

        <div class="form-group">
            <label>Role</label>

            <asp:DropDownList ID="ddlRole"
                runat="server"
                CssClass="form-control">

                <asp:ListItem Text="-- Select Role --"
                    Value="0">
                </asp:ListItem>

                <asp:ListItem Text="Maker"
                    Value="2">
                </asp:ListItem>

                <asp:ListItem Text="Checker"
                    Value="3">
                </asp:ListItem>

            </asp:DropDownList>
        </div>

        <asp:Button ID="btnCreateUser"
            runat="server"
            Text="Create User"
            CssClass="btn"
            OnClick="btnCreateUser_Click" />

        <br />

        <asp:Label ID="lblMessage"
            runat="server"
            CssClass="message">
        </asp:Label>

    </div>

</asp:Content>
