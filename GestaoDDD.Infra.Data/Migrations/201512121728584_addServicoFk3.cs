namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addServicoFk3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Servico", "Categoria_cat_Id", "dbo.Categoria");
            DropIndex("dbo.Servico", new[] { "Categoria_cat_Id" });
            DropColumn("dbo.Servico", "categoria_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Servico", "Categoria_cat_Id", c => c.Int());
            CreateIndex("dbo.Servico", "Categoria_cat_Id");
            AddForeignKey("dbo.Servico", "Categoria_cat_Id", "dbo.Categoria", "cat_Id");
        }
    }
}
