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
    public class SkinsController : ControllerBase
    {
        private readonly Context _context;
        private IInvestmentContainerService _investmentContainerService;

        public SkinsController(Context context, IInvestmentContainerService investmentBagService)
        {
            _context = context;
            _investmentContainerService = investmentBagService;
        }

        [HttpGet("AddSkin"), ActionName("Add a skin to container")]
        [Authorize]
        public async Task<IActionResult> AddSkin()
        {
            
        }

        [HttpPost("Create"), ActionName("Create an investment container")]
        [Authorize]
        public async Task<IActionResult> DeleteSkin(CreateInvestmentContainerDTO DTO)
        {
           
        }

        [HttpDelete("Delete"), ActionName("Delete Investment Bag")]
        [Authorize]
        public async Task<IActionResult> CheckPrice(DeleteInvestmentContainerDTO model)
        {
            
        }
    }
}
