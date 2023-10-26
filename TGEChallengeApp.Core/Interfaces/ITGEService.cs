using TGEChallengeApp.Core.Models.API;

namespace TGEChallengeApp.Core.Interfaces
{
    public interface ITGEService
    {
        Task<APIResponse> AddPostcodes(IEnumerable<string> postcodes);
        Task<Dictionary<string, int>> CalculateDistrictCount(IEnumerable<string> postcodes);
        Task<APIResponse> Delete(string postcodeToDelete);
        Task<APIResponse> GetDistrictCount();
        Task<IEnumerable<string>> GetPostcodes();
        Task<IEnumerable<string>> GetValidPostcodes(IEnumerable<string> postcodes);
        bool IsValidPostcode(string postcode);
        Task<APIResponse> ValidatePostcodes();
    }
}