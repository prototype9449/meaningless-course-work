﻿using System;
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
    public class GroupsController : ApiController
    {
        private OnlineShopContext db = new OnlineShopContext();

        // GET: api/Groups
        public IQueryable<Group> GetGroups()
        {
            return db.Groups;
        }

        // GET: api/Groups/5
        [ResponseType(typeof(Group))]
        public async Task<IHttpActionResult> GetGroup(int id)
        {
            Group group = await db.Groups.Where(x => x.id == id).Include(x => x.Employees).FirstOrDefaultAsync();
            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        // PUT: api/Groups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGroup(int id, Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.id)
            {
                return BadRequest();
            }

            db.Entry(group).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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
        [ResponseType(typeof(Group))]
        public async Task<IHttpActionResult> PostGroup(Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Groups.Add(group);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = group.id }, group);
        }

        // DELETE: api/Groups/5
        [ResponseType(typeof(Group))]
        public async Task<IHttpActionResult> DeleteGroup(int id)
        {
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            db.Groups.Remove(group);
            await db.SaveChangesAsync();

            return Ok(group);
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