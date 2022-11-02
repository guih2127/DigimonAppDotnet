using AutoMapper;
using DigimonApp.Domain.Models;
using DigimonApp.Domain.Services;
using DigimonApp.Extensions;
using DigimonApp.Resources;
using Microsoft.AspNetCore.Mvc;

namespace DigimonApp.Controllers
{
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper, IUsersService usersService)
        {
            _mapper = mapper;
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _usersService.SaveAsync(user, resource.Password);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        [HttpPost("/api/[controller]/login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _usersService.LoginAsync(resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Token);
        }
    }
}
