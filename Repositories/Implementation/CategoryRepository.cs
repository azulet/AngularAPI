using Code.Models.Domain;
using Code.Repositories.Interface;
using Code_Pulse_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Code.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid Id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
