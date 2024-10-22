using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Enrollment
{
    public class EnrollmentDto
    {
     public int CourseId { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Credits { get; set; }
    public DateTime EnrollmentDate { get; set; } // Enrollment attribute
    public string Status { get; set; }
    public bool IsCompleted { get; set; }
    }

}