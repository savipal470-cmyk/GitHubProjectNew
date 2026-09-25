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
    public partial class Maker : System.Web.UI.Page
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                lblWelcome.Text =
                    "Welcome, " + Session["UserName"].ToString();

                LoadCategories();
                LoadMyRequests();
            }
        }
        private void LoadCategories()
        {
            using (SqlConnection sc =
                   new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT CategoryId, CategoryName
                    FROM Categories
                    WHERE IsActive = 1
                    ORDER BY CategoryName";

                using (SqlCommand cmd =
                       new SqlCommand(query, sc))
                {
                    sc.Open();

                    SqlDataReader reader =
                        cmd.ExecuteReader();

                    ddlCategory.DataSource = reader;
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryId";

                    ddlCategory.DataBind();
                }
            }

            ddlCategory.Items.Insert(
                0,
                new System.Web.UI.WebControls.ListItem(
                    "-- Select Category --",
                    "0"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServiceName.Text))
            {
                lblMessage.Text = "Please enter service name.";
                return;
            }

            if (ddlCategory.SelectedValue == "0")
            {
                lblMessage.Text = "Please select category.";
                return;
            }

            decimal price;

            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                lblMessage.Text = "Please enter valid price.";
                return;
            }

            int duration;

            if (!int.TryParse(txtDuration.Text, out duration))
            {
                lblMessage.Text = "Please enter valid duration.";
                return;
            }

            int makerId =
                Convert.ToInt32(Session["UserId"]);

            using (SqlConnection sc =
                   new SqlConnection(ConnectionString))
            {
                string query = @"
                    INSERT INTO ApprovalRequests
                    (
                        EntityName,
                        EntityId,
                        ActionType,
                        OldValue,
                        NewValue,
                        MakerId,
                        Status,
                        CreatedDate
                    )
                    VALUES
                    (
                        'Services',
                        NULL,
                        'ADD',
                        NULL,
                        @NewValue,
                        @MakerId,
                        'Pending',
                        GETDATE()
                    )";

                using (SqlCommand cmd =
                       new SqlCommand(query, sc))
                {
                    string newValue =
                        "ServiceName=" + txtServiceName.Text +
                        "; CategoryId=" + ddlCategory.SelectedValue +
                        "; Price=" + price +
                        "; Duration=" + duration +
                        "; Description=" + txtDescription.Text;

                    cmd.Parameters.AddWithValue(
                        "@NewValue", newValue);

                    cmd.Parameters.AddWithValue(
                        "@MakerId", makerId);

                    sc.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            lblMessage.Text =
                "Request submitted successfully for approval.";

            ClearFields();

            LoadMyRequests();
        }

        private void LoadMyRequests()
        {
            int makerId =
                Convert.ToInt32(Session["UserId"]);

            using (SqlConnection sc =
                   new SqlConnection(ConnectionString))
            {
                string query = @"
                    SELECT
                        ApprovalId,
                        EntityName,
                        ActionType,
                        NewValue,
                        Status,
                        CreatedDate,
                        Remarks
                    FROM ApprovalRequests
                    WHERE MakerId = @MakerId
                    ORDER BY CreatedDate DESC";

                using (SqlCommand cmd =
                       new SqlCommand(query, sc))
                {
                    cmd.Parameters.AddWithValue(
                        "@MakerId", makerId);

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    gvRequests.DataSource = dt;
                    gvRequests.DataBind();
                }
            }
        }
        private void ClearFields()
        {
            txtServiceName.Text = "";
            txtPrice.Text = "";
            txtDuration.Text = "";
            txtDescription.Text = "";
            ddlCategory.SelectedIndex = 0;
        }
    }
}
