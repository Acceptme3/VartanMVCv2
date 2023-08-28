using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Domain.Repositories.Abstract
{
    public interface IClientRepository
    {
        IQueryable<Client> GetAll();
        Client? GetById(Guid id);
        Client? GetByName(string name);
        Client? GetByPhone(string phone);      
        void Added(Client entity);
        void Update(Client client);
        Task AddedAsync(Client client);
        void DeleteEntity(Guid id);
        void DeleteEntity(string phone);
        Task<List<Client>> GetAllAsync();
    }
}
