using AutoMapper;
using StudentAdminPortal.API.DTOS;
using StudentAdminPortal.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Profiles
{
    public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentRequest, Student>
    {
        public void Process(UpdateStudentRequest source, Student destination)
        {
            destination.Address = new Address()
            {
                PostalAddress=source.PostalAddress,
                PhysicalAddress=source.PhysicalAddress
            };
        }
    }
}
