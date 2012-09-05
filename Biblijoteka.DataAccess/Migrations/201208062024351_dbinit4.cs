namespace Biblioteka.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class dbinit4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "BooksCopys", name: "Book_ID", newName: "BookID");
            RenameColumn(table: "BooksCopys", name: "Library_ID", newName: "LibraryID");
        }
        
        public override void Down()
        {
            RenameColumn(table: "BooksCopys", name: "LibraryID", newName: "Library_ID");
            RenameColumn(table: "BooksCopys", name: "BookID", newName: "Book_ID");
        }
    }
}
