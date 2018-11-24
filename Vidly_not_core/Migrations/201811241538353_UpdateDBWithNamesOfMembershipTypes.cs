namespace Vidly_not_core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDBWithNamesOfMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Pay as you go' WHERE MembershipTypeId = 1");
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Monthly' WHERE MembershipTypeId = 2");
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Quaterly' WHERE MembershipTypeId = 3");
            Sql("UPDATE MembershipTypes SET MembershipTypeName = 'Yearly' WHERE MembershipTypeId = 4");
        }
        
        public override void Down()
        {
        }
    }
}
