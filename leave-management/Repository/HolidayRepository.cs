using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly ApplicationDbContext _db;
        public HolidayRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Holiday entity)
        {
            await _db.Holidays.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Holiday entity)
        {
            _db.Holidays.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Holiday>> FindAll()
        {
            var holidays = await _db.Holidays.ToListAsync();
            return holidays;
        }

        public async Task<Holiday> FindById(int id)
        {
            var holidays = await _db.Holidays.FindAsync(id);
            return holidays;
        }

        public async Task<bool> isExists(int id)
        {
            var exists = await _db.Holidays.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Holiday entity)
        {
            _db.Holidays.Update(entity);
            return await Save();
        }
    }
}
