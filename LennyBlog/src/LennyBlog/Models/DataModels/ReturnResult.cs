using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models.DataModels
{
    public class ReturnResult<T>
    {
        //状态  ok error
        public string Status { set; get; }

        //返回结果
        public T Result { set; get; }

        public string ReturnMsg { set; get; }
    }

    public class ReturnResult
    {
        //状态  ok error
        public string Status { set; get; }

        public string ReturnMsg { set; get; }
    }
}
