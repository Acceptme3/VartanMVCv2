using System.ComponentModel.DataAnnotations;


namespace VartanMVCv2.Domain.Entities
{
    public class WorkServices : EntitiesBase
    {
        [Required(ErrorMessage = "Укажите название услуги! Это поле не может быть пустым")]
        [Display(Name = "Название (Заголовок)")]
        public override string Title { get => base.Title; set => base.Title = value; }
        [Display(Name = "Описание")]
        public override string? Description { get => base.Description; set => base.Description = value; }
        [Display(Name = "Титульная картинка")]
        public override string? TitleImagePath { get => base.TitleImagePath; set => base.TitleImagePath = value; }
        [Display(Name = "Стоимость услуги")]
        public string Price { get; set; } = "default_price";
        public string Deadline { get; set; } = "default_deadline";
        public int DisplayOrder { get; set; }

        public virtual List<WorksCategory> WorksCategories { get; set; }  = new List<WorksCategory>();

        public WorkServices()
        {
            Title = "default_Services";
            Price = "default_Price";
        }
    }
}
