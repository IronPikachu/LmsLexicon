using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.CORE.Dto;
public class CourseDto
{
    public virtual string Title { get; set; }
    public virtual DateTime StartTime { get; set; }
    public virtual DateTime EndDate => StartTime.AddMonths(3); // 3 months after startdate
}

