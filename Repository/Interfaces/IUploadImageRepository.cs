﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repository.Interfaces
{
    public interface IUploadImageRepository
    {
        Task<string> Upload(IFormFile file, string fileName);

    }
}
