namespace VidlyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres (GenreName) VALUES ( 'Romantic')");
            Sql("Insert into Genres (GenreName) VALUES ( 'Action')");
            Sql("Insert into Genres (GenreName) VALUES ( 'Thriller')");
            Sql("Insert into Genres (GenreName) VALUES ( 'Drama')");
        }
        
        public override void Down()
        {
        }
    }
}
