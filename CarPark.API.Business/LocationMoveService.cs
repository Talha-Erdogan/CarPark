using CarPark.API.Business.Interfaces;
using CarPark.API.Business.Models;
using CarPark.API.Business.Models.LocationMove;
using CarPark.API.Data;
using CarPark.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CarPark.API.Business
{
    public class LocationMoveService : ILocationMoveService
    {
        private IConfiguration _config;

        public LocationMoveService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<LocationMoveWithDetail> GetAllPaginatedWithDetailBySearchFilter(LocationMoveSearchFilter searchFilter)
        {
            PaginatedList<LocationMoveWithDetail> resultList = new PaginatedList<LocationMoveWithDetail>(new List<LocationMoveWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from lm in dbContext.LocationMove
                            from l in dbContext.Location.Where(x => x.Id == lm.LocationId).DefaultIfEmpty()
                            from c in dbContext.Car.Where(x => x.Id == lm.CarId).DefaultIfEmpty()
                            select new LocationMoveWithDetail()
                            {
                                Id = lm.Id,
                                CarId = lm.CarId,
                                LocationId = lm.LocationId,
                                EntryDate = lm.EntryDate,
                                ExitDate = lm.ExitDate,
                                Car_Plate = c == null ? String.Empty : c.Plate,
                                Location_Name = l == null ? String.Empty : l.Name
                            };

                // filtering
                if (!string.IsNullOrEmpty(searchFilter.Filter_Car_Plate))
                {
                    query = query.Where(r => r.Car_Plate.Contains(searchFilter.Filter_Car_Plate));
                }
                if (!string.IsNullOrEmpty(searchFilter.Filter_Location_Name))
                {
                    query = query.Where(r => r.Location_Name.Contains(searchFilter.Filter_Location_Name));
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
                resultList = new PaginatedList<LocationMoveWithDetail>(
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

        public List<LocationWithDetail> GetAllLocationListWithDetail()
        {
            List<LocationWithDetail> resultList = new List<LocationWithDetail>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var locationList = dbContext.Location.AsNoTracking().ToList();
                var locationMoveList = dbContext.LocationMove.Where(x => x.ExitDate == null && x.EntryDate != null).ToList();
                foreach (var location in locationList)
                {
                    if (locationMoveList.Where(x => x.LocationId == location.Id).Any())
                    {
                        resultList.Add(new LocationWithDetail()
                        {
                            LocationId = location.Id,
                            LocationName = location.Name,
                            IsEmpty = false
                        });
                    }
                    else
                    {
                        resultList.Add(new LocationWithDetail()
                        {
                            LocationId = location.Id,
                            LocationName = location.Name,
                            IsEmpty = true
                        });
                    }
                }
            }
            return resultList;
        }

        public LocationWithDetail GetLocationByLocationIdWithDetail(int locationId)
        {
            LocationWithDetail result = null;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var location = dbContext.Location.Where(x => x.Id == locationId).AsNoTracking().FirstOrDefault();
                if (location == null)
                {
                    return result;
                }
                var locationMoveList = dbContext.LocationMove.Where(x => x.LocationId == locationId && x.ExitDate == null && x.EntryDate != null).FirstOrDefault();
                if (locationMoveList == null)
                {
                    result = new LocationWithDetail()
                    {
                        LocationId = location.Id,
                        IsEmpty = true,
                        LocationName = location.Name,
                        
                    };
                }
                else
                {
                    result = new LocationWithDetail()
                    {
                        LocationId = location.Id,
                        IsEmpty = false,
                        LocationName = location.Name,
                        LocationMove_ID = locationMoveList.Id,
                    };
                }
            }
            return result;
        }

        public List<Car> GetAllCarWhichIsNotLocationMove()
        {
            List<Car> resultList = null;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var locationMoveCarId = dbContext.LocationMove.Where(x => x.ExitDate == null && x.EntryDate != null).Select(x => x.CarId);
                resultList = dbContext.Car.Where(x => !locationMoveCarId.Contains(x.Id)).AsNoTracking().ToList();
            }
            return resultList;
        }


        public List<LocationMove> GetAll()
        {
            List<LocationMove> resultList = new List<LocationMove>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                resultList.AddRange(dbContext.LocationMove.AsNoTracking().ToList());
            }
            return resultList;
        }

        public LocationMove GetById(int id)
        {
            LocationMove result = null;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.LocationMove.Where(a => a.Id == id).AsNoTracking().SingleOrDefault();
            }
            return result;
        }

        public int Add(LocationMove record)
        {
            int result = 0;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }
            return result;
        }

        public int Update(LocationMove record)
        {
            int result = 0;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }
            return result;
        }
    }
}
