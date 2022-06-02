using Lab_1.Models;
using Lab_1.Models.DTO;
using Lab_1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab_1.Controllers
{
    [Route("api/InvestmentBag")]
    [ApiController]
    public class InvestmentBagController : ControllerBase
    {
        private readonly Context _context;
        private IInvestmentBagService _investmentBagService;

        public InvestmentBagController(Context context, IInvestmentBagService investmentBagService)
        {
            _context = context;
            _investmentBagService = investmentBagService;
        }


        [HttpPost("create"), ActionName("Create Investment Bag")]        
        [Authorize]
        public async Task<IActionResult> ChangePassword(CreateInvestmentBagDTO DTO, int userId)
        {
            try
            {
                await _investmentBagService.Create();
                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Something went wrong in method {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name}. {ex}");
            }
        }
    }
}
