namespace Better.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_creationdate_post : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "CreationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Posts", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Posts", "Content", c => c.String());
            DropColumn("dbo.Posts", "CreationDate");
        }
    }
}
