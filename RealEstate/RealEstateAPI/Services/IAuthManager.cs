using RealEstateAPI.ModelsDTO;
using System.Threading.Tasks;

namespace RealEstateAPI.Services
{
    /// <summary>
    /// IAuthManager will define all methods we need for authentication
    /// </summary>
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}
