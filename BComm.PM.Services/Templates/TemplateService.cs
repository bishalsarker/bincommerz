using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Template;
using BComm.PM.Models.Templates;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Services.Templates
{
    public class TemplateService : ITemplateService
    {
        private readonly ICommandsRepository<Template> _templateCommandsRepository;
        private readonly ITemplateQueryRepository _templateQueryRepository;
        private readonly IMapper _mapper;

        public TemplateService(
            ICommandsRepository<Template> templateCommandsRepository,
            ITemplateQueryRepository templateQueryRepository,
            IMapper mapper)
        {
            _templateCommandsRepository = templateCommandsRepository;
            _templateQueryRepository = templateQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> GetTemplate(string templateId)
        {
            var templateModel = await _templateQueryRepository.GetTemplate(templateId);
            var templateResponse = _mapper.Map<TemplateResponse>(templateModel);

            return new Response()
            {
                Data = templateResponse,
                IsSuccess = true
            };
        }

        public async Task<Response> GetAllTemplates(string shopId)
        {
            var templateModels = await _templateQueryRepository.GetTemplates(shopId);
            var templateResponses = new List<TemplateResponse>();

            foreach (var templateModel in templateModels)
            {
                var templateResponse = _mapper.Map<TemplateResponse>(templateModel);
                templateResponses.Add(templateResponse);
            }

            return new Response()
            {
                Data = templateResponses,
                IsSuccess = true
            };
        }

        public async Task<Response> AddTemplate(TemplatePayload templateUpdateRequest, string shopId)
        {
            try
            {
                var templateModel = _mapper.Map<Template>(templateUpdateRequest);
                templateModel.HashId = Guid.NewGuid().ToString("N");
                templateModel.CreatedOn = DateTime.UtcNow;
                templateModel.ShopId = shopId;

                if (templateModel.IsDefault)
                {
                    var existingDefaultTemplate = await _templateQueryRepository.GetDefaultTemplate(shopId);
                    if (existingDefaultTemplate != null)
                    {
                        existingDefaultTemplate.IsDefault = false;
                        await _templateCommandsRepository.Update(existingDefaultTemplate);
                    }
                }

                await _templateCommandsRepository.Add(templateModel);

                return new Response()
                {
                    Data = new { Id = templateModel.HashId },
                    Message = "Template Added Successfully",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Message = "Error: " + ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> UpdateTemplate(TemplateUpdatePayload templateUpdateRequest)
        {
            try
            {
                var existingTemplateModel = await _templateQueryRepository.GetTemplate(templateUpdateRequest.Id);

                if (existingTemplateModel != null)
                {
                    var templateModel = _mapper.Map<Template>(templateUpdateRequest.TemplateData);
                    existingTemplateModel.Name = templateModel.Name;
                    existingTemplateModel.Content = templateModel.Content;
                    existingTemplateModel.IsDefault = templateModel.IsDefault;

                    if (existingTemplateModel.IsDefault)
                    {
                        var existingDefaultTemplate = await _templateQueryRepository.GetDefaultTemplate(existingTemplateModel.ShopId);
                        if (existingDefaultTemplate != null)
                        {
                            existingDefaultTemplate.IsDefault = false;
                            await _templateCommandsRepository.Update(existingDefaultTemplate);
                        }
                    }

                    await _templateCommandsRepository.Update(existingTemplateModel);

                    return new Response()
                    {
                        Data = new { Id = templateModel.HashId },
                        Message = "Template Updated Successfully",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Message = "Error: No template exists!",
                        IsSuccess = false
                    };
                }

            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Message = "Error: " + ex.Message,
                    IsSuccess = false
                };
            }
        }
    }
}
