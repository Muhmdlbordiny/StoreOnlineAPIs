using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreCore.G02.Dto.Autho;
using StoreCore.G02.Entites.Identity;
using StoreCore.G02.RepositriesContract;
using StoreOnline.G02.Error;
using StoreOnline.G02.Extension;
using System.Security.Claims;

namespace StoreOnline.G02.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<Appuser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(
            IUserService userService,
            UserManager<Appuser> userManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("login")] //baseurl /api/Account/login
        public async Task <ActionResult<UserDto>> Login(LoginDto loginDto)
        {
           var userdto =await _userService.LoginAsync(loginDto);
            if(userdto is null )
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(userdto);
        }
        [HttpPost("register")] //baseurl /api/Account/login
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userdto = await _userService.RegisterAsync(registerDto);
            if (userdto is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"Invaild Registeration!!"));
            return Ok(userdto);
        }
        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var useremail =  User.FindFirstValue(ClaimTypes.Email);
            if (useremail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var user = await _userManager.FindByEmailAsync(useremail);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(new UserDto
            {
                
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }
        [HttpGet("Address")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
        {
            //var useremail = User.FindFirstValue(ClaimTypes.Email);
            //if (useremail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<AddressDto>(user.Address));
            
        }
        [HttpPut("updateAddress")]
        [Authorize]
        public async Task< ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto UpdatedAddress)
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var address = _mapper.Map<AddressDto, Address>(UpdatedAddress);
            address.Id = user.Address.Id; // update same object 
            user.Address = address;
           var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(UpdatedAddress);

        }


    }
}
