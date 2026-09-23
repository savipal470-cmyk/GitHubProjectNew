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
    public partial class ViewUsers : System.Web.UI.Page
    {
        string Connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetUser();
            }
        }

        protected void GetUser()
        {
            using (SqlConnection con = new SqlConnection(Connection))
            {
                string query= @"select * from Users U Inner Join Roles R On U.RoleID=R.RoleID order by U.UserID Desc";

                using (SqlCommand sql = new SqlCommand(query, con))
                {
                    SqlDataAdapter sd = new SqlDataAdapter(sql);
                    
                     DataTable dt=new DataTable();
                     sd.Fill(dt);
                     gvUsers.DataSource = dt;
                     gvUsers.DataBind();  
                }
            }
        }
    }
}