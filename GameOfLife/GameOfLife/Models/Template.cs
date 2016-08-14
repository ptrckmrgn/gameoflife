using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Results;
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

        [Required, Range(1, 100)]
        public int Height { get; set; }

        [Required, Range(1, 100)]
        public int Width { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Cells { get; set; }

        //[Required]
        public virtual ApplicationUser User { get; set; }
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