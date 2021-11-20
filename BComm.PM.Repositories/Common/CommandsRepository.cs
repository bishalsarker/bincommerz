using BComm.PM.Models.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Common
{
    public class CommandsRepository<T> : ICommandsRepository<T> where T : BaseEntity
    {
        private readonly IConfiguration _configuration;

        public CommandsRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task Add(T entity)
        {
            using (var context = new MainDbContext(_configuration))
            {
                entity.CreatedOn = DateTime.UtcNow;

                await context.AddAsync<T>(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(T entity)
        {
            using (var context = new MainDbContext(_configuration))
            {
                context.Update<T>(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(T entity)
        {
            using (var context = new MainDbContext(_configuration))
            {
                context.Remove<T>(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
