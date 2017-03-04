using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Api.Regions
{
    public class CountriesController : ApiController
    {
        // GET api/<controller>
        public List<RegionCountry> Get()
        {
            List<RegionCountry> result;
            using (var context = new EFDBContext())
            {
                result = context.Countries.ToList();
            }
            return result;
        }

        // GET api/<controller>/5
        public RegionCountry Get(int id)
        {
            RegionCountry result;
            using (var context = new EFDBContext())
            {
                result = context.Countries.FirstOrDefault(m => m.Id == id);
            }
            return result;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}