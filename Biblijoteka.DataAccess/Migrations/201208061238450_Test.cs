namespace Biblioteka.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("Books", "Publisher", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("Books", "Publisher");
        }
    }
}
