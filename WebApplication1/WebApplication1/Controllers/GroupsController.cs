using DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace WebApplication1.Controllers
{
    [EnableCors("http://localhost:8080", "*", "*")]
    public class GroupsController : ApiController
    {
        private OnlineShopContext db = new OnlineShopContext();

        private IEnumerable<GroupModel> MapToList(IQueryable<Group> groups)
        {
            return groups.ToList().Select(x =>
                new GroupModel()
                {
                    id = x.id,
                    Name = x.Name,
                    EmployeeIds = string.Join(", ", x.Employees.ToList().Select(y => y.id)),
                    Description = x.Description
                });
        }

        // GET: api/Groups
        public IEnumerable<GroupModel> GetGroups(int EmployeeId = -1)
        {
            if (EmployeeId == -1)
            {
                return MapToList(db.Groups);
            }

            return MapToList(db.Groups.Include(x => x.Employees)
                .Where(x => x.Employees.Count(y => y.id == EmployeeId) != 0));
        }

        // GET: api/Groups/5
        [ResponseType(typeof(GroupModel))]
        public IHttpActionResult GetGroup(int id)
        {
            Group group = db.Groups.Include(x => x.Employees).FirstOrDefault(x => x.id == id);
            if (group == null)
            {
                return NotFound();
            }

            return Ok(new GroupModel()
            {
                id = group.id,
                Name = group.Name,
                EmployeeIds = string.Join(", ", group.Employees.ToList().Select(y => y.id)),
                Description = group.Description
            });
        }

        // PUT: api/Groups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGroup(int id, GroupModel groupModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = db.Groups.Include(x => x.Employees).FirstOrDefault(x => x.id == groupModel.id);

            if (id != groupModel.id || group == null)
            {
                return BadRequest();
            }

            var oldEmployeeIds = group.Employees.Select(x => x.id);
            var newEmployeeIds = groupModel.EmployeeIds.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x));

            var employeeIdsToAdd = newEmployeeIds.Except(oldEmployeeIds);
            var employeeIdsToRemove = oldEmployeeIds.Except(newEmployeeIds);

            var employeesToAdd = db.Employees.Where(x => employeeIdsToAdd.Contains(x.id));
            var employeesToRemove = db.Employees.Where(x => employeeIdsToRemove.Contains(x.id));
            foreach(var employee in employeesToAdd)
            {
                group.Employees.Add(employee);
            }
            foreach(var employee in employeesToRemove)
            {
                group.Employees.Remove(employee);
            }

            db.Entry(group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        [ResponseType(typeof(GroupModel))]
        public IHttpActionResult PostGroup(GroupModel groupModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeIds = groupModel.EmployeeIds.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x));
            var employees = db.Employees.Where(x => employeeIds.Contains(x.id)).ToList();

            var newGroup = new Group()
            {
                Name = groupModel.Name,
                Description = groupModel.Description,
                Employees = employees
            };
            db.Groups.Add(newGroup);
            db.SaveChanges();
            groupModel.id = newGroup.id;

            return CreatedAtRoute("DefaultApi", new { id = newGroup.id }, groupModel);
        }

        // DELETE: api/Groups/5
        [ResponseType(typeof(GroupModel))]
        public IHttpActionResult DeleteGroup(int id)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            db.Groups.Remove(group);
            db.SaveChanges();

            return Ok(new GroupModel()
            {
                id = group.id,
                Name = group.Name,
                EmployeeIds = string.Join(", ", group.Employees.ToList().Select(y => y.id)),
                Description = group.Description
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

        private bool GroupExists(int id)
        {
            return db.Groups.Count(e => e.id == id) > 0;
        }
    }
}