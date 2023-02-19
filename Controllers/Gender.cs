using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DTOS;
using StudentAdminPortal.API.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Gender : ControllerBase
    {
        private readonly IStudentRepository repo;
        private readonly IMapper mapper;

        public Gender(IStudentRepository repo,IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllGender()
        {
            var genderData = await repo.GetAllGender();
            if (genderData == null || !genderData.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<GenderDto>>(genderData));
        }
    }
}
