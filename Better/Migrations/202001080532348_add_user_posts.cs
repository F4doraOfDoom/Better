namespace Better.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_user_posts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Posts", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
        }
    }
}
