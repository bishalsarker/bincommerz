using BComm.PM.Models.Products;
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
    public class ProductQueryRepository : IProductQueryRepository
    {
        public async Task<IEnumerable<Product>> GetProducts(string shopId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.HashId, {0}.Name, {0}.Description, {0}.Price, {0}.Discount," +
                    "{1}.Directory as ImageDirectory, {1}.ThumbnailImage as ImageUrl " +
                    "from {0} " +
                    "left join {1} on {0}.ImageUrl={1}.HashId " +
                    "where {0}.ShopId=@shopid",
                    TableNameConstants.ProductsTable,
                    TableNameConstants.ImagesTable)
                    .ToString();

                return await conn.QueryAsync<Product>(query, new { @shopid = shopId });
            }
        }

        public async Task<Product> GetProduct(string tagId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select Id, ShopId, Name, Description from {0} where HashId=@tagid", TableNameConstants.ProductsTable)
                    .ToString();

                var model = await conn.QueryAsync<Product>(query, new { @tagid = tagId });

                return model.FirstOrDefault();
            }
        }
    }
}
