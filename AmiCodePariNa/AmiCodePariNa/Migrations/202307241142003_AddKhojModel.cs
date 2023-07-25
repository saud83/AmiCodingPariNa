namespace AmiCodePariNa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKhojModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Khojs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Khojs", "UserId", "dbo.Users");
            DropIndex("dbo.Khojs", new[] { "UserId" });
            DropTable("dbo.Khojs");
        }
    }
}
