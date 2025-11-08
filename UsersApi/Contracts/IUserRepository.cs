using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersApi.Models;

namespace UsersApi.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
        Task DeactivateAsync(User user);
        Task ActivateAsync(User user);
    }
}
