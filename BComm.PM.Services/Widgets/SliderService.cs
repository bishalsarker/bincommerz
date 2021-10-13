using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Widgets;
using BComm.PM.Models.Images;
using BComm.PM.Models.Widgets;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using BComm.PM.Services.Common;
using BComm.PM.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Widgets
{
    public class SliderService : ISliderService
    {
        private readonly IImageService _imageService;
        private readonly ICommandsRepository<Slider> _sliderCommandsRepository;
        private readonly ICommandsRepository<SliderImage> _sliderImageCommandsRepository;
        private readonly ISliderQueryRepository _sliderQueryRepository;
        private readonly IHostingEnvironment _env;
        private readonly IMapper _mapper;

        public SliderService(
            IImageService imageService,
            ICommandsRepository<Slider> sliderCommandsRepository,
            ICommandsRepository<SliderImage> sliderImageCommandsRepository,
            ISliderQueryRepository sliderQueryRepository,
            IMapper mapper,
            IHostingEnvironment env)
        {
            _imageService = imageService;
            _sliderCommandsRepository = sliderCommandsRepository;
            _sliderImageCommandsRepository = sliderImageCommandsRepository;
            _sliderQueryRepository = sliderQueryRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<Response> AddSlider(SliderPayload newSliderRequest, string shopId)
        {
            try
            {
                var sliderModel = _mapper.Map<Slider>(newSliderRequest);
                sliderModel.HashId = Guid.NewGuid().ToString("N");
                sliderModel.ShopId = shopId;
                await _sliderCommandsRepository.Add(sliderModel);

                return new Response()
                {
                    Data = new { Id = sliderModel.HashId },
                    Message = "Slider Created Successfully",
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

        public async Task<Response> AddSliderImage(SliderImagePayload newSliderImageRequest)
        {
            try
            {
                var sliderImageModel = _mapper.Map<SliderImage>(newSliderImageRequest);
                sliderImageModel.HashId = Guid.NewGuid().ToString("N");
                sliderImageModel.CreatedOn = DateTime.UtcNow;

                var sliderImage = new ImageInfo(newSliderImageRequest.Image, sliderImageModel.HashId, _env);
                var imageModel = await _imageService.AddImage(sliderImage);

                sliderImageModel.ImageId = imageModel.HashId;
                await _sliderImageCommandsRepository.Add(sliderImageModel);

                return new Response()
                {
                    Data = new { Id = sliderImageModel.HashId },
                    Message = "Slider Image Added Successfully",
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

        public async Task<Response> UpdateSliderImage(SliderImageUpdatePayload newSliderImageRequest)
        {
            try
            {
                var existingSliderImageModel = await _sliderQueryRepository.GetSliderImage(newSliderImageRequest.Id);

                if (existingSliderImageModel != null)
                {
                    var sliderImageModel = _mapper.Map<SliderImage>(newSliderImageRequest.SliderImage);
                    existingSliderImageModel.Title = sliderImageModel.Title;
                    existingSliderImageModel.Description = sliderImageModel.Description;
                    existingSliderImageModel.ButtonText = sliderImageModel.ButtonText;
                    existingSliderImageModel.ButtonUrl = sliderImageModel.ButtonUrl;

                    if(!string.IsNullOrEmpty(newSliderImageRequest.SliderImage.Image))
                    {
                        var newSliderImage = new ImageInfo(newSliderImageRequest.SliderImage.Image, Guid.NewGuid().ToString("N"), _env);
                        var imageModel = await _imageService.UpdateImage(existingSliderImageModel.ImageId, newSliderImage);
                        existingSliderImageModel.ImageId = imageModel.HashId;
                    }

                    await _sliderImageCommandsRepository.Update(existingSliderImageModel);

                    return new Response()
                    {
                        Data = new { Id = sliderImageModel.HashId },
                        Message = "Slider Image Updated Successfully",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Message = "Error: No slider image exists!",
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

        public async Task<Response> UpdateSlider(SliderUpdatePayload newSliderRequest)
        {
            try
            {
                var existingSliderModel = await _sliderQueryRepository.GetSlider(newSliderRequest.Id);

                if (existingSliderModel != null)
                {
                    var sliderModel = _mapper.Map<Slider>(newSliderRequest.Slider);
                    existingSliderModel.Name = sliderModel.Name;
                    existingSliderModel.Type = sliderModel.Type;

                    await _sliderCommandsRepository.Update(existingSliderModel);

                    return new Response()
                    {
                        Data = new { Id = sliderModel.HashId },
                        Message = "Slider Image Updated Successfully",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Message = "Error: No slider image exists!",
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

        public async Task<Response> GetAllSliders(string shopId)
        {
            var sliderModels = await _sliderQueryRepository.GetSliders(shopId);
            var sliderResponses = new List<SliderResponse>();

            foreach(var sliderModel in sliderModels)
            {
                var sliderResponse = _mapper.Map<SliderResponse>(sliderModel);
                sliderResponses.Add(sliderResponse);
            }

            return new Response()
            {
                Data = sliderResponses,
                IsSuccess = true
            };
        }

        public async Task<Response> GetSlider(string sliderId)
        {
            var sliderModel = await _sliderQueryRepository.GetSlider(sliderId);

            if (sliderModel != null)
            {
                var sliderResponse = _mapper.Map<SliderResponse>(sliderModel);

                return new Response()
                {
                    Data = sliderResponse,
                    IsSuccess = true
                };
            }
            else
            {
                return new Response()
                {
                    Message = "Slider doesn't exist",
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> GetSlides(string sliderId)
        {
            var sliderImageModels = await _sliderQueryRepository.GetSliderImages(sliderId);
            var sliderImageResponses = new List<SliderImageResponse>();

            foreach (var sliderModel in sliderImageModels)
            {
                var sliderImageResponse = _mapper.Map<SliderImageResponse>(sliderModel);
                sliderImageResponses.Add(sliderImageResponse);
            }

            return new Response()
            {
                Data = sliderImageResponses,
                IsSuccess = true
            };
        }

        public async Task<Response> GetSlide(string slideId)
        {
            var sliderImageModel = await _sliderQueryRepository.GetSliderImage(slideId);
            var sliderImageResponse = _mapper.Map<SliderImageResponse>(sliderImageModel);

            if (sliderImageModel != null)
            {
                return new Response()
                {
                    Data = sliderImageResponse,
                    IsSuccess = true
                };
            }
            else
            {
                return new Response()
                {
                    Message = "Slide doesn't exist",
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> DeleteSlide(string slideId)
        {
            var slideModel = await _sliderQueryRepository.GetSliderImage(slideId);

            try
            {
                if (slideModel != null)
                {
                    await _sliderQueryRepository.DeleteSliderImage(slideId);

                    return new Response()
                    {
                        IsSuccess = true,
                        Message = "Slide deleted"
                    };
                }
                else
                {
                    return new Response()
                    {
                        IsSuccess = false,
                        Message = "Slide doesn't exist"
                    };
                }
            }
            catch(Exception ex)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Error: " + ex.Message
                };
            }
        }
    }
}
