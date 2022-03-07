using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVirtualWallet.Data;
using MyVirtualWallet.Models;
using MyVirtualWallet.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVirtualWallet.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("UsersWithAccount")]
        public async Task<IActionResult> GetUsersWithAccountMinified()
        {
            var mappedList = _mapper.Map<List<ApplicationUser>, List<ApplicationUserDTO>>(_context.Users.Include(o => o.AccountDetails).ToList());

            return Ok(mappedList.Select(o => new { userName = o.UserName, accountDetailsID = o.AccountDetails.ObjectID}));
        }

        [HttpGet]
        [Route("UserDetails")]
        public async Task<IActionResult> GetUserAccountDetails(Guid userID)
        {
            var result = await _context.AccountDetails.FirstOrDefaultAsync(i => i.UserID == userID);

            if (result != null)
            {
                var mappedObj = _mapper.Map<AccountDetails, AccountDetailsDTO>(result);
                return Ok(mappedObj);
            }

            return NotFound("User account details not found.");
        }
    }
}
