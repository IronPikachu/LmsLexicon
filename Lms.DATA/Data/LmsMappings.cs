using AutoMapper;
using Lms.CORE.Dto;
using Lms.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.DATA.Data;
public class LmsMappings : Profile
{
    public LmsMappings()
    {
        CreateMap<Course, CourseDto>();
        CreateMap<Module, ModuleDto>();
    }
}
