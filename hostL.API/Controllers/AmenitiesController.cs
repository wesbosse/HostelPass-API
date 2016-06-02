﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using hostL.API.Infrastructure;
using hostL.API.Models;

namespace hostL.API.Controllers
{
    public class AmenitiesController : ApiController
    {
        private HostLDataContext db = new HostLDataContext();

        // GET: api/Amenities
        public IQueryable<Amenity> GetAmenities()
        {
            return db.Amenities;
        }

        // GET: api/Amenities/5
        [ResponseType(typeof(Amenity))]
        public IHttpActionResult GetAmenity(int id)
        {
            Amenity amenity = db.Amenities.Find(id);
            if (amenity == null)
            {
                return NotFound();
            }

            return Ok(amenity);
        }

        // PUT: api/Amenities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAmenity(int id, Amenity amenity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != amenity.AmenityId)
            {
                return BadRequest();
            }

            db.Entry(amenity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmenityExists(id))
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

        // POST: api/Amenities
        [ResponseType(typeof(Amenity))]
        public IHttpActionResult PostAmenity(Amenity amenity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Amenities.Add(amenity);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = amenity.AmenityId }, amenity);
        }

        // DELETE: api/Amenities/5
        [ResponseType(typeof(Amenity))]
        public IHttpActionResult DeleteAmenity(int id)
        {
            Amenity amenity = db.Amenities.Find(id);
            if (amenity == null)
            {
                return NotFound();
            }

            db.Amenities.Remove(amenity);
            db.SaveChanges();

            return Ok(amenity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AmenityExists(int id)
        {
            return db.Amenities.Count(e => e.AmenityId == id) > 0;
        }
    }
}