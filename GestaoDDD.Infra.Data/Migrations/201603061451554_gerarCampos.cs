namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gerarCampos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orcamento", "orc_latitude", c => c.String(maxLength: 500, unicode: false));
            AddColumn("dbo.Orcamento", "orc_longitude", c => c.String(maxLength: 500, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orcamento", "orc_longitude");
            DropColumn("dbo.Orcamento", "orc_latitude");
        }
    }
}
