using BComm.PM.Models.Processes;
using BComm.PM.Repositories.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public class ProcessQueryRepository : IProcessQueryRepository
    {
        public async Task<Process> GetProcess(string processId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where HashId=@processid", TableNameConstants.ProcessTable)
                    .ToString();

                var result = await conn.QueryAsync<Process>(query, new { @processid = processId });

                return result.FirstOrDefault();
            }
        }

        public async Task<Process> GetNextProcess(string shopId, int currentStep)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where ShopId=@shopid and StepNumber=@nextstep", TableNameConstants.ProcessTable)
                    .ToString();

                var result = await conn.QueryAsync<Process>(query, new { @shopid = shopId, @nextstep = currentStep + 1 });

                return result.FirstOrDefault();
            }
        }
    }
}
