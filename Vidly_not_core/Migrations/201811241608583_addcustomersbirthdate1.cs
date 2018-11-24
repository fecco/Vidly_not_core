namespace Vidly_not_core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcustomersbirthdate1 : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE Customers ALTER COLUMN CustomersBirthDate DateTime NULL");
        }
        
        public override void Down()
        {
        }
    }
}
