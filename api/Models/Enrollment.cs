using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Enrollments")]
    public class Enrollment
    {
        public string AppUserId { get; set; } // Foreign Key
        public AppUser AppUser { get; set; } // Navigation property

        public int CourseID { get; set; } // Foreign Key
        public Course Course { get; set; } // Navigation property

        public DateTime EnrollmentDate { get; set; }

        public string Grade { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty; //such as "Active", "Completed", "Dropped"

        public bool IsCompleted { get; set; } //indicates if the course is completed
    }
}
