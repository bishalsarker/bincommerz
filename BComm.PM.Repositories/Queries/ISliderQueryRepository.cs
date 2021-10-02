using BComm.PM.Models.Widgets;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface ISliderQueryRepository
    {
        Task<SliderImage> GetSliderImage(string sliderImageId);
    }
}