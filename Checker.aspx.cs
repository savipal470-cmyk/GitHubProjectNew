using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GitHubTest
{
    public partial class Checker : System.Web.UI.Page
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        
            protected void Page_Load(object sender, EventArgs e)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                
                if (Session["RoleId"] == null ||
                    Convert.ToInt32(Session["RoleId"]) != 3)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                if (!IsPostBack)
                {
                    lblWelcome.Text =
                        "Welcome, " + Session["UserName"].ToString();

                    LoadPendingRequests();
                }
            }
        
            private void LoadPendingRequests()
            {
                string query = @"
                SELECT
                    A.ApprovalId,
                    A.EntityName,
                    A.ActionType,
                    U.UserName AS MakerName,
                    A.Status,
                    A.CreatedDate
                FROM ApprovalRequests A
                INNER JOIN Users U
                    ON A.MakerId = U.UserId
                WHERE A.Status = 'Pending'
                ORDER BY A.CreatedDate DESC";


                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlDataAdapter da =
                        new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        gvPendingRequests.DataSource = dt;
                        gvPendingRequests.DataBind();
                    }
                }
            }
        
            protected void gvPendingRequests_RowCommand(
                object sender,
                System.Web.UI.WebControls.GridViewCommandEventArgs e)
            {
                int approvalId =
                    Convert.ToInt32(e.CommandArgument);


                if (e.CommandName == "ViewRequest")
                {
                    ViewRequest(approvalId);
                }

                else if (e.CommandName == "ApproveRequest")
                {
                    ApproveRequest(approvalId);
                }

                else if (e.CommandName == "RejectRequest")
                {
                    RejectRequest(approvalId);
                }
            }
        
            private void ViewRequest(int approvalId)
            {
                string query = @"
                SELECT
                    A.ApprovalId,
                    A.EntityName,
                    A.ActionType,
                    A.OldValue,
                    A.NewValue,
                    U.UserName AS MakerName
                FROM ApprovalRequests A
                INNER JOIN Users U
                    ON A.MakerId = U.UserId
                WHERE A.ApprovalId = @ApprovalId";


                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@ApprovalId", approvalId);

                        con.Open();

                        using (SqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblApprovalId.Text =
                                    "Approval ID: " +
                                    reader["ApprovalId"].ToString();

                                lblEntity.Text =
                                    "Entity: " +
                                    reader["EntityName"].ToString();

                                lblAction.Text =
                                    "Action: " +
                                    reader["ActionType"].ToString();

                                lblMaker.Text =
                                    "Maker: " +
                                    reader["MakerName"].ToString();

                                lblOldValue.Text =
                                    "Old Value: " +
                                    reader["OldValue"].ToString();

                                lblNewValue.Text =
                                    "New Value: " +
                                    reader["NewValue"].ToString();
                            }
                        }
                    }
                }
            }


            // Approve Request
            private void ApproveRequest(int approvalId)
            {
                string query = @"
                UPDATE ApprovalRequests
                SET
                    Status = 'Approved',
                    CheckerId = @CheckerId,
                    Remarks = @Remarks,
                    CheckedDate = GETDATE()
                WHERE ApprovalId = @ApprovalId
                AND Status = 'Pending'";


                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@ApprovalId", approvalId);

                        cmd.Parameters.AddWithValue(
                            "@CheckerId",
                            Convert.ToInt32(Session["UserId"]));

                        cmd.Parameters.AddWithValue(
                            "@Remarks",
                            txtRemarks.Text.Trim());

                        con.Open();

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            lblMessage.Text =
                                "Request approved successfully.";

                            txtRemarks.Text = "";

                            LoadPendingRequests();
                        }
                    }
                }
            }
        
            private void RejectRequest(int approvalId)
            {
                string query = @"
                UPDATE ApprovalRequests
                SET
                    Status = 'Rejected',
                    CheckerId = @CheckerId,
                    Remarks = @Remarks,
                    CheckedDate = GETDATE()
                WHERE ApprovalId = @ApprovalId
                AND Status = 'Pending'";


                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@ApprovalId", approvalId);

                        cmd.Parameters.AddWithValue(
                            "@CheckerId",
                            Convert.ToInt32(Session["UserId"]));

                        cmd.Parameters.AddWithValue(
                            "@Remarks",
                            txtRemarks.Text.Trim());

                        con.Open();

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            lblMessage.Text =
                                "Request rejected successfully.";

                            txtRemarks.Text = "";

                            LoadPendingRequests();
                        }
                    }
                }
            }
        }
    }
