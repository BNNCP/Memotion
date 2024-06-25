using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Dtos.Account;
using memotion_core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace memotion_core.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        public AccountController(UserManager<AppUser> _userManager)
        {
            userManager = _userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] RegisterDto registerDto){
            try{
                if(!ModelState.IsValid) return BadRequest(ModelState);

                AppUser appUser = new AppUser{
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };

                var createdUser = await userManager.CreateAsync(appUser, registerDto.Password);

                if(createdUser.Succeeded){
                    var roleResult = await userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded) return Ok("User Created");
                    else{
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else{
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch(Exception e){
                return StatusCode(500, e);
            }
        }
    }
}