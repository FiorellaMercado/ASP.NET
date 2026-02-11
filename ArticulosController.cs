using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using TechCity.Entidades;
using Microsoft.EntityFrameworkCore;
using System;


namespace TechCity
{
    [ApiController]
    [Route("api/articulos")]
    public class ArticulosController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public ArticulosController(ApplicationDBContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Articulo>>> Get()
        {
            return await context.Articulos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Articulo articulo)
        {
            var existeNombreArticulo = await context.Articulos.AnyAsync(x => x.Nombre == articulo.Nombre);
            if (existeNombreArticulo)
            {
                return BadRequest($"va exite un articulo con el nombre {articulo.Nombre}");
            }

            context.Add(articulo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{Id:int}")] // api/articulo/1

        public async Task<ActionResult> Put(Articulo articulo, int Id)
        {
            if (articulo.Id != Id)
            {
                return BadRequest("El id del articulo no coincide con el id de la URL");
            }

            var existe = await context.Articulos.AnyAsync(x => x.Id == Id);
            if (!existe)
            {
                return NotFound();
            }

            context.Update(articulo);
            await context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{Id:int}")] // api/articulos/1

        public async Task<ActionResult> Delete(int Id)
        {
            var existe = await context.Articulos.AnyAsync(x => x.Id == Id);
            if (!existe){
                return NotFound();
            }
            context.Remove(new Articulo() { Id = Id });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
