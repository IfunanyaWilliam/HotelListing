using HotelListing.DTOs;
using System.Threading.Tasks;

namespace HotelListing.Contracts
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDto userDto);
        Task<string> CreateToken();
    }
}
