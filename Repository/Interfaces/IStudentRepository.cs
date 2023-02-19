using StudentAdminPortal.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudent();
        Task<Student> GetById(Guid studentId);
        Task<List<Gender>> GetAllGender();
        Task<bool> Exists(Guid StudentId);

        Task<Student> UpdateStudentAsync(Guid id, Student request);
        Task<Student> DeleteStudentAsync(Guid studentId);

        Task<Student> AddStudentAsync(Student request);

        Task<bool> UploadImage(Guid studentId,string profileImageUrl);
    }
}
