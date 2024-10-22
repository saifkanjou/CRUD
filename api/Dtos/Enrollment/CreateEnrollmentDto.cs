using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Enrollment
{
    public class CreateEnrollmentDto
    {
    public string Status { get; set; }  =string.Empty;
    public bool IsCompleted { get; set; }
    }
}