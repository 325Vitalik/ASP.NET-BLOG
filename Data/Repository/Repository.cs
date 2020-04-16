using FirstBlog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstBlog.Data.Repository
{
    public class Repository : IRepository
    {
        //TODO: Дописати методи для юзерів та лайків
        //private DbSet<PostLikeDislike> PostLikeDislike { get; set; }
        //private IList<User> Users { get; set; }

        private AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            this._ctx = ctx;
            //PostLikeDislike = _ctx.PostLikeDislike;
            //Users = _ctx.Users.ToList();
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

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0) return true;
            return false;
        }

        public User GetUser(string userName)
        {
            return _ctx.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public string GetUserId(string userName)
        {
            return _ctx.Users.FirstOrDefault(u => u.UserName == userName)?.Id;
        }

        public bool? GetVote(string userName, int postId)
        {
            var userId = GetUserId(userName);
            return _ctx.PostLikeDislike.FirstOrDefault(p => p.PostId == postId && p.UserId == userId)?.LikeDislike;
        }

        public void AddVote(PostLikeDislike pld)
        {
            _ctx.PostLikeDislike.Add(pld);
        }

        public void RemoveVote(string userName, int postId)
        {
            var userId = GetUserId(userName);
            var userVote = _ctx.PostLikeDislike.FirstOrDefault(p => p.PostId == postId && p.UserId == userId);
            if (userVote != null) _ctx.PostLikeDislike.Remove(userVote);
        }

        public IList<PostLikeDislike> GetAllVotesOfPost(int postId)
        {
            var post = GetPost(postId);
            _ctx.Entry(post).Collection("PostLikes").Load();
            return post.PostLikes;
        }

    }
}
