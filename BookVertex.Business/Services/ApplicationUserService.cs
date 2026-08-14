using BookVertex.Business.Services.IServices;
using BookVertex.DataAccess.Data;
using BookVertex.Models;
using Microsoft.EntityFrameworkCore;

namespace BookVertex.Business.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _context.ApplicationUsers.ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
