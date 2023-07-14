using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahl_Vidzy
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Video> Videos { get; set; }
        public Genre()
        {
            Videos = new List<Video>();
        }
    }
}
