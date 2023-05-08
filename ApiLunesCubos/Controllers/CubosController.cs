using ApiLunesCubos.Models;
using ApiLunesCubos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLunesCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase
    {
        private RepositoryCubos repo;
        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Cubo>>> Get()
        {
            return await repo.GetCubosAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Cubo>> FindPersonaje(int id)
        {
            return await repo.FindCuboAsync(id);
        }
        [HttpGet]
        [Route("[action]/{marca}")]
        public async Task<ActionResult<List<Cubo>>>GetCubosMarca(string marca)
        {
            return await repo.FindCubosPorMarca(marca);
        }
        [HttpPost]
        public async Task<ActionResult> InsertarCubo(Cubo cubo)
        {
            await this.repo.InsertarCubo(cubo.IdCubo,cubo.nombre,cubo.marca,cubo.imagen,cubo.precio);
            return Ok();
        }
    }
}
