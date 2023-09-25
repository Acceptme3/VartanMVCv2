using Microsoft.Build.Framework;

namespace VartanMVCv2.Domain.Entities
{
    public class WorksName:EntitiesBase
    {
        [Required]
        public override string? Title { get => base.Title; set => base.Title = value; }
        [Required]
        public WorksList WorksCategory { get; set; } = null!;
            
        public WorksName()
        {
            ID = Guid.NewGuid();
        }
    }
}
