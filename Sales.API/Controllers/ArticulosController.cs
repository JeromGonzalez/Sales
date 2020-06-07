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
using System.Web.Http.Description;
using Sales.Common.Models;
using Sales.Domain.Models;

namespace Sales.API.Controllers
{
    public class ArticulosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Articulos
        public IQueryable<Articulos> GetArticulos()
        {
            return db.Articulos;
        }

        // GET: api/Articulos/5
        [ResponseType(typeof(Articulos))]
        public async Task<IHttpActionResult> GetArticulos(int id)
        {
            Articulos articulos = await db.Articulos.FindAsync(id);
            if (articulos == null)
            {
                return NotFound();
            }

            return Ok(articulos);
        }

        // PUT: api/Articulos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArticulos(int id, Articulos articulos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articulos.CodProducto)
            {
                return BadRequest();
            }

            db.Entry(articulos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticulosExists(id))
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

        // POST: api/Articulos
        [ResponseType(typeof(Articulos))]
        public async Task<IHttpActionResult> PostArticulos(Articulos articulos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Articulos.Add(articulos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = articulos.CodProducto }, articulos);
        }

        // DELETE: api/Articulos/5
        [ResponseType(typeof(Articulos))]
        public async Task<IHttpActionResult> DeleteArticulos(int id)
        {
            Articulos articulos = await db.Articulos.FindAsync(id);
            if (articulos == null)
            {
                return NotFound();
            }

            db.Articulos.Remove(articulos);
            await db.SaveChangesAsync();

            return Ok(articulos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticulosExists(int id)
        {
            return db.Articulos.Count(e => e.CodProducto == id) > 0;
        }
    }
}