using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.LocationMove;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business.Interfaces
{
    /// <summary>
    /// Location move client service interface with refit implementation
    /// </summary>
    [Headers("Authorization: Bearer")]
    public interface ILocationMoveService
    {
        [Get("/items?CurrentPage={currentPage}&PageSize={pageSize}&SortOn={sortOn}&SortDirection={sortDirection}&Plate={plate}&LocationName={locationName}")]
        Task<Return<PaginatedList<LocationMoveWithDetail>>> GetAllPaginatedWithDetailBySearchFilter([Query] int currentPage, [Query] int pageSize, [Query] string sortOn, [Query] string sortDirection, [Query] string plate, [Query] string locationName);

        [Get("/with-detail")]
        Task<Return<List<LocationWithDetail>>> GetAllWithDetail();
        //car-list
        [Get("/car-list")]
        Task<Return<List<Models.Car.Car>>> GetAllCarWhichIsNotLocationMove();

        [Get("/{id}")]
        Task<Return<LocationMove>> GetById(int id);


        [Get("/{locationId}/with-detail")]
        Task<Return<LocationWithDetail>> GetLocationByLocationIdWithDetail(int locationId);

        [Post("")]
        Task<Return<LocationMove>> Add(AddRequestModel addRequestModel);

        //[Multipart]
        //[Post("/upload-file")]
        //Task<Return<string>> UploadFile(ByteArrayPart imageFile);

        [Put("/{id}")]
        Task<Return<LocationMove>> Edit(int id, AddRequestModel editRequestModel);

    }
}
