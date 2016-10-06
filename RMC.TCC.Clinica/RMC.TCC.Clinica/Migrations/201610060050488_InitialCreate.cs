namespace RMC.TCC.Clinica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consulta",
                c => new
                    {
                        idConsulta = c.Int(nullable: false, identity: true),
                        dtConsulta = c.DateTime(nullable: false, storeType: "date"),
                        profSaude_idProfSaude = c.Int(nullable: false),
                        paciente_IdPaciente = c.Int(nullable: false),
                        horaConsulta = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.idConsulta)
                .ForeignKey("dbo.Paciente", t => t.paciente_IdPaciente, cascadeDelete: true)
                .ForeignKey("dbo.ProfSaude", t => t.profSaude_idProfSaude)
                .Index(t => new { t.idConsulta, t.paciente_IdPaciente }, unique: true, name: "UQ_Consulta_Paciente")
                .Index(t => new { t.idConsulta, t.profSaude_idProfSaude }, unique: true, name: "UQ_Consulta_ProfSaude");
            
            CreateTable(
                "dbo.Paciente",
                c => new
                    {
                        idPaciente = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 255, unicode: false),
                        cpf = c.String(nullable: false, maxLength: 11, unicode: false),
                        telefone = c.String(maxLength: 11, unicode: false),
                        endereco = c.String(maxLength: 255, unicode: false),
                        dtNascimento = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.idPaciente)
                .Index(t => t.cpf, unique: true);
            
            CreateTable(
                "dbo.Convenio",
                c => new
                    {
                        paciente_IdPaciente = c.Int(nullable: false),
                        numConvenio = c.String(nullable: false, maxLength: 45, unicode: false),
                        nomeConvenio = c.String(nullable: false, maxLength: 45, unicode: false),
                    })
                .PrimaryKey(t => t.paciente_IdPaciente)
                .ForeignKey("dbo.Paciente", t => t.paciente_IdPaciente)
                .Index(t => t.paciente_IdPaciente)
                .Index(t => new { t.numConvenio, t.nomeConvenio }, unique: true, name: "UQ_numConvenio_nomeConvenio");
            
            CreateTable(
                "dbo.Prontuario",
                c => new
                    {
                        paciente_IdPaciente = c.Int(nullable: false),
                        procedimentos = c.String(unicode: false, storeType: "text"),
                        prescricoes = c.String(unicode: false, storeType: "text"),
                        historico = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.paciente_IdPaciente)
                .ForeignKey("dbo.Paciente", t => t.paciente_IdPaciente)
                .Index(t => t.paciente_IdPaciente);
            
            CreateTable(
                "dbo.Exame",
                c => new
                    {
                        idExame = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 255, unicode: false),
                        descricao = c.String(nullable: false, unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.idExame);
            
            CreateTable(
                "dbo.ProfSaude",
                c => new
                    {
                        idProfSaude = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 45, unicode: false),
                        cpf = c.String(nullable: false, maxLength: 11, unicode: false),
                        telefone = c.String(nullable: false, maxLength: 11, unicode: false),
                        endereco = c.String(nullable: false, maxLength: 255, unicode: false),
                        crm = c.Int(nullable: false),
                        dtNascimento = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.idProfSaude)
                .Index(t => t.cpf, unique: true)
                .Index(t => t.crm, unique: true);
            
            CreateTable(
                "dbo.Funcionario",
                c => new
                    {
                        idFuncionario = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 45, unicode: false),
                        cpf = c.String(nullable: false, maxLength: 11, unicode: false),
                        telefone = c.String(nullable: false, maxLength: 11, unicode: false),
                        dtNascimento = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.idFuncionario)
                .Index(t => t.cpf, unique: true);
            
            CreateTable(
                "dbo.Prontuario_has_Exame",
                c => new
                    {
                        exame_idExame = c.Int(nullable: false),
                        prontuario_IdProntuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.exame_idExame, t.prontuario_IdProntuario })
                .ForeignKey("dbo.Exame", t => t.exame_idExame, cascadeDelete: true)
                .ForeignKey("dbo.Prontuario", t => t.prontuario_IdProntuario, cascadeDelete: true)
                .Index(t => t.exame_idExame)
                .Index(t => t.prontuario_IdProntuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Consulta", "profSaude_idProfSaude", "dbo.ProfSaude");
            DropForeignKey("dbo.Consulta", "paciente_IdPaciente", "dbo.Paciente");
            DropForeignKey("dbo.Prontuario", "paciente_IdPaciente", "dbo.Paciente");
            DropForeignKey("dbo.Prontuario_has_Exame", "prontuario_IdProntuario", "dbo.Prontuario");
            DropForeignKey("dbo.Prontuario_has_Exame", "exame_idExame", "dbo.Exame");
            DropForeignKey("dbo.Convenio", "paciente_IdPaciente", "dbo.Paciente");
            DropIndex("dbo.Prontuario_has_Exame", new[] { "prontuario_IdProntuario" });
            DropIndex("dbo.Prontuario_has_Exame", new[] { "exame_idExame" });
            DropIndex("dbo.Funcionario", new[] { "cpf" });
            DropIndex("dbo.ProfSaude", new[] { "crm" });
            DropIndex("dbo.ProfSaude", new[] { "cpf" });
            DropIndex("dbo.Prontuario", new[] { "paciente_IdPaciente" });
            DropIndex("dbo.Convenio", "UQ_numConvenio_nomeConvenio");
            DropIndex("dbo.Convenio", new[] { "paciente_IdPaciente" });
            DropIndex("dbo.Paciente", new[] { "cpf" });
            DropIndex("dbo.Consulta", "UQ_Consulta_ProfSaude");
            DropIndex("dbo.Consulta", "UQ_Consulta_Paciente");
            DropTable("dbo.Prontuario_has_Exame");
            DropTable("dbo.Funcionario");
            DropTable("dbo.ProfSaude");
            DropTable("dbo.Exame");
            DropTable("dbo.Prontuario");
            DropTable("dbo.Convenio");
            DropTable("dbo.Paciente");
            DropTable("dbo.Consulta");
        }
    }
}
