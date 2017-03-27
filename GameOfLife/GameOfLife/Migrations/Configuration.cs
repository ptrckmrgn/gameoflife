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
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                // Create "Admin" role
                var role = new IdentityRole();
                role.Name = "Admin";

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                roleManager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "ptrckmrgn"))
            {
                // Create admin user
                var user = new ApplicationUser();
                user.UserName = "ptrckmrgn";
                user.Email = "patrick.morgan@live.com.au";
                string password = "s3469597";

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                userManager.Create(user, password);
                userManager.AddToRole(user.Id, "Admin");

                // Create basic templates
                Template template;

                template = new Template
                {
                    Name = "Blinker",
                    Height = 5,
                    Width = 5,
                    Cells = "XXXXXXXOXXXXOXXXXOXXXXXXX",
                    User = user,
                    UserId = user.Id
                };
                context.Templates.Add(template);

                template = new Template
                {
                    Name = "Glider",
                    Height = 20,
                    Width = 20,
                    Cells = "XXXXXXXXXXXXXXXXXXXXXOXOXXXXXXXXXXXXXXXXXXOOXXXXXXXXXXXXXXXXXXOXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                    User = user,
                    UserId = user.Id
                };
                context.Templates.Add(template);

                template = new Template
                {
                    Name = "Toad",
                    Height = 6,
                    Width = 6,
                    Cells = "XXXXXXXXXOXXXOXXOXXOXXOXXXOXXXXXXXXX",
                    User = user,
                    UserId = user.Id
                };
                context.Templates.Add(template);

                template = new Template
                {
                    Name = "Beacon",
                    Height = 6,
                    Width = 6,
                    Cells = "XXXXXXXOOXXXXOOXXXXXXOOXXXXOOXXXXXXX",
                    User = user,
                    UserId = user.Id
                };
                context.Templates.Add(template);

                template = new Template
                {
                    Name = "Lightweight Spaceship (LWSS)",
                    Height = 7,
                    Width = 40,
                    Cells = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXOXXOXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXOXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXOXXXOXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXOOOOXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                    User = user,
                    UserId = user.Id
                };
                context.Templates.Add(template);

                template = new Template
                {
                    Name = "Pulsar",
                    Height = 17,
                    Width = 17,
                    Cells = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXOOOXXXOOOXXXXXXXXXXXXXXXXXXXXXXXOXXXXOXOXXXXOXXXXOXXXXOXOXXXXOXXXXOXXXXOXOXXXXOXXXXXXOOOXXXOOOXXXXXXXXXXXXXXXXXXXXXXXXXOOOXXXOOOXXXXXXOXXXXOXOXXXXOXXXXOXXXXOXOXXXXOXXXXOXXXXOXOXXXXOXXXXXXXXXXXXXXXXXXXXXXXOOOXXXOOOXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                    User = user,
                    UserId = user.Id
                };
                context.Templates.Add(template);

                template = new Template
                {
                    Name = "Pentadecathlon",
                    Height = 18,
                    Width = 11,
                    Cells = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXOXXXXXXXXXOOOXXXXXXXOOOOOXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXOOOOOXXXXXXXOOOXXXXXXXXXOXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                    User = user,
                    UserId = user.Id
                };
                context.Templates.Add(template);

                template = new Template
                {
                    Name = "Gosper Glider Gun",
                    Height = 40,
                    Width = 40,
                    Cells = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXOXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXOXOXXXXXXXXXXXXXXXXXXXXXXXXXXXOOXXXXXXOOXXXXXXXXXXXXXOOXXXXXXXXXXXXXXOXXXOXXXXOOXXXXXXXXXXXXXOOXXXOOXXXXXXXXOXXXXXOXXXOOXXXXXXXXXXXXXXXXXXOOXXXXXXXXOXXXOXOOXXXXOXOXXXXXXXXXXXXXXXXXXXXXXXXXOXXXXXOXXXXXXXOXXXXXXXXXXXXXXXXXXXXXXXXXXOXXXOXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXOOXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                    User = user,
                    UserId = user.Id
                };
                context.Templates.Add(template);

                template = new Template
                {
                    Name = "YOLO",
                    Height = 3,
                    Width = 3,
                    Cells = "XXXXOXXXX",
                    User = user,
                    UserId = user.Id
                };
                context.Templates.Add(template);
            }
        }
    }
}
