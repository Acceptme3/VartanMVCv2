using System.ComponentModel.DataAnnotations;

namespace VartanMVCv2.Domain.Entities
{
    public class Feedback : EntitiesBase
    {       
        public override string? TitleImagePath { get => base.TitleImagePath; set => base.TitleImagePath = value; }
        [Required(ErrorMessage = "Это поле не должно быть пустым")]
        public string FeedbackClientName { get; set; } = "";
        [Required(ErrorMessage = "Это поле не должно быть пустым")]
        public string FeedbackText { get; set; } = "";      
        [EmailAddress(ErrorMessage = "Введите корректный email")]
        public string? FeedbackEmail { get; set; } = "";
        [DataType(DataType.PhoneNumber, ErrorMessage = "Введите корректный номер телефона")]
        [StringLength(18, MinimumLength = 16, ErrorMessage = "Введите корректный номер телефона")]
        public string? FeedbackPhone { get; set; } = "";

        public bool? FeedbackEnabled { get; set; }

        public DateTime? registrationDate { get; set; }

        private static string defaultPath = "/images/img-7.png";

        public Feedback()
        {
            ID = Guid.NewGuid();

            if (TitleImagePath == null)
            {
                TitleImagePath = defaultPath;
            }

            registrationDate = DateTime.Now;

            FeedbackEnabled = false;
        }
    }
}
