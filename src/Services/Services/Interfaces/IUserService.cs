using Services.DTO;

namespace Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateAsync(UserDTO userDTO);
        Task<UserDTO> UpdateAsync(UserDTO userDTO);
        Task RemoveAsync(long id);
        Task<UserDTO> GetByIdAsync(long id);
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByEmailAsync(string email); 
        Task<List<UserDTO>> SearchByEmailAsync(string email);
        Task<List<UserDTO>> SearchByNameAsync(string name);
    }
}