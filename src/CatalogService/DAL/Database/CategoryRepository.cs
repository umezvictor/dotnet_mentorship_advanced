using BLL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Database;
public sealed class CategoryRepository(ApplicationDbContext _context) : ICategoryRepository
{
    public async Task<int> CreateAsync(Category category, CancellationToken cancellationToken)
    {
        if (!category.IsValid())
        {
            throw new ArgumentException("Invalid category");
        }
        var createdItem = await _context.Category.AddAsync(category);
        await _context.SaveChangesAsync(cancellationToken);

        return createdItem.Entity.Id;

    }

    public async Task<Category?> GetByIdAsync(int id, CancellationToken? cancellationToken)
    {
        return await _context.Category.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        return await _context.Category.AsNoTracking().ToListAsync(cancellationToken);
    }





    public async Task DeleteAsync(int categoryId, CancellationToken cancellationToken)
    {
        var category = await _context.Category
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);

        if (category != null)
        {
            _context.Category.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        var updatedItem = _context.Category.Update(category);
        await _context.SaveChangesAsync();
        return updatedItem.Entity;
    }
}