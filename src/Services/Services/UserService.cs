
using AutoMapper;
using Core.Exceptions;
using Domain.Entities;
using EscNet.Cryptography.Interfaces;
using Infra.Repositories.Interfaces;
using Services.DTO;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRijndaelCryptography _rijndaelCryptography;

        public UserService(IMapper mapper, IUserRepository userRepository, IRijndaelCryptography rijndaelCryptography)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _rijndaelCryptography = rijndaelCryptography;
        }

        public async Task<UserDTO> CreateAsync(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByEmail(userDTO.Email);

            if(userExists != null)
                throw new DomainException("Já existe esse usuário cadastrado com email informado");

            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(_rijndaelCryptography.Encrypt(user.Password));


            var userCreated = await _userRepository.Create(user);

            return _mapper.Map<UserDTO>(userCreated);

        }
        public async Task<UserDTO> UpdateAsync(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetById(userDTO.Id);

            if(userExists == null)
                throw new DomainException("Não existe usuario com id informado");

            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(_rijndaelCryptography.Encrypt(user.Password));

            var userUpdated = await _userRepository.Updade(user);

            return _mapper.Map<UserDTO>(userUpdated);
           
        }
        public async Task RemoveAsync(long id)
        {
            await _userRepository.Remove(id);
        }

        public async Task<UserDTO> GetByIdAsync(long id)
        {
            var user = await _userRepository.GetById(id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var allUsers = await _userRepository.GetAll();

            return _mapper.Map<List<UserDTO>>(allUsers);
        }
        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmail(email);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> SearchByEmailAsync(string email)
        {
            var allUsers = await _userRepository.SearchByEmail(email);

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByNameAsync(string name)
        {
            var allUsers = await _userRepository.SearchByEmail(name);

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

       
    }
}