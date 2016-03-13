namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizarOrcamento : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Orcamento");
            AddColumn("dbo.Orcamento", "orc_prazo", c => c.String(nullable: false, maxLength: 500, unicode: false));
            AddColumn("dbo.Orcamento", "orc_nome_solicitante", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AddColumn("dbo.Orcamento", "orc_email_solicitante", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AddColumn("dbo.Orcamento", "orc_telefone_solicitante", c => c.String(nullable: false, maxLength: 500, unicode: false));
            AddColumn("dbo.Orcamento", "orc_endereco_solicitante", c => c.String(maxLength: 200, unicode: false));
            AlterColumn("dbo.Orcamento", "orc_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Orcamento", "orc_endereco", c => c.String(nullable: false, maxLength: 500, unicode: false));
            AlterColumn("dbo.Orcamento", "orc_descricao", c => c.String(nullable: false, maxLength: 500, unicode: false));
            AddPrimaryKey("dbo.Orcamento", "orc_Id");
            CreateIndex("dbo.Orcamento", "orc_Id");
            AddForeignKey("dbo.Orcamento", "orc_Id", "dbo.Categoria", "cat_Id");
            AddForeignKey("dbo.Orcamento", "orc_Id", "dbo.Servico", "serv_Id");
            DropColumn("dbo.Orcamento", "orc_numero");
            DropColumn("dbo.Orcamento", "orc_bairro");
            DropColumn("dbo.Orcamento", "orc_cidade");
            DropColumn("dbo.Orcamento", "orc_cep");
            DropColumn("dbo.Orcamento", "orc_referencia");
            DropColumn("dbo.Orcamento", "orc_Dias_Prazo");
            DropColumn("dbo.Orcamento", "orc_Frequencia_Prazo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orcamento", "orc_Frequencia_Prazo", c => c.Int(nullable: false));
            AddColumn("dbo.Orcamento", "orc_Dias_Prazo", c => c.Int(nullable: false));
            AddColumn("dbo.Orcamento", "orc_referencia", c => c.String(maxLength: 150, unicode: false));
            AddColumn("dbo.Orcamento", "orc_cep", c => c.String(nullable: false, maxLength: 9, unicode: false));
            AddColumn("dbo.Orcamento", "orc_cidade", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.Orcamento", "orc_bairro", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.Orcamento", "orc_numero", c => c.Int(nullable: false));
            DropForeignKey("dbo.Orcamento", "orc_Id", "dbo.Servico");
            DropForeignKey("dbo.Orcamento", "orc_Id", "dbo.Categoria");
            DropIndex("dbo.Orcamento", new[] { "orc_Id" });
            DropPrimaryKey("dbo.Orcamento");
            AlterColumn("dbo.Orcamento", "orc_descricao", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.Orcamento", "orc_endereco", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.Orcamento", "orc_Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Orcamento", "orc_endereco_solicitante");
            DropColumn("dbo.Orcamento", "orc_telefone_solicitante");
            DropColumn("dbo.Orcamento", "orc_email_solicitante");
            DropColumn("dbo.Orcamento", "orc_nome_solicitante");
            DropColumn("dbo.Orcamento", "orc_prazo");
            AddPrimaryKey("dbo.Orcamento", "orc_Id");
        }
    }
}
