﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseApiWithIdentity.DataAccess.Models
{
    public class ResponseModel
    {
        public bool IsSucceed { get; set; }
        public object Result { get; set; } = new object();
        public string Message { get; set; }
        public int StatusCode { get; set; } = 200;
        public MyException Exception { get; set; } = new MyException();

        public ResponseModel Succeed(object Result = null)
        {
            IsSucceed = true;
            this.Result = Result;
            return this;
        }

        public ResponseModel Failed(string msg = "")
        {
            IsSucceed = false;
            Message = msg;
            return this;
        }

        public ResponseModel NotFound()
        {
            Message = "This item does not existed!";
            IsSucceed = false; 
            return this;
        }

        public ResponseModel NotFound(string msg)
        {
            Message = msg;
            IsSucceed = false;
            return this;
        }

        public ResponseModel Forbiden()
        {
            Message = "Access denied! You do not have permission to access.";
            IsSucceed = false; 
            return this;
        }

        public ResponseModel UnAuthorize()
        {
            Message = "Please login first!";
            IsSucceed = false; 
            return this;
        }
    }

    public class MyException
    {
        public string Message { get; set; }

        public string InnerMessage { get; set; }

        public string StackTrace { get; set; }
    }
}
