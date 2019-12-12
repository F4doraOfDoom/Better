namespace Better.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_user_psots : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "User_UserId", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "User_UserId" });
            AddColumn("dbo.Users", "PostId", c => c.Byte(nullable: false));
            AddColumn("dbo.Users", "Post_PostId", c => c.Int());
            CreateIndex("dbo.Users", "Post_PostId");
            AddForeignKey("dbo.Users", "Post_PostId", "dbo.Posts", "PostId");
            DropColumn("dbo.Posts", "User_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "User_UserId", c => c.Int());
            DropForeignKey("dbo.Users", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.Users", new[] { "Post_PostId" });
            DropColumn("dbo.Users", "Post_PostId");
            DropColumn("dbo.Users", "PostId");
            CreateIndex("dbo.Posts", "User_UserId");
            AddForeignKey("dbo.Posts", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
