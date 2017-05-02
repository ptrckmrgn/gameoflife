using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using GameOfLife.Models;

namespace GameOfLife.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsolutePath != "/Admin/Login.aspx")
            {
                // Check user authorisation
                try
                {
                    if (!Roles.IsUserInRole(HttpContext.Current.User.Identity.GetUserName(), "Admin"))
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

        protected void LogOff(object sender, EventArgs e)
        {
            var AuthenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            Response.Redirect("/");
        }
    }
}