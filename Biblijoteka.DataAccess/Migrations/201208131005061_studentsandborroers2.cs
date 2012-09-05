namespace Biblioteka.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class studentsandborroers2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Borroers", newName: "Borrowers");
        }
        
        public override void Down()
        {
            RenameTable(name: "Borrowers", newName: "Borroers");
        }
    }
}
