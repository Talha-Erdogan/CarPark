using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.Web.Business.Models.ProfilePersonnel
{
    public class AddRequestModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int PersonnelId { get; set; }
    }
}
