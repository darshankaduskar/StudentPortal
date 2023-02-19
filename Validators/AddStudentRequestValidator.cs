using FluentValidation;
using StudentAdminPortal.API.DTOS;
using StudentAdminPortal.API.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Validators
{
    public class AddStudentRequestValidator:AbstractValidator<AddStudentRequest>
    {
        public AddStudentRequestValidator(IStudentRepository repo)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).GreaterThan(99999).LessThan(1000000000);
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = repo.GetAllGender().Result.ToList().FirstOrDefault(x => x.Id == id);

                if (gender != null)
                {
                    return true;
                }
                return false;
            }).WithMessage("Please select valid Gender");
        }
    }
}
