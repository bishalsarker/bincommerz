using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Models.Products
{
    public class FilterQuery
    {
        [FromQuery(Name = "tag_id")]
        public string TagId {get ;set;}

        [FromQuery(Name = "cat_slug")]
        public string CatSlug { get; set; }

        [FromQuery(Name = "keyword")]
        public string Keyword { get; set; }

        [FromQuery(Name = "sort_by")]
        public string SortBy { get; set; }

        [FromQuery(Name = "page_number")]
        public int PageNumber { get; set; }

        [FromQuery(Name = "page_size")]
        public int PageSize { get; set; }
    }
}
