using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LabWeb.Data;
using LabWeb.Models;

namespace LabWeb.Controllers
{
    public class ProductController : ApiController
    {
        ProductDAO productoDAO;

        public ProductController()
        {
            productoDAO = new ProductDAO();
        }

        // GET: api/Product/5
        [HttpGet]
        public IHttpActionResult getProducts()
        {
            return Ok(productoDAO.getProductos());
        }

        //// PUT: api/Product/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutProducts(int id, Products products)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != products.ProductID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(products).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductsExists(id))
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

        //// POST: api/Product
        //[ResponseType(typeof(Products))]
        //public IHttpActionResult PostProducts(Products products)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Products.Add(products);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = products.ProductID }, products);
        //}

        //// DELETE: api/Product/5
        //[ResponseType(typeof(Products))]
        //public IHttpActionResult DeleteProducts(int id)
        //{
        //    Products products = db.Products.Find(id);
        //    if (products == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Products.Remove(products);
        //    db.SaveChanges();

        //    return Ok(products);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool ProductsExists(int id)
        //{
        //    return db.Products.Count(e => e.ProductID == id) > 0;
        //}
    }
}