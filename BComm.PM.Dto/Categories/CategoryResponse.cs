using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BComm.PM.Dto.Categories
{
    public class CategoryResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string TagHashId { get; set; }

        public string TagName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public IEnumerable<CategoryResponse> Subcategories { get; set; }
    }
}
