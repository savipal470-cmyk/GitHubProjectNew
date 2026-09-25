<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Maker.aspx.cs" Inherits="GitHubTest.Maker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Maker Dashboard</h2>
    <asp:Label ID="lblWelcome"
        runat="server"
        Font-Bold="true">
    </asp:Label>

    <hr />

    <h3>Create Service Request</h3>

    <table>

        <tr>
            <td>Service Name:</td>
            <td>
                <asp:TextBox ID="txtServiceName"
                    runat="server">
                </asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Category:</td>
            <td>
                <asp:DropDownList ID="ddlCategory"
                    runat="server">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>Price:</td>
            <td>
                <asp:TextBox ID="txtPrice"
                    runat="server">
                </asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Duration:</td>
            <td>
                <asp:TextBox ID="txtDuration"
                    runat="server">
                </asp:TextBox>
                Minutes
            </td>
        </tr>

        <tr>
            <td>Description:</td>
            <td>
                <asp:TextBox ID="txtDescription"
                    runat="server"
                    TextMode="MultiLine">
                </asp:TextBox>
            </td>
        </tr>

        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnSubmit"
                    runat="server"
                    Text="Submit for Approval"
                    OnClick="btnSubmit_Click" />
            </td>
        </tr>

    </table>

    <br />

    <asp:Label ID="lblMessage"
        runat="server"
        ForeColor="Green">
    </asp:Label>

    <hr />

    <h3>My Requests</h3>

    <asp:GridView ID="gvRequests"
        runat="server"
        AutoGenerateColumns="true">
    </asp:GridView>
</asp:Content>
