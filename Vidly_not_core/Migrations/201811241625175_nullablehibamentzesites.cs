namespace Vidly_not_core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullablehibamentzesites : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomersBirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CustomersBirthDate", c => c.DateTime(nullable: false));
        }
    }
}
