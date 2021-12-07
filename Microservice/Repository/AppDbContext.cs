﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Models;

namespace Microservice.Repository

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) :base (opt)
        {

        }

        public DbSet<Platform> Platforms { get; set; }
    }
}
