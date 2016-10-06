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
        [DisplayName("ID Funcionário")]
        public int idFuncionario { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [DisplayName("Nome")]
        [StringLength(45, ErrorMessage = "O campo Nome deve conter no máximo 45 caracteres")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [DisplayName("CPF")]
        [StringLength(11, ErrorMessage = "O campo CPF deve conter no máximo 11 caracteres")]
        [Index(IsUnique = true)]
        public string cpf { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório")]
        [DisplayName("Telefone")]
        [StringLength(11, ErrorMessage = "O campo Telefone deve conter no máximo 11 caracteres")]
        public string telefone { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório")]
        public DateTime dtNascimento { get; set; }
    }
}
