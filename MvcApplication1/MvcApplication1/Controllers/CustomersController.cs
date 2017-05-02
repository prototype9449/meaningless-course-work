using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MvcApplication1.Controllers
{
    [AllowCrossSiteJson]
    public class CustomersController : ApiController
    {
        private OnlineShopContext _context;
        public CustomersController()
        {
            _context = new OnlineShopContext();
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _context.Customers.ToList();
        }

        [HttpGet]
        public Customer Get(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No customer with Id = {0}", id)),
                    ReasonPhrase = "Customer ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
            return customer;
        }

        [HttpPost]
        public async Task<Customer> Post([FromBody]Customer model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Customers.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        [HttpPut]
        public async Task<Customer> Put(int id, [FromBody]Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                var item = _context.Customers.Find(id);

                if (item == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                item.Address = customer.Address;
                item.City = customer.City;
                item.CompanyName = customer.CompanyName;
                item.Phone = customer.Phone;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!CustomerExists(id))
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }
                    else
                    {
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }
                }

                return item;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<Customer> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Count(e => e.Id == id) > 0;
        }
    }
}