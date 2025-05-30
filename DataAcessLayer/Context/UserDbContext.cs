﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAcessLayer.Context
{
    public class UserDbContext:DbContext
    {

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }


    }
}
