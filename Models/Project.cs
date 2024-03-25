using System;
using System.Collections.Generic; // Required for using List<>
using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
  public class Project
  {
    [Key]
    public int ProjectId { get; set; }

    [Required(ErrorMessage = "Project name is required.")]
    public required string Name { get; set; }


    public string? Description { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [DataType(DataType.Date)]
    [Display(Name = "Updated At")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [DataType(DataType.Date)]
    [Display(Name = "Ended At")]
    public DateTime? EndedAt { get; set; }

    public string? Status { get; set; } = "Active";



    public ICollection<ProjectTask> ProjectTasks { get; set; } = [];

    public int TaskCount
    {
      get { return CountTasks(); }
    }

    public int CompletedTaskCount
    {
      get { return CountCompletedTasks(); }
    }

    public int RemainingTaskCount
    {
      get { return TaskCount - CompletedTaskCount; }
    }

    private int CountTasks()
    {
      int count = 0;
      foreach (var task in ProjectTasks)
      {
        count++;
      }
      return count;
    }

    private int CountCompletedTasks()
    {
      int count = 0;
      foreach (var task in ProjectTasks)
      {
        if (task.Status == "Completed")
        {
          count++;
        }
      }
      return count;
    }
  }
}
