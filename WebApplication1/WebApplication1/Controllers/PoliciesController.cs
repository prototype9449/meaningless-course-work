using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using ExpressionBuilder;
using Microsoft.CSharp;
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

        // PUT: api/Policies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPolicy(int id, PolicyModel policyModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var policy = db.Predicates.Include(x => x.Groups).FirstOrDefault(x => x.id == policyModel.id);

            if (id != policyModel.id || policy == null)
            {
                return BadRequest();
            }

            var oldGroupIds = policy.Groups.Select(x => x.id);
            var newGroupIds = policyModel.GroupIds.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x));

            var groupsIdsToAdd = newGroupIds.Except(oldGroupIds);
            var groupsIdsToRemove = oldGroupIds.Except(newGroupIds);

            var groupsToAdd = db.Groups.Where(x => groupsIdsToAdd.Contains(x.id));
            var groupsToRemove = db.Groups.Where(x => groupsIdsToRemove.Contains(x.id));

            foreach (var group in groupsToAdd)
            {
                policy.Groups.Add(group);
            }
            foreach (var group in groupsToRemove)
            {
                policy.Groups.Remove(group);
            }

            policy.TableName = policyModel.TableName;
            policy.Value = policyModel.PredicateValue;
            policy.Assembly = Expressions.BuildAssemblyFromExpression(policyModel.PredicateValue);

            db.Entry(policy).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicyExists(id))
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
                Assembly = Expressions.BuildAssemblyFromExpression(policy.PredicateValue),
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

        // DELETE: api/Policies/5
        [ResponseType(typeof(PolicyModel))]
        public IHttpActionResult DeletePolicy(int id)
        {
            var predicate = db.Predicates.Find(id);
            if (predicate == null)
            {
                return NotFound();
            }

            db.Predicates.Remove(predicate);
            db.SaveChanges();

            return Ok(new PolicyModel()
            {
                id = predicate.id,
                PredicateValue = predicate.Value,
                GroupIds = string.Join(", ", predicate.Groups.ToList()),
                TableName = predicate.TableName
            });
        }

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