<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewUsers.aspx.cs" Inherits="GitHubTest.ViewUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
            <asp:GridView runat="server" ID="gvUsers">
                <Columns>      
        <asp:BoundField
            DataField="UserId"
            HeaderText="User ID" />

        <asp:BoundField
            DataField="UserName"
            HeaderText="User Name" />

        <asp:BoundField
            DataField="FullName"
            HeaderText="Full Name" />

        <asp:BoundField
            DataField="Email"
            HeaderText="Email" />

        <asp:BoundField
            DataField="Mobile"
            HeaderText="Mobile" />

        <asp:BoundField
            DataField="RoleName"
            HeaderText="Role" />

        <asp:CheckBoxField
            DataField="IsActive"
            HeaderText="Active" />

                </Columns>
            </asp:GridView>
                </div>
        </div>
    </form>
</body>
</html>
