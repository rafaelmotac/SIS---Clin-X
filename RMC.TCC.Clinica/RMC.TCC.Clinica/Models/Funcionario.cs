namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Funcionario")]
    public partial class Funcionario
    {
        [Key]
        public int idFuncionario { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Nome do Funcionário")]
        public string nome { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("CPF")]
        public string cpf { get; set; }

        [Required]
        [StringLength(45)]
        [DisplayName("Telefone")]
        public string telefone { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Data de Nascimento")]
        public DateTime dtNascimento { get; set; }
    }
}
