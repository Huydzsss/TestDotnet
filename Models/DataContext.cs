namespace Asp.Net_MvcWeb_Pj3.Aptech.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// Lớp ngữ cảnh dữ liệu, mô hình hóa MySQL DB Server
// KHông nói gì thì nó để mã hóa là: latin1_swedish_ci
// mà cái mình cần thì là: utf8mb4
// using MySql.EntityFrameworkCore.Extensions;

public class DataContext : DbContext
{
    // Các DbSet cho các bảng trong DB
    public DbSet<Department> Department { get; set; }
    public DbSet<Employee> Employee { get; set; }

    // Constructor không tham số
    public DataContext()
    {
    }

    // Constructor nhận DbContextOptions
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Kết nối cơ sở dữ liệu SQLite3
        options.UseSqlite(@"Data Source=.\data\data.db"); 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Department>().ToTable("Department"); 
        modelBuilder.Entity<Employee>().ToTable("Employee");  
    }
}
