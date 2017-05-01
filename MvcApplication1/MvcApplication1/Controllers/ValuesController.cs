using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class CustomerController : ApiController
    {
        private OnlineShopContext _context;
        public CustomerController()
        {
            _context = new OnlineShopContext();
        }
        // GET api/values
        public IEnumerable<Customer> Get()
        {
            return _context.Customers.ToList();
        }

        // GET api/values/5
        public Customer Get(int id)
        {
            return _context.Customers.Find(id);
        }

        // POST api/values
        public void Post([FromBody]Customer value)
        {
            
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Customer value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}