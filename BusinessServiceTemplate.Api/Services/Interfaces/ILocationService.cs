namespace BusinessServiceTemplate.Api.Services.Interfaces
{
    public interface ILocationService
    {
        Task<List<string>> GetAllLocations();
        Task<List<string>> SearchLocations(string postcode);
    }
}
