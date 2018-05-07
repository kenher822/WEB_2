namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Linq;

    [Table("ALUMNO")]
    public partial class ALUMNO
    {
        public ALUMNO()
        {
            CURSOS = new List<CURSO>();
        }
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string NOMBRE { get; set; }

        [Required]
        [StringLength(50)]
        public string APELLIDO { get; set; }

        public ICollection<CURSO> CURSOS { get; set; }

        public List<ALUMNO> Listar()
        {
            var alumno = new List<ALUMNO>();
            try
            {
                using (var context = new TestContext())
                {
                    alumno = context.ALUMNO.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return alumno;
        }

        public ALUMNO Obtener(int id)
        {
            var alumno = new ALUMNO();
            try
            {
                using (var context = new TestContext())
                {
                    alumno = context.ALUMNO
                                .Include("CURSOS")
                                .Where(x => x.ID == id)
                                .Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return alumno;
        }

        public void Guardar()
        {
            try
            {
                using (var context = new TestContext())
                {
                    if (this.ID == 0)
                    {
                        context.Entry(this).State = EntityState.Added;                        
                    }
                    else
                    {
                        context.Database.ExecuteSqlCommand(
                            "DELETE FROM ALUMNOCURSO WHERE ALUMNO_ID =  @ID", new SqlParameter("ID", this.ID));
                        var cursoBK = this.CURSOS;
                        this.CURSOS = null;
                        context.Entry(this).State = EntityState.Modified;
                        this.CURSOS = cursoBK;
                    }
                    foreach (var c in this.CURSOS)
                        context.Entry(c).State = EntityState.Unchanged;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void Eliminar(int id)
        {
            try
            {
                using (var context = new TestContext())
                {
                    context.Entry(new ALUMNO { ID = id}).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
