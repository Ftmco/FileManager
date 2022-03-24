using File.Entity.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.DataBase.Context;

public class FileContext : DbContext
{
    public static string ConnectionString { get; set; }

    public FileContext(string connectionString)
    {
        if (!string.IsNullOrEmpty(connectionString))
            ConnectionString = connectionString;
    }

    public FileContext(DbContextOptions<FileContext> options) : base(options)
    {

    }

    public virtual DbSet<Application> Application { get; set; }

    public virtual DbSet<Entity.File.Directory> Directory { get; set; }

    public virtual DbSet<Entity.File.File> File { get; set; }
}