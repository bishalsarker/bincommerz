using BComm.PM.Dto.Common;
using BComm.PM.Dto.Widgets;
using System.Threading.Tasks;

namespace BComm.PM.Services.Widgets
{
    public interface ISliderService
    {
        Task<Response> AddSlider(SliderPayload newSliderRequest, string shopId);
        Task<Response> AddSliderImage(SliderImagePayload newSliderImageRequest);
        Task<Response> GetAllSliders(string shopId);
        Task<Response> GetSlides(string slideId);
        Task<Response> UpdateSliderImage(SliderImageUpdatePayload newSliderImageRequest);
    }
}