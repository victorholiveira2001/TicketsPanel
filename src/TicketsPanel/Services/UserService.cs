﻿using TicketsPanel.Data;
using TicketsPanel.Models;

namespace TicketsPanel.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpdateManagerId(int departmentId, User user)
        {
            user.DepartmentId = departmentId;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
