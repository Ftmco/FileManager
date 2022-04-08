using File.Entity;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<DirectoryFile> File { get; set; }

    public virtual DbSet<FDirectory> Directory { get; set; }

}