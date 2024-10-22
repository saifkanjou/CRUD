using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? Title { get; set; } = null;
        public string? Code  { get; set; } = null;

        public string? SortBy { get; set; } = null;

        public bool IsDescinding { get; set; } = false;

        public int PageNumber { get; set; } = 1; //by default 1

        public int PageSize { get; set; } = 20; //20 courses by default

    }
}