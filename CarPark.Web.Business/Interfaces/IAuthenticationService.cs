using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Authentication;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business.Interfaces
{
    /// <summary>
    /// Authentication client service interface with refit implementation
    /// </summary>
    [Headers("Authorization: Bearer")]
    public interface IAuthenticationService
    {
        [Post("/Token")]
        Task<Return<TokenResponseModel>> Token(TokenRequestModel requestModel);

    }
}
