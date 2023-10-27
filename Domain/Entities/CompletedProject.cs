using System.ComponentModel.DataAnnotations;

namespace VartanMVCv2.Domain.Entities
{
    public class CompletedProject : EntitiesBase
    {
        [Required(ErrorMessage = "Это поле не должно быть пустым")]
        [Display(Name = "Название (Заголовок)")]
        public override string Title { get => base.Title!; set => base.Title = value; }
        [Display(Name = "Титульная картинка")]
        public override string? TitleImagePath { get => base.TitleImagePath; set => base.TitleImagePath = value; }

        public virtual List<CompletedProjectPhoto> Photos { get; set; } = new List<CompletedProjectPhoto>();

        public CompletedProject()
        {
           ID = Guid.NewGuid();
           TitleImagePath = $"/images/projectPhotos/default.png";
        }
    }

}
