namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consulta")]
    public partial class Consulta
    {
        [Key]
        [Index("UQ_Consulta_Paciente", 1, IsUnique = true)]
        [Index("UQ_Consulta_ProfSaude", 1, IsUnique = true)]
        [DisplayName("ID da Consulta")]
        public int idConsulta { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Data da Consulta")]
        [Required(ErrorMessage = "O campo Data de Consulta é obrigatório")]
        public DateTime dtConsulta { get; set; }

        [Index("UQ_Consulta_ProfSaude", 2, IsUnique = true)]
        [Required]
        [ForeignKey("ProfSaude")]
        public int profSaude_idProfSaude { get; set; }

        [Index("UQ_Consulta_Paciente", 2, IsUnique = true)]
        [Required]
        [ForeignKey("Paciente")]
        public int paciente_IdPaciente { get; set; }

        [Required(ErrorMessage = "O campo Hora da Consulta é obrigatório")]
        [DisplayName("Hora da Consulta")]
        public TimeSpan horaConsulta { get; set; }

        public virtual Paciente Paciente { get; set; }

        public virtual ProfSaude ProfSaude { get; set; }
    }
}
