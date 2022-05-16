using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data;
using TodoApi.Entities;
using TodoApi.Helpers;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TodoContext _context;

        public UserController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            try
            {
                User addedUser = new User();
                addedUser = user;
                addedUser.PasswordSalt = SaltGenerator.GenerateSalt();
                addedUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password + addedUser.PasswordSalt);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string email)
        {
            try
            {
                var deletedUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                _context.Users.Remove(deletedUser);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
