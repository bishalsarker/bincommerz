using AutoMapper;
using BComm.PM.Dto.Processes;
using BComm.PM.Models.Processes;

namespace BComm.PM.Services.Mappings
{
    public class ProcessMappings : Profile
    {
        public ProcessMappings()
        {
            CreateMap<Process, ProcessResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId));
        }
    }
}
