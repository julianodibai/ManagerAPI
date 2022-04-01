
using AutoMapper;
using Core.Exceptions;
using Domain.Entities;
using Infra.Repositories.Interfaces;
using Services.DTO;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetById(long id)
        {
            var user = await _userRepository.GetById(id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var allUsers = await _userRepository.GetAll();

            return _mapper.Map<List<UserDTO>>(allUsers);
        }
        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByEmail(userDTO.Email);

            if(userExists != null)
                throw new DomainException("Já existe esse usuário cadastrado com email informado");

            var user = _mapper.Map<User>(userDTO);
            user.Validate(); 
            var userCreated = await _userRepository.Create(user);

            return _mapper.Map<UserDTO>(userCreated);

        }
        public async Task<UserDTO> Updade(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByEmail(userDTO.Email);

            if(userExists == null)
                throw new DomainException("Não existe usuario com email informado");

            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            var userUpdated = await _userRepository.Create(user);

            return _mapper.Map<UserDTO>(userUpdated);
           
        }
        public async Task Remove(long id)
        {
            await _userRepository.Remove(id);
        }


        public async Task<UserDTO> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> SearchByEmail(string email)
        {
            var allUsers = await _userRepository.SearchByEmail(email);

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByName(string name)
        {
            var allUsers = await _userRepository.SearchByEmail(name);

            return _mapper.Map<List<UserDTO>>(allUsers);
        }
    }
}