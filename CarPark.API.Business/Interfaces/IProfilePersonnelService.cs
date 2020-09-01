using CarPark.API.Business.Models;
using CarPark.API.Business.Models.ProfilePersonnel;
using CarPark.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Interfaces
{
    public interface IProfilePersonnelService
    {
        PaginatedList<Personnel> GetAllPersonnelPaginatedWithDetailBySearchFilter(ProfilePersonnelSearchFilter searchFilter);
        PaginatedList<Personnel> GetAllPersonnelWhichIsNotIncludedPaginatedWithDetailBySearchFilter(ProfilePersonnelSearchFilter searchFilter);
        List<Profile> GetAllProfileByCurrentUser(int personnelId);
        List<Profile> GetAllProfileByPersonnelIdAndAuthCode(int personnelId, string authCode);
        int Add(ProfilePersonnel record);
        int DeleteByProfileIdAndPersonnelId(int profileId, int personnelId);
    }
}
