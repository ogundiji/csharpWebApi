namespace WebApiFundamental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adderrorlogger : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ErrorLoggers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Error_message = c.String(unicode: false, storeType: "text"),
                        Error_message_detail = c.String(unicode: false, storeType: "text"),
                        Controller_Name = c.String(maxLength: 50),
                        Error_Logged_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ErrorLoggers");
        }
    }
}
