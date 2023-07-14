namespace Ahl_Vidzy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClassificationToVideosTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Classification", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "Classification");
        }
    }
}
