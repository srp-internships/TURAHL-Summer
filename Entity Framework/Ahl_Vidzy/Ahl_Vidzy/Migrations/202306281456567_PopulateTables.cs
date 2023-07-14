namespace Ahl_Vidzy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTables : DbMigration
    {
        public override void Up()
        {
            Sql(@"Insert into Genres (Name)
                Values
                ('Comedy'),
                ('Drama'),
                ('Hayoti'),
                ('Tragedy'),
                ('Thriller'),
                ('Trailer'),
                ('Opera'),
                ('Series')
                ");

            Sql(@"
                INSERT INTO Videos (Name, ReleaseDate)
                VALUES 
                	('Skibidi', 2021-07-06), 
                	('Farcry', 2021-07-06), 
                	('Trillir', 2021-07-06), 
                	('Cyberpunc2047', 2021-07-06), 
                	('Operabalet', 2021-07-06), 
                	('Klon', 2021-07-06),
                    ('Klown', 2021-07-06)
                ");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM GENRES WHERE ID BETWEEN 1 AND 8");
            Sql("DELETE FROM VIDEOS WHERE ID BETWEEN 1 AND 7");
        }
    }
}
