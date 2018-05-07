namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestContext : DbContext
    {
        public TestContext()
            : base("name=TestContext")
        {
        }

        public virtual DbSet<ALUMNO> ALUMNO { get; set; }
        public virtual DbSet<CURSO> CURSO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ALUMNO>()
                .HasMany(e=> e.CURSOS)
                .WithMany(e => e.ALUMNO)
                .Map(m => m.ToTable("ALUMNOCURSO"));
        }
    }
}
