using EcommerceAPI.DTO;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AdminController(UserManager<ApplicationUser> userManager, IConfiguration Configuration)
        {
            this.userManager = userManager;
            configuration = Configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //save data base
            ApplicationUser userModel = new ApplicationUser();
            userModel.UserName = registerDto.fullName;
            userModel.Email = registerDto.email;
            //userModel.address.city = registerDto.address.city;
            //userModel.address.street = registerDto.address.street;
            //userModel.address.postalCode = registerDto.address.postalCode;
            //foreach (var mob in registerDto.mobileNum)
            //{
            //    userModel.mobileNum.Add(new Mobiles { Mobile = mob });
            //}
            //userModel.deliveryOptions = registerDto.deliveryOptions;
            //userModel.specificDays = registerDto.specificDays;

            IdentityResult result = await userManager.CreateAsync(userModel, registerDto.password);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //check identityt  "Create Token" ==Cookie
            ApplicationUser userModel = await userManager.FindByNameAsync(loginDto.UserName);
            var roles = await userManager.GetRolesAsync(userModel);
            if (userModel != null)
            {
                if (await userManager.CheckPasswordAsync(userModel, loginDto.Password) == true)
                {
                    //toke base on Claims "Name &Roles &id " +Jti ==>unique Key Token "String"
                    var mytoken = await GenerateToke(userModel);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo
                    });
                }
                else
                {
                    //return BadRequest("User NAme and PAssword Not Valid");
                    return Unauthorized();//401
                }
            }
            return Unauthorized();
        }
        [NonAction]
        public async Task<JwtSecurityToken> GenerateToke(ApplicationUser userModel)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("intakeNo", "42"));//Custom
            claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id));
            claims.Add(new Claim(ClaimTypes.Role, Roles.Admin)); // Add Admin Role
            var roles = await userManager.GetRolesAsync(userModel);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //Jti "Identifier Token
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //---------------------------------(: Token :)--------------------------------------
            var key =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecrtKey"]));
            var mytoken = new JwtSecurityToken(
                audience: configuration["JWT:ValidAudience"],
                issuer: configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials:
                       new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return mytoken;
        }


    }
}
