using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Courses")]

    public class Course
    {
        public int CourseId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Credits { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }
}