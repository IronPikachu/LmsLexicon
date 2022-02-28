using Lms.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.CORE.Dto;
public class CourseModuleDto : CourseDto
{
    public string Title { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndDate => StartTime.AddMonths(3); // 3 months after startdate
    public ICollection<Module> Modules { get; set; }
}
