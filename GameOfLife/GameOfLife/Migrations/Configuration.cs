namespace GameOfLife.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                
                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (!context.Users.Any(u => u.UserName == "admin@gmail.com"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // Here we create a Admin super user who will maintain the website
                var user = new ApplicationUser();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";
                string password = "rmit1234";
                userManager.Create(user, password);
                userManager.AddToRole(user.Id, "Admin");

                //var passwordHash = new PasswordHasher();
                //string password = passwordHash.HashPassword("rmit");
                //context.Users.AddOrUpdate(u => u.UserName,
                //    new ApplicationUser
                //    {
                //        UserName = "admin@gmail.com",
                //        Email = "admin@gmail.com",
                //        PasswordHash = password,
                //    });
                //var user = context.Users.Where(u => u.Email.Equals("admin@gmail.com")).FirstOrDefault();
                //userManager.AddToRole(user.Id, "Admin");
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
