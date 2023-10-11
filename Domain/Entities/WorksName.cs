using System.ComponentModel.DataAnnotations;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.Domain.Entities
{
    public class WorksName:Works
    {      
        public override string? Title { get => base.Title; set => base.Title = value; }
        public WorksList? WorksCategory { get; set; } 
            
        public WorksName()
        {
            ID = Guid.NewGuid();
            ParentReference = WorksCategory;
        }
    }
}
