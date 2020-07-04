namespace VidlyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenametoMembershiptype : DbMigration
    {
        public override void Up()
        {
            Sql("Update MemberShipTypes SET Name = 'Pay as you Go' WHERE Id =1");
            Sql("Update MemberShipTypes SET Name = 'Monthly' WHERE Id =2");
            Sql("Update MemberShipTypes SET Name = 'Quarterly' WHERE Id =3");
            Sql("Update MemberShipTypes SET Name = 'Yearly' WHERE Id =4");

        }
        
        public override void Down()
        {
        }
    }
}
