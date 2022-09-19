using AutoMapper;
using Company.DAL.Entities;
using Company.Presentation_Layer.Models;

namespace Company.Presentation_Layer.Mappers
{
    public class EmployeeProfile : Profile  
    {
        public EmployeeProfile()
        {
            CreateMap< Employee, EmployeeViewModel>().ReverseMap();  
        } 
    }
}
