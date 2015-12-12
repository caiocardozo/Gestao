namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addServicoFk4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Servico", "categoria_id_cat_Id", c => c.Int());
            CreateIndex("dbo.Servico", "categoria_id_cat_Id");
            AddForeignKey("dbo.Servico", "categoria_id_cat_Id", "dbo.Categoria", "cat_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Servico", "categoria_id_cat_Id", "dbo.Categoria");
            DropIndex("dbo.Servico", new[] { "categoria_id_cat_Id" });
            DropColumn("dbo.Servico", "categoria_id_cat_Id");
        }
    }
}
