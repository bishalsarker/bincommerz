using BComm.PM.Models.Widgets;
using BComm.PM.Repositories.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public class SliderQueryRepository : ISliderQueryRepository
    {
        private readonly string _connectionString;

        public SliderQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<IEnumerable<Slider>> GetSliders(string shopId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where ShopId=@shopid", TableNameConstants.SlidersTable)
                    .ToString();

                return await conn.QueryAsync<Slider>(query, new { @shopid = shopId });
            }
        }

        public async Task<Slider> GetSlider(string sliderId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where HashId=@sliderid", TableNameConstants.SlidersTable)
                    .ToString();

                var result = await conn.QueryAsync<Slider>(query, new { @sliderid = sliderId });

                return result.FirstOrDefault();
            }
        }

        public async Task<SliderImage> GetSliderImage(string sliderImageId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.*, " +
                    "{1}.OriginalImage as ImageName, {1}.Directory as ImageDirectory " +
                    "from {0} " +
                    "left join {1} on {1}.HashId = {0}.ImageId " +
                    "where {0}.HashId=@slideid",
                    TableNameConstants.SliderImagesTable,
                    TableNameConstants.ImagesTable)
                    .ToString();

                var result = await conn.QueryAsync<SliderImage>(query, new { @slideid = sliderImageId });

                return result.FirstOrDefault();
            }
        }

        public async Task DeleteSliderImage(string sliderImageId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("delete from {0} where HashId=@slideid", TableNameConstants.SliderImagesTable)
                    .ToString();

                await conn.ExecuteAsync(query, new { @slideid = sliderImageId });
            }
        }

        public async Task<IEnumerable<SliderImage>> GetSliderImages(string sliderId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select {0}.HashId, {0}.Title, {0}.Description, {0}.ButtonText, {0}.ButtonUrl, {0}.ImageId, " +
                    "{1}.OriginalImage as ImageName, {1}.Directory as ImageDirectory " +
                    "from {0} " +
                    "left join {1} on {1}.HashId = {0}.ImageId " +
                    "where {0}.SliderId=@sliderid",
                    TableNameConstants.SliderImagesTable,
                    TableNameConstants.ImagesTable)
                    .ToString();

                return await conn.QueryAsync<SliderImage>(query, new { @sliderid = sliderId });
            }
        }
    }
}
