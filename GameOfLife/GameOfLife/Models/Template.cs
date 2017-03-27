using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace GameOfLife.Models
{
    // Template object
    [Validator(typeof(TemplateValidator))]
    public class Template
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Range(1, 100)]
        public int Height { get; set; }

        [Required, Range(1, 100)]
        public int Width { get; set; }

        [Required]
        public string Cells { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        //// Constructor
        //public Template(string name, int height, int width, string cells, ApplicationUser user)
        //{
        //    this.Name = name;
        //    this.Height = height;
        //    this.Width = width;
        //    this.Cells = cells;
        //    this.User = user;
        //    this.UserId = User.Id;
        //}
    }

    public class TemplateValidator : AbstractValidator<Template>
    {
        public TemplateValidator()
        {
            RuleFor(template => template.Cells)
                .Must((template, cells) => cells.Length == template.Height * template.Width)
                .WithMessage("There must be exactly {0} cells.", template => template.Height * template.Width);
        }

        private bool BeValidLength(Template template, int height, int width)
        {
            return template.Cells.Length == template.Height * template.Width;
        }
    }

}