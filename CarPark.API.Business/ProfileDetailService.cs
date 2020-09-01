using CarPark.API.Business.Interfaces;
using CarPark.API.Data;
using CarPark.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPark.API.Business
{
    public class ProfileDetailService : IProfileDetailService
    {
        private IConfiguration _config;

        public ProfileDetailService(IConfiguration config)
        {
            _config = config;
        }

        public List<Auth> GetAllAuthByCurrentUser(int personnelId)
        {
            List<Auth> resultList = new List<Auth>();

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from pd in dbContext.ProfileDetail
                            join pe in dbContext.ProfilePersonnel on pd.ProfileId equals pe.ProfileId
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            join p in dbContext.Profile on pd.ProfileId equals p.Id
                            where
                                pe.PersonnelId == personnelId && p.IsDeleted == false && a.IsDeleted == false
                            select a;

                // as no tracking
                query = query.AsNoTracking();

                resultList.AddRange(query.ToList());

            }
            return resultList;
        }

        public List<Auth> GetAllAuthByProfileId(int profileId)
        {
            List<Auth> resultList = new List<Auth>();

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from pd in dbContext.ProfileDetail
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            join p in dbContext.Profile on pd.ProfileId equals p.Id
                            where a.IsDeleted == false && p.IsDeleted == false
                            where
                                pd.ProfileId == profileId
                            orderby a.Name ascending
                            select a;

                // as no tracking
                query = query.AsNoTracking();

                resultList.AddRange(query.ToList());

            }
            return resultList;
        }

        public List<Auth> GetAllAuthByProfileIdWhichIsNotIncluded(int profileId)
        {
            List<Auth> resultList = new List<Auth>();

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var profile = dbContext.Profile.Where(p => p.Id == profileId).AsNoTracking().FirstOrDefault();
                var query = from pd in dbContext.ProfileDetail
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            join p in dbContext.Profile on pd.ProfileId equals p.Id
                            where
                                pd.ProfileId == profileId && a.IsDeleted == false && p.IsDeleted == false
                            select a;

                // as no tracking
                var queryIdList = query.AsNoTracking().Select(r => r.Id);
                var query2 = from a in dbContext.Auth
                             where !queryIdList.Contains(a.Id) && a.IsDeleted == false
                             orderby a.Name ascending
                             select a;

                resultList.AddRange(query2.AsNoTracking().ToList());
            }

            return resultList;
        }

        public string GetAllAuthCodeByPersonnelIdAsConcatenateString(int personnelId)
        {
            string result = "";
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from pd in dbContext.ProfileDetail
                            join pe in dbContext.ProfilePersonnel on pd.ProfileId equals pe.ProfileId
                            join a in dbContext.Auth on pd.AuthId equals a.Id
                            join p in dbContext.Profile on pd.ProfileId equals p.Id
                            where
                                pe.PersonnelId == personnelId && p.IsDeleted == false && a.IsDeleted == false
                            select a.Code;

                // as no tracking
                query = query.AsNoTracking();

                // kayıtlar Distinct yapılır
                var codeList = query.ToList();
                if (codeList!=null)
                {
                    result = string.Join(",", codeList.Distinct());
                }
            }
            return result;
        }

        public int Add(ProfileDetail record)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                ProfileDetail existrecord = dbContext.ProfileDetail.Where(pd => pd.ProfileId == record.ProfileId && pd.AuthId == record.AuthId).FirstOrDefault();
                if (existrecord == null)
                {
                    dbContext.Entry(record).State = EntityState.Added;
                    result = dbContext.SaveChanges();
                }
            }

            return result;
        }

        public int DeleteByProfileIdAndAuthId(int profileId, int authId)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                ProfileDetail record = dbContext.ProfileDetail.Where(pd => pd.ProfileId == profileId && pd.AuthId == authId).AsNoTracking().SingleOrDefault();
                dbContext.Entry(record).State = EntityState.Deleted;
                result = dbContext.SaveChanges();
            }

            return result;
        }
    }
}
