using Microsoft.EntityFrameworkCore;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.Domain.Repositories.EntityFramework
{
    public class ClientRepository : IClientRepository
    {
        private readonly AplicationDBContext _context;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(AplicationDBContext dBContext, ILogger<ClientRepository> logger)
        {
            _context = dBContext;
            _logger = logger;
        }


        public void Added(Client client)
        {
            _context.Set<Client>().Add(client);
            _context.SaveChanges();
            _logger.LogInformation($"Объект {client.Name} успешно добавлен. Завершил выполнение метод Home/???/ClientRepository/Added");
        }

        public async Task AddedAsync (Client client) 
        {
            await _context.Set<Client>().AddAsync(client);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Объект {client.Name} успешно добавлен. Завершил выполнение асинхронный метод Home/???/ClientRepository/AddedAsync");
        }

        public void DeleteEntity(Guid id)
        {
            var currentClient = GetById(id);
            if (currentClient != null) 
            {
             _context.Set<Client>().Remove(currentClient);
             _context.SaveChanges();
            }        
        }

        public void DeleteEntity(string phone)
        {
            var currentClient = GetByPhone(phone);
            if (currentClient != null)
            {
                _context.Set<Client>().Remove(currentClient);
                _context.SaveChanges();
            }
        }

        public IQueryable<Client> GetAll()
        {
            return _context.Set<Client>();
        }

        public async Task<List<Client>> GetAllAsync()
        {
            IQueryable<Client> clients = GetAll();
            List<Client> clientList = await clients.ToListAsync();
            return clientList;
        }

        public Client? GetById(Guid id)
        {
            Client? currentClient = _context.Set<Client>().FirstOrDefault(x => x.Id == id);
            if (currentClient == null) 
            {
                Exception ex = new Exception("ClientRepository (GetByID)- [Не найдено объекта с данным идентификатором.]");
                _logger.LogError(ex.Message);
                return null;
            }
            return currentClient;
        }

        public Client? GetByName(string name)
        {
            Client? currentClient = _context.Set<Client>().FirstOrDefault(x => x.Name == name);
            if (currentClient == null)
            {
                Exception ex = new Exception("ClientRepository (GetByID)- [Не найдено объекта с данным именем.]");
                _logger.LogError(ex.Message);
                return null;
            }
            return currentClient;
        }

        public Client? GetByPhone(string phone)
        {
            Client? currentClient = _context.Set<Client>().FirstOrDefault(x => x.Phone == phone);
            if (currentClient == null)
            {
                Exception ex = new Exception("ClientRepository (GetByID)- [Не найдено объекта с указанным номером телефона.]");
                _logger.LogError(ex.Message);
                return null;
            }
            return currentClient;
        }

    }
}
