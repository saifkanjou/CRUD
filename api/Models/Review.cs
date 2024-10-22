using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
        [Table("Reviews")]

    public class Review
    {
        public int Id { get; set; }

        public string Content { get; set; } = String.Empty;

        public int Rating { get; set; } // Rating out of 5 

        public DateTime CreatedOn { get; set; } = DateTime.Now;

//one-To-many with the course
        public int? CourseId { get; set; } // Foreign Key to Course
        public Course? Course { get; set; } // Navigation property to the related course

        //one to one
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}