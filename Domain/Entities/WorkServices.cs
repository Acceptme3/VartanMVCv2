using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.Domain.Entities
{
    public class WorkServices : Works
    {
        [Required(ErrorMessage = "Это поле не должно быть пустым")]
        [Display(Name = "Название (Заголовок)")]
        public override string? Title { get => base.Title; set => base.Title = value; }
        [Display(Name = "Описание")]
        public override string? Description { get => base.Description; set => base.Description = value; }
        [Display(Name = "Титульная картинка")]
        public override string? TitleImagePath { get => base.TitleImagePath; set => base.TitleImagePath = value; }

        public List<WorksList> worksLists { get; set; } = new List<WorksList>();

        public WorkServices()
        {
            ID = Guid.NewGuid();
        }
    }
}
