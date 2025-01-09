namespace GeyikLoung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ProductId", c => c.Int(nullable: false));
        }
    }
}
