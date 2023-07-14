using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahl_Vidzy.EntityConfiguration
{
    public class VideoConfiguration : EntityTypeConfiguration<Video>
    {
        public VideoConfiguration()
        {
            HasRequired(v => v.Genre)
            .WithMany(g => g.Videos)
            .HasForeignKey(g => g.GenreId);

            Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(255);

            HasMany(v => v.Tags)
            .WithMany(g => g.Videos)
            .Map(m => {
                m.ToTable("VideoTags");
                m.MapLeftKey("VideoId");
                m.MapRightKey("TagId");
            });
        }
    }
}
