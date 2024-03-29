using PregaDays.Models.Domain;

namespace PregaDays.Repositories
{
    public interface IDescriptionRepository
    {
        Task<List<Day>> GetAllAsync();
        Task<Day?> GetByIdAsync(Guid id);
        Task<Day?> CreateAsync(Day day);
        Task<Day?> DeleteAsync(Guid id);
        Task<Day?> UpdateAsync(Guid id, Day day);
    }
}
