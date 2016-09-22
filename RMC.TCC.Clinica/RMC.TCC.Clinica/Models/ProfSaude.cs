namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProfSaude")]
    public partial class ProfSaude
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProfSaude()
        {
            Consulta = new HashSet<Consulta>();
            Dentista = new HashSet<Dentista>();
            Medico = new HashSet<Medico>();
        }

        [Key]
        public int idProfSaude { get; set; }

        [Required]
        [StringLength(255)]
        public string nome { get; set; }

        [Required]
        [StringLength(255)]
        public string cpf { get; set; }

        [Required]
        [StringLength(255)]
        public string telefone { get; set; }

        [Required]
        [StringLength(255)]
        public string endereco { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consulta> Consulta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dentista> Dentista { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medico> Medico { get; set; }
    }
}
