using Lab_1.Models;
using Lab_1.Models.DTO;
using Lab_1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Lab_1.Controllers
{
    [Route("api/InvestmentBag")]
    [ApiController]
    public class InvestmentContainerController : ControllerBase
    {
        private readonly Context _context;
        private IInvestmentContainerService _investmentContainerService;

        public InvestmentContainerController(Context context, IInvestmentContainerService investmentBagService)
        {
            _context = context;
            _investmentContainerService = investmentBagService;
        }

        [HttpGet("getcontainerslist"), ActionName("Get lost of all investment containers")]
        [Authorize]
        public async Task<IActionResult> GetContainersList()
        {
            try
            {
                var list = await _investmentContainerService.GetContainersList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Something went wrong in method {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name}. {ex}");
            }
        }

        [HttpPost("Create"), ActionName("Create an investment container")]
        [Authorize]
        public async Task<IActionResult> CreateInverstmentContainer(CreateInvestmentContainerDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var currentIdentity = User.Identity as ClaimsIdentity;
            var currentUserId = currentIdentity.FindFirst("id").Value;
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == currentUserId);

            var isPremium = currentIdentity.FindFirst("isPremium").Value;

            if (isPremium == "False")
            {
                var containers = _context.InvestmentContainers.Where(x => x.Owner == currentUser).ToList().Count;
                if (containers >= 5)
                    return BadRequest("Containers limit exceeded. Upgrade to pro to have more containers!");
            }
            try
            {
                await _investmentContainerService.Create(DTO, currentUser);
                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Something went wrong in method {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name}. {ex}");
            }                        
        }

        [HttpDelete("Delete"), ActionName("Delete Investment Bag")]
        [Authorize]
        public async Task<IActionResult> DeleteInverstmentContainer(DeleteInvestmentContainerDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _investmentContainerService.Delete(model);
                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Something went wrong in method {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name}. {ex}");
            }                        
        }
    }
}
