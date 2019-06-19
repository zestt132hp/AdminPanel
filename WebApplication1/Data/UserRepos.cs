using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class UserRepos:IRepository<User>
    {
        private readonly AdministratorContext _db;
        private bool _disposed;
        public UserRepos(IConfiguration config)
        {
            _db = new AdministratorContext(new DbContextOptions<AdministratorContext>(), config);
            _db.Configuring();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }

        public async Task<IEnumerable<User>> GetList()
        {
            return await Task.Run((() => _db.Users.ToList()));
        }

        public async Task<User> Get(Guid id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<bool> Create(User item)
        {
            if (AlreadyExists(item.UserId))
                return false;
            await _db.Users.AddAsync(item);
            return true;
        }

        public async Task<User> Update(User item)
        {
            if (!AlreadyExists(item.UserId))
                return null;
            _db.Entry(item).State = EntityState.Modified;
            await Task.Run(() => _db.Users.Update(item));
            return await _db.Users.FindAsync(item.UserId);
        }

        public async Task<bool> Delete(Guid id)
        {
            if (!AlreadyExists(id))
                return false;
            User user = await _db.Users.FindAsync(id);
            _db.Users.Remove(user);
            Save();
            return true;
        }

        public async void Save()
        {
            await _db.SaveChangesAsync();
        }

        private bool AlreadyExists(Guid id)
        {
            return _db.Users.AsParallel().All(x => x.UserId == id);
        }
    }
}
