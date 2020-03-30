using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstBlog.Models
{
    public class Like
    {
        public int Id { get; set; }

        public int PostId { get; set; } = 0;
        public string UserName { get; set; } = "";
    }
}
