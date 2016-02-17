namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attdb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orcamento", "orc_Endereco", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AddColumn("dbo.Orcamento", "orc_numero", c => c.Int(nullable: false));
            AddColumn("dbo.Orcamento", "orc_bairro", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.Orcamento", "orc_cidade", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.Orcamento", "orc_cep", c => c.String(nullable: false, maxLength: 9, unicode: false));
            AddColumn("dbo.Orcamento", "orc_referencia", c => c.String(maxLength: 150, unicode: false));
            DropColumn("dbo.Orcamento", "orc_Endereco_Servico");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orcamento", "orc_Endereco_Servico", c => c.String(maxLength: 200, unicode: false));
            DropColumn("dbo.Orcamento", "orc_referencia");
            DropColumn("dbo.Orcamento", "orc_cep");
            DropColumn("dbo.Orcamento", "orc_cidade");
            DropColumn("dbo.Orcamento", "orc_bairro");
            DropColumn("dbo.Orcamento", "orc_numero");
            DropColumn("dbo.Orcamento", "orc_Endereco");
        }
    }
}
