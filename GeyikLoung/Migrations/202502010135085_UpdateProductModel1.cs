namespace GeyikLoung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductModel1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Products", "AltKategoriId");
            AddForeignKey("dbo.Products", "AltKategoriId", "dbo.AltKategoris", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "AltKategoriId", "dbo.AltKategoris");
            DropIndex("dbo.Products", new[] { "AltKategoriId" });
        }
    }
}
