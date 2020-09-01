using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Auth;
using CarPark.Web.Business.Models.Profile;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business.Interfaces
{
    /// <summary>
    /// Profile Detail client service interface with refit implementation
    /// </summary>
    [Headers("Authorization: Bearer")]
    public interface IProfileDetailService
    {
        [Get("/GetAllAuthByCurrentUser?PersonnelId={personnelId}")]
        Task<Return<List< Auth>>> GetAllAuthByCurrentUser([Query] int personnelId);
    }
}
