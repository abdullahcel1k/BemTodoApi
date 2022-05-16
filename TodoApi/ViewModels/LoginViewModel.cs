using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ResponseModel {
        public  bool IsSucced { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
