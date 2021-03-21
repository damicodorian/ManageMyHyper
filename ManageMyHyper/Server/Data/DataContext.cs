using ManageMyHyper.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyHyper.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<WorkTaskPriority> WorkTaskPriorities { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
