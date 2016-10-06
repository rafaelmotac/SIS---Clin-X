namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Exame")]
    public partial class Exame
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exame()
        {
            Prontuario = new HashSet<Prontuario>();
        }

        [Key]
        [DisplayName("ID Exame")]
        public int idExame { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Nome Exame")]
        public string nome { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [DisplayName("Desc. Exame")]
        public string descricao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prontuario> Prontuario { get; set; }
    }
}
