using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Course
{
    public class UpdateCourseDto
    {
        [Required(ErrorMessage = "Course code is required")]
        [StringLength(10, ErrorMessage = "Course code cannot be longer than 10 characters")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Range(1, 6, ErrorMessage = "Credits must be between 1 and 6")]
        public int Credits { get; set; }
    }
}