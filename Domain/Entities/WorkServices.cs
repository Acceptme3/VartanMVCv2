using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace VartanMVCv2.Domain.Entities
{
    public class WorkServices : EntitiesBase
    {
        [Required]
        [Display(Name = "Название (Заголовок)")]
        public override string? Title { get => base.Title; set => base.Title = value; }
        [Display(Name = "Описание")]
        public override string? Description { get => base.Description; set => base.Description = value; }
        [Display(Name = "Титульная картинка")]
        public override string? TitleImagePath { get => base.TitleImagePath; set => base.TitleImagePath = value; }
    }
}
