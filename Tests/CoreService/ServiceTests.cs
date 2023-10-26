using Microsoft.Extensions.DependencyInjection;
using TGEChallengeApp.Core.Services;
using TGEChallengeApp.Core.Services.DataAccess;
using Xunit;

namespace Tests.CoreService
{
    public class ServiceTests : IClassFixture<TestSetup>
    {
        private ServiceProvider _serviceProvider;
        private TGEService _tGEService;
        public ServiceTests(TestSetup testSetup)
        {

            _serviceProvider = testSetup.ServiceProvider;
            _tGEService = _serviceProvider.GetService<TGEService>();
        }

        public static IEnumerable<object[]> PostcodeTestData => new List<object[]>()
        {
            new object[] { new List<string>()
            {
                "CH62 1HF","CH62 1HF","CH62 1HF", "CH42 1HE","CH42 1HE", "CH32 1HD"
            }, new Dictionary<string,int>() { {"CH62", 3 }, { "CH42", 2 }, {"CH32", 1 }
            } }
        };

        public static IEnumerable<object[]> PostcodeTestDataAdd => new List<object[]>()
        {
            new object[] { new List<string>()
            {
                "CH62 1HF", "CH42 1HE","CH32 1HD"
            }
        } 
        };

        [Fact]
        public async void GetPostcodes_GetsPostcodes()
        {
            var tgeService = new TGEService(new DataAccessService());
            var result = await tgeService.GetPostcodes();

            Assert.NotEmpty(result);
        }


        [Theory]
        [MemberData(nameof(PostcodeTestData))]
        public async void CalculateDistricts_CalculatesDistricts(IEnumerable<string> postcodes, Dictionary<string, int> districtCounts)
        {
            var tgeService = new TGEService(new DataAccessService());
            var result = await tgeService.CalculateDistrictCount(postcodes);

            Assert.Equal(districtCounts, result);
        }

        [Theory]
        [InlineData("CH62 1HF")]
        [InlineData("CH42 1HE")]
        [InlineData("CH32 1HD")]
        public void ValidatesPostcodes_Correctly(string postcode)
        {
            var tgeService = new TGEService(new DataAccessService());
            var result = tgeService.IsValidPostcode(postcode);

            Assert.True(result);
        }

        [Theory]
        [InlineData("C222 1HF")]
        [InlineData("CH621HF")]
        [InlineData("")]
        [InlineData(" ")]
        public async void ValidatesInvalidPostcodes_Correctly(string postcode)
        {
            var tgeService = new TGEService(new DataAccessService());
            var result = tgeService.IsValidPostcode(postcode);

            Assert.False(result);
        }

        [Fact]
        public async void ValidatePostcodes_RemovesInvalidPostcodes()
        {

            var tgeService = new TGEService(new DataAccessService());
            var result = await tgeService.ValidatePostcodes();

            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData("CH62 1HF")]
        public async void AddPostcode_AddsPostcodeSingle(string postcode)
        {
            var postcodes = new List<string>() { postcode };
            var tgeService = new TGEService(new DataAccessService());
            var added = await tgeService.AddPostcodes(postcodes);

            var getPostcodes = await tgeService.GetPostcodes();
            Assert.Contains(postcode, getPostcodes);
        }

        [Theory]
        [InlineData("CHZZZZHF")]
        public async void AddPostcode_DoesNotAddInvalid(string postcode)
        {
            var postcodes = new List<string>() { postcode };
            var tgeService = new TGEService(new DataAccessService());
            var added = await tgeService.AddPostcodes(postcodes);

            var getPostcodes = await tgeService.GetPostcodes();
            Assert.DoesNotContain(postcode, getPostcodes);
        }

        [Theory]
        [MemberData(nameof(PostcodeTestDataAdd))]
        public async void AddPostcode_AddsPostcodeMultiple(IEnumerable<string> postcodes)
        {
            
            var tgeService = new TGEService(new DataAccessService());
            var added = await tgeService.AddPostcodes(postcodes);

            var getPostcodes = await tgeService.GetPostcodes();
            for(var i = 0; i < postcodes.Count(); i++)
            {
                Assert.Contains(postcodes.ElementAt(i), getPostcodes);
            }
        }

        [Theory]
        [InlineData("CH62 1HF")]
        [InlineData("CH42 1HE")]
        [InlineData("CH32 1HD")]
        public async void DeletePostcode_RemovesPostcodeSingle(string postcode)
        {
            var tgeService = new TGEService(new DataAccessService());
            var result = await tgeService.Delete(postcode);
            var getPostcodes = await tgeService.GetPostcodes();

            Assert.DoesNotContain(postcode, getPostcodes);
        }
        
    }
}
