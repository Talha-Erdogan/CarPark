using CarPark.API.Business.Models;
using CarPark.API.Business.Models.Auth;
using CarPark.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Interfaces
{
    public interface IAuthService
    {
        PaginatedList<Auth> GetAllPaginatedWithDetailBySearchFilter(AuthSearchFilter searchFilter);
        List<Auth> GetAll();
        Auth GetById(int id);
        int Add(Auth record);
        int Update(Auth record);
        int Delete(int id, int deletedBy);
    }
}
