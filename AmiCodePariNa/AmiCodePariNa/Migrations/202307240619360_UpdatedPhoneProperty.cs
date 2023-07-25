namespace AmiCodePariNa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPhoneProperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Phone", c => c.Int(nullable: false));
        }
    }
}
