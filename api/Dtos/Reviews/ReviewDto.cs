using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Reviews
{
    public class ReviewDto
    {
        public int Id { get; set; }
        
        public string CreatedBy { get; set; } = string.Empty;
        public string Content { get; set; } = String.Empty;

        public int Rating { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? CourseId { get; set; }
    }
}