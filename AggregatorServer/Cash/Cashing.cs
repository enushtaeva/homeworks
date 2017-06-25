using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AggregatorServer.Models;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Web.Hosting;
using System.Net;
using System.Drawing;
using AggregatorServer.DBContext;

namespace AggregatorServer.Cash
{
    public class Cashing
    {
        public static string Path = HostingEnvironment.ApplicationPhysicalPath;

        public static void Start(string cashquery)
        {
            while (true)
            {
                lock (cashquery)
                {
                    if (!Directory.Exists((Path + @"Cash\" + cashquery + @"\Images")))
                        Directory.CreateDirectory(Path + @"Cash\"+cashquery+@"\Images");
                    List<string> files = Directory.EnumerateFiles(Path + @"Cash\"+cashquery+@"\Images\").ToList();

                    files.ForEach(file =>
                    {
                        File.Delete(file);
                    });

                    int imageNum = 0;

                    AggregatorModel model = new AggregatorModel();
                    SearchResult result = model.Search(cashquery);

                    foreach (var post in result.Posts)
                    {
                        if (post.Image == "")
                            continue;
                        WebRequest requestPic = WebRequest.Create(post.Image);
                        WebResponse responsePic = requestPic.GetResponse();
                        Image webImage = Image.FromStream(responsePic.GetResponseStream());

                        post.Image = @"\Cash\" + cashquery + @"\Images\" + imageNum + ".jpg";
                        webImage.Save(Path + post.Image);
                        imageNum++;
                    }
                    DBWorker dbworker = new DBWorker();
                    dbworker.DeleteAllPostsByHashTag(null);
                    dbworker.DeleteAllPostsByHashTag(cashquery);
                    dbworker.DeletePagination(cashquery);
                    dbworker.AddAllPosts(result.Posts,cashquery);
                    dbworker.AddPagination(new Paginatins()
                    {
                        VKPagination = result.VKPagination,
                        InstagrammPaginatin = result.InstPagination,
                        TwitterPaginatin = result.TwitterPagination,
                        HashTag = cashquery
                    });
                    File.WriteAllText(Path + @"Cash\" + cashquery + ".json", JsonConvert.SerializeObject(result));
                }
                Thread.Sleep(new TimeSpan(1, 0, 0));
            }
        }
    }
}