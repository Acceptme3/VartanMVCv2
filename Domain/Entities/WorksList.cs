using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace VartanMVCv2.Domain.Entities
{
    public class WorksList : EntitiesBase
    {
        [Required]
        [Display(Name = "Название категории")]
        public override string Title { get => base.Title!; set => base.Title = value; }
        [Required]
        public WorkServices Services { get; set; } = null!;
    }
}
