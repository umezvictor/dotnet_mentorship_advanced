using Shared.Dto;
using Shared.ResponseObjects;

namespace BLL.Services;
public interface ICategoryService
{
	Task<Response<string>> AddCategoryAsync (AddCategoryRequest request, CancellationToken cancellationToken);
	Task<Response<string>> DeleteCategoryAsync (DeleteCategoryRequest request, CancellationToken cancellationToken);
	Task<Response<List<CategoryDto>>> GetCategoriesAsync (CancellationToken cancellationToken);
	Task<Response<CategoryDto>> GetCategoryById (int id, CancellationToken cancellationToken);
	Task<Response<string>> UpdateCategoryAsync (UpdateCategoryRequest request, CancellationToken cancellationToken);
}