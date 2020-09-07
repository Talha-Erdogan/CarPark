using CarPark.API.Business.Models;
using CarPark.API.Business.Models.LocationMove;
using CarPark.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Interfaces
{
    public interface ILocationMoveService
    {
        PaginatedList<LocationMoveWithDetail> GetAllPaginatedWithDetailBySearchFilter(LocationMoveSearchFilter searchFilter);
        List<LocationWithDetail> GetAllLocationListWithDetail();
        LocationWithDetail GetLocationByLocationIdWithDetail(int locationId);
        List<Car> GetAllCarWhichIsNotLocationMove();
        List<LocationMove> GetAll();
        LocationMove GetById(int id);
        int Add(LocationMove record);
        int Update(LocationMove record);
    }
}
