// IPostDAO.cs
using PortfolioWebsite.Models;
using System.Collections.Generic;

namespace PortfolioWebsite.DAO.Interfaces
{
    public interface IPostDAO
    {
        List<Post> GetAllPosts();
        Post GetPostById(int id);
        void CreatePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int id);
    }
}
