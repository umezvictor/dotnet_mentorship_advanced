using AutoMapper;
using BLL.Abstractions;
using DAL.Database.Repository;
using DAL.Entities;
using Shared.Dto;
using Shared.ResponseObjects;

namespace BLL.Services;
public sealed class CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ILinkService linkService) : ICategoryService
{
    public async Task<Response<string>> AddCategoryAsync(AddCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request);
        await categoryRepository.CreateAsync(category, cancellationToken);
        return new Response<string>(ResponseMessage.CategoryAdded);
    }

    public async Task<Response<string>> DeleteCategoryAsync(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {

        await categoryRepository.DeleteAsync(request.Id, cancellationToken);
        return new Response<string>(ResponseMessage.CategoryDeleted);
    }

    public async Task<Response<List<CategoryDto>>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllCategoriesAsync(cancellationToken);
        return new Response<List<CategoryDto>>(mapper.Map<List<CategoryDto>>(categories), ResponseMessage.Success);
    }

    public async Task<Response<string>> UpdateCategoryAsync(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
        {
            return new Response<string>(ResponseMessage.CategoryNotFound, false);
        }

        await categoryRepository.UpdateAsync(mapper.Map(request, category), cancellationToken);
        return new Response<string>(ResponseMessage.CategoryUpdated);
    }

    public async Task<Response<CategoryDto>> GetCategoryById(int id, CancellationToken cancellationToken)
    {

        var category = await categoryRepository.GetByIdAsync(id, cancellationToken);
        if (category is null)
            return new Response<CategoryDto>(ResponseMessage.CategoryNotFound, false);

        CategoryDto categoryDto = mapper.Map<CategoryDto>(category);
        AddLinksForCategory(categoryDto);
        return new Response<CategoryDto>(categoryDto, ResponseMessage.Success);
    }

    private void AddLinksForCategory(CategoryDto categoryDto)
    {
        categoryDto.Links.Add(linkService.GenerateLinks(
               "GetCategory",
               new { id = categoryDto.Id },
               "self",
               "GET"));

        categoryDto.Links.Add(linkService.GenerateLinks(
              "DeleteCategory",
              new { id = categoryDto.Id },
              "delete-category",
              "DELETE"));

        categoryDto.Links.Add(linkService.GenerateLinks(
              "UpdateCategory",
              new { id = categoryDto.Id },
              "update-category",
              "PUT"));

    }
}
