namespace FirstBlog.Models
{
    public class PostLikeDislike
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public bool LikeDislike { get; set; }
    }
}
