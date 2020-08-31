using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Models
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
