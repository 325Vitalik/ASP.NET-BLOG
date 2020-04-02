using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string Image { get; set; } = "";
        public int Rating { get; set; } = 0;

        public DateTime WasCreated { get; set; } = DateTime.Now;
    }
}
