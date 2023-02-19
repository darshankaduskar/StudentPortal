using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DTOS;
using StudentAdminPortal.API.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Student : ControllerBase
    {
        private readonly IStudentRepository repo;
        private readonly IMapper mapper;
        private readonly IUploadImageRepository imageRepo;

        public Student(IStudentRepository repo,IMapper mapper, IUploadImageRepository imageRepo)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.imageRepo = imageRepo;
        }
        [HttpGet("students")]
        
        public async Task<IActionResult> GetAllStudents()
        {
            var studentsdata =await  repo.GetAllStudent();
            
            return Ok(mapper.Map<List<StudentDto>>(studentsdata));
        }

        [HttpGet("students/{id:guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var student= await repo.GetById(id);
          
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPut("edit/{studentId:guid}")]

        public async Task<IActionResult> UpdateStudentASync([FromRoute] Guid studentId,[FromBody] UpdateStudentRequest studentUpdate)
        {
     if(await repo.Exists(studentId))
            {
                var updateStudent = await repo.UpdateStudentAsync(studentId, mapper.Map<Models.Student>(studentUpdate));
                if (updateStudent != null)
                {
                    return Ok(mapper.Map<Models.Student>(updateStudent));
                }
            }
            return NotFound();

        }


        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
            if(await repo.Exists(id))
            {
                var studentData =await repo.DeleteStudentAsync(id);
                 return Ok(mapper.Map<Models.Student>(studentData));

            }
            return NotFound();
        }

        [HttpPost("add")]

        public async Task<Models.Student> AddNewStudent([FromBody] AddStudentRequest student)

        {
            //var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

            //var fileImagePath = await imageRepo.Upload(profileImage, fileName);

            //Models.Student request = new Models.Student();
            //request.FirstName = student.FirstName;
            //request.LastName = student.LastName;
            //request.DateOfBirth = student.DateOfBirth;
            //request.Email = student.Email;
            //request.Mobile = student.Mobile;
            //request.GenderId = student.GenderId;
            ////request.Gender.Id = student.GenderId;
          
            //request.Id = Guid.NewGuid();
            //request.Address = new Models.Address()
            //{
            //    Id = Guid.NewGuid(),
            //    PhysicalAddress = student.PhysicalAddress,
            //    PostalAddress = student.PostalAddress

            //};
  
            var request=mapper.Map<Models.Student>(student);




            var studentData=await repo.AddStudentAsync(request);

            return mapper.Map<Models.Student>(studentData);
            


        }

        [HttpPost("upload/{id:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute]Guid id, IFormFile profileImage)
        {

            if(await repo.Exists(id))
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                var fileImagePath=await imageRepo.Upload(profileImage,fileName);

                if(await repo.UploadImage(id, fileImagePath))
                {
                    return Ok(fileImagePath);
                }

                return StatusCode(StatusCodes.Status500InternalServerError,"Error at Uploading");

            }

            return NotFound();
        }
    }
}
