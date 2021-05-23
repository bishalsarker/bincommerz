using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Processes;
using BComm.PM.Models.Processes;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services
{
    public class ProcessService : IProcessService
    {
        private readonly ICommandsRepository<Process> _processCommandsRepository;
        private readonly IProcessQueryRepository _processQueryRepository;
        private readonly IMapper _mapper;

        public ProcessService(
            ICommandsRepository<Process> processCommandsRepository,
            IProcessQueryRepository processQueryRepository,
            IMapper mapper)
        {
            _processCommandsRepository = processCommandsRepository;
            _processQueryRepository = processQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> GetNextProcess(string shopId, int currentStep)
        {
            var processModel = await _processQueryRepository.GetNextProcess(shopId, currentStep);

            return new Response()
            {
                Data = processModel != null ? _mapper.Map<ProcessResponse>(processModel) : new ProcessResponse(),
                IsSuccess = true
            };
        }
    }
}
