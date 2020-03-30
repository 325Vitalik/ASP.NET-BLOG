using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstBlog.Models
{
    public class Dislike
    {
        public int Id { get; set; }

        public int PostId { get; set; } = 0;
        public string UserName { get; set; } = "";
    }
}
