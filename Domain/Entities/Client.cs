using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Policy;
using VartanMVCv2;

namespace VartanMVCv2.Domain.Entities
{
    public class Client
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [StringLength(20, ErrorMessage = "Длина имени должна быть не более 20 символов")]
        public string Name { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Введите корректный email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Введите корректный номер телефона")]
        public string Phone { get; set; } = null!;

        public DateTime? CallTime { get; set; }

        public static List<Client> clients = new List<Client>();

        public Client() 
        {
           Id = Guid.NewGuid();
        } 


        public static void PrintClient()
        {
            if (clients.Count > 0) 
            {
                foreach (var client in clients) 
                {
                    Debug.WriteLine(
                        $"Объект #{clients.IndexOf(client)}: " +
                        $"Id Объекта: {client.Id.ToString()} " +
                        $"Имя объекта {client.Name} " +
                        $"Телефон {client.Phone} " +
                        $"Email {client.Email} " +
                        $"Время звонка {client.CallTime} ");
                }
            }
            else { Debug.WriteLine("Список объектов пуст"); }
        }



    }
}
