<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Checker.aspx.cs" Inherits="GitHubTest.Checker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .checker-container {
            padding: 25px;
        }

        .page-title {
            font-size: 26px;
            font-weight: bold;
            margin-bottom: 20px;
        }

        .request-section {
            margin-top: 25px;
        }

            .request-section h3 {
                margin-bottom: 15px;
            }

        .grid {
            width: 100%;
            border-collapse: collapse;
        }

            .grid th {
                padding: 10px;
                text-align: left;
            }

            .grid td {
                padding: 10px;
            }

        .btn {
            padding: 6px 12px;
            margin-right: 5px;
            cursor: pointer;
        }

        .remarks {
            margin-top: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="checker-container">
  
        <div class="page-title">Checker Dashboard </div>
      
        <asp:Label ID="lblWelcome" runat="server" Text="Welcome, Checker"> </asp:Label>
  
        <div class="request-section">
            <h3>Pending Approval Requests</h3>
            <asp:GridView ID="gvPendingRequests" runat="server" AutoGenerateColumns="False" CssClass="grid" DataKeyNames="ApprovalId" OnRowCommand="gvPendingRequests_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ApprovalId" HeaderText="Approval ID" />
                    <asp:BoundField DataField="EntityName" HeaderText="Entity" />
                    <asp:BoundField DataField="ActionType" HeaderText="Action" />
                    <asp:BoundField DataField="MakerName" HeaderText="Maker" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="Submitted Date" DataFormatString="{0:dd-MM-yyyy HH:mm}" />
                
                    <asp:TemplateField HeaderText="View">
                        <ItemTemplate>
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn" CommandName="ViewRequest" CommandArgument='<%# Eval("ApprovalId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
 
                    <asp:TemplateField HeaderText="Approve">
                        <ItemTemplate>
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn" CommandName="ApproveRequest" CommandArgument='<%# Eval("ApprovalId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Reject">
                        <ItemTemplate>
                            <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn" CommandName="RejectRequest" CommandArgument='<%# Eval("ApprovalId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="request-section">
            <h3>Request Details</h3>
            <asp:Label ID="lblApprovalId" runat="server" Text="Approval ID:"> </asp:Label>
            <br />
            <br />
            <asp:Label ID="lblEntity" runat="server" Text="Entity:"> </asp:Label>
            <br />
            <br />
            <asp:Label ID="lblAction" runat="server" Text="Action:"> </asp:Label>
            <br />
            <br />
            <asp:Label ID="lblMaker" runat="server" Text="Maker:"> </asp:Label>
            <br />
            <br />
            <asp:Label ID="lblOldValue" runat="server" Text="Old Value:"> </asp:Label>
            <br />
            <br />
            <asp:Label ID="lblNewValue" runat="server" Text="New Value:"> </asp:Label>
        </div>

        <div class="remarks">
            <h3>Remarks</h3>
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="4" Columns="50"> </asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblMessage" runat="server"> </asp:Label>
        </div>
    </div>
</asp:Content>
