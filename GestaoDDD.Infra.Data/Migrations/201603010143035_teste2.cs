namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        cat_Id = c.Int(nullable: false, identity: true),
                        cat_Nome = c.String(nullable: false, maxLength: 200, unicode: false),
                        data_Inclusao = c.DateTime(nullable: false),
                        data_Alteracao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.cat_Id);
            
            CreateTable(
                "dbo.Servico",
                c => new
                    {
                        serv_Id = c.Int(nullable: false, identity: true),
                        serv_Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        cat_Id = c.Int(nullable: false),
                        data_Inclusao = c.DateTime(nullable: false),
                        data_Alteracao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.serv_Id)
                .ForeignKey("dbo.Categoria", t => t.cat_Id)
                .Index(t => t.cat_Id);
            
            CreateTable(
                "dbo.ComoFunciona",
                c => new
                    {
                        cf_Id = c.Int(nullable: false, identity: true),
                        cf_Ordem = c.Int(nullable: false),
                        cf_Informacao = c.String(nullable: false, maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.cf_Id);
            
            CreateTable(
                "dbo.Orcamento",
                c => new
                    {
                        orc_Id = c.Int(nullable: false, identity: true),
                        orc_Endereco = c.String(nullable: false, maxLength: 200, unicode: false),
                        orc_numero = c.Int(nullable: false),
                        orc_bairro = c.String(nullable: false, maxLength: 100, unicode: false),
                        orc_cidade = c.String(nullable: false, maxLength: 100, unicode: false),
                        orc_cep = c.String(nullable: false, maxLength: 9, unicode: false),
                        orc_referencia = c.String(maxLength: 150, unicode: false),
                        orc_Descricao = c.String(nullable: false, maxLength: 200, unicode: false),
                        orc_Dias_Prazo = c.Int(nullable: false),
                        orc_Frequencia_Prazo = c.Int(nullable: false),
                        data_Inclusao = c.DateTime(nullable: false),
                        data_Alteracao = c.DateTime(nullable: false),
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
                        data_Inclusao = c.DateTime(nullable: false),
                        data_Alteracao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prestador",
                c => new
                    {
                        pres_Id = c.Int(nullable: false, identity: true),
                        pres_Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        pres_Cpf_Cnpj = c.String(nullable: false, maxLength: 14, unicode: false),
                        pres_Endereco = c.String(maxLength: 200, unicode: false),
                        pres_Raio_Recebimento = c.String(maxLength: 500, unicode: false),
                        pres_Email = c.String(maxLength: 100, unicode: false),
                        pres_Telefone_Residencial = c.String(maxLength: 14, unicode: false),
                        pres_Telefone_Celular = c.String(maxLength: 14, unicode: false),
                        status = c.Int(nullable: false),
                        data_Inclusao = c.DateTime(nullable: false),
                        data_Alteracao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.pres_Id);
            
            CreateTable(
                "dbo.ServicoPrestador",
                c => new
                    {
                        serv_Pres_Id = c.Int(nullable: false, identity: true),
                        data_Inclusao = c.DateTime(nullable: false),
                        data_Alteracao = c.DateTime(nullable: false),
                        prestador_Id_pres_Id = c.Int(),
                        servico_Id_serv_Id = c.Int(),
                    })
                .PrimaryKey(t => t.serv_Pres_Id)
                .ForeignKey("dbo.Prestador", t => t.prestador_Id_pres_Id)
                .ForeignKey("dbo.Servico", t => t.servico_Id_serv_Id)
                .Index(t => t.prestador_Id_pres_Id)
                .Index(t => t.servico_Id_serv_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServicoPrestador", "servico_Id_serv_Id", "dbo.Servico");
            DropForeignKey("dbo.ServicoPrestador", "prestador_Id_pres_Id", "dbo.Prestador");
            DropForeignKey("dbo.Pessoa", "usu_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Servico", "cat_Id", "dbo.Categoria");
            DropIndex("dbo.ServicoPrestador", new[] { "servico_Id_serv_Id" });
            DropIndex("dbo.ServicoPrestador", new[] { "prestador_Id_pres_Id" });
            DropIndex("dbo.Pessoa", new[] { "usu_id" });
            DropIndex("dbo.Servico", new[] { "cat_Id" });
            DropTable("dbo.ServicoPrestador");
            DropTable("dbo.Prestador");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Orcamento");
            DropTable("dbo.ComoFunciona");
            DropTable("dbo.Servico");
            DropTable("dbo.Categoria");
        }
    }
}
