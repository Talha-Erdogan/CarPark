using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Location;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business.Interfaces
{
    /// <summary>
    /// Location client service interface with refit implementation
    /// </summary>
    [Headers("Authorization: Bearer")]
    public interface ILocationService
    {
        [Get("/items?CurrentPage={currentPage}&PageSize={pageSize}&SortOn={sortOn}&SortDirection={sortDirection}&Name={name}")]
        Task<Return<PaginatedList<Location>>> GetAllPaginatedWithDetailBySearchFilter([Query] int currentPage, [Query] int pageSize, [Query] string sortOn, [Query] string sortDirection, [Query] string name);

        [Get("/{id}")]
        Task<Return<Location>> GetById(int id);

        [Post("")]
        Task<Return<Location>> Add(AddRequestModel addUsefulLink);

        //[Multipart]
        //[Post("/upload-file")]
        //Task<Return<string>> UploadFile(ByteArrayPart imageFile);

        [Put("/{id}")]
        Task<Return<Location>> Edit(int id, AddRequestModel editRequestModel);


    }
}
