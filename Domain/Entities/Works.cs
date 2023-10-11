using System.ComponentModel.DataAnnotations.Schema;

namespace VartanMVCv2.Domain.Entities
{
    public abstract class Works: EntitiesBase
    {
        [NotMapped]
        protected Works? ParentReference { get; set; }
    }
}
