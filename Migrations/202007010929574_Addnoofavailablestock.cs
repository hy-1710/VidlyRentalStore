namespace VidlyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addnoofavailablestock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NoofAvailableStock", c => c.Byte(nullable: false));
            Sql("Update Movies Set NoinStock = NoofAvailableStock  ");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NoofAvailableStock");
        }
    }
}
