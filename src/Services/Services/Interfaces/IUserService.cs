using Services.DTO;

namespace Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetById(long id);
        Task<List<UserDTO>> GetAll();
        Task<UserDTO> Create(UserDTO userDTO);
        Task<UserDTO> Updade(UserDTO userDTO);
        Task Remove(long id);
        Task<UserDTO> GetByEmail(string email); 
        Task<List<UserDTO>> SearchByEmail(string email);
        Task<List<UserDTO>> SearchByName(string name);
    }
}