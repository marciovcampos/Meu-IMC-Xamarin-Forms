using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalcFreelancer.Domain.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> Find(string id);
        Task<TEntity> GetFirst();
        Task<IEnumerable<TEntity>> GetAll();
        Task Insert(TEntity tEntity);
        Task Update(TEntity tEntity);
        Task Delete(TEntity tEntity);
    }
}
