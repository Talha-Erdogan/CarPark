using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Profile;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business.Interfaces
{
    /// <summary>
    /// profile client service interface with refit implementation
    /// </summary>
    [Headers("Authorization: Bearer")]
    public interface IProfileService
    {
        [Get("/items?CurrentPage={currentPage}&PageSize={pageSize}&SortOn={sortOn}&SortDirection={sortDirection}&Code={code}&Name={name}")]
        Task<Return<PaginatedList<Profile>>> GetAllPaginatedWithDetailBySearchFilter([Query] int currentPage, [Query] int pageSize, [Query] string sortOn, [Query] string sortDirection, [Query] string code, [Query] string name);

        [Get("/{id}")]
        Task<Return<Profile>> GetById(int id);

        [Post("")]
        Task<Return<Profile>> Add(AddRequestModel addRequestModel);

        //[Multipart]
        //[Post("/upload-file")]
        //Task<Return<string>> UploadFile(ByteArrayPart imageFile);

        [Put("/{id}")]
        Task<Return<Profile>> Edit(int id, AddRequestModel editRequestModel);

        [Delete("/{id}")]
        Task<Return<int>> Delete(int id);


    }
}
