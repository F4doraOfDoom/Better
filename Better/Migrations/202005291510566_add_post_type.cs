namespace Better.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_post_type : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Type");
        }
    }
}
