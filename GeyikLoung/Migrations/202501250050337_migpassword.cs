namespace GeyikLoung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migpassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "PasswordHash", c => c.String(nullable: false));
            AddColumn("dbo.Admins", "PasswordSalt", c => c.String());
            AddColumn("dbo.Admins", "Role", c => c.String());
            AlterColumn("dbo.Admins", "UserName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Admins", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "Password", c => c.String());
            AlterColumn("dbo.Admins", "UserName", c => c.String());
            DropColumn("dbo.Admins", "Role");
            DropColumn("dbo.Admins", "PasswordSalt");
            DropColumn("dbo.Admins", "PasswordHash");
        }
    }
}
