using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGEChallengeApp.DataAccess.API;
using TGEChallengeApp.Interfaces;

namespace TGEChallengeApp.DataAccess
{
    public class PostcodeManager : IPostcodeManager
    {
        private readonly ITGEChallengeAPIService _api;

        public PostcodeManager(ITGEChallengeAPIService api)
        {
            _api = api;
        }

        public async Task<IEnumerable<string>> GetAllPostcodesAsync()
        {
            List<string> allPostcodes = new List<string>();
            var response = await _api.Get();
            if(!response.IsSuccess)
            {

            }

            allPostcodes = JsonConvert.DeserializeObject<List<string>>(response.JSONData);

            return allPostcodes;
        }

        public async Task AddNewPostcodes(IEnumerable<string> postcodes)
        {
            var response = await _api.PostAsync(postcodes);
        }

        public void DeletePostcode(string postcodeToDelete)
        {
           _api.Delete(postcodeToDelete);
        }

        public async Task<string> GetPostcodeDistrictsAsync()
        {
            var response = await _api.GetDistricts();
            return response.JSONData;
        }


        public async Task ValidatePostcodes()
        {
            await _api.ValidatePostcodes();
        }
    }
}
