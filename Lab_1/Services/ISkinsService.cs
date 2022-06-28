using Lab_1.Models;
using Lab_1.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_1.Services
{
    public interface ISkinsService
    {
        Task AddSkin(AddSkinDTO DTO, InvestmentContainer container);
        Task<List<AddSkinDTO>> GetContainerContent(int containerId);
        Task Delete(int skinId);
    }

    public class SkinsService : ISkinsService
    {

        private readonly Context _context;

        public SkinsService(Context context)
        {
            _context = context;
        }

        public async Task AddSkin(AddSkinDTO DTO, InvestmentContainer container)
        {
            /*var container = await _context.InvestmentContainers.FirstOrDefaultAsync(x => x.Id == DTO.containerId);
            if (container == null)
            {
                throw new Exception("No such container found");
            }*/

            _context.Skins.Add(new Skin
            {
                Name = DTO.Name,
                Amount = DTO.Amount,
                BoughtFor = DTO.BoudghtFor,
                PercentGoal = DTO.PercentGoal,
                container = container
            });

            await _context.SaveChangesAsync();
            return;
        }

        public async Task<List<AddSkinDTO>> GetContainerContent(int containerId)
        {
            var container = await _context.InvestmentContainers.Include(x => x.Skins).FirstOrDefaultAsync(x => x.Id == containerId);
            if (container == default)
                throw new Exception("Such container doesn't exist.");

            var skins = await _context.Skins.Where(x => x.container == container).Select(x => new AddSkinDTO
            {
                Name = x.Name,
                BoudghtFor = x.BoughtFor,
                PercentGoal = x.PercentGoal,
                Amount = x.Amount,
                containerId = x.container.Id
            }).ToListAsync();
            if (skins.Count != 0)
            {
                return skins;
            }
            else throw new Exception("This container has no skins.");
        }       

        public async Task Delete(int skinId)
        {
            var currentSkin = await _context.Skins.FirstOrDefaultAsync(x => x.Id == skinId);
            if (currentSkin != default)
            {
                _context.Skins.Remove(currentSkin);
                await _context.SaveChangesAsync();
                return;
            }
            else throw new Exception("No such container existing");
        }

    }
}
