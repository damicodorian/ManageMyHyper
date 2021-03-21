using AutoMapper;
using ManageMyHyper.Server.Data;
using ManageMyHyper.Server.Services;
using ManageMyHyper.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManageMyHyper.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;
        private readonly IMapper _mapper;

        public UserController(DataContext context, IUtilityService utilityService, IMapper mapper)
        {
            _context = context;
            _utilityService = utilityService;
            _mapper = mapper;
        }

        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            var user = await _utilityService.GetUser();

            var dbUsers = await _context.Users.Include(u => u.UserRole).ToListAsync();
            var users = new List<UserDTO>();

            foreach(var dbUser in dbUsers)
            {
                UserDTO userDto = new UserDTO();
                _mapper.Map(dbUser, userDto);
                users.Add(userDto);
            }
            return Ok(users);
        }

        [HttpGet("GetWorkTasks")]
        public async Task<IActionResult> GetWorkTasks()
        {
            var user = await _utilityService.GetUser();
            //TODO
            return Ok("TODO");
        }
    }
}
