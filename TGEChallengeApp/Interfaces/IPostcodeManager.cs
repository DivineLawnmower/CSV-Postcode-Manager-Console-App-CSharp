namespace TGEChallengeApp.Interfaces
{
    public interface IPostcodeManager
    {
        Task<IEnumerable<string>> GetAllPostcodesAsync();
        Task AddNewPostcodes(IEnumerable<string> postcodes);
        void DeletePostcode(string postcodeToDelete);
        Task<string> GetPostcodeDistrictsAsync();
        Task ValidatePostcodes();

    }
}
