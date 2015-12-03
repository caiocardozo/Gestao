namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterDomains : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        cat_Id = c.Int(nullable: false, identity: true),
                        cat_Nome = c.String(nullable: false, maxLength: 200, unicode: false),
                        data_inclusao = c.DateTime(nullable: false),
                        data_alteracao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.cat_Id);
            
            CreateTable(
                "dbo.Servico",
                c => new
                    {
                        serv_Id = c.Int(nullable: false, identity: true),
                        serv_nome = c.String(maxLength: 500, unicode: false),
                        data_inclusao = c.DateTime(nullable: false),
                        data_alteracao = c.DateTime(nullable: false),
                        Categoria_cat_Id = c.Int(),
                    })
                .PrimaryKey(t => t.serv_Id)
                .ForeignKey("dbo.Categoria", t => t.Categoria_cat_Id)
                .Index(t => t.Categoria_cat_Id);
            
            CreateTable(
                "dbo.Orcamento",
                c => new
                    {
                        orc_Id = c.Int(nullable: false, identity: true),
                        orc_endereco_servico = c.String(maxLength: 500, unicode: false),
                        orc_descricao = c.String(maxLength: 500, unicode: false),
                        orc_dias_prazo = c.Int(nullable: false),
                        orc_frequencia_prazo = c.Int(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                        data_alteracao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.orc_Id);
            
            CreateTable(
                "dbo.Prestador",
                c => new
                    {
                        pres_Id = c.Int(nullable: false, identity: true),
                        pres_nome = c.String(maxLength: 500, unicode: false),
                        pres_cpf_cnpj = c.String(maxLength: 500, unicode: false),
                        pres_endereco = c.String(maxLength: 500, unicode: false),
                        pres_raio_recebimento = c.String(maxLength: 500, unicode: false),
                        pres_email = c.String(maxLength: 500, unicode: false),
                        pres_telefone_residencial = c.String(maxLength: 500, unicode: false),
                        pres_telefone_celular = c.String(maxLength: 500, unicode: false),
                        status = c.Int(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                        data_alteracao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.pres_Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        usu_Id = c.Int(nullable: false, identity: true),
                        usu_endereco = c.String(maxLength: 500, unicode: false),
                        usu_email = c.String(maxLength: 500, unicode: false),
                        data_inclusao = c.DateTime(nullable: false),
                        data_alteracao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.usu_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Servico", "Categoria_cat_Id", "dbo.Categoria");
            DropIndex("dbo.Servico", new[] { "Categoria_cat_Id" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Prestador");
            DropTable("dbo.Orcamento");
            DropTable("dbo.Servico");
            DropTable("dbo.Categoria");
        }
    }
}
