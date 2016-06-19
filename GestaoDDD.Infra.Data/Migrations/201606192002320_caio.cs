namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class caio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orcamento", "status", c => c.Int(nullable: false));
            DropColumn("dbo.IndiqueProfissional", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IndiqueProfissional", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.Orcamento", "status");
        }
    }
}
