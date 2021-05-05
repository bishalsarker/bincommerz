using BComm.PM.Models.Images;
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
    public class ImagesQueryRepository : IImagesQueryRepository
    {
        public async Task<Image> GetImage(string imageId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where HashId=@imageid", TableNameConstants.ImagesTable)
                    .ToString();

                var model = await conn.QueryAsync<Image>(query, new { @imageid = imageId });

                return model.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Image>> GetImageGallery(string productId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.Directory, {0}.ThumbnailImage, {0}.OriginalImage, {0}.HashId " +
                    "from {0} " +
                    "inner join {1} on {0}.HashId={1}.ImageId " +
                    "and {1}.ProductId=@productid",
                    TableNameConstants.ImagesTable,
                    TableNameConstants.ImageGalleryTable)
                    .ToString();

                return await conn.QueryAsync<Image>(query, new { @productid = productId });
            }
        }

        public async Task DeleteImagesByProductId(string productId)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=bincommerz;Trusted_Connection=True;"))
            {
                var query = new StringBuilder()
                    .AppendFormat("delete from {0} where ProductId=@productid", TableNameConstants.ImageGalleryTable)
                    .ToString();

                await conn.ExecuteAsync(query, new { @productid = productId });
            }
        }
    }
}
