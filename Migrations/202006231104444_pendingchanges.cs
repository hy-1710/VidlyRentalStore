namespace VidlyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pendingchanges : DbMigration
    {
        public override void Up()
        {
            Sql("Update Customers Set DOB='1962-01-19 00:00:00:000' ");
            AlterColumn("dbo.Customers", "DOB", c => c.DateTime(nullable: false));
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "DOB", c => c.DateTime());
        }
    }
}
