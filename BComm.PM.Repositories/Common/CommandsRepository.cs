using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Common
{
    public class CommandsRepository<T> where T : BaseEntity
    {
        public async Task Add(T entity)
        {
            using(var context = new MainDbContext())
            {
                await context.AddAsync<T>(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(T entity)
        {
            using (var context = new MainDbContext())
            {
                context.Update<T>(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(T entity)
        {
            using (var context = new MainDbContext())
            {
                context.Remove<T>(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
