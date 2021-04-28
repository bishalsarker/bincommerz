using BComm.PM.Models.Tags;
using BComm.PM.Repositories.Common;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public class TagsQueryRepository : ITagsQueryRepository
    {
        public async Task<IEnumerable<ProductTags>> GetTagsByProductId(string productId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select TagHashId from {0} where ProductHashId=@productid", TableNameConstants.ProductTagsTable)
                    .ToString();

               return await conn.QueryAsync<ProductTags>(query, new { @productid = productId });
            }
        }

        public async Task<Tag> GetTag(string tagId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select Id, ShopId, Name, Description from {0} where HashId=@tagid", TableNameConstants.TagsTable)
                    .ToString();

                var model = await conn.QueryAsync<Tag>(query, new { @tagid = tagId });

                return model.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Tag>> GetTags(string shopId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {  
                var query = new StringBuilder()
                    .AppendFormat("select Id, HashId, Name, Description from {0} where ShopId=@shopid", TableNameConstants.TagsTable)
                    .ToString();

                return await conn.QueryAsync<Tag>(query, new { @shopid = shopId });
            }
        }
    }
}
