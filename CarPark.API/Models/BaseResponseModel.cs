using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Models
{
    public class BaseResponseModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int Status { get; set; }
        public List<ReturnError> Errors { get; set; }

        //display languages
        public string DisplayLanguage { get; set; }
    }
}
