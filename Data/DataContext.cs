﻿using filmsitesi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace filmsitesi.Data
{
    public class DataContext: IdentityDbContext<AppUser, AppRole, int>
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }
    }
}
