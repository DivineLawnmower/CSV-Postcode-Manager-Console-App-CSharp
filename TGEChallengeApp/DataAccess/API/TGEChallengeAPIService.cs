using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using TGEChallengeApp.Core.Models.API;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using TGEChallengeApp.Interfaces;
using TGEChallengeApp.Core.Interfaces;

namespace TGEChallengeApp.DataAccess.API
{
    public class TGEChallengeAPIService : ITGEChallengeAPIService
    {
        private static IDummyTGEChallengeAPI _dummyAPI;
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
       public TGEChallengeAPIService(IDummyTGEChallengeAPI dummyApi)
        {
            _dummyAPI = dummyApi;
        }

        public async Task<APIResponse> Get()
        {
            var response = await _dummyAPI.Get();
            return response;
        }

        public async Task<APIResponse> PostAsync(IEnumerable<string> data)
        {
            var response = await _dummyAPI.PostAsync(data);
            return response;
        }

        public async Task<APIResponse> Delete(string postcodeToDelete)
        {
            var response = await _dummyAPI.Delete(postcodeToDelete);
            return response;
        }

        public async Task<APIResponse> GetDistricts()
        {
            var response = await _dummyAPI.GetByDistrict();
            return response;
        }

        public async Task<APIResponse> ValidatePostcodes()
        {
            var response = await _dummyAPI.ValidatePostcodes();
            return response;
        }
    }
}
