using Auth.Applicaton.Additionally;
using Auth.Applicaton.Exceptions;
using Auth.Applicaton.Interfaces;
using Auth.Applicaton.User.Repository;
using Auth.Contracts.DTO;
using Auth.Domain.Enums;
using Auth.Domain.Models;
using AutoMapper;

namespace AuthServiceApplicaton.Authentication.Services
{
    /// <inheritdoc cref="IAuthenticationService"/>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private IJwtProvider _jwtProvider;

        /// <summary>
        /// Конструктор класса <see cref="AuthenticationService"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="mapper">Объект для маппинга между моделями.</param>
        /// <param name="jwtProvider">Поставщик JWT токенов для генерации и валидации токенов.</param>
        public AuthenticationService(
            IUserRepository userRepository,
            IMapper mapper,
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
        }

        /// <inheritdoc/>
        public async Task<string> Login(LoginDto dto, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.FindWhere(u => u.Login == dto.Login, cancellationToken);

            if (existingUser is null) throw new InvalidLoginDataException();

            var encriptedPasword = EncriptPasswordUser.EncryptPassword(dto.Password);
            if (EncriptPasswordUser.Verify(dto.Password, encriptedPasword)) throw new InvalidLoginDataException();

            var token = _jwtProvider.GenerateToken(existingUser);

            return token;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
        }

        /// <inheritdoc/>
        public async Task<Guid> Register(RegisterDto dto, CancellationToken cancellationToken)
        {
            if (await _userRepository.FindWhere(x => x.Login == dto.Login, cancellationToken) is not null)
            {
                throw new InvalidLoginDataException("Пользователь с таким логином уже существует.");
            }

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = EncriptPasswordUser.EncryptPassword(dto.Password);
            user.Role = Role.User.ToString();
            var result = await _userRepository.Add(user, cancellationToken);
            return result.Id;
        }
    }
}
