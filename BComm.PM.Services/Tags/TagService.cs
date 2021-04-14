using AutoMapper;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Tags;
using BComm.PM.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly ICommandsRepository<Tag> _commandsRepository;
        private readonly IMapper _mapper;

        public TagService(ICommandsRepository<Tag> commandsRepository, IMapper mapper)
        {
            _commandsRepository = commandsRepository;
            _mapper = mapper;
        }

        public async Task AddNewTag(TagPayload newTagRequest)
        {
            await _commandsRepository.Add(_mapper.Map<Tag>(newTagRequest));
        }
    }
}
