using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VartanMVCv2.Domain.Entities
{
    public class CompletedProjectPhoto:EntitiesBase
    {
        public virtual CompletedProject Project { get; set; } = new CompletedProject();
        public Guid CompletedProjectID { get; set; } 
        public override string? Description { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; }

        public CompletedProjectPhoto()
        {
            ID = Guid.NewGuid();
            IsSelected = false;
        }
    }
}
