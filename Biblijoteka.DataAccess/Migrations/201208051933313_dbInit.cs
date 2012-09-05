namespace Biblioteka.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class dbInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        ISBN10 = c.String(maxLength: 10),
                        ISBN13 = c.String(maxLength: 14),
                        Edition = c.Int(nullable: false),
                        PublicationDate = c.DateTime(),
                        Description = c.String(maxLength: 250),
                        AuthorID = c.Int(nullable: false),
                        Library_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Auhtors", t => t.AuthorID, cascadeDelete: true)
                .ForeignKey("Libraries", t => t.Library_ID)
                .Index(t => t.AuthorID)
                .Index(t => t.Library_ID);
            
            CreateTable(
                "Auhtors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Surname = c.String(nullable: false, maxLength: 20),
                        Title = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BooksCopys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Copies = c.Int(nullable: false),
                        Book_ID = c.Int(nullable: false),
                        Library_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Books", t => t.Book_ID, cascadeDelete: true)
                .ForeignKey("Libraries", t => t.Library_ID, cascadeDelete: true)
                .Index(t => t.Book_ID)
                .Index(t => t.Library_ID);
            
            CreateTable(
                "Libraries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Descriptions = c.String(nullable: false),
                        Location = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        LibraryID = c.Int(nullable: false),
                        Position = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Libraries", t => t.LibraryID, cascadeDelete: true)
                .Index(t => t.LibraryID);
            
            CreateTable(
                "Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Surname = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropIndex("Users", new[] { "LibraryID" });
            DropIndex("BooksCopys", new[] { "Library_ID" });
            DropIndex("BooksCopys", new[] { "Book_ID" });
            DropIndex("Books", new[] { "Library_ID" });
            DropIndex("Books", new[] { "AuthorID" });
            DropForeignKey("Users", "LibraryID", "Libraries");
            DropForeignKey("BooksCopys", "Library_ID", "Libraries");
            DropForeignKey("BooksCopys", "Book_ID", "Books");
            DropForeignKey("Books", "Library_ID", "Libraries");
            DropForeignKey("Books", "AuthorID", "Auhtors");
            DropTable("Students");
            DropTable("Users");
            DropTable("Libraries");
            DropTable("BooksCopys");
            DropTable("Auhtors");
            DropTable("Books");
        }
    }
}
