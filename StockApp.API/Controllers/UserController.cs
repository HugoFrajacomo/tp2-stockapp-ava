using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            // Validações básicas (podem ser expandidas conforme necessário)
            if (userRegisterDTO == null || string.IsNullOrEmpty(userRegisterDTO.UserName) || string.IsNullOrEmpty(userRegisterDTO.Password))
            {
                return BadRequest("Invalid user data");
            }

            // Criação da entidade User
            var user = new User
            {
                Username = userRegisterDTO.UserName,
                PasswordHash = System.Text.Encoding.UTF8.GetBytes(userRegisterDTO.Password),
                Role = userRegisterDTO.Role
            };

            // Adiciona o usuário ao repositório
            await _userRepository.AddAsync(user);

            return Ok("User registered successfully");
        }

        [Authorize(Roles = "AdminRole")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (userRegisterDTO == null || string.IsNullOrEmpty(userRegisterDTO.UserName) || string.IsNullOrEmpty(userRegisterDTO.Password))
            {
                _logger.LogError("Invalid user data recived");
                return BadRequest("Invalid user data");
            }

            var user = new User
            {
                Username = userRegisterDTO.UserName,
                PasswordHash = System.Text.Encoding.UTF8.GetBytes(userRegisterDTO.Password),
                Role = userRegisterDTO.Role
            };

            await _userRepository.AddAsync(user);

            _logger.LogInformation("Admin user registered successfully");

            return Ok();
        }
    }
}



