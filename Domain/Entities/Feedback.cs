using System.ComponentModel.DataAnnotations;

namespace VartanMVCv2.Domain.Entities
{
    public class Feedback : EntitiesBase
    {
        [Required]
        public override string? TitleImagePath { get => base.TitleImagePath; set => base.TitleImagePath = value; }
        [Required]
        public string FeedbackClientName { get; set; } = null!;
        [Required]
        public string FeedbackText { get; set; } = null!;
    }
}
