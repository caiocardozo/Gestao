namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class caio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mensagem = c.String(maxLength: 500, unicode: false),
                        Controller = c.String(maxLength: 500, unicode: false),
                        View = c.String(maxLength: 500, unicode: false),
                        data_alteracao = c.DateTime(nullable: false),
                        data_inclusao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Prestador", "cidade", c => c.String(maxLength: 500, unicode: false));
            AddColumn("dbo.Prestador", "estado", c => c.Int(nullable: false));
            AddColumn("dbo.IndiqueProfissional", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Orcamento", "cidade", c => c.String(maxLength: 500, unicode: false));
            AddColumn("dbo.Orcamento", "estado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orcamento", "estado");
            DropColumn("dbo.Orcamento", "cidade");
            DropColumn("dbo.IndiqueProfissional", "Type");
            DropColumn("dbo.Prestador", "estado");
            DropColumn("dbo.Prestador", "cidade");
            DropTable("dbo.Log");
        }
    }
}
