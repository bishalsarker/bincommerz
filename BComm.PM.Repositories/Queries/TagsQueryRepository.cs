using BComm.PM.Models.Tags;
using BComm.PM.Repositories.Common;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public class TagsQueryRepository : ITagsQueryRepository
    {
        public async Task<IEnumerable<Tag>> GetTags(string shopId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {  
                var query = new StringBuilder()
                    .AppendFormat("select Id, Name, Description from {0} where ShopId=@shopid", TableNameConstants.TagsTable)
                    .ToString();

                return await conn.QueryAsync<Tag>(query, new { @shopid = shopId });
            }
        }
    }
}
