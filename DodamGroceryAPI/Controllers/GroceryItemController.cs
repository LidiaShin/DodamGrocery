using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using DodamGroceryAPI.Models;

namespace DodamGroceryAPI.Controllers
{
    public class GroceryItemController : ApiController
    {
        private GroceryItemData db = new GroceryItemData();

        //GET: api/GroceryItem
        //public IQueryable<GroceryItem> GetGroceryItems()
        //{
        //    return db.GroceryItems;
        //}

        // GET: api/GroceryItem/5
        [ResponseType(typeof(GroceryItem))]
        public IHttpActionResult GetGroceryItem(int id)
        {
            GroceryItem groceryItem = db.GroceryItems.Find(id);
            if (groceryItem == null)
            {
                return NotFound();
            }

            return Ok(groceryItem);
        }


        [ResponseType(typeof(GroceryItem))]
        [HttpGet]
        public HttpResponseMessage ItemByType(string type = "All")
        {
            using (GroceryItemData items = new GroceryItemData())
            {
                switch (type.ToLower())
                {
                    case "fruit":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            items.GroceryItems.Where(e => e.ItemType.ToLower() == "fruit").ToList());

                    case "bakery":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            items.GroceryItems.Where(e => e.ItemType.ToLower() == "bakery").ToList());

                    case "vegitable":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            items.GroceryItems.Where(e => e.ItemType.ToLower() == "vegitable").ToList());

                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, items.GroceryItems.ToList());
                            

                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "please try again");

                }

            }
        }


        // PUT: api/GroceryItem/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGroceryItem(int id, GroceryItem groceryItem)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != groceryItem.ID)
            {
                return BadRequest();
            }

            db.Entry(groceryItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryItemExists(id))
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

        // POST: api/GroceryItem
        [ResponseType(typeof(GroceryItem))]
        public IHttpActionResult PostGroceryItem(GroceryItem groceryItem)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.GroceryItems.Add(groceryItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = groceryItem.ID }, groceryItem);
        }

        // DELETE: api/GroceryItem/5
        [ResponseType(typeof(GroceryItem))]
        public IHttpActionResult DeleteGroceryItem(int id)
        {
            GroceryItem groceryItem = db.GroceryItems.Find(id);
            if (groceryItem == null)
            {
                return NotFound();
            }

            db.GroceryItems.Remove(groceryItem);
            db.SaveChanges();

            return Ok(groceryItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroceryItemExists(int id)
        {
            return db.GroceryItems.Count(e => e.ID == id) > 0;
        }
    }
}