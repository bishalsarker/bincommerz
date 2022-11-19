using BComm.PM.Models.Common;
using BComm.PM.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Migrators
{
    public class DataMigrator
    {
        private readonly MainDbContext _mainContext;
        private readonly LagecyDbContext _lagecyContext;

        public DataMigrator(IConfiguration config)
        {
            _mainContext = new MainDbContext(config);
            _lagecyContext = new LagecyDbContext(config);
        }

        public async Task Migrate()
        {
            await CopyData(_lagecyContext.Tags, _mainContext.Tags);
            await CopyData(_lagecyContext.Categories, _mainContext.Categories);
            await CopyData(_lagecyContext.Products, _mainContext.Products);
            await CopyData(_lagecyContext.ProductTags, _mainContext.ProductTags);
            await CopyData(_lagecyContext.Images, _mainContext.Images);
            await CopyData(_lagecyContext.ImageGallery, _mainContext.ImageGallery);
            await CopyData(_lagecyContext.Processes, _mainContext.Processes);
            await CopyData(_lagecyContext.Orders, _mainContext.Orders);
            await CopyData(_lagecyContext.DeliveryCharges, _mainContext.DeliveryCharges);
            await CopyData(_lagecyContext.OrderItems, _mainContext.OrderItems);
            await CopyData(_lagecyContext.OrderProcessLogs, _mainContext.OrderProcessLogs);
            await CopyData(_lagecyContext.OrderPaymentLogs, _mainContext.OrderPaymentLogs);
            await CopyData(_lagecyContext.Coupons, _mainContext.Coupons);
            await CopyData(_lagecyContext.Pages, _mainContext.Pages);
            await CopyData(_lagecyContext.Sliders, _mainContext.Sliders);
            await CopyData(_lagecyContext.SliderImages, _mainContext.SliderImages);
            await CopyData(_lagecyContext.Shops, _mainContext.Shops);
            await CopyData(_lagecyContext.Users, _mainContext.Users);
            await CopyData(_lagecyContext.Subscriptions, _mainContext.Subscriptions);
            await CopyData(_lagecyContext.UrlMappings, _mainContext.UrlMappings);

            _mainContext.SaveChanges();
        }

        private async Task CopyData<T>(DbSet<T> source, DbSet<T> destination) where T : BaseEntity
        {
            var allData = await source.ToListAsync();

            foreach (var item in allData)
            {
                await destination.AddAsync(item);
            }

            Console.WriteLine("Copied Data Into Table: " + nameof(T));
        }
    }
}
