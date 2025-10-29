using Domain.Entities;

namespace BLL.Abstractions;
public interface ICategoryRepository
{
    Task<int> CreateAsync(Category category, CancellationToken cancellationToken);
    Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<Category?> GetByIdAsync(int id, CancellationToken? cancellationToken);
    Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken);
    Task DeleteAsync(int categoryId, CancellationToken cancellationToken);
}
