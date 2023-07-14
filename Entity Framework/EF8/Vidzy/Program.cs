using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace Vidzy
{
    class Program
    {
        static void AddTerminator1Video()
        {
            using (var context = new VidzyContext())
            {
                var actionExists = context.Videos.Where(v => v.Genre.Name == "Action").FirstOrDefault();
                if (actionExists == null)
                {
                    context.Videos.Add(new Video()
                    {
                        Name = "Terminator 1",
                        ReleaseDate = new DateTime(1984, 10, 26),
                        Genre = new Genre() { Name = "Action" },
                        Classification = Classification.Silver
                    });
                }
                context.SaveChanges();
            }
        }

        static void AddClassicsAndDramaTags()
        {
            using (var context = new VidzyContext())
            {
                var exists = context.Tags.Where(t => t.Name == "classics" || t.Name == "drama").FirstOrDefault();
                if (exists == null)
                {
                    context.Tags.Add(new Tag(){Name = "classics"});
                    context.Tags.Add(new Tag() { Name = "drama" });
                    context.SaveChanges();
                }
            }
        }

        static void AddTagsForFather()
        {
            using (var context = new VidzyContext())
            {
                Tag classicsTag = context.Tags.Where(v => v.Name == "classics").FirstOrDefault()?? new Tag { Name = "classiscs" };
                Tag dramaTag = context.Tags.Where(v => v.Name == "drama").FirstOrDefault() ?? new Tag { Name = "drama" };
                Tag comedy = context.Tags.Where(t => t.Name == "comedy").FirstOrDefault() ?? new Tag { Name = "comedy" };

                context.Tags.Add(comedy);
                context.Tags.Add(classicsTag);
                context.Tags.Add(dramaTag);

                Video father = context.Videos.Where(v=>v.Id == 1).First();

                father.AddTag(classicsTag);
                father.AddTag(dramaTag);
                father.AddTag(comedy);
                context.SaveChanges();
            }
        }

        static void RemoveComdeyForFather()
        {
            using (var context = new VidzyContext())
            {
                Video father = context.Videos.Where(v => v.Id == 1).First();

                father.RemoveTag("comedy");
                context.SaveChanges();
            }
        }

        static void RemoveFather()
        {
            using (var context = new VidzyContext())
            {
                Video father = context.Videos.Where(v => v.Id == 1).First();
                context.Videos.Remove(father);
                context.SaveChanges();
            }
        }

        static void RemoveAction()
        {
            using (var context = new VidzyContext())
            {
                IEnumerable<Video> actionVideos = context.Videos.Where(v => v.Genre.Name == "Action");
                context.Videos.RemoveRange(actionVideos);

                Tag action = context.Tags.Where(t=>t.Name == "Action").First();
                context.Tags.Remove(action);
            }
        }
        static void Main(string[] args)
        {
        }
    }
}
