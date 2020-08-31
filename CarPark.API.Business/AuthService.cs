using CarPark.API.Business.Interfaces;
using CarPark.API.Business.Models;
using CarPark.API.Business.Models.Auth;
using CarPark.API.Data;
using CarPark.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace CarPark.API.Business
{
    public class AuthService : IAuthService
    {
        private IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<Auth> GetAllPaginatedWithDetailBySearchFilter(AuthSearchFilter searchFilter)
        {
            PaginatedList<Auth> resultList = new PaginatedList<Auth>(new List<Auth>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from a in dbContext.Auth
                            where a.IsDeleted == false
                            select a;

                // filtering
                if (!string.IsNullOrEmpty(searchFilter.Filter_Code))
                {
                    query = query.Where(r => r.Code.Contains(searchFilter.Filter_Code));
                }

                if (!string.IsNullOrEmpty(searchFilter.Filter_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Name));
                }

                // asnotracking
                query = query.AsNoTracking();

                //total count
                var totalCount = query.Count();

                //sorting
                if (!string.IsNullOrEmpty(searchFilter.SortOn))
                {
                    // using System.Linq.Dynamic.Core; nuget paketi ve namespace eklenmelidir, dynamic order by yapmak icindir
                    query = query.OrderBy(searchFilter.SortOn + " " + searchFilter.SortDirection.ToUpper());
                }
                else
                {
                    // deefault sıralama vermek gerekiyor yoksa skip metodu hata veriyor ef 6'da -- 28.10.2019 15:40
                    // https://stackoverflow.com/questions/3437178/the-method-skip-is-only-supported-for-sorted-input-in-linq-to-entities
                    query = query.OrderBy(r => r.Id);
                }

                //paging
                query = query.Skip((searchFilter.CurrentPage - 1) * searchFilter.PageSize).Take(searchFilter.PageSize);


                resultList = new PaginatedList<Auth>(
                    query.ToList(),
                    totalCount,
                    searchFilter.CurrentPage,
                    searchFilter.PageSize,
                    searchFilter.SortOn,
                    searchFilter.SortDirection
                    );
            }

            return resultList;
        }

        public List<Auth> GetAll()
        {
            List<Auth> resultList = new List<Auth>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                resultList.AddRange(dbContext.Auth.Where(x => x.IsDeleted == false).AsNoTracking().ToList());
            }
            return resultList;
        }

        public Auth GetById(int id)
        {
            Auth result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.Auth.Where(a => a.Id == id && a.IsDeleted == false).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public int Add(Auth record)
        {
            int result = 0;
            record.IsDeleted = false;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(Auth record)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Delete(int id, int deletedBy)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var auth = dbContext.Auth.Where(x => x.Id == id).FirstOrDefault();
                auth.IsDeleted = true;
                auth.DeletedBy = deletedBy;
                auth.DeletedDateTime = DateTime.Now;
                dbContext.Entry(auth).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }
    }
}
