#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Lab_1.Models;
using Lab_1.Services;
using Microsoft.AspNetCore.Authorization;
using Lab_1.Models.DTO;

namespace Lab_1.Controllers
{   
    [ApiController]
    [Route("users")]

    public class UsersController : Controller
    {        
        private readonly Context _context;
        private IUserService _userService;        

        public UsersController(Context context, IUserService userService)
        {
            _context = context;
            _userService = userService;            
        }

        // PATCH

        [HttpPatch("{userId}"), ActionName("Edit data for concrete user")]
        [SwaggerOperation(
            Tags = new[] { "User Data" },
            Summary = "/api/users/{userId}",
            Description = "Edit data for concrete user (except edit role)"
        )]
        [Authorize]
        public async Task<IActionResult> ChangePassword(EditUserDataDTO editData ,int userId)
        {
            try
            {
                await _userService.ChangePassword(editData, userId);
                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Something went wrong in method {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name}. {ex}");
            }            
        }

        // DELETE

        [HttpDelete("{userId}"), ActionName("Delete concrete user")]
        [SwaggerOperation(
            Tags = new[] { "User Data" },
            Summary = "/api/users/{userId}",
            Description = "Delete concrete user (except edit role). Available only for admin"
        )]
        [Authorize] 
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (_userService.CheckIfUserIsAdmin(HttpContext))
            {
                try
                {
                    await _userService.DeleteUser(userId);
                    return Ok($"User with id {userId} was successfully deleted.");
                }
                catch (Exception ex)
                {
                    return StatusCode(404, $"Something went wrong in method {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name}. {ex}");
                }
            }
            else return StatusCode(403, "This endpoint is avaliable for admin only.");

        }
       
    }
}
