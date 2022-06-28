using Lab_1.Models;
using Lab_1.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lab_1.Services
{
    public interface ISkinsService
    {
        
    }

    public class SkinsService : ISkinsService
    {

        private readonly Context _context;

        public SkinsService(Context context)
        {
            _context = context;
        }

        public async Task Create(CreateInvestmentContainerDTO DTO, User Owner)
        {
            
        }

        public async Task<List<InvestmentContainer>> GetContainersList()
        {
            
        }

        public async Task AddSkin()
        {

        }

        public async Task Delete(DeleteInvestmentContainerDTO model)
        {
            var currentContainer = await _context.InvestmentContainers.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (currentContainer != default)
            {
                _context.InvestmentContainers.Remove(currentContainer);
                await _context.SaveChangesAsync();
                return;
            }
            else throw new Exception("No such container existing");
        }

    }
}
