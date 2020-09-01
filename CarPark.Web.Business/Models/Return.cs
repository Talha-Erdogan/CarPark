using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Web.Business.Models
{
    public class Return<T> : BaseResponseModel
    {
        public T Data { get; set; }
    }
    public class ReturnError
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
