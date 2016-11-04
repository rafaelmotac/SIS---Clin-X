namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

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

        [Required(ErrorMessage = "O campo Nome � obrigat�rio")]
        [StringLength(255)]
        [DisplayName("Nome Paciente")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo CPF � obrigat�rio")]
        [StringLength(maximumLength: 11, MinimumLength = 6, ErrorMessage = "O campo CPF deve conter no m�nimo 6 e no m�ximo 11 caracteres")]
        [Index(IsUnique =true)]
        [Remote("verificaCpf","Pacientes","cpf",AdditionalFields = "idPaciente",ErrorMessage = "CPF j� existe no banco de dados!")]
        [DisplayName("CPF")]
        public string cpf { get; set; }

        [StringLength(11,ErrorMessage = "Telefone deve conter no m�nimo 7 caracteres e no m�ximo 12",MinimumLength = 7)]
        [DisplayName("Telefone")]
        public string telefone { get; set; }

        [StringLength(255,ErrorMessage = "Endere�o deve conter no m�ximo 255 caracteres")]
        [DisplayName("Endere�o")]
        public string endereco { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dtNascimento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consulta> Consulta { get; set; }

        public virtual Convenio Convenio { get; set; }

        public virtual Prontuario Prontuario { get; set; }

    }
}
