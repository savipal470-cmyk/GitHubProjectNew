<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GitHubTest.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="form-group">
            <label>UserName</label>
            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-Control" PlaceHolder="Enter UserName"></asp:TextBox>
           </div>
            <div class="form-group">
            <label>Password</label>
            <asp:TextBox ID="txtPassword" CssClass="form-Control" runat="server" PlaceHolder="Enter Password"></asp:TextBox>
             </div>
            <div class="form-group">
                <asp:Button runat="server" Text="Submit" CssClass="btn" ID="btnSubmit" OnClick="btnSubmit_Click" />
            </div>

            <div>
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
