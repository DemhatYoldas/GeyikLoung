namespace GeyikLoung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "AltKategoriId", "dbo.AltKategoris");
            DropIndex("dbo.Products", new[] { "AltKategoriId" });
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Name", c => c.String());
            CreateIndex("dbo.Products", "AltKategoriId");
            AddForeignKey("dbo.Products", "AltKategoriId", "dbo.AltKategoris", "Id");
        }
    }
}
