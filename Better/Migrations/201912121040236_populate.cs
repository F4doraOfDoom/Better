namespace Better.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populate : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Users (Username, Passowrd, Email) VALUES ('Yoni', 'Gay Nigga', 'gay@lol.xd')");
            Sql("INSERT INTO Users (Username, Passowrd, Email) VALUES ('Omer', 'PasswordOP', 'omer@5000.4')");
            Sql("INSERT INTO Users (Username, Passowrd, Email) VALUES ('Elay', '1F8', 'blabla@gmail.com')");
        }
        
        public override void Down()
        {
        }
    }
}
