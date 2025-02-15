using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class autoresController : ControllerBase
    {
        private readonly BibliotecaContext _context;
        public autoresController(BibliotecaContext context)
        {
            _context = context;


        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            var autores = (from a in _context.autor
                           select new
                           {
                               a.id_autor,
                               a.nombre,
                               a.nacionalidad
                           }).ToList();

            if (autores.Count == 0)
            {
                return NotFound();
            }
            return Ok(autores);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            var autor = (from a in _context.autor
                         where a.id_autor == id
                         select a).FirstOrDefault();
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByName(string filtro)
        {
            var autor = (from a in _context.autor
                         where a.nombre.Contains(filtro)
                         select a).FirstOrDefault();
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarAutor([FromBody] Autor autor)
        {
            try
            {
                _context.autor.Add(autor);
                _context.SaveChanges();
                return Ok(autor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarAutor(int id, [FromBody] Autor autorModificar)
        {
            var autorActual = _context.autor.Find(id);
            if (autorActual == null)
            {
                return NotFound();
            }

            autorActual.nombre = autorModificar.nombre;
            autorActual.nacionalidad = autorModificar.nacionalidad;

            _context.Entry(autorActual).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(autorModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarAutor(int id)
        {
            var autor = _context.autor.Find(id);
            if (autor == null)
            {
                return NotFound();
            }

            _context.autor.Remove(autor);
            _context.SaveChanges();

            return Ok(autor);
        }
    }
}
