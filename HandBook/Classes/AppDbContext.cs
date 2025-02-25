using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HandBook.Classes
{
    public class AppDbContext : DbContext
    {
        public DbSet<handBook> HandBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite(@"Data Source=D:\TRPO\Practice\HandBook\HandBook\handbook.db");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка для класса handBook
            modelBuilder.Entity<handBook>()
                .HasKey(h => h.Id); // Указываем, что поле Id является первичным ключом

            // Настройка связей между объектами
            modelBuilder.Entity<handBook>()
                .OwnsOne(h => h.car); // Вложенный объект Car
            modelBuilder.Entity<handBook>()
                .OwnsOne(h => h.data); // Вложенный объект PasportsData
        }
    }
}
