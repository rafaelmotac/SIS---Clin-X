namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Convenio")]
    public partial class Convenio
    {
        
        /*[Key]
        public int idConvenio { get; set; }*/

        [Required]
        [StringLength(45)]
        public string numConvenio { get; set; }

        [Required]
        [StringLength(45)]
        public string nomeConvenio { get; set; }
        
        [Key, ForeignKey("Paciente")]
        public int paciente_IdPaciente { get; set; }

        public Paciente Paciente { get; set; }

        
    }
}
