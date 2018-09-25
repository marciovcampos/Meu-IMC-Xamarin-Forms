using CalcFreelancer.Domain.Core.Models;
using CalcFreelancer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalcFreelancer.Infra.Data.Repository
{
    public class SqLiteRepository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        public async Task Delete(TEntity tEntity)
        {
            await DatabaseContext.Database.DeleteAsync(tEntity);
        }

        public async Task<TEntity> Find(string id)
        {
            return await DatabaseContext.Database.Table<TEntity>().Where(x => x.Id == id).FirstAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DatabaseContext.Database.Table<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetFirst()
        {
            return await DatabaseContext.Database.Table<TEntity>().FirstOrDefaultAsync();
        }

        public async Task Insert(TEntity tEntity)
        {
            await DatabaseContext.Database.InsertAsync(tEntity);
        }

        public async Task Update(TEntity tEntity)
        {
            await DatabaseContext.Database.UpdateAsync(tEntity);
        }
    }
}
