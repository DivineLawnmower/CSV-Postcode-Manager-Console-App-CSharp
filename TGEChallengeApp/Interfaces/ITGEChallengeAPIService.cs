using TGEChallengeApp.Core.Models.API;

namespace TGEChallengeApp.Interfaces
{
    public interface ITGEChallengeAPIService
    {
        Task<APIResponse> Delete(string postcodeToDelete);
        Task<APIResponse> Get();
        Task<APIResponse> GetDistricts();
        Task<APIResponse> PostAsync(IEnumerable<string> data);
        Task<APIResponse> ValidatePostcodes();
    }
}