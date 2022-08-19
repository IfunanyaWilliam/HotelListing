using HotelListing.DTOs;
using System.Threading.Tasks;

namespace HotelListing.Contracts
{
    public interface IAuthManager
    {
        Task<bool> ValidateUserAsync(LoginDto userDto);
        Task<string> CreateTokenAsync();
    }
}
