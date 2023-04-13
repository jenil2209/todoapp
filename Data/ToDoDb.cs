using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using Microsoft.Data.Sqlite;

namespace WebApplication2.Data
{
    public class ToDoDb : DbContext
    {
        public string DbPath { get; }

        public ToDoDb(DbContextOptions<ToDoDb> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "blogging.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<Todoitem> ToDos { get; set; }

    }
}
