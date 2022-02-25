using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.CORE.Entities;
public class Course
{
    public int Id{ get; set; }
    [Required]
    [MaxLength(100)]
    public string Title{ get; set; }
    public DateTime StartTime { get; set; }
    public ICollection<Module> Modules { get; set; }
}

