using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

namespace GameOfLife.Admin
{
    public partial class Templates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check user authorisation
            try
            {
                if (!Roles.IsUserInRole(User.Identity.GetUserName(), "Admin"))
                {
                    Response.Redirect("/Admin/Login.aspx");
                }
            }
            catch (NullReferenceException)
            {
                Response.Redirect("/Admin/Login.aspx");
            }
        }
    }
}