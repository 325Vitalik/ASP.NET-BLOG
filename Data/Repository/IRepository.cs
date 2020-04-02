using FirstBlog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstBlog.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        void AddPost(Post post);
        void RemovePost(int id);
        void UpdatePost(Post post);
        User GetUser(string userName);
        string GetUserId(string userName);
        bool? GetVote(string userNmae, int postId);
        List<PostLikeDislike> GetAllVotesOfPost(int postId);
        void AddVote(PostLikeDislike pld);
        void RemoveVote(string userName, int postId);

        Task<bool> SaveChangesAsync();
    }
}
