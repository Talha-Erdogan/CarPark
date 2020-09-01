using CarPark.API.Business.Models;
using CarPark.API.Business.Models.Personnel;
using CarPark.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Business.Interfaces
{
    public interface IPersonnelService
    {
        PaginatedList<Personnel> GetAllPaginatedWithDetailBySearchFilter(PersonnelSearchFilter searchFilter);
        List<Personnel> GetAll();
        Personnel GetById(int id);
        Personnel GetByUserNameAndPassword(string userName, string password);
        int Add(Personnel record);
        int Update(Personnel record);
    }
}
