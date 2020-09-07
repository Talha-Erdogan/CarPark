using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Personnel;
using CarPark.Web.Business.Models.Profile;
using CarPark.Web.Business.Models.ProfilePersonnel;
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

        [Get("/GetAllPersonnelPaginatedWithDetail?ProfileId={profileId}&Personnel_Name={personnel_Name}&Personnel_LastName={personnel_LastName}&CurrentPage={currentPage}&PageSize={pageSize}&SortOn={sortOn}&SortDirection={sortDirection}")]
        Task<Return<PaginatedList<Personnel>>> GetAllPersonnelPaginatedWithDetail([Query] int profileId, [Query] string personnel_Name, [Query] string personnel_LastName, [Query] int currentPage, [Query] int pageSize, [Query] string sortOn, [Query] string sortDirection);

        [Get("/GetAllPersonnelWhichIsNotIncludedPaginatedWithDetail?ProfileId={profileId}&Personnel_Name={personnel_Name}&Personnel_LastName={personnel_LastName}&CurrentPage={currentPage}&PageSize={pageSize}&SortOn={sortOn}&SortDirection={sortDirection}")]
        Task<Return<PaginatedList<Personnel>>> GetAllPersonnelWhichIsNotIncludedPaginatedWithDetail([Query] int profileId, [Query] string personnel_Name, [Query] string personnel_LastName, [Query] int currentPage, [Query] int pageSize, [Query] string sortOn, [Query] string sortDirection);

        [Post("")]
        Task<Return<ProfilePersonnel>> Add(Business.Models.ProfilePersonnel.AddRequestModel addRequestModel);


        [Delete("/?ProfileId={profileId}&PersonnelId={personnelId}")]
        Task<Return<int>> Delete([Query] int profileId, [Query] int personnelId);
    }
}
