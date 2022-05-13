using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApi.ViewModels;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;

        public AuthController(IConfiguration _config)
        {
            config = _config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] LoginViewModel userForm)
        {
            if (ModelState.IsValid)
            {
                
                if (userForm.Username == "Abdullah")
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(config["Application:Secret"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Audience = "TodoApi",
                        Issuer = "burasını anlamaıdm",
                        Subject = new ClaimsIdentity(
                            new Claim[]{
                             new Claim(ClaimTypes.Name, userForm.Username)
                            }
                        ),
                        // TODO : burada kullanıcının tokenları alınıp varsa onlar basılmalı JWT içine
                        // token süresini belirledik
                        Expires = DateTime.UtcNow.AddDays(1),

                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(new { Token = tokenString, Test = "Test" });
                }
                else
                {
                    return Ok(new { Code = 404, Message = "Kullanıcı adı veya şifre yanlış." });
                }
            }
            else
            {
                return Ok(new { Code = 413, Message = "Form yanlış doldurulmuş." });
            }

        }

    }
}
