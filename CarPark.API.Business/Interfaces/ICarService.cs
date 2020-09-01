using CarPark.API.Business.Models;
using CarPark.API.Business.Models.Car;
using CarPark.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Interfaces
{
   public interface ICarService
    {
        PaginatedList<Car> GetAllPaginatedWithDetailBySearchFilter(CarSearchFilter searchFilter);
        List<Car> GetAll();
        Car GetById(int id);
        int Add(Car record);
        int Update(Car record);
    }
}
