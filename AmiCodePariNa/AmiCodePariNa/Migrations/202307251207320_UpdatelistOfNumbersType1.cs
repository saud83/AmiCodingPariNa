namespace AmiCodePariNa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatelistOfNumbersType1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Khojs", "listOfNumbers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Khojs", "listOfNumbers");
        }
    }
}
