using BComm.PM.Models.Common;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Common
{
    public interface ICommandsRepository<T> where T : BaseEntity
    {
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
    }
}