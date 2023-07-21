// PostSqlDAO.cs
using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Models;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioWebsite.DAO
{
    public class PostSqlDAO : IPostDAO
    {
        private readonly PortfolioContext _context;

        public PostSqlDAO(PortfolioContext context)
        {
            _context = context;
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            Post post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
        }
    }
}
