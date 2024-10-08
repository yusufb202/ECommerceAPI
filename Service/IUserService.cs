﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        Task<User> ValidateUserAsync(string username, string password);
        Task SeedAdminUserAsync();
        string GenerateJwtToken(User user);
    }
}
