using FineArts.Entities;
using Microsoft.EntityFrameworkCore;

namespace FineArts.Dal
{
    public class FineArtsContext : DbContext //implementa código encargado de realizar comnunicación con la BD
    {
        //DbSet representación mediante propiedades de nuestras tablas
        public DbSet<Teacher> Teachers => Set<Teacher>(); //{ get; set; }
        public DbSet<Student> Students => Set<Student>();//{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //llama a la clase base de DbContext
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FineArts");
            //base.OnConfiguring(optionsBuilder);
        }
    }
}