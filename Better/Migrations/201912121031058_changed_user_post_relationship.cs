namespace Better.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_user_post_relationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "User_UserId", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "User_UserId" });
            RenameColumn(table: "dbo.Posts", name: "User_UserId", newName: "UserId");
            AlterColumn("dbo.Posts", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "UserId");
            AddForeignKey("dbo.Posts", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "UserId" });
            AlterColumn("dbo.Posts", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Posts", name: "UserId", newName: "User_UserId");
            CreateIndex("dbo.Posts", "User_UserId");
            AddForeignKey("dbo.Posts", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
