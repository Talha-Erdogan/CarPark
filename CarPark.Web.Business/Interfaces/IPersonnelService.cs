using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Personnel;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business.Interfaces
{
    /// <summary>
    /// personnel client service interface with refit implementation
    /// </summary>
    [Headers("Authorization: Bearer")]
    public interface IPersonnelService
    {
        [Get("/items?CurrentPage={currentPage}&PageSize={pageSize}&SortOn={sortOn}&SortDirection={sortDirection}&Name={name}&LastName={lastName}")]
        Task<Return<PaginatedList<Personnel>>> GetAllPaginatedWithDetailBySearchFilter([Query] int currentPage, [Query] int pageSize, [Query] string sortOn, [Query] string sortDirection, [Query] string name, [Query] string lastName);

        [Get("/{id}")]
        Task<Return<Personnel>> GetById(int id);

        [Post("")]
        Task<Return<Personnel>> Add(AddRequestModel addUsefulLink);

        //[Multipart]
        //[Post("/upload-file")]
        //Task<Return<string>> UploadFile(ByteArrayPart imageFile);

        [Put("/{id}")]
        Task<Return<Personnel>> Edit(int id, AddRequestModel editRequestModel);

    }
}
