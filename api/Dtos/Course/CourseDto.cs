using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Reviews;

namespace api.Dtos.Course
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Credits { get; set; }

        public List<ReviewDto>? Reviews { get; set; }

    }
}