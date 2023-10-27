using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.Domain.Entities
{
    public class WorksCategory : EntitiesBase
    {
        [Required(ErrorMessage = "Укажите название категории")]
        [Display(Name = "Название категории")]
        public override string Title { get => base.Title; set => base.Title = value; }

        public virtual WorkServices Services { get; set; }
        public Guid WorkServicesID { get; set; }

        public virtual List<Work> Works { get; set; } = new List<Work>();

        public WorksCategory() 
        {
            ID = Guid.NewGuid();
            Title = "default_CategoryName";
            Services = new WorkServices();
        }

        public WorksCategory(string title, WorkServices services)
        {
            ID = Guid.NewGuid();
            Title = title;
            Services = services;
        }
    }
}
