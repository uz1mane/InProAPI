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
    [Route("api/Skins")]
    [ApiController]
    public class SkinsController : ControllerBase
    {
        private readonly Context _context;
        private IInvestmentContainerService _investmentContainerService;
        private ISkinsService _skinsService;

        public SkinsController(Context context, IInvestmentContainerService investmentBagService, ISkinsService skinsService)
        {
            _context = context;
            _investmentContainerService = investmentBagService;
            _skinsService = skinsService;
        }

        [HttpPost("AddSkin/{containerId}"), ActionName("Add a skin to container")]
        [Authorize]
        public async Task<IActionResult> AddSkin(int containerId, AddSkinDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentIdentity = User.Identity as ClaimsIdentity;
            var currentUserId = currentIdentity.FindFirst("id").Value;
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == currentUserId);

            var currentContainer = await _context.InvestmentContainers.FirstOrDefaultAsync(x => x.Id == containerId);

            var isPremium = currentIdentity.FindFirst("isPremium").Value;

            if (isPremium == "False")
            {
                var skins = _context.Skins.Where(x => x.container == currentContainer).ToList().Count;
                if (skins >= 10)
                    return BadRequest("Containers limit exceeded. Upgrade to pro to have more containers!");
            }
            try
            {
                _skinsService.AddSkin(DTO, currentContainer);
                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Something went wrong in method {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name}. {ex}");
            }
        }

        [HttpDelete("DeleteSkin/{skinId}"), ActionName("Delete skin")]
        [Authorize]
        public async Task<IActionResult> DeleteSkin(int skinId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _skinsService.Delete(skinId);
                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Something went wrong in method {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name}. {ex}");
            }
        }

        [HttpGet("GetSkinsForSomeContainer/{containerId}"), ActionName("Get all skins in a certain containers")]
        [Authorize]
        public async Task<IActionResult> GetSkinsForSomeContainer(int containerId)
        {
            try
            {
                var list = await _skinsService.GetContainerContent(containerId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Something went wrong in method {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name}. {ex}");
            }
        }

       /* [HttpDelete("Delete"), ActionName("Delete Investment Bag")]
        [Authorize]
        public async Task<IActionResult> CheckPrice(DeleteInvestmentContainerDTO model)
        {
            
        }*/
    }
}
