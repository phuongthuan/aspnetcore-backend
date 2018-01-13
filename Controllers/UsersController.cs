using System;
using AutoMapper;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

using vega.Helpers;
using vega.Dtos;
using vega.Models;
using vega.Services;

namespace vega.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
 
        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
 
        // [AllowAnonymous]
        // [HttpPost("authenticate")]
        // public IActionResult Authenticate([FromBody]UserDto userDto)
        // {
        //     var user = _userService.Authenticate(userDto.Username, userDto.Password);
 
        //     if (user == null)
        //         return Unauthorized();
 
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(new Claim[] 
        //         {
        //             new Claim(ClaimTypes.Name, user.Id.ToString())
        //         }),
        //         Expires = DateTime.UtcNow.AddDays(7),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //     };
        //     var token = tokenHandler.CreateToken(tokenDescriptor);
        //     var tokenString = tokenHandler.WriteToken(token);
 
        //     return Ok(new {
        //         Id = user.Id,
        //         Username = user.Username,
        //         FirstName = user.FirstName,
        //         LastName = user.LastName,
        //         Token = tokenString
        //     });

        // }
 
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            try
            {
                _userService.Create(user, userDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }
 
        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }
 
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user =  _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
 
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;
 
            try
            {
                _userService.Update(user, userDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }
 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}