using System.ComponentModel.DataAnnotations;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.Domain.Entities
{
    public class Work : EntitiesBase
    {
        [Required(ErrorMessage = "Укажите наименование работы")]
        public override string Title { get => base.Title; set => base.Title = value; }
        public virtual WorksCategory WorksCategory { get; set; }

        public Work() 
        {
            ID = Guid.NewGuid();
            Title = "default_Works";
            WorksCategory = new WorksCategory();
        }   

        public Work(string title, WorksCategory category)
        {
            Title = title;
            ID = Guid.NewGuid();
            WorksCategory = category;
        }
    }
}
