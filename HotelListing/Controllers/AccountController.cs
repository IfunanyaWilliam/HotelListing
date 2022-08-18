using AutoMapper;
using HotelListing.DTOs;
using HotelListing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        //private readonly SignInManager<ApiUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApiUser> userManager,
                                 //SignInManager<ApiUser> signInManager,
                                 ILogger<AccountController> logger,
                                 IMapper mapper)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"Registration Attempt for {userDto.Email}");

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                await _userManager.AddToRolesAsync(user, userDto.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        //{
        //    _logger.LogInformation($"Login Attempt for {loginDto.Email}");

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

        //        if (!result.Succeeded)
        //            return Unauthorized(loginDto);

        //        return Accepted();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
        //        return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
        //    }
        //}
    }
}
