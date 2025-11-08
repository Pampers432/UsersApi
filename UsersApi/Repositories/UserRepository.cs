using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersApi.Contracts;
using UsersApi.Data;
using UsersApi.Models;

namespace UsersApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersApiDbContext _context;

        public UserRepository(UsersApiDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.users.Include(u => u.Role).ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateAsync(User user)
        {
            user.IsActive = false;
            await _context.SaveChangesAsync();

            // TODO: вызвать SoftDelete продуктов в микросервисе продуктов
        }

        public async Task ActivateAsync(User user)
        {
            user.IsActive = true;            
            await _context.SaveChangesAsync();

            // TODO: восстановить продукты в микросервисе продуктов
        }
    }
}
