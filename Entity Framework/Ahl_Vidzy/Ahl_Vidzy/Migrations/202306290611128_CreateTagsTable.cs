namespace Ahl_Vidzy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTagsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.VideoTags",
                c => new
                    {
                        VideoId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VideoId, t.TagId })
                .ForeignKey("dbo.Videos", t => t.VideoId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.VideoId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.VideoTags", "VideoId", "dbo.Videos");
            DropIndex("dbo.VideoTags", new[] { "TagId" });
            DropIndex("dbo.VideoTags", new[] { "VideoId" });
            DropTable("dbo.VideoTags");
            DropTable("dbo.Tags");
        }
    }
}
