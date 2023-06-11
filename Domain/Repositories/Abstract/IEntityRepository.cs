using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Domain.Repositories.Abstract
{
    public interface IEntityRepository<T> where T : EntitiesBase
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        T GetByTitle(string title);
        void SaveEntities(T entity);
        void Added(T entity);
        void DeleteEntity(int id);
        Task<List<T>> GetAllAsync();
    }
}
