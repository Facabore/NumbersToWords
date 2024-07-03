using System.Data.Common;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NumbersToVoice.Entities;

namespace NumbersToVoice.Database;

public sealed class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
        try
        {
            var dbCreation = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (dbCreation == null) return;
            if(!dbCreation.CanConnect()) dbCreation.Create();
            if(!dbCreation.HasTables()) dbCreation.CreateTables();

        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(table =>
        {
            table.HasKey(id => id.idUser);

            table.Property(name => name.nameUser).IsRequired().HasMaxLength(20);
            table.Property(password => password.passwordUser).IsRequired();
            
            table.Property(email => email.emailUser).IsRequired();
            table.HasIndex(email => email.emailUser).IsUnique();
        });

        modelBuilder.Entity<User>().ToTable("users");
    }
}