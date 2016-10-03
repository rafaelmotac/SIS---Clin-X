namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Paciente")]
    public partial class Paciente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paciente()
        {
            Consulta = new HashSet<Consulta>();
        }

        [Key]
        public int idPaciente { get; set; }

        [Required]
        [StringLength(255)]
        public string nome { get; set; }

        [Required]
        [StringLength(255)]
        [Index(IsUnique =true)]
        public string cpf { get; set; }

        [Required]
        [StringLength(255)]
        public string telefone { get; set; }

        [Required]
        [StringLength(255)]
        public string endereco { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dtNascimento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consulta> Consulta { get; set; }

        public virtual Convenio Convenio { get; set; }

        public virtual Prontuario Prontuario { get; set; }

    }
}
