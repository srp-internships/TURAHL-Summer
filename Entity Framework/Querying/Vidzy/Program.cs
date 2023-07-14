

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vidzy
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            //VidzyContext context = new VidzyContext();
            //var videos = context.Videos.Where(v => v.Genre.Name == "Action").OrderBy(v=>v.Name);

            //foreach ( var video in videos ) { Console.WriteLine(video.Name); }


            //2
            //using (var context = new VidzyContext())
            //{
            //    var videos = context.Videos
            //        .Where(v => v.Classification == (Classification)2 && v.Genre.Name.Equals("drama"))
            //        .OrderBy(v => v.ReleaseDate);

            //    foreach (var video in videos) { Console.WriteLine(video.Name); }
            //}


            //3
            //using (var context = new VidzyContext())
            //{
            //    var videos = context.Videos
            //        .Select(v => new { MovieName = v.Name, Genre = v.Genre.Name });

            //    foreach (var video in videos)
            //    {
            //        Console.WriteLine("{0}{1}",video.MovieName,video.Genre);
            //    }
            //}


            //4
            //using (var context = new VidzyContext())
            //{
            //    var groups = context.Videos
            //        .GroupBy(v => v.Classification)
            //        .Select(g => new
            //        {
            //            Classification = g.Key,
            //            Movies = g.ToList()
            //        }); ;


            //    foreach (var group in groups)
            //    {
            //        Console.WriteLine("Classification : " + group.Classification);
            //        foreach (var item in group.Movies)
            //        {
            //            Console.WriteLine('\t' + item.Name);
            //        }
            //    }
            //}

            //using (var context = new VidzyContext())
            //{
            //    var groups = context.Videos
            //        .GroupBy(v => v.Classification.ToString())
            //        .Select(g => new
            //        {
            //            Key = g.Key,
            //            Count = g.Count()
            //        });

            //    foreach(var group in groups)
            //    {
            //        Console.WriteLine("{0}({1})", group.Key, group.Count);
            //    }
            //}


            using (var context = new VidzyContext())
            {
                var groups = context.Videos
                    .GroupBy(v => v.Classification.ToString())
                    .Select(g => new
                    {
                        Key = g.Key,
                        Count = g.Count()
                    });

                foreach (var group in groups)
                {
                    Console.WriteLine("{0}({1})", group.Key, group.Count);
                }
            }

            Console.ReadKey();
        }
    }
}
