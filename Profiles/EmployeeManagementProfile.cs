using AutoMapper;
using EmployeeManagementApp.DTOs;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Profiles
{
    public class EmployeeManagementProfile : Profile
    {
        public EmployeeManagementProfile()
        {
            CreateMap<Employee, EmployeeGetDto>().ReverseMap();
        }

    }
}
