using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public class HandleResult
    {

        public HandleResult() 
        {
            IsSuccess = true;
            Message = "操作成功！";
        }
        public HandleResult(bool iss, string msg)
        {
            IsSuccess = iss;
            Message = msg;
        }
        public HandleResult(bool iss)
        {
            IsSuccess = iss;
            Message = "操作成功！"; 
        }
        public HandleResult(string msg)
        {
            IsSuccess = true;
            Message = msg;
        }
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

    }
}
