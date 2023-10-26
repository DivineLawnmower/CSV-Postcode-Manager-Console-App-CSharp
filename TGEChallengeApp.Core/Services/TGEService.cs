using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TGEChallengeApp.Core.Interfaces;
using TGEChallengeApp.Core.Models.API;

namespace TGEChallengeApp.Core.Services
{
    public class TGEService : ITGEService
    {
        private string _postcodeValidationRegex;
        private string _postcodeFilePathCSV;
        private string _districtFilePathCSV;
        private string _postcodeFileHeaders;
        private IDataAccessService _dataAccessService;
        public TGEService(IDataAccessService dataAccessService)
        {

            _dataAccessService = dataAccessService;
            _postcodeFilePathCSV = @"../../../../DataSource/postcode_data.csv";
            _districtFilePathCSV = @"../../../../DataSource/districtCount.csv";
            _postcodeValidationRegex = "^[a-z]{1,2}\\d[a-z\\d]?\\s\\d[a-z]{2}$";
            _postcodeFileHeaders = "Postcode District, Count";
        }

        public async Task<IEnumerable<string>> GetPostcodes()
        {
            IEnumerable<string> fileData = new List<string>();
            try
            {
                fileData = await _dataAccessService.Read(_postcodeFilePathCSV);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            var postcodeData = fileData.Select(line => line.Split(',')[0]);

            return postcodeData;
        }

        public async Task<APIResponse> GetDistrictCount()
        {
            var postcodes = await GetPostcodes();
            var districts = await CalculateDistrictCount(postcodes);

            await WriteDistrictsCSV(_districtFilePathCSV, districts);

            return new APIResponse()
            {
                IsSuccess = true
            };
        }

        public async Task<Dictionary<string, int>> CalculateDistrictCount(IEnumerable<string> postcodes)
        {

            Dictionary<string, int> districts = new Dictionary<string, int>();
            var grouped = postcodes.GroupBy(x => x.Split(" ")[0]);

            for (var i = 0; i < grouped.Count(); i++)
            {
                var district = grouped.ElementAt(i);
                districts.Add(district.Key, district.Count());
            }
            return districts;
        }

        private async Task WriteDistrictsCSV(string path, Dictionary<string, int> districts)
        {
            var stringsToWrite = new List<string>();
            stringsToWrite.Add(_postcodeFileHeaders);
            for (var i = 0; i < districts.Count(); i++)
            {
                var district = districts.ElementAt(i);
                stringsToWrite.Add($"{district.Key},{district.Value}");
            }

            await _dataAccessService.Write(path, stringsToWrite);
        }

        public async Task<APIResponse> Delete(string postcodeToDelete)
        {
            var postcodes = await GetPostcodes();
            var toKeep = new List<string>();
            for (var i = 0; i < postcodes.Count(); i++)
            {
                var line = postcodes.ElementAt(i);
                if(line != postcodeToDelete)
                {
                    toKeep.Add(line);
                }
            }

            await _dataAccessService.Write(_postcodeFilePathCSV, toKeep);

            return new APIResponse
            {
                IsSuccess = true,

            };
        }

        public async Task<APIResponse> AddPostcodes(IEnumerable<string> postcodes)
        {

            postcodes = await GetValidPostcodes(postcodes);
            if (!postcodes.Any())
            {
                return new APIResponse { IsSuccess = false };
            }
            await _dataAccessService.Append(_postcodeFilePathCSV, postcodes);

            return new APIResponse
            {
                IsSuccess = true
            };
        }
        public bool IsValidPostcode(string postcode)
        {
            RegexOptions options = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            var match = Regex.Match(postcode, _postcodeValidationRegex, options);
            return match.Success;
        }
        public async Task<IEnumerable<string>> GetValidPostcodes(IEnumerable<string> postcodes)
        {
            IList<string> valid = new List<string>();

            for (var i = 0; i < postcodes.Count(); i++)
            {
                
                string input = postcodes.ElementAt(i);
                if (IsValidPostcode(input))
                {
                    valid.Add(input);
                } else
                {
                    Debug.WriteLine($"INVALID {input}");
                }
            }

            return valid;
        }
        public async Task<APIResponse> ValidatePostcodes()
        {
            IEnumerable<string> postcodes = await GetPostcodes();

            var valid = await GetValidPostcodes(postcodes);

            await _dataAccessService.Write(_postcodeFilePathCSV, valid);

            return new APIResponse()
            {
                IsSuccess = true
            };

        }
    }
}
