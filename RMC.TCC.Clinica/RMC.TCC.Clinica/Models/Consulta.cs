namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Consulta")]
    public partial class Consulta
    {
        [Key]
        [DisplayName("ID da Consulta")]
        public int idConsulta { get; set; }

        [Index("UQ_Consulta_Paciente", 1, IsUnique = true)]
        [Index("UQ_Consulta_ProfSaude", 1, IsUnique = true)]
        [Column(TypeName = "date")]
        [DisplayName("Data da Consulta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "O campo Data da Consulta é obrigatório")]
        public DateTime dtConsulta { get; set; }

        [Index("UQ_Consulta_ProfSaude", 2, IsUnique = true)]
        [Required(ErrorMessage = "O campo Prof.Saude é obrigatório")]
        [ForeignKey("ProfSaude")]
        [Display(Name = "Prof.Saude")]
        [Remote("verificaConsulta", "Consultas", "profSaude_idProfSaude", AdditionalFields = "dtConsulta,horaConsulta,paciente_IdPaciente,idConsulta", ErrorMessage = "ProfSaude Possui consulta")]
        public int profSaude_idProfSaude { get; set; }

        [Index("UQ_Consulta_Paciente", 2, IsUnique = true)]
        [Required(ErrorMessage = "O Campo Paciente é obrigatório")]
        [ForeignKey("Paciente")]
        [Display(Name = "Paciente")]
        public int paciente_IdPaciente { get; set; }

        [Index("UQ_Consulta_Paciente", 3, IsUnique = true)]
        [Index("UQ_Consulta_ProfSaude", 3, IsUnique = true)]
        [Required(ErrorMessage = "O campo Hora da Consulta é obrigatório")]
        [DisplayName("Hora da Consulta")]
        public TimeSpan horaConsulta { get; set; }

        public virtual Paciente Paciente { get; set; }

        public virtual ProfSaude ProfSaude { get; set; }
    }
}
