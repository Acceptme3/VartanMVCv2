using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Domain.Repositories.Abstract
{
    public interface IEntityRepository<T> where T : EntitiesBase
    {
        IQueryable<T> GetAll();
        T GetById(Guid id);
        T GetByTitle(string title);
        void SaveEntities(T entity);
        void Added(T entity);
        Task AddedAsync(T entity);
        void DeleteEntity(Guid id);
        Task<List<T>> GetAllAsync();
    }
}
