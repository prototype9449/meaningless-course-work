using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApplication1;

namespace WebApplication1.Controllers
{
    [EnableCors("http://localhost:8080", "*", "*")]
    public class PoliciesController : ApiController
    {
        private OnlineShopContext db = new OnlineShopContext();

        // GET: api/Policies
        public IEnumerable<PolicyModel> GetPolicies()
        {
            return db.Predicates.Include(x => x.Groups).ToList().Select(x =>
            new PolicyModel()
            {
                id = x.id,
                PredicateValue = x.Value,
                GroupIds = string.Join(", ", x.Groups.ToList().Select(y => y.id)),
                TableName = x.TableName
            }
            );
        }

        // GET: api/Policies/5
        [ResponseType(typeof(PolicyModel))]
        public IHttpActionResult GetPolicy(int predicateId)
        {
            Predicate predicate = db.Predicates.Where(x => x.id == predicateId).Include(x => x.Groups).FirstOrDefault();

            if (predicate == null)
            {
                return NotFound();
            }

            return Ok(new PolicyModel()
            {
                id = predicateId,
                PredicateValue = predicate.Value,
                GroupIds = string.Join(", ", predicate.Groups.ToList()),
                TableName = predicate.TableName
            });
        }

        //// PUT: api/Policies/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutPolicy(string id, Policy policy)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != policy.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(policy).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PolicyExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Policies
        [ResponseType(typeof(PolicyModel))]
        public IHttpActionResult PostPolicy(PolicyModel policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var groupIds = policy.GroupIds.Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x));
            var groups = db.Groups.Where(x => groupIds.Contains(x.id)).ToList();

            var predicate = new Predicate()
            {
                TableName = policy.TableName,
                Value = policy.PredicateValue,
                Groups = groups
            };
            db.Predicates.Add(predicate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PolicyExists(predicate.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = predicate.id }, policy);
        }

        //// DELETE: api/Policies/5
        //[ResponseType(typeof(Policy))]
        //public IHttpActionResult DeletePolicy(string id)
        //{
        //    Policy policy = db.Policies.Find(id);
        //    if (policy == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Policies.Remove(policy);
        //    db.SaveChanges();

        //    return Ok(policy);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PolicyExists(int id)
        {
            return db.Predicates.Count(e => e.id == id) > 0;
        }
    }
}