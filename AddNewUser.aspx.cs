using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GitHubTest
{
    public partial class AddNewUser : System.Web.UI.Page
    {
        string ConnectionString1 = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string mobile = txtMobile.Text.Trim();
            string password = txtPassword.Text.Trim();

            int roleId = Convert.ToInt32(ddlRole.SelectedValue);

            if (string.IsNullOrEmpty(userName) ||
                string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(password) ||
                roleId == 0)
            {
                lblMessage.Text = "Please enter all required details.";
                return;
            }

            using (SqlConnection con =
                   new SqlConnection(ConnectionString1))
            {
                string checkQuery = @"
                    SELECT COUNT(*)
                    FROM Users
                    WHERE UserName = @UserName";

                using (SqlCommand checkCmd =
                       new SqlCommand(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@UserName", userName);

                    con.Open();

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        lblMessage.Text = "Username already exists.";
                        return;
                    }
                }

                string insertQuery = @"
                    INSERT INTO Users
                    (
                        UserName,
                        PasswordHash,
                        FullName,
                        Email,
                        Mobile,
                        RoleId,
                        IsActive,
                        CreatedDate
                    )
                    VALUES
                    (
                        @UserName,
                        @Password,
                        @FullName,
                        @Email,
                        @Mobile,
                        @RoleId,
                        1,
                        GETDATE()
                    )";

                using (SqlCommand cmd =
                       new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Mobile", mobile);
                    cmd.Parameters.AddWithValue("@RoleId", roleId);

                    cmd.ExecuteNonQuery();
                }
            }

            lblMessage.Text = "User created successfully.";

            ClearFields();
        }

        private void ClearFields()
        {
            txtUserName.Text = "";
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtPassword.Text = "";

            ddlRole.SelectedIndex = 0;
        }
    }
}