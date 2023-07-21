// ICommentDAO.cs
using PortfolioWebsite.Models;
using System.Collections.Generic;

namespace PortfolioWebsite.DAO.Interfaces
{
    public interface ICommentDAO
    {
        void AddComment(Comment comment);
        void DeleteComment(int commentId);
        List<Comment> GetCommentsByPostId(int postId);
        Comment GetCommentById(int commentId);
        void UpdateComment(Comment comment);
        List<Comment> GetAllComments();
    }
}
