using Microsoft.EntityFrameworkCore;
using YouthOpportunities.Domain.Categories;

namespace YouthOpportunities.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
