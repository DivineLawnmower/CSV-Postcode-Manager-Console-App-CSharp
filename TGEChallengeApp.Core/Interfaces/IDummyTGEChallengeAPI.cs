using TGEChallengeApp.Core.Models.API;

namespace TGEChallengeApp.Core.Interfaces
{
    public interface IDummyTGEChallengeAPI
    {
        Task<APIResponse> Delete(string postcodeToDelete);
        Task<APIResponse> Get();
        Task<APIResponse> GetByDistrict();
        Task<APIResponse> PostAsync(IEnumerable<string> postcodes);
        Task<APIResponse> ValidatePostcodes();
    }
}