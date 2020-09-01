using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Profile;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business.Interfaces
{
    /// <summary>
    /// Profile Personnel client service interface with refit implementation
    /// </summary>
    [Headers("Authorization: Bearer")]
    public interface IProfilePersonnelService
    {
        [Get("/GetAllProfileByCurrentUser?PersonnelId={personnelId}")]
        Task<Return<List<Profile>>> GetAllProfileByCurrentUser([Query] int personnelId);

    }
}
