using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class IndexApartmentsViewResult
    {
        public List<Apartment> Apartments { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}