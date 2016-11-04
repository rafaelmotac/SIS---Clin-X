namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

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

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [DisplayName("Nome Prof.Saude")]
        [StringLength(45,ErrorMessage = "O campo Nome deve conter no máximo 45 caracteres")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [DisplayName("CPF")]
        [StringLength(maximumLength: 11, MinimumLength = 6, ErrorMessage = "O campo CPF deve conter no mínimo 6 e no máximo 11 caracteres")]
        [Index(IsUnique = true)]
        [Remote("verificaCpf", "ProfSaudes", "cpf", AdditionalFields = "idProfSaude", ErrorMessage = "CPF já existe no banco de dados!")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório")]
        [DisplayName("Telefone")]
        [StringLength(11, ErrorMessage = "Telefone deve conter no mínimo 7 caracteres e no máximo 12", MinimumLength = 7)]
        public string telefone { get; set; }

        [Required(ErrorMessage = "O campo Endereço é obrigatório")]
        [DisplayName("Endereço")]
        [StringLength(255, ErrorMessage = "O campo Endereço deve conter no máximo 255 caracteres")]
        public string endereco { get; set; }

        [Required(ErrorMessage = "O campo CRM é obrigatório")]
        [DisplayName("CRM")]
        [Range(4,9999999999)]
        [Index(IsUnique =true)]
        [Remote("verificaCrm", "ProfSaudes", "crm", AdditionalFields = "idProfSaude", ErrorMessage = "CRM já existe no banco de dados!")]
        public int crm { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dtNascimento { get; set; }
    
        public virtual ICollection<Consulta> Consulta { get; set; }
        
    }
}
