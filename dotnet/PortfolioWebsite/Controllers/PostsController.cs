// PostsController.cs
using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Interfaces;
using PortfolioWebsite.Models;
using System.Collections.Generic;

namespace PortfolioWebsite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostDAO _postDAO;

        public PostsController(IPostDAO postDAO)
        {
            _postDAO = postDAO;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAllPosts()
        {
            List<Post> posts = _postDAO.GetAllPosts();

            if (posts.Count == 0)
            {
                return NoContent();
            }

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public ActionResult<Post> GetPostById(int id)
        {
            Post post = _postDAO.GetPostById(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        public ActionResult<Post> CreatePost(Post newPost)
        {
            _postDAO.CreatePost(newPost);

            return Ok(newPost);
        }

        [HttpPut("{id}")]
        public ActionResult<Post> UpdatePost(int id, Post updatedPost)
        {
            Post existingPost = _postDAO.GetPostById(id);

            if (existingPost == null)
            {
                return NotFound();
            }

            existingPost.Title = updatedPost.Title;
            existingPost.Content = updatedPost.Content;
            _postDAO.UpdatePost(existingPost);

            return Ok(existingPost);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePost(int id)
        {
            Post existingPost = _postDAO.GetPostById(id);

            if (existingPost == null)
            {
                return NotFound();
            }

            _postDAO.DeletePost(id);

            return NoContent();
        }
    }
}
