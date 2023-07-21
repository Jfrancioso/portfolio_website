// CommentsController.cs
using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Interfaces;
using PortfolioWebsite.Models;
using System.Collections.Generic;

namespace PortfolioWebsite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly ICommentDAO _commentDAO;

        public CommentsController(ICommentDAO commentDAO)
        {
            _commentDAO = commentDAO;
        }

        [HttpGet("{postId}")]
        public ActionResult<IEnumerable<Comment>> GetCommentsByPostId(int postId)
        {
            List<Comment> comments = _commentDAO.GetCommentsByPostId(postId);

            if (comments.Count == 0)
            {
                return NoContent();
            }

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> GetCommentById(int id)
        {
            Comment comment = _commentDAO.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPost]
        public ActionResult<Comment> AddNewComment(Comment newComment)
        {
            _commentDAO.AddComment(newComment);

            return Ok(newComment);
        }

        [HttpPut("{id}")]
        public ActionResult<Comment> UpdateComment(int id, Comment updatedComment)
        {
            Comment existingComment = _commentDAO.GetCommentById(id);

            if (existingComment == null)
            {
                return NotFound();
            }

            existingComment.Content = updatedComment.Content;
            _commentDAO.UpdateComment(existingComment);

            return Ok(existingComment);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteComment(int id)
        {
            Comment existingComment = _commentDAO.GetCommentById(id);

            if (existingComment == null)
            {
                return NotFound();
            }

            _commentDAO.DeleteComment(id);

            return NoContent();
        }
    }
}
