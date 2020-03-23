using NLog;
using BlogsConsole.Models;
using System;
using System.Linq;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {

                // Create and save a new Blog
                Console.Write("Blog Manager\n1. Display all blogs\n2. Add Blog\n3. Create Post");
                
                var name = Console.ReadLine();
                var db = new BloggingContext();
                switch (name)
                {
                    case "1":
                        var blog = new Blog { Name = name };

                        
                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);
                        break;
                    case "2":
                        // Display all Blogs from the database
                        var query = db.Blogs.OrderBy(b => b.Name);

                        Console.WriteLine("All blogs in the database:");
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.Name);
                        }
                        break;
                    case "3":
                        Console.WriteLine("Please select the blog to post to:");
                        var search = db.Blogs.OrderBy(b => b.Name);
                        
                        foreach(var item in search)
                        {
                            Console.WriteLine(item.Name);
                        }
                        var select = Console.ReadLine();
                        foreach(var item in search)
                        {
                            if(select == item.Name)
                            {
                                Console.WriteLine("Enter the Title of the Post");
                                var title = Console.ReadLine();
                                var post = new Post { Title = title };
                                Console.WriteLine("Enter the content of the Post");
                                var content = Console.ReadLine();
                                post.Content = content;
                                db.AddPost(post);
                                
                            }
                        }
                        break;
                    default: break;
                }
                

                
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            logger.Info("Program ended");
        }
    }
}
