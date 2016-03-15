namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class att2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orcamento", "orc_Id", "dbo.Categoria");
            DropForeignKey("dbo.Servico", "cat_Id", "dbo.Categoria");
            DropColumn("dbo.Categoria", "cat_Id");
            RenameColumn(table: "dbo.Categoria", name: "orc_Id", newName: "cat_Id");
            DropPrimaryKey("dbo.Categoria");
            AlterColumn("dbo.Categoria", "cat_Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Categoria", "cat_Id");
            CreateIndex("dbo.Categoria", "cat_Id");
            AddForeignKey("dbo.Servico", "cat_Id", "dbo.Categoria", "cat_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Servico", "cat_Id", "dbo.Categoria");
            DropIndex("dbo.Categoria", new[] { "cat_Id" });
            DropPrimaryKey("dbo.Categoria");
            AlterColumn("dbo.Categoria", "cat_Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Categoria", "cat_Id");
            RenameColumn(table: "dbo.Categoria", name: "cat_Id", newName: "orc_Id");
            AddColumn("dbo.Categoria", "cat_Id", c => c.Int(nullable: false, identity: true));
            AddForeignKey("dbo.Servico", "cat_Id", "dbo.Categoria", "cat_Id");
            AddForeignKey("dbo.Orcamento", "orc_Id", "dbo.Categoria", "cat_Id");
        }
    }
}
