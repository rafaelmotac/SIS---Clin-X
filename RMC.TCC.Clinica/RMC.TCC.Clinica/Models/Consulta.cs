namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consulta")]
    public partial class Consulta
    {
        [Key]
        public int idConsulta { get; set; }

        [Column(TypeName = "date")]
        public DateTime dtConsulta { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dtFim { get; set; }

        public int profSaude_idProfSaude { get; set; }

        public int paciente_IdPaciente { get; set; }

        public TimeSpan horaInicioConsulta { get; set; }

        public TimeSpan? horaFimConsulta { get; set; }

        public virtual Paciente Paciente { get; set; }

        public virtual ProfSaude ProfSaude { get; set; }
    }
}
