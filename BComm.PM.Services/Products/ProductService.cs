using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Dto.Tags;
using BComm.PM.Models.Products;
using BComm.PM.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ICommandsRepository<Product> _commandsRepository;
        // private readonly ITagsQueryRepository _tagsQueryRepository;
        private readonly IMapper _mapper;

        public ProductService(
            ICommandsRepository<Product> commandsRepository,
            // ITagsQueryRepository tagsQueryRepository,
            IMapper mapper)
        {
            _commandsRepository = commandsRepository;
            // _tagsQueryRepository = tagsQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> AddNewProduct(ProductPayload newProductRequest)
        {
            var productModel = _mapper.Map<Product>(newProductRequest);
            productModel.HashId = Guid.NewGuid().ToString("N");
            productModel.ShopId = "vbt_xyz";
            await _commandsRepository.Add(productModel);

            return new Response()
            {
                Data = new ProductResponse() { Id = productModel.HashId },
                Message = "Product Created Successfully",
                IsSuccess = true
            };
        }
    }
}
