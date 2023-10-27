using System.Text;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Models
{
    public class SortingManager  // класс для сортировки списков полученных из бд
    {
        private readonly DataManager _dataManager;
        private readonly ILogger<SortingManager> _logger;

        public SortingManager(DataManager dataManager, ILogger<SortingManager> logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        public IEnumerable<Client> GetAllClientForADate(IEnumerable<Client> clientsList,DateTime dateTime, SortingTime sortingTime) 
        {
            IEnumerable<Client> all = clientsList;
            DateOnly date = DateOnly.FromDateTime(dateTime);

            if (sortingTime == SortingTime.Day) 
            {
               IEnumerable<Client> result = from client in all where DateOnly.FromDateTime(client.RegistrationDate) == date orderby client.RegistrationDate select client;
               return result;
            }

            if (sortingTime == SortingTime.Month)
            {
                IEnumerable<Client> result = from client in all where DateOnly.FromDateTime(client.RegistrationDate).Month == date.Month orderby client.RegistrationDate select client;
                return result;
            }

            if (sortingTime == SortingTime.Year)
            {
                IEnumerable<Client> result = from client in all where DateOnly.FromDateTime(client.RegistrationDate).Year == date.Year orderby client.RegistrationDate select client;
                return result;
            }

            return all;
        }

        public static IEnumerable<Client> GetAllClientForACondition(IEnumerable<Client> clientsList, bool condition) 
        {
            IEnumerable<Client> all = clientsList;
            List<Client> result = new List<Client>();
            foreach (Client client in all) 
            {
                if (client.IsComplete == condition) 
                {
                    result.Add(client);
                    
                }
            }
            return result;
        }

        public static string ConvertPropertyToString<T>(List<T> objects, string propertyName)
        {
            StringBuilder result = new StringBuilder();

            foreach (T obj in objects)
            {
                var property = typeof(T).GetProperty(propertyName);
                if (property != null)
                {
                    var value = property.GetValue(obj);
                    result.Append(value?.ToString() ?? "null");
                }
                else
                {
                    result.Append("Property not found");
                }
                result.Append(',');
            }

            if (result.Length > 0)
            {
                result.Length -= 1;
            }

            return result.ToString();
        }
    }

    public enum SortingTime 
    {
        Day, Month, Year
    }
}
