using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AccountController : BaseApiController
  {
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    public AccountController(DataContext context, ITokenService tokenService)
    {
      _tokenService = tokenService;
      _context = context;

    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(RegisterModel register)
    {
      if (await UserExists(register.Username)) return BadRequest("Username is already taken");

      using var hmac = new HMACSHA512();

      var user = new AppUser
      {
        UserName = register.Username.ToLower(),
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
        PasswordSalt = hmac.Key
      };

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return new User
      {
        UserName = user.UserName,
        Token = _tokenService.CreateToken(user)
      };
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(LoginModel login)
    {
      var user = await _context.Users.SingleOrDefaultAsync(appuser => appuser.UserName == login.UserName);

      if (user == null) return Unauthorized();

      using var hmac = new HMACSHA512(user.PasswordSalt);

      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

      for (int i = 0; i < computedHash.Length; i++)
      {
        if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
      }

      return new User
      {
        UserName = user.UserName,
        Token = _tokenService.CreateToken(user)
      };
    }

    private async Task<bool> UserExists(string username)
    {
      return await _context.Users.AnyAsync(appuser => appuser.UserName == username.ToLower());
    }
  }
}