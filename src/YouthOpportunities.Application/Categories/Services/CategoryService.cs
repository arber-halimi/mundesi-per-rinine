using Microsoft.EntityFrameworkCore;
using YouthOpportunities.Application.Categories.Requests;
using YouthOpportunities.Application.Categories.Responses;
using YouthOpportunities.Application.Common.Interfaces;
using YouthOpportunities.Application.Common.Results;
using YouthOpportunities.Domain.Categories;

namespace YouthOpportunities.Application.Categories.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly IApplicationDbContext _context;

    public CategoryService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IReadOnlyList<CategoryDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .Select(category => ToDto(category))
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<CategoryDto>>.Success(categories);
    }

    public async Task<Result<CategoryDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == id, cancellationToken);

        if(category is null)
        {
            return Result<CategoryDto>.Failure(CategoryErrors.NotFound(id));
        }

        var result = ToDto(category);

        return Result<CategoryDto>.Success(result);
    }

    public async Task<Result<CategoryDto>> CreateAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var slug = NormalizeSlug(request.Slug);

        var slugExists = await _context.Categories
            .AnyAsync(category => category.Slug == slug, cancellationToken);

        if (slugExists)
        {
            return Result<CategoryDto>.Failure(CategoryErrors.SlugAlreadyExists(slug));
        }

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Slug = slug,
            Description = string.IsNullOrWhiteSpace(request.Description)
                ? null
                : request.Description.Trim(),
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        };

        _context.Categories.Add(category);

        await _context.SaveChangesAsync(cancellationToken);

        var result = ToDto(category);

        return Result<CategoryDto>.Success(result);
    }

    public async Task<Result<CategoryDto>> UpdateAsync(
        Guid id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == id, cancellationToken);

        if (category is null)
        {
            return Result<CategoryDto>.Failure(CategoryErrors.NotFound(id));
        }

        var slug = NormalizeSlug(request.Slug);

        var slugExists = await _context.Categories
            .AnyAsync(category => category.Id != id && category.Slug == slug, cancellationToken);

        if (slugExists)
        {
            return Result<CategoryDto>.Failure(CategoryErrors.SlugAlreadyExists(slug));
        }

        category.Name = request.Name.Trim();
        category.Slug = slug;
        category.Description = string.IsNullOrWhiteSpace(request.Description)
            ? null
            : request.Description.Trim();
        category.IsActive = request.IsActive;
        category.UpdatedAtUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        var result = ToDto(category);

        return Result<CategoryDto>.Success(result);
    }

    public async Task<Result> DeactivateAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == id, cancellationToken);

        if (category is null)
        {
            return Result.Failure(CategoryErrors.NotFound(id));
        }

        category.IsActive = false;
        category.UpdatedAtUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private static CategoryDto ToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            IsActive = category.IsActive,
            CreatedAtUtc = category.CreatedAtUtc,
            UpdatedAtUtc = category.UpdatedAtUtc
        };
    }

    private static string NormalizeSlug(string slug)
    {
        return slug.Trim().ToLowerInvariant();
    }
}
