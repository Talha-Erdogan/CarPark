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

        [Get("/GetAllAuthByProfileId?PersonnelId={personnelId}")]
        Task<Return<List<Auth>>> GetAllAuthByProfileId([Query] int personnelId);

        [Get("/GetAllAuthByProfileIdWhichIsNotIncluded?PersonnelId={personnelId}")]
        Task<Return<List<Auth>>> GetAllAuthByProfileIdWhichIsNotIncluded([Query] int personnelId);

        [Post("")]
        Task<Return<Auth>> Add(Business.Models.ProfileDetail.AddRequestModel addRequestModel);

        [Delete("/?ProfileId={profileId}&AuthId={authId}")]
        Task<Return<int>> Delete([Query] int profileId, [Query] int authId);
    }
}
