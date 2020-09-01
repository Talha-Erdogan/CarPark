using CarPark.API.Business.Models;
using CarPark.API.Business.Models.Profile;
using CarPark.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Interfaces
{
   public interface IProfileService
    {
        PaginatedList<Profile> GetAllPaginatedWithDetailBySearchFilter(ProfileSearchFilter searchFilter);
        List<Profile> GetAll();
        Profile GetById(int id);
        int Add(Profile record);
        int Update(Profile record);
    }
}
