using CarPark.API.Business.Models;
using CarPark.API.Business.Models.Location;
using CarPark.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Interfaces
{
    public interface ILocationService
    {
        PaginatedList<Location> GetAllPaginatedWithDetailBySearchFilter(LocationSearchFilter searchFilter);
        List<Location> GetAll();
        Location GetById(int id);
        int Add(Location record);
        int Update(Location record);

    }
}
