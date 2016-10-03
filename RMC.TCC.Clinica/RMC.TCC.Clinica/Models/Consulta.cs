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
        [Index("UQ_Consulta_Paciente", 1, IsUnique = true)]
        [Index("UQ_Consulta_ProfSaude", 1, IsUnique = true)]
        public int idConsulta { get; set; }

        [Column(TypeName = "date")]
        public DateTime dtConsulta { get; set; }

        [Index("UQ_Consulta_ProfSaude", 2, IsUnique = true)]
        [Required]
        [ForeignKey("ProfSaude")]
        public int profSaude_idProfSaude { get; set; }

        [Index("UQ_Consulta_Paciente", 2, IsUnique = true)]
        [Required]
        [ForeignKey("Paciente")]
        public int paciente_IdPaciente { get; set; }

        public TimeSpan horaConsulta { get; set; }

        public virtual Paciente Paciente { get; set; }

        public virtual ProfSaude ProfSaude { get; set; }
    }
}
