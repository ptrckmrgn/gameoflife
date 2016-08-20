using GameOfLife.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(GameOfLife.Startup))]
namespace GameOfLife
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
