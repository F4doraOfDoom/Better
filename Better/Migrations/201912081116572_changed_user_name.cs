namespace Better.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_user_name : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Passowrd = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            DropTable("dbo.UserModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        UserModelId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Passowrd = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserModelId);
            
            DropTable("dbo.Users");
        }
    }
}
