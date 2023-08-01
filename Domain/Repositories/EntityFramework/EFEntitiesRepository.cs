﻿using Microsoft.EntityFrameworkCore;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain.Repositories.Abstract;

namespace VartanMVCv2.Domain.Repositories.EntityFramework
{
    public class EFEntitiesRepository <T> : IEntityRepository<T> where T  :EntitiesBase
    {
        private readonly AplicationDBContext _dbContext;
        private readonly ILogger<EFEntitiesRepository<T>> _logger;

        public EFEntitiesRepository(AplicationDBContext dBContext, ILogger<EFEntitiesRepository<T>> logger)
        {
            _dbContext = dBContext;
            _logger = logger;
        }

        public void DeleteEntity(int id)
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

        public T GetById(int id)
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
    }
}
