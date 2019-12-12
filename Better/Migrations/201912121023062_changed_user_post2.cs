namespace Better.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_user_post2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.Users", new[] { "Post_PostId" });
            AddColumn("dbo.Posts", "User_UserId", c => c.Int());
            CreateIndex("dbo.Posts", "User_UserId");
            AddForeignKey("dbo.Posts", "User_UserId", "dbo.Users", "UserId");
            DropColumn("dbo.Users", "PostId");
            DropColumn("dbo.Users", "Post_PostId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Post_PostId", c => c.Int());
            AddColumn("dbo.Users", "PostId", c => c.Byte(nullable: false));
            DropForeignKey("dbo.Posts", "User_UserId", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "User_UserId" });
            DropColumn("dbo.Posts", "User_UserId");
            CreateIndex("dbo.Users", "Post_PostId");
            AddForeignKey("dbo.Users", "Post_PostId", "dbo.Posts", "PostId");
        }
    }
}
