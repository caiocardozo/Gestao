namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class att : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IndiqueProfissional",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Telefone = c.String(nullable: false, maxLength: 20, unicode: false),
                        Email_Empresa = c.String(maxLength: 128, unicode: false),
                        Estado = c.Int(nullable: false),
                        Cidade = c.String(nullable: false, maxLength: 128, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 256, unicode: false),
                        Email_Solicitante = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IndiqueProfissional");
        }
    }
}
