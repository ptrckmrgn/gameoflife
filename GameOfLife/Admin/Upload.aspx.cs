using GameOfLife.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GameOfLife.Admin
{
    public partial class Upload : System.Web.UI.Page
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (TemplateUpload.PostedFile.FileName != "")
            {
                string filename = TemplateUpload.FileName;
                string name = filename.Substring(0, filename.Length - 4);
                string ext = filename.Substring(filename.Length - 4, 4);

                if (ext == ".txt")
                {
                    string content = new StreamReader(TemplateUpload.PostedFile.InputStream).ReadToEnd();

                    string[] templateArray = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                    // Get cells string
                    string cells = "";
                    for (int i = 2; i < templateArray.Count() - 1; i++)
                    {
                        cells += templateArray[i];
                    }

                    // Create new template
                    Template template = new Template();
                    template.Name = name;
                    template.Height = Int32.Parse(templateArray[0]);
                    template.Width = Int32.Parse(templateArray[1]);
                    template.Cells = cells;
                    template.User = db.Users.Find(User.Identity.GetUserId());

                    // Add to database
                    db.Templates.Add(template);
                    db.SaveChanges();

                    Message.Text = "Upload successful!";
                    Message.CssClass = "text-success";
                }
                else
                {
                    Message.Text = "Upload needs to be a text file.";
                    Message.CssClass = "text-danger";
                }
            }
            else
            {
                Message.Text = "You did not specify a file to upload.";
                Message.CssClass = "text-danger";
            }
        }
    }
}