using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { set; get; }

        public string Password { set; get; }
    }
}
