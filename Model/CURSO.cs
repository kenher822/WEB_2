namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("CURSO")]
    public partial class CURSO
    {
        public CURSO()
        {
            ALUMNO = new List<ALUMNO>();
        }
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string NOMBRE { get; set; }

        public ICollection<ALUMNO> ALUMNO { get; set; }

        public  List<CURSO> Todos()
        {
            var cursos = new List<CURSO>();
            try
            {
                using (var context = new TestContext())
                {
                    cursos = context.CURSO.ToList();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return cursos;
        }
    }
}
