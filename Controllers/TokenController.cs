using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using vega.Models;
using vega.Services;
using vega.Dtos;
using vega.Persistence;
using System.Linq;

namespace vega.Controllers
{
  [Route("api/[controller]")]
  public class TokenController : Controller
  {
    private IConfiguration _config;
    private IUserService _userService;

    public TokenController(IUserService userService, IConfiguration config)
    {
      _config = config;
      _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult CreateToken([FromBody]UserDto userDto)
    {
      IActionResult response = Unauthorized();
      var user = _userService.Authenticate(userDto.Username, userDto.Password);

      if (user != null)
      {
        var tokenString = BuildToken(user);

        response = Ok(new {
          Id = user.Id, 
          FirstName = user.FirstName,
          LastName = user.LastName,
          UserName = user.Username,
          token = tokenString });
      }

      return response;
    }
    private string BuildToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        _config["Jwt:Issuer"],
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}