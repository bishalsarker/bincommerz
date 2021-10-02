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
                    .AppendFormat("select * from {0} where HashId=@shopid", TableNameConstants.SlidersTable)
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

                var result = await conn.QueryAsync<Slider>(query, new { @hashid = sliderId });

                return result.FirstOrDefault();
            }
        }

        public async Task<SliderImage> GetSliderImage(string sliderImageId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where HashId=@hashid", TableNameConstants.SliderImagesTable)
                    .ToString();

                var result = await conn.QueryAsync<SliderImage>(query, new { @hashid = sliderImageId });

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<SliderImage>> GetSliderImages(string sliderId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var query = new StringBuilder()
                    .AppendFormat("select * from {0} where SliderId=@sliderid", TableNameConstants.SliderImagesTable)
                    .ToString();

                return await conn.QueryAsync<SliderImage>(query, new { @sliderid = sliderId });
            }
        }
    }
}
