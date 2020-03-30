using FirstBlog.Models;
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
        Like GetLike(int postId, string userName);
        Dislike GetDislike(int postId, string userName);
        void Like(int postId, string userName);
        void Dislike(int postId, string userName);
        void DeleteLike(int postId, string userName);
        void DeleteDislike(int postId, string userName);
        void UpdatePost(Post post);

        Task<bool> SaveChangesAsync();
    }
}
