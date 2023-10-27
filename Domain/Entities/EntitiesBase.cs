using System.ComponentModel.DataAnnotations;

namespace VartanMVCv2.Domain.Entities
{
    public abstract class EntitiesBase
    {
        [Key]
        [Required]
        public Guid ID { get; set; }

        [Display(Name = "Название (Заголовок)")]
        public virtual string Title { get; set; } = "default";

        [Display(Name = "Описание")]
        public virtual string? Description { get; set; }

        [Display(Name = "Титульная картинка")]
        public virtual string? TitleImagePath { get; set; }

        [Display(Name = "SEO-Метатэг Title")]
        public string? MetaTitle { get; set; }

        [Display(Name = "SEO-Метатэг Discription")]
        public string? MetaDescription { get; set; }

        [Display(Name = "SEO-Метатэг Keywords")]
        public string? MetaKeywords { get; set; }
    }

}
