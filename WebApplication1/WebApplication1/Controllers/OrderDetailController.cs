using DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApplication1;

namespace WebApplication1.Controllers
{
    [EnableCors("http://localhost:8080", "*", "*")]
    public class OrderDetailController : ApiController
    {
        private OnlineShopContext db = new OnlineShopContext();

        // GET: api/OrderDetail
        public IQueryable<OrderDetail> GetOrderDetails()
        {
            return db.OrderDetails;
        }

        // GET: api/OrderDetail/5
        [ResponseType(typeof(OrderDetail))]
        public async Task<IHttpActionResult> GetOrderDetail(int id)
        {
            OrderDetail OrderDetail = await db.OrderDetails.FindAsync(id);
            if (OrderDetail == null)
            {
                return NotFound();
            }

            return Ok(OrderDetail);
        }

        // PUT: api/OrderDetail/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrderDetail(int id, OrderDetail OrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != OrderDetail.OrderId)
            {
                return BadRequest();
            }

            db.Entry(OrderDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OrderDetail
        [ResponseType(typeof(OrderDetail))]
        public async Task<IHttpActionResult> PostOrderDetail(OrderDetail OrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderDetails.Add(OrderDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderDetailExists(OrderDetail.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = OrderDetail.OrderId }, OrderDetail);
        }

        // DELETE: api/OrderDetail/5
        [ResponseType(typeof(OrderDetail))]
        public async Task<IHttpActionResult> DeleteOrderDetail(int id)
        {
            OrderDetail OrderDetail = await db.OrderDetails.FindAsync(id);
            if (OrderDetail == null)
            {
                return NotFound();
            }

            db.OrderDetails.Remove(OrderDetail);
            await db.SaveChangesAsync();

            return Ok(OrderDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderDetailExists(int id)
        {
            return db.OrderDetails.Count(e => e.OrderId == id) > 0;
        }
    }
}