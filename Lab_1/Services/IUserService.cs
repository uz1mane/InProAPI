using Lab_1.Models;
using Lab_1.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Lab_1.Services
{
    public interface IUserService
    {
        Task CreateUser(RegistrationDTO model);             
        Task ChangePassword(EditUserDataDTO userData, int id);
        Task DeleteUser(int id);
        bool CheckIfUserChecksHimself(int userId, HttpContext context);
        bool CheckIfUserIsAdmin(HttpContext context);
    }

    public class UserService : IUserService
    {
        private readonly Context _context;

        public UserService(Context context)
        {
            _context = context;
        }
        public async Task CreateUser(RegistrationDTO model)
        {
            await _context.AddAsync(new User
            {                
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                IsPremium = false
            });
            await _context.SaveChangesAsync();
        }            

        public async Task ChangePassword(EditUserDataDTO userData, int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != default)
            {
                if (userData.Password != null)
                    user.Password = userData.Password;                
                await _context.SaveChangesAsync();
                return;
            }
            else throw new Exception("User with such ID was not found");
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != default)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            } 
            else throw new Exception("User with such ID was not found.");            
        }

        public bool CheckIfUserChecksHimself(int userId, HttpContext context)
        {
            var authenticatedUserId = context.User.Claims.Where(c => c.Type == "id")
                                        .Select(c => c.Value).SingleOrDefault();
            if (authenticatedUserId == userId.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckIfUserIsAdmin(HttpContext context)
        {
            var role = context.User.Claims.Where(c => c.Type == "roleName")
                                .Select(c => c.Value).SingleOrDefault();
            if (role == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}
