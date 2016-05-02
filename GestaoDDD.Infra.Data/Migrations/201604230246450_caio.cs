namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class caio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 200, unicode: false),
                        data_alteracao = c.DateTime(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Servico",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        categoria_id = c.Int(nullable: false),
                        data_alteracao = c.DateTime(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.categoria_id)
                .Index(t => t.categoria_id);
            
            CreateTable(
                "dbo.ComoFunciona",
                c => new
                    {
                        cf_Id = c.Int(nullable: false, identity: true),
                        cf_Ordem = c.Int(nullable: false),
                        cf_Informacao = c.String(nullable: false, maxLength: 500, unicode: false),
                        data_alteracao = c.DateTime(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.cf_Id);
            
            CreateTable(
                "dbo.Contato",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        email = c.String(nullable: false, maxLength: 500, unicode: false),
                        telefone = c.String(nullable: false, maxLength: 500, unicode: false),
                        mensagem = c.String(nullable: false, maxLength: 500, unicode: false),
                        data_alteracao = c.DateTime(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IndiqueProfissional",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nome_profissional = c.String(nullable: false, maxLength: 256, unicode: false),
                        telefone = c.String(nullable: false, maxLength: 20, unicode: false),
                        email_empresa = c.String(maxLength: 128, unicode: false),
                        estado = c.Int(nullable: false),
                        cidade = c.String(nullable: false, maxLength: 128, unicode: false),
                        nome_solicitante = c.String(nullable: false, maxLength: 256, unicode: false),
                        email_solicitante = c.String(nullable: false, maxLength: 128, unicode: false),
                        data_alteracao = c.DateTime(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                        Servico_serv_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Servico", t => t.Servico_serv_Id)
                .Index(t => t.Servico_serv_Id);
            
            CreateTable(
                "dbo.Orcamento",
                c => new
                    {
                        orc_Id = c.Int(nullable: false, identity: true),
                        descricao = c.String(nullable: false, maxLength: 500, unicode: false),
                        endereco = c.String(nullable: false, maxLength: 500, unicode: false),
                        prazo = c.String(nullable: false, maxLength: 500, unicode: false),
                        solicitante = c.String(nullable: false, maxLength: 200, unicode: false),
                        email_solicitante = c.String(nullable: false, maxLength: 200, unicode: false),
                        telefone_solicitante = c.String(nullable: false, maxLength: 500, unicode: false),
                        endereco_solicitante = c.String(maxLength: 200, unicode: false),
                        latitude = c.String(maxLength: 500, unicode: false),
                        servico_id = c.Int(nullable: false),
                        longitude = c.String(maxLength: 500, unicode: false),
                        data_alteracao = c.DateTime(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.orc_Id);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        usu_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        pes_nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        pes_cpf = c.String(nullable: false, maxLength: 11, unicode: false),
                        pes_rg = c.String(maxLength: 9, unicode: false),
                        pes_endereco = c.String(maxLength: 200, unicode: false),
                        pes_numero = c.Int(nullable: false),
                        pes_bairro = c.String(maxLength: 100, unicode: false),
                        pes_cidade = c.String(maxLength: 100, unicode: false),
                        pes_cep = c.String(maxLength: 9, unicode: false),
                        data_alteracao = c.DateTime(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.usu_id)
                .ForeignKey("dbo.AspNetUsers", t => t.usu_id)
                .Index(t => t.usu_id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, unicode: false),
                        Email = c.String(nullable: false, maxLength: 256, unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 500, unicode: false),
                        SecurityStamp = c.String(maxLength: 500, unicode: false),
                        PhoneNumber = c.String(maxLength: 500, unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prestador",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        cpf_cnpj = c.String(nullable: false, maxLength: 14, unicode: false),
                        endereco = c.String(maxLength: 200, unicode: false),
                        raio = c.String(maxLength: 500, unicode: false),
                        email = c.String(maxLength: 100, unicode: false),
                        telefone_fixo = c.String(maxLength: 14, unicode: false),
                        celular = c.String(maxLength: 14, unicode: false),
                        status = c.Int(nullable: false),
                        data_alteracao = c.DateTime(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                        Usuario_Id = c.String(maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.ServicoPrestador",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        data_alteracao = c.DateTime(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                        prestador_Id_pres_Id = c.Int(),
                        servico_Id_serv_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prestador", t => t.prestador_Id_pres_Id)
                .ForeignKey("dbo.Servico", t => t.servico_Id_serv_Id)
                .Index(t => t.prestador_Id_pres_Id)
                .Index(t => t.servico_Id_serv_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServicoPrestador", "servico_Id_serv_Id", "dbo.Servico");
            DropForeignKey("dbo.ServicoPrestador", "prestador_Id_pres_Id", "dbo.Prestador");
            DropForeignKey("dbo.Prestador", "Usuario_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pessoa", "usu_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.IndiqueProfissional", "Servico_serv_Id", "dbo.Servico");
            DropForeignKey("dbo.Servico", "categoria_id", "dbo.Categoria");
            DropIndex("dbo.ServicoPrestador", new[] { "servico_Id_serv_Id" });
            DropIndex("dbo.ServicoPrestador", new[] { "prestador_Id_pres_Id" });
            DropIndex("dbo.Prestador", new[] { "Usuario_Id" });
            DropIndex("dbo.Pessoa", new[] { "usu_id" });
            DropIndex("dbo.IndiqueProfissional", new[] { "Servico_serv_Id" });
            DropIndex("dbo.Servico", new[] { "categoria_id" });
            DropTable("dbo.ServicoPrestador");
            DropTable("dbo.Prestador");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Orcamento");
            DropTable("dbo.IndiqueProfissional");
            DropTable("dbo.Contato");
            DropTable("dbo.ComoFunciona");
            DropTable("dbo.Servico");
            DropTable("dbo.Categoria");
        }
    }
}
