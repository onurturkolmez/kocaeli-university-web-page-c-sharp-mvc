namespace YazLab1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "tip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "tip");
        }
    }
}
