using CarPark.Web.Business.Models;
using CarPark.Web.Business.Models.Auth;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Web.Business.Interfaces
{
    /// <summary>
    /// Auth client service interface with refit implementation
    /// </summary>
    [Headers("Authorization: Bearer")]
    public interface IAuthService
    {
        [Get("/?CurrentPage={currentPage}&PageSize={pageSize}&SortOn={sortOn}&SortDirection={sortDirection}&Code={code}&Name={name}")]
        Task<Return<PaginatedList<Auth>>> GetAllPaginatedWithDetailBySearchFilter([Query] int currentPage, [Query] int pageSize, [Query] string sortOn, [Query] string sortDirection, [Query] string code, [Query] string name);

        [Get("/{id}")]
        Task<Return<Auth>> GetById(int id);

        [Post("")]
        Task<Return<Auth>> Add(AddRequestModel addRequestModel);

        //[Multipart]
        //[Post("/upload-file")]
        //Task<Return<string>> UploadFile(ByteArrayPart imageFile);

        [Put("/{id}")]
        Task<Return<Auth>> Edit(int id, AddRequestModel editRequestModel);

        [Delete("/{id}")]
        Task<Return<int>> Delete(int id);

    }
}
