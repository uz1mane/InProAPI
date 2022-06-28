using Lab_1.Models;
using Lab_1.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Lab_1.Services
{
    public interface IInvestmentContainerService
    {
        Task Create(CreateInvestmentContainerDTO DTO, User Owner);
        Task<List<InvestmentContainer>> GetContainersList();
        Task AddSkin();
        Task Delete(int containerId);
    }

    public class InvestmentContainerService : IInvestmentContainerService
    {

        private readonly Context _context;

        public InvestmentContainerService(Context context)
        {
            _context = context;
        }

        public async Task Create(CreateInvestmentContainerDTO DTO, User Owner)
        {
            _context.InvestmentContainers.Add(new InvestmentContainer
            {
                Name = DTO.Name,
                Owner = Owner,
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<InvestmentContainer>> GetContainersList ()
        {
            var containersList = _context.InvestmentContainers.Include(x => x.Owner).ToList();
            if (containersList == null)
                throw new Exception("No containers yet");
            return containersList;
        }

        public async Task AddSkin()
        {

        }

        public async Task Delete(int containerId)
        {
            var currentContainer = await _context.InvestmentContainers.FirstOrDefaultAsync(x => x.Id == containerId);
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
