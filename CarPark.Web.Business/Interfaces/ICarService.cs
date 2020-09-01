using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Car;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business.Interfaces
{
    /// <summary>
    /// Car client service interface with refit implementation
    /// </summary>
    [Headers("Authorization: Bearer")]
    public interface ICarService
    {
        [Get("/items?CurrentPage={currentPage}&PageSize={pageSize}&SortOn={sortOn}&SortDirection={sortDirection}&Plate={plate}")]
        Task<Return<PaginatedList<Car>>> GetAllPaginatedWithDetailBySearchFilter([Query] int currentPage, [Query] int pageSize, [Query] string sortOn, [Query] string sortDirection, [Query] string plate);

        [Get("/{id}")]
        Task<Return<Car>> GetById(int id);

        [Post("")]
        Task<Return<Car>> Add(AddRequestModel addUsefulLink);

        //[Multipart]
        //[Post("/upload-file")]
        //Task<Return<string>> UploadFile(ByteArrayPart imageFile);

        [Put("/{id}")]
        Task<Return<Car>> Edit(int id, AddRequestModel editRequestModel);


    }
}
