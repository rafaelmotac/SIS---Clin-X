namespace RMC.TCC.Clinica.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ClinicaDb : DbContext
    {
        public ClinicaDb()
            : base("name=ClinicaDb")
        {
        }

        public virtual DbSet<Consulta> Consulta { get; set; }
        public virtual DbSet<Convenio> Convenio { get; set; }
        public virtual DbSet<Exame> Exame { get; set; }
        public virtual DbSet<Funcionario> Funcionario { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<ProfSaude> ProfSaude { get; set; }
        public virtual DbSet<Prontuario> Prontuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Convenio>()
                .Property(e => e.numConvenio)
                .IsUnicode(false);

            modelBuilder.Entity<Convenio>()
                .Property(e => e.nomeConvenio)
                .IsUnicode(false);

            modelBuilder.Entity<Exame>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Exame>()
                .Property(e => e.descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Exame>()
                .HasMany(e => e.Prontuario)
                .WithMany(e => e.Exame)
                .Map(m => m.ToTable("Prontuario_has_Exame").MapLeftKey("exame_idExame").MapRightKey("prontuario_IdProntuario"));

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.cpf)
                .IsUnicode(false);

            modelBuilder.Entity<Funcionario>()
                .Property(e => e.telefone)
                .IsUnicode(false);

            modelBuilder.Entity<Paciente>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Paciente>()
                .Property(e => e.cpf)
                .IsUnicode(false);

            modelBuilder.Entity<Paciente>()
                .Property(e => e.telefone)
                .IsUnicode(false);

            modelBuilder.Entity<Paciente>()
                .Property(e => e.endereco)
                .IsUnicode(false);

            //modelBuilder.Entity<Paciente>()
              //  .HasMany(e => e.Consulta)
                //.WithRequired(e => e.Paciente)
                //.HasForeignKey(e => e.paciente_IdPaciente);

            //modelBuilder.Entity<Paciente>()
            //.HasMany(e => e.Prontuario)
            //.WithRequired(e => e.Paciente)
            //.HasForeignKey(e => e.paciente_IdPaciente);

            modelBuilder.Entity<ProfSaude>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<ProfSaude>()
                .Property(e => e.cpf)
                .IsUnicode(false);

            modelBuilder.Entity<ProfSaude>()
                .Property(e => e.telefone)
                .IsUnicode(false);

            modelBuilder.Entity<ProfSaude>()
                .Property(e => e.endereco)
                .IsUnicode(false);

            modelBuilder.Entity<ProfSaude>()
                .HasMany(e => e.Consulta)
                .WithRequired(e => e.ProfSaude)
                .HasForeignKey(e => e.profSaude_idProfSaude)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prontuario>()
                .Property(e => e.procedimentos)
                .IsUnicode(false);

            modelBuilder.Entity<Prontuario>()
                .Property(e => e.prescricoes)
                .IsUnicode(false);

            modelBuilder.Entity<Prontuario>()
                .Property(e => e.historico)
                .IsUnicode(false);
        }
    }
}
