namespace Biblioteka.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class dbInit2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Books", "Library_ID", "Libraries");
            DropIndex("Books", new[] { "Library_ID" });
            DropColumn("Books", "Library_ID");
        }
        
        public override void Down()
        {
            AddColumn("Books", "Library_ID", c => c.Int());
            CreateIndex("Books", "Library_ID");
            AddForeignKey("Books", "Library_ID", "Libraries", "ID");
        }
    }
}
