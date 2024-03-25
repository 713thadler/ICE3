using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab1.Models
{
    public class ProjectTask
    {
        // Removed the static counter for ID assignment.
        // Rely on the database to generate the ID when the entity is added.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectTaskId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string? Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        [Required]
        public int ProjectId { get; set; } // Foreign key

        [ForeignKey("ProjectId")]
        public virtual Project? Project { get; set; } // Navigation property for the related Project entity

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } = "Not Started";

        // Additional fields to enhance task tracking
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public required string ProjectName { get; set; }

        public bool IsCompleted => Status.Equals("Completed", StringComparison.OrdinalIgnoreCase);
    }
}
