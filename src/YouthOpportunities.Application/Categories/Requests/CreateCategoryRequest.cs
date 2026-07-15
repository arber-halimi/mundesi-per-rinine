using System;
using System.Collections.Generic;
using System.Text;

namespace YouthOpportunities.Application.Categories.Requests
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
