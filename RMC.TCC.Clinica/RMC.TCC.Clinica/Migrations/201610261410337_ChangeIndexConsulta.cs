namespace RMC.TCC.Clinica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIndexConsulta : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Consulta", "UQ_Consulta_Paciente");
            DropIndex("dbo.Consulta", "UQ_Consulta_ProfSaude");
            CreateIndex("dbo.Consulta", new[] { "dtConsulta", "paciente_IdPaciente", "horaConsulta" }, unique: true, name: "UQ_Consulta_Paciente");
            CreateIndex("dbo.Consulta", new[] { "dtConsulta", "profSaude_idProfSaude", "horaConsulta" }, unique: true, name: "UQ_Consulta_ProfSaude");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Consulta", "UQ_Consulta_ProfSaude");
            DropIndex("dbo.Consulta", "UQ_Consulta_Paciente");
            CreateIndex("dbo.Consulta", new[] { "idConsulta", "profSaude_idProfSaude" }, unique: true, name: "UQ_Consulta_ProfSaude");
            CreateIndex("dbo.Consulta", new[] { "idConsulta", "paciente_IdPaciente" }, unique: true, name: "UQ_Consulta_Paciente");
        }
    }
}
