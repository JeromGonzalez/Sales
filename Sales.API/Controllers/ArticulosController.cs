namespace Sales.API.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Common.Models;
    using Domain.Models;

    public class ArticulosController : ApiController
    {
        private DataContext db = new DataContext();

        [HttpGet]
        public IQueryable<Articulos> GetArticulos()
        {
            return this.db.Articulos.OrderBy(p => p.Descripcion);
        }

        // GET: api/Articulos/5
        [ResponseType(typeof(Articulos))]
        public async Task<IHttpActionResult> GetArticulos(int id)
        {
            Articulos articulos = await this.db.Articulos.FindAsync(id);
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

            this.db.Entry(articulos).State = EntityState.Modified;

            try
            {
                await this.db.SaveChangesAsync();
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
            articulos.Activo = true;
            articulos.FechaAlata = DateTime.Now.ToUniversalTime(); 

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.db.Articulos.Add(articulos);
            await this.db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = articulos.CodProducto }, articulos);
        }

        // DELETE: api/Articulos/5
        [ResponseType(typeof(Articulos))]
        public async Task<IHttpActionResult> DeleteArticulos(int id)
        {
            Articulos articulos = await this.db.Articulos.FindAsync(id);
            if (articulos == null)
            {
                return NotFound();
            }

            this.db.Articulos.Remove(articulos);
            await this.db.SaveChangesAsync();

            return Ok(articulos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticulosExists(int id)
        {
            return this.db.Articulos.Count(e => e.CodProducto == id) > 0;
        }
    }
}