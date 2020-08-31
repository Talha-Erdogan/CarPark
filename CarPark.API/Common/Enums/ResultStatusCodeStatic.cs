using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.API.Common.Enums
{
    public class ResultStatusCodeStatic
    {
        #region Http Status Code
        //200-> OK
        //201-> Created(Tabloya veri eklendiğinde)
        //204-> No Content(veri silindikten sonra dönülebilir.)
        //400-> Bad Request(ekleme de ve ya guncelleme de modele uygun veri gönderilmemiş ise döndürülebilir.)
        //401-> Unauthorized 
        //404-> Not Found(istenilen veri yoksa kullanılabilir veya guncelleme yapılacak veri var ise ve parametre ile gönderilen Id 'ye ait veri yoksa kullanılabilir.)
        //500-> Internal Server Error(Genel sorun ve hata mesajı olarak kullanılabilir.)
        #endregion
        public const int Success = 200;
        public const int Created = 201;
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int Error = 404;
        public const int InternalServerError = 500;
    }
}
