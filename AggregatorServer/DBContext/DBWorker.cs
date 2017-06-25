using SearchLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AggregatorServer.DBContext
{
    public class DBWorker
    {
        public List<GeneralPost> GetAllPostsByHashTag(string hashtag)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                using (AdminContext db = new AdminContext())
                {
                    return db.Posts.Where(x=>x.HashTag==hashtag).ToList();
                }
             
            }
        }
        public Paginatins  GetPaginations(string hashtag)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                using (AdminContext db = new AdminContext())
                {
                    return db.Paginations.Where(x => x.HashTag == hashtag).First();
                }

            }
        }
        public void AddAllPosts(List<GeneralPost> posts,string cashquery)
        {
            foreach(GeneralPost post in posts)
            {
                post.HashTag = cashquery;
            }
            using (SqlConnection cn = new SqlConnection())
            {
                using (AdminContext db = new AdminContext())
                {
                    db.Posts.AddRange(posts);
                    db.SaveChanges();
                }

            }
        }
        public void AddPagination(Paginatins pagination)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                using (AdminContext db = new AdminContext())
                {
                    db.Paginations.Add(pagination);
                    db.SaveChanges();
                }

            }
        }

        public void DeleteAllPostsByHashTag(string hashtag)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                using (AdminContext db = new AdminContext())
                {
                    IEnumerable<GeneralPost> postsdelete = db.Posts.Where(x => x.HashTag == hashtag);
                    db.Posts.RemoveRange(postsdelete);
                    db.SaveChanges();
                }

            }

        }
        public void DeletePagination(string hashtag)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                using (AdminContext db = new AdminContext())
                {
                    IEnumerable<Paginatins> pagedelete = db.Paginations.Where(x => x.HashTag == hashtag);
                    db.Paginations.RemoveRange(pagedelete);
                    db.SaveChanges();
                }

            }
        }
        public void AddUser(User user)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                using (AdminContext db = new AdminContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }

            }
        }

    }
}