namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prontuario")]
    public partial class Prontuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prontuario()
        {
            Exame = new HashSet<Exame>();
        }

        [Key]
        [DisplayName("ID do Prontuário")]
        public int idProntuario { get; set; }

        [Column(TypeName = "text")]       
        [DisplayName("Procedimentos")]
        public string procedimentos { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Prescrições")]
        public string prescricoes { get; set; }

        [DisplayName("ID do Paciente")]
        [Index(IsUnique = true)]
        public int paciente_IdPaciente { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Histórico do Prontuário")]
        public string historico { get; set; }

        public virtual Paciente Paciente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exame> Exame { get; set; }
    }
}
