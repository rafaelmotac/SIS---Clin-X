namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Funcionario")]
    public partial class Funcionario
    {
        [Key]
        [DisplayName("ID Funcion�rio")]
        public int idFuncionario { get; set; }

        [Required(ErrorMessage = "O campo Nome � obrigat�rio")]
        [DisplayName("Nome Funcionario")]
        [StringLength(45, ErrorMessage = "O campo Nome deve conter no m�ximo 45 caracteres")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo CPF � obrigat�rio")]
        [DisplayName("CPF")]
        [StringLength(11, ErrorMessage = "O campo CPF deve conter no m�ximo 11 caracteres")]
        [Index(IsUnique = true)]
        [Remote("verificaCpf", "Funcionarios", "cpf", AdditionalFields = "idFuncionario", ErrorMessage = "CPF j� existe no banco de dados!")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "O campo Telefone � obrigat�rio")]
        [DisplayName("Telefone")]
        [StringLength(11, ErrorMessage = "Telefone deve conter no m�nimo 7 caracteres e no m�ximo 12", MinimumLength = 7)]
        public string telefone { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "O campo Data de Nascimento � obrigat�rio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dtNascimento { get; set; }
    }
}
