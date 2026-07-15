using YouthOpportunities.Application.Categories.Requests;
using YouthOpportunities.Application.Categories.Responses;
using YouthOpportunities.Application.Common.Results;

namespace YouthOpportunities.Application.Categories.Services;

public interface ICategoryService
{
    Task<Result<IReadOnlyList<CategoryDto>>> GetAllAsync(CancellationToken cancellationToken);

    Task<Result<CategoryDto>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<Result<CategoryDto>> CreateAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken);

    Task<Result<CategoryDto>> UpdateAsync(
        Guid id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken);

    Task<Result> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken);
}
