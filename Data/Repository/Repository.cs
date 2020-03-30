using FirstBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstBlog.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            this._ctx = ctx;
        }

        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _ctx.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return _ctx.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }

        public void Like(int postId, string userName)
        {
            var like = new Like() { PostId = postId, UserName = userName};
            _ctx.Likes.Add(like);
        }
        
        public void Dislike(int postId, string userName)
        {
            var dislike = new Dislike() { PostId = postId, UserName = userName };
            _ctx.Dislike.Add(dislike);
        }
        
        public void DeleteLike(int postId, string userName)
        {
            var like = _ctx.Likes.FirstOrDefault(l => l.PostId == postId && l.UserName == userName);
            if(like != null)_ctx.Likes.Remove(like);
        }

        public void DeleteDislike(int postId, string userName)
        {
            var dislike = _ctx.Dislike.First(l => l.PostId == postId && l.UserName == userName);
            if(dislike != null)_ctx.Dislike.Remove(dislike);
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0) return true;
            return false;
        }

        public Like GetLike(int postId, string userName)
        {
            return _ctx.Likes.FirstOrDefault(l => l.PostId == postId && l.UserName == userName);
        }

        public Dislike GetDislike(int postId, string userName)
        {
            return _ctx.Dislike.FirstOrDefault(l => l.PostId == postId && l.UserName == userName);
        }
    }
}
