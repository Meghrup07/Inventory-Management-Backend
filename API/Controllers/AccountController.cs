using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Users;
using API.Interface;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(DataContext context, IMapper mapper, ITokenService tokenService) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserNameExists(registerDTO.UserName)) return BadRequest("UserName is taken");

            using var hmac = new HMACSHA512();

            var user = mapper.Map<AppUser>(registerDTO);

            user.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
            user.passwordSalt = hmac.Key;

            await context.AddAsync(user);
            await context.SaveChangesAsync();

            return new UserDTO
            {
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            };

        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await context.Users.Where(u => u.UserName == loginDTO.UserName).FirstOrDefaultAsync();

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.passwordSalt);

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.passwordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDTO
            {
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            };

        }

        private async Task<bool> UserNameExists(string username)
        {
            return await context.Users.AnyAsync(u => u.UserName == username);
        }

    }
}