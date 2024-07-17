
using Data.Models.CoverageModels;

namespace Core.CoverageServices
{
    public interface ICoverageService
    {
        Task<IEnumerable<CoverageLocation>> GetAllCoverageLocationsAsync();
        Task<bool> IsInCoverageAreaAsync(double latitude, double longitude);

    }
}
