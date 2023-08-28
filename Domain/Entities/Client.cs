using Azure;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Policy;
using VartanMVCv2;

namespace VartanMVCv2.Domain.Entities
{
    public class Client: IValidatableObject
    {
        [Required]
        public Guid Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsComplete { get; set; }

        [Required(ErrorMessage = "Поле ИМЯ пустое, либо содержит некорректное значение.")]
        [StringLength(20, ErrorMessage = "Длина имени должна быть не более 20 символов")]
        public string Name { get; set; } = "";

        [EmailAddress(ErrorMessage = "Введите корректный email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Поле ТЕЛЕФОН пустое, либо содержит некорректное значение.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Введите корректный номер телефона")]
        [StringLength(18, MinimumLength = 16, ErrorMessage = "Введите корректный номер телефона")]
        public string Phone { get; set; } = "";

        [StringLength(150, MinimumLength = 10, ErrorMessage = "Длина текста не должна быть более 150 символов и менее 10 символов")]
        public string? HisQuestion { get; set; }

        public DateTime? CallTime { get; set; }

        public Client() 
        {
            Id = Guid.NewGuid();
            RegistrationDate = DateTime.Now;
            IsComplete = false;
        } 

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (Name == "admin")
            {
                errors.Add(new ValidationResult("Некорректное имя", new List<string>() { "Name" }));
            }

            return errors;
        }
    }
}
