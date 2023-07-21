// CommentSqlDAO.cs
using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Models;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioWebsite.DAO
{
    public class CommentSqlDAO : ICommentDAO
    {
        private readonly PortfolioContext _context;

        public CommentSqlDAO(PortfolioContext context)
        {
            _context = context;
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void DeleteComment(int commentId)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            return _context.Comments.Where(c => c.PostId == postId).ToList();
        }

        public Comment GetCommentById(int commentId)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == commentId);
        }

        public void UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }

        public List<Comment> GetAllComments()
        {
            return _context.Comments.ToList();
        }
    }
}
