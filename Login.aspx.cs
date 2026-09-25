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
    public partial class Login : System.Web.UI.Page
    {

        string Connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string UserName = txtUserName.Text;
            string Password = txtPassword.Text;
            using (SqlConnection con = new SqlConnection(Connection))
            {
                string Query = @"Select * from Users where UserName=@UserName and PasswordHash=@PasswordHash";
                using (SqlCommand sql = new SqlCommand(Query, con))
                {
                    sql.Parameters.AddWithValue("@UserName",UserName);
                    sql.Parameters.AddWithValue("PasswordHash", Password);
                    con.Open();

                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            int UserID=Convert.ToInt32(reader["UserID"]);
                            Session["UserId"] = UserID;
                            string UserName1 = reader["UserName"].ToString();
                            Session["UserName"] = UserName1;
                            int RoleID = Convert.ToInt32(reader["RoleID"]);

                            if(RoleID==1)
                            {
                                Response.Redirect("AddNewUser.aspx");
                            }
                            else if (RoleID == 2)
                            {
                                Response.Redirect("Maker.aspx");
                            }
                            else if (RoleID == 3)
                            {
                                Response.Redirect("Checker.aspx");
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Invalid UserName and Password";
                        }
                    }
                }
            }  
        }
    }
}