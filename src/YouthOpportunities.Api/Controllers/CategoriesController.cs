using Microsoft.AspNetCore.Mvc;
using YouthOpportunities.Api.Extensions;
using YouthOpportunities.Application.Categories.Requests;
using YouthOpportunities.Application.Categories.Services;

namespace YouthOpportunities.Api.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetAllAsync(cancellationToken);

        return result.ToActionResult(this, Ok);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategoryByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetByIdAsync(id, cancellationToken);

        return result.ToActionResult(this, Ok);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.CreateAsync(request, cancellationToken);

        return result.ToActionResult(
            this,
            category => CreatedAtAction(
                nameof(GetCategoryByIdAsync),
                new { id = category.Id },
                category));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        Guid id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateAsync(id, request, cancellationToken);

        return result.ToActionResult(this, Ok);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeactivateAsync(id, cancellationToken);

        return result.ToActionResult(this, NoContent);
    }
}
