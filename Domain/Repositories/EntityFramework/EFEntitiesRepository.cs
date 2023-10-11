using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq.Expressions;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.Domain.Repositories.EntityFramework
{
    public class EFEntitiesRepository <T> : IEntityRepository<T> where T:EntitiesBase
    {
        private readonly AplicationDBContext _dbContext;
        private readonly ILogger<EFEntitiesRepository<T>> _logger;

        public EFEntitiesRepository()
        {
            
        }

        public EFEntitiesRepository(AplicationDBContext dBContext, ILogger<EFEntitiesRepository<T>> logger)
        {
            _dbContext = dBContext;
            _logger = logger;
        }

        public void DeleteEntity(Guid id)
        {
            var entity = GetById(id);
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            IQueryable<T> entities = GetAll();
            List<T> entitiesList = await entities.ToListAsync();
            return entitiesList;
        }

        public T GetById(Guid id)
        {
            return _dbContext.Set<T>().FirstOrDefault(x => x.ID == id)!;
        }

        public T GetByTitle(string title)
        {
            return _dbContext.Set<T>().FirstOrDefault(x => x.Title == title)!;
        }

        public void SaveEntities(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Added(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public async Task AddedAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            _logger.LogInformation($"Объект {entity.Title} успешно добавлен. Завершил выполнение асинхронный метод Home/???/EFRepository/AddedAsync");
            await _dbContext.SaveChangesAsync();          
        }

        public IEnumerable<EntitiesBase> GetAllWorksAsync()
        {
            IEnumerable<WorkServices> workServices = _dbContext.Set<WorkServices>().ToList();
            IEnumerable<WorksList> workLists = _dbContext.Set<WorksList>().ToList();
            IEnumerable<WorksName> workNames = _dbContext.Set<WorksName>().ToList();
            IEnumerable<EntitiesBase> entities = workServices.Cast<EntitiesBase>().Concat(workLists.Cast<EntitiesBase>()).Concat(workNames.Cast<EntitiesBase>());          
            return entities;
        }
    }
}
