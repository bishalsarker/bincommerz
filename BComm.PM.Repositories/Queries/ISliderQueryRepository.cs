using BComm.PM.Models.Widgets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface ISliderQueryRepository
    {
        Task DeleteSliderImage(string sliderImageId);
        Task<Slider> GetSlider(string sliderId);
        Task<SliderImage> GetSliderImage(string sliderImageId);
        Task<IEnumerable<SliderImage>> GetSliderImages(string sliderId);
        Task<IEnumerable<Slider>> GetSliders(string shopId);
    }
}