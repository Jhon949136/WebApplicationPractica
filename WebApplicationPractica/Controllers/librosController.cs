using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class librosController : ControllerBase
    {
        private readonly BibliotecaContext _context;
        public librosController(BibliotecaContext context)
        {
            _context = context;


        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            var libros = (from l in _context.libro
                          join a in _context.autor on l.autorid equals a.id_autor
                          select new
                          {
                              l.id_libro,
                              l.titulo,
                              l.aniopublicacion,
                              l.categoriaid,
                              l.resumen,
                              Autor = a.nombre
                          }).ToList();

            if (libros.Count == 0)
            {
                return NotFound();
            }
            return Ok(libros);
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            var libro = (from l in _context.libro
                         where l.id_libro == id
                         select l).FirstOrDefault();
            if (libro == null)
            {
                return NotFound();
            }
            return Ok(libro);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarLibro([FromBody] Libro libro)
        {
            try
            {
                _context.libro.Add(libro);
                _context.SaveChanges();
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarLibro(int id, [FromBody] Libro libroModificar)
        {
            var libroActual = _context.libro.Find(id);
            if (libroActual == null)
            {
                return NotFound();
            }

            libroActual.titulo = libroModificar.titulo;
            libroActual.aniopublicacion = libroModificar.aniopublicacion;
            libroActual.autorid = libroModificar.autorid;
            libroActual.categoriaid = libroModificar.categoriaid;
            libroActual.resumen = libroModificar.resumen;

            _context.Entry(libroActual).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(libroModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarLibro(int id)
        {
            var libro = _context.libro.Find(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.libro.Remove(libro);
            _context.SaveChanges();

            return Ok(libro);
        }
    }
}
