namespace Better.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserModels");
        }
    }
}
