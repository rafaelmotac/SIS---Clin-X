namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Convenio")]
    public partial class Convenio
    {
        
        /*[Key]
        public int idConvenio { get; set; }*/

        [Required]
        [StringLength(45, ErrorMessage = "N� de Conv�nio deve conter no m�ximo 45 caracteres")]
        [DisplayName("N�. de Conv�nio")]
        [Index("UQ_numConvenio_nomeConvenio", 1, IsUnique = true)]
        public string numConvenio { get; set; }

        [Required]
        [StringLength(45, ErrorMessage = "Nome do Conv�nio deve conter no m�ximo 45 caracteres")]
        [DisplayName("Nome do Conv�nio")]
        [Index("UQ_numConvenio_nomeConvenio", 2, IsUnique = true)]
        public string nomeConvenio { get; set; }
        
        [Key, ForeignKey("Paciente")]
        [DisplayName("ID Paciente")]
        public int paciente_IdPaciente { get; set; }

        public virtual Paciente Paciente { get; set; }

        
    }
}
