using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using TGEChallengeApp.Core.Interfaces;
using TGEChallengeApp.Core.Models.API;

namespace TGEChallengeApp.DataAccess.API
{
    public class DummyTGEChallengeAPI : IDummyTGEChallengeAPI
    {
        private readonly string _postcodeFilePathCSV;
        private readonly ITGEService _tgeService;
        public DummyTGEChallengeAPI(ITGEService tGEService)
        {
            _postcodeFilePathCSV = @"../../../../DataSource/postcode_data.csv";
            _tgeService = tGEService;
        }

        public async Task<APIResponse> Get()
        {
            var postcodeData = await _tgeService.GetPostcodes();

            string jsonData = JsonSerializer.Serialize(postcodeData);

            return new APIResponse
            {
                JSONData = jsonData,
                IsSuccess = true,
            };
        }

        public async Task<APIResponse> ValidatePostcodes()
        {
            return await _tgeService.ValidatePostcodes();
        }

        public async Task<APIResponse> GetByDistrict()
        {
            return await _tgeService.GetDistrictCount();
        }

        public async Task<APIResponse> PostAsync(IEnumerable<string> postcodes)
        {
            return await _tgeService.AddPostcodes(postcodes);
        }

        public async Task<APIResponse> Delete(string postcodeToDelete)
        {
            return await _tgeService.Delete(postcodeToDelete);
        }
    }
}
