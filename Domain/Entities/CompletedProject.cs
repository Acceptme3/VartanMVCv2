using System.ComponentModel.DataAnnotations;

namespace VartanMVCv2.Domain.Entities
{
    public class CompletedProject:EntitiesBase
    {
        [Required]
        [Display(Name = "Название (Заголовок)")]
        public override string? Title { get => base.Title; set => base.Title = value; }
        [Required]
        [Display(Name = "Титульная картинка")]
        public override string? TitleImagePath { get => base.TitleImagePath; set => base.TitleImagePath = value; }
    }
}
