using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Models.Almacen.Articulo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ArticulosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Articulos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloViewModel>> Listar()
        {
            var articulo = await _context.Articulos.Include(a => a.categoria).ToListAsync();

            return articulo.Select(a => new ArticuloViewModel
            {
                idarticulo = a.idarticulo,
                idcategoria = a.idcategoria,
                categoria = a.categoria.nombre,
                codigo = a.codigo,
                nombre = a.nombre,
                stock = a.stock,
                precio_venta = a.precio_venta,
                descripcion = a.descripcion,
                condicion = a.condicion
            });
        }

        // GET: api/Articulos/Mostrar/5

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ArticuloViewModel>> Mostrar([FromRoute] int id)
        {
            var articulo = await _context.Articulos.Include(c=>c.categoria)
                .SingleOrDefaultAsync(a=>a.idarticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            return new ArticuloViewModel
            {
                idarticulo = articulo.idarticulo,
                idcategoria = articulo.idcategoria,
                categoria = articulo.categoria.nombre,
                codigo = articulo.codigo,
                nombre = articulo.nombre,
                stock = articulo.stock,
                precio_venta = articulo.precio_venta,
                descripcion = articulo.descripcion,
                condicion = articulo.condicion
            };
        }

        // POST: api/Categorias/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult<Articulo>> Crear(CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Articulo articulo = new Articulo
            {
                idcategoria = model.idcategoria,
                codigo = model.codigo,
                nombre = model.nombre,
                stock = model.stock,
                precio_venta = model.precio_venta,
                descripcion = model.descripcion,
                condicion = true
            };

            _context.Articulos.Add(articulo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {

                return BadRequest();
            }



            return Ok();
        }

        // PUT: api/Articulos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar(ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idcategoria <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.idarticulo == model.idarticulo);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.idcategoria = model.idcategoria;
            articulo.codigo = model.codigo;
            articulo.nombre = model.nombre;
            articulo.precio_venta = model.precio_venta;
            articulo.stock = model.stock;
            articulo.descripcion = model.descripcion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar excepcion
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Articulos/Activar/id
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.idarticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.condicion = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar excepcion
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Articulos/Desactivar/id
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.idarticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.condicion = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar excepcion
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Articulos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Categoria>> Eliminar(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }

            _context.Articulos.Remove(articulo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch 
            {
                return BadRequest();
            }


            return Ok(articulo);
        }

        private bool ArticuloExists(int id)
        {
            return _context.Articulos.Any(e => e.idarticulo == id);
        }
    }
}
