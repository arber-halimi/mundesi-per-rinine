using YouthOpportunities.Application.Common.Results;

namespace YouthOpportunities.Application.Categories
{
    public static class CategoryErrors
    {
        public static Error NotFound(Guid id)
        {
            return new Error(
                "Category.NotFound",
                $"Category with id '{id}' was not found.",
                ResultStatus.NotFound);
        }

        public static Error SlugAlreadyExists(string slug)
        {
            return new Error(
                "Category.SlugAlreadyExists",
                $"A category with slug '{slug}' already exists.",
                ResultStatus.Conflict);
        }
    }
}
