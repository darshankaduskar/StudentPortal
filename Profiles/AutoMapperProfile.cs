using AutoMapper;
using StudentAdminPortal.API.DTOS;
using StudentAdminPortal.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<UpdateStudentRequest, Student>().AfterMap<UpdateStudentRequestAfterMap>();
            CreateMap<AddStudentRequest, Student>().AfterMap<AddStudentReqAfterMap>();

            
        }
        
    }
}
