using Microsoft.EntityFrameworkCore;
using PregaDays.Data;
using PregaDays.Models.Domain;

namespace PregaDays.Repositories
{
    public class SqlDescriptionRepository : IDescriptionRepository
    {
        private readonly PregaDaysDbContext context;
        public SqlDescriptionRepository(PregaDaysDbContext dbContext)
        {
            this.context = dbContext;
        }
        public async Task<Day> CreateAsync(Day day)
        {
            await context.Days.AddAsync(day);
            await context.SaveChangesAsync();
            return day;
        }

        public async Task<Day?> DeleteAsync(Guid id)
        {
            var day = await context.Days.FirstOrDefaultAsync(x => x.Id == id);
            if(day == null)
            {
                return null;
            }
            context.Days.Remove(day);
            await context.SaveChangesAsync();
            return day;
        }

        public async Task<List<Day>> GetAllAsync()
        {
            return await context.Days.ToListAsync();
        }

        public async Task<Day?> GetByIdAsync(Guid id)
        {
            return await context.Days.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Day?> UpdateAsync(Guid id, Day day)
        {
            var oldDay = await context.Days.FirstOrDefaultAsync(x => x.Id == id);
            if (oldDay == null)
            {
                return null;
            }
            oldDay.Description = day.Description;
            oldDay.DayNumber = day.DayNumber;
            await context.SaveChangesAsync();
            return oldDay;
        }
    }
}
