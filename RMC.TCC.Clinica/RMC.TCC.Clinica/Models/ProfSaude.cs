namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProfSaude")]
    public partial class ProfSaude
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProfSaude()
        {
            Consulta = new List<Consulta>();                      
        }

        [Key]
        public int idProfSaude { get; set; }

        [Required(ErrorMessage = "O campo Nome � obrigat�rio")]
        [DisplayName("Nome Prof.Saude")]
        [StringLength(45,ErrorMessage = "O campo Nome deve conter no m�ximo 45 caracteres")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo CPF � obrigat�rio")]
        [DisplayName("CPF")]
        [StringLength(11,ErrorMessage = "O campo CPF deve conter no m�ximo 11 caracteres")]
        [Index(IsUnique = true)]
        public string cpf { get; set; }

        [Required(ErrorMessage = "O campo Telefone � obrigat�rio")]
        [DisplayName("Telefone")]
        [StringLength(11, ErrorMessage = "Telefone deve conter no m�nimo 7 caracteres e no m�ximo 12", MinimumLength = 7)]
        public string telefone { get; set; }

        [Required(ErrorMessage = "O campo Endere�o � obrigat�rio")]
        [DisplayName("Endere�o")]
        [StringLength(255, ErrorMessage = "O campo Endere�o deve conter no m�ximo 255 caracteres")]
        public string endereco { get; set; }

        [Required(ErrorMessage = "O campo CRM � obrigat�rio")]
        [DisplayName("CRM")]
        [Range(4,9999999999)]
        [Index(IsUnique =true)]
        public int crm { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "O campo Data de Nascimento � obrigat�rio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dtNascimento { get; set; }
    
        public virtual ICollection<Consulta> Consulta { get; set; }
        
    }
}
