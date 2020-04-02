﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstBlog.Models
{
    public class User : IdentityUser
    {
        public override string Id { get => base.Id; set => base.Id = value; }
        public IList<PostLikeDislike> PostLikes { get; set; }

        public User() : base()
        {
                
        }

        public User(string userName) : base(userName)
        {

        }
    }
}
