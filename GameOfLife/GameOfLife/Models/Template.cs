using FluentValidation;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GameOfLife.Models
{
    // Template object
    [Validator(typeof(TemplateValidator))]
    public class Template
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public string Cells { get; set; }
        
        public virtual ApplicationUser User { get; set; }
    }

    public class TemplateValidator : AbstractValidator<Template>
    {
        public TemplateValidator()
        {
            RuleFor(template => template.Cells).Length(template => template.Height * template.Width).WithMessage("Incorrect number of cells.");
            RuleFor(template => template.Name).Must(template => template.Contains("abc")).WithMessage("ABC!!!");
        }
    }
}