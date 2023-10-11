using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.Domain.Entities
{
    public class WorksList : Works
    {
        [Display(Name = "Название категории")]
        public override string? Title { get => base.Title!; set => base.Title = value; }

        public WorkServices? Services { get; set; } 

        public List<WorksName> worksNames { get; set; } = new List<WorksName>();

        public WorksList()
        {
            ID = Guid.NewGuid();
            ParentReference = Services;
        }
    }
}
