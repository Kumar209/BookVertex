using BookVertex.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookVertex.Business.Services.IServices
{
    public interface IApplicationUserService
    {
        Task<ApplicationUser?> GetUserByIdAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    }
}
