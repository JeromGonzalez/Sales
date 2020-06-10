namespace Sales.Backend.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Backend.Models;
    using Common.Models;
    using Backend.Helpers;
    using System;

    public class ArticulosController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        public async Task<ActionResult> Index()
        {
            return View(await this.db.Articulos.OrderBy(a=> a.Descripcion).ToListAsync());
        }

        // GET: Articulos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = await this.db.Articulos.FindAsync(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return View(articulos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ArticuloVista view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Articulos";
                if (view.ArchivoImagen != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ArchivoImagen, folder);
                    pic = $"{folder}/{pic}";
                }

                var articulos = this.ToArticulo(view, pic);

                this.db.Articulos.Add(articulos);
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Articulos ToArticulo(ArticuloVista view, string pic)
        {
            return new Articulos
            {
                Descripcion = view.Descripcion,
                Activo   = view.Activo,
                RutaImagen  = pic,
                Comentario  = view.Comentario,
                FechaAlata  = view.FechaAlata ,
                PVP = view.PVP ,
                CodProducto = view.CodProducto 
            };
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = await this.db.Articulos.FindAsync(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }

            var view = this.ToVista(articulos);
            return View(view);
        }

        private ArticuloVista ToVista(Articulos art)
        {
            return new ArticuloVista
            {
                Descripcion = art.Descripcion,
                Activo = art.Activo,
                RutaImagen = art.RutaImagen,
                Comentario = art.Comentario,
                FechaAlata = art.FechaAlata,
                PVP = art.PVP,
                CodProducto = art.CodProducto
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ArticuloVista view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Articulos";
                if (view.ArchivoImagen != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ArchivoImagen, folder);
                    pic = $"{folder}/{pic}";
                }

                var articulos = this.ToArticulo(view, pic);
                this.db.Entry(articulos).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = await this.db.Articulos.FindAsync(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return View(articulos);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Articulos articulos = await this.db.Articulos.FindAsync(id);
            this.db.Articulos.Remove(articulos);
            await this.db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
