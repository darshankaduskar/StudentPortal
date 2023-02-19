using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;

        public StudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }

        public async Task<Student> DeleteStudentAsync(Guid studentId)
        {
            var studentData =await GetById(studentId);
            if (studentData != null)
            {
                 context.Student.Remove(studentData);
                 await context.SaveChangesAsync();
                return studentData;
            }

            return null;

        }
        public async Task<bool> UploadImage(Guid studentId, string profileImageUrl)
        {
            var student = await GetById(studentId);
            if (student != null)
            {
                student.ProfileImageUrl = profileImageUrl;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Student> AddStudentAsync(Student request)
        {
           // request.ProfileImageUrl = profileImageUrl;
            var student =await context.Student.AddAsync(request);
            await context.SaveChangesAsync();
            return student.Entity;
        }

        public async Task<bool> Exists(Guid StudentId)
        {
           return await context.Student.AnyAsync(x=>x.Id==StudentId);
        }

        public async Task<List<Gender>> GetAllGender()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<List<Student>> GetAllStudent()
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> GetById(Guid studentId)
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<Student> UpdateStudentAsync(Guid id, Student request)
        {
           var existingStudent= await context.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x => x.Id == id);
            if (existingStudent != null)
            {

                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;
                    await context.SaveChangesAsync();
                return existingStudent;

            }
            return null;

        }


    }
}
