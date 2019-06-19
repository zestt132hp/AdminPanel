using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AnnouncementRepos:IRepository<Announcement>
    {
        private bool _disposed;
        private readonly AdministratorContext _db;
        public AnnouncementRepos(IConfiguration config)
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
        public async Task<IEnumerable<Announcement>> GetList()
        {
            return await Task.Run(()=>_db.Announcement.Include(x=>x.User).ToList());
        }

        public async Task<Announcement> Get(Guid id)
        {
            return await _db.Announcement.FindAsync(id);
        }

        public async Task<bool> Create(Announcement item)
        {
            if(AlreadyExist(item.AnnouncementId))
                return false;
            await _db.Announcement.AddAsync(item);
            return true;
        }

        public async Task<Announcement> Update(Announcement item)
        {
            if (!AlreadyExist(item.AnnouncementId))
                return null;
            _db.Entry(item).State = EntityState.Modified;
            await Task.Run(() => _db.Announcement.Update(item));
            return await _db.Announcement.FindAsync(item.AnnouncementId);
        }

        public async Task<bool> Delete(Guid id)
        {
            if (!AlreadyExist(id))
                return false;
            Announcement announcement = await _db.Announcement.FindAsync(id);
            _db.Announcement.Remove(announcement);
            Save();
            return true;
        }

        public async void Save()
        {
            await _db.SaveChangesAsync();
        }

        private bool AlreadyExist(Guid id)
        {
            return _db.Announcement.AsParallel().All(x => x.AnnouncementId == id);
        }
    }
}
