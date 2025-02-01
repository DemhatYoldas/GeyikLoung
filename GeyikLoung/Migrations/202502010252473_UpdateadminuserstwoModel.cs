namespace GeyikLoung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateadminuserstwoModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminUsers", "Password", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.AdminUsers", "PasswordHash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdminUsers", "PasswordHash", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.AdminUsers", "Password");
        }
    }
}
