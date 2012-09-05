namespace Biblioteka.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class studentsandborroers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Borroers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        BookCopyID = c.Int(nullable: false),
                        On = c.DateTime(nullable: false),
                        Return = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Students", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("BooksCopys", t => t.BookCopyID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.BookCopyID);
            
        }
        
        public override void Down()
        {
            DropIndex("Borroers", new[] { "BookCopyID" });
            DropIndex("Borroers", new[] { "StudentID" });
            DropForeignKey("Borroers", "BookCopyID", "BooksCopys");
            DropForeignKey("Borroers", "StudentID", "Students");
            DropTable("Borroers");
        }
    }
}
