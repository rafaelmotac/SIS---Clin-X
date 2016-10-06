namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
        [DisplayName("ID Paciente")]
        public int idPaciente { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(255)]
        [DisplayName("Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [StringLength(11,ErrorMessage = "CPF deve conter no máxmimo 11 caracteres")]
        [Index(IsUnique =true)]
        [DisplayName("CPF")]
        public string cpf { get; set; }

        [StringLength(11,ErrorMessage = "Telefone deve conter no mínimo 7 caracteres e no máximo 12",MinimumLength = 7)]
        [DisplayName("Telefone")]
        public string telefone { get; set; }

        [StringLength(255,ErrorMessage = "Endereço deve conter no máximo 255 caracteres")]
        [DisplayName("Endereço")]
        public string endereco { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Data de Nascimento")]
        public DateTime? dtNascimento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consulta> Consulta { get; set; }

        public virtual Convenio Convenio { get; set; }

        public virtual Prontuario Prontuario { get; set; }

    }
}
