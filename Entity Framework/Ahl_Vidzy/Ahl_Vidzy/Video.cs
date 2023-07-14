using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahl_Vidzy
{
    public enum Classification : byte
    {
        Silver = 3,
        Gold = 2,
        Platinum = 1
    }
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public Classification Classification { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
