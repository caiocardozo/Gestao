namespace GestaoDDD.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Usuario", newName: "AspNetUsers");
            DropPrimaryKey("dbo.AspNetUsers");
            AddColumn("dbo.AspNetUsers", "Id", c => c.String(nullable: false, maxLength: 128, unicode: false));
            AddColumn("dbo.AspNetUsers", "Email", c => c.String(nullable: false, maxLength: 256, unicode: false));
            AddColumn("dbo.AspNetUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PasswordHash", c => c.String(maxLength: 500, unicode: false));
            AddColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String(maxLength: 500, unicode: false));
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(maxLength: 500, unicode: false));
            AddColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 256, unicode: false));
            AddPrimaryKey("dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "usu_Id");
            DropColumn("dbo.AspNetUsers", "usu_endereco");
            DropColumn("dbo.AspNetUsers", "usu_email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "usu_email", c => c.String(maxLength: 500, unicode: false));
            AddColumn("dbo.AspNetUsers", "usu_endereco", c => c.String(maxLength: 500, unicode: false));
            AddColumn("dbo.AspNetUsers", "usu_Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.AspNetUsers");
            DropColumn("dbo.AspNetUsers", "UserName");
            DropColumn("dbo.AspNetUsers", "AccessFailedCount");
            DropColumn("dbo.AspNetUsers", "LockoutEnabled");
            DropColumn("dbo.AspNetUsers", "LockoutEndDateUtc");
            DropColumn("dbo.AspNetUsers", "TwoFactorEnabled");
            DropColumn("dbo.AspNetUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "SecurityStamp");
            DropColumn("dbo.AspNetUsers", "PasswordHash");
            DropColumn("dbo.AspNetUsers", "EmailConfirmed");
            DropColumn("dbo.AspNetUsers", "Email");
            DropColumn("dbo.AspNetUsers", "Id");
            AddPrimaryKey("dbo.AspNetUsers", "usu_Id");
            RenameTable(name: "dbo.AspNetUsers", newName: "Usuario");
        }
    }
}
