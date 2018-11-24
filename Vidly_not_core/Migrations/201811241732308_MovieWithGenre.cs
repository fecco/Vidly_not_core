namespace Vidly_not_core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieWithGenre : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        MovieGenreId = c.Byte(nullable: false),
                        MovieGenreName = c.String(),
                    })
                .PrimaryKey(t => t.MovieGenreId);

            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "Stock", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "MovieGenreId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Movies", "MovieGenreId");
            AddForeignKey("dbo.Movies", "MovieGenreId", "dbo.MovieGenres", "MovieGenreId", cascadeDelete: true);

            Sql("INSERT INTO MovieGenres (MovieGenreId, MovieGenreName) VALUES (1, 'Comedy')");
            Sql("INSERT INTO MovieGenres (MovieGenreId, MovieGenreName) VALUES (2, 'Action')");
            Sql("INSERT INTO MovieGenres (MovieGenreId, MovieGenreName) VALUES (3, 'Family')");
            Sql("INSERT INTO MovieGenres (MovieGenreId, MovieGenreName) VALUES (4, 'Romance')");
            Sql("INSERT INTO MovieGenres (MovieGenreId, MovieGenreName) VALUES (5, 'Mafia')");

            Sql("INSERT INTO Movies (Name, ReleaseDate, DateAdded, Stock, MovieGenreId) VALUES ('The Terminator', '1992.10.11', '2018.11.24', 10, 2)");
            Sql("INSERT INTO Movies (Name, ReleaseDate, DateAdded, Stock, MovieGenreId) VALUES ('The Godfather', '1972.11.22', '2018.11.23', 50, 5)");
            Sql("INSERT INTO Movies (Name, ReleaseDate, DateAdded, Stock, MovieGenreId) VALUES ('Hangover', '2000.11.22', '2018.11.24', 11, 1)");
            Sql("INSERT INTO Movies (Name, ReleaseDate, DateAdded, Stock, MovieGenreId) VALUES ('Die Hard 3.', '2010.11.22', '2018.11.24', 12, 2)");
            Sql("INSERT INTO Movies (Name, ReleaseDate, DateAdded, Stock, MovieGenreId) VALUES ('Titanic', '2011.11.22', '2018.11.24', 13, 4)");
            Sql("INSERT INTO Movies (Name, ReleaseDate, DateAdded, Stock, MovieGenreId) VALUES ('Toy Story', '2012.11.22', '2018.11.24', 14, 3)");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "MovieGenreId", "dbo.MovieGenres");
            DropIndex("dbo.Movies", new[] { "MovieGenreId" });
            AlterColumn("dbo.Movies", "Name", c => c.String());
            DropColumn("dbo.Movies", "MovieGenreId");
            DropColumn("dbo.Movies", "Stock");
            DropColumn("dbo.Movies", "DateAdded");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropTable("dbo.MovieGenres");
        }
    }
}
