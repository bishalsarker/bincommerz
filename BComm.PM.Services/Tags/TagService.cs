using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Dto.Tags;
using BComm.PM.Models.Tags;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly ICommandsRepository<Tag> _commandsRepository;
        private readonly ITagsQueryRepository _tagsQueryRepository;
        private readonly IMapper _mapper;

        public TagService(
            ICommandsRepository<Tag> commandsRepository,
            ITagsQueryRepository tagsQueryRepository,
            IMapper mapper)
        {
            _commandsRepository = commandsRepository;
            _tagsQueryRepository = tagsQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> AddNewTag(TagPayload newTagRequest)
        {
            var tagModel = _mapper.Map<Tag>(newTagRequest);
            tagModel.ShopId = "vbt_xyz";
            await _commandsRepository.Add(tagModel);

            return new Response()
            {
                Data = _mapper.Map<TagsResponse>(tagModel),
                Message = "Tag Created Successfully",
                IsSuccess = true
            };
        }

        public async Task<Response> GetTags(string shopId)
        {
            var tagModels = await _tagsQueryRepository.GetTags(shopId);
            return new Response()
            {
                Data = _mapper.Map<IEnumerable<TagsResponse>>(tagModels),
                IsSuccess = true
            };
        }
    }
}
