using ApiLunesCubos.Models;
using ApiLunesCubos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiLunesCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private RepositoryCubos repo;
        public UsuariosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            return await repo.GetUsuariosAsync();
        }
        [HttpPost]
        public async Task<ActionResult> InsertarUsuario(Usuario usuario)
        {
            await this.repo.InsertarUsuario(usuario.IdUsuario,usuario.Nombre,usuario.Email,usuario.Pass,usuario.Imagen);
            return Ok();
        }
        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> PerfilUsuario()
        {
            Claim claim = HttpContext.User.Claims
               .SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario=
                claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>
                (jsonUsuario);
            return usuario;
        }
        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<CompraCubos>>>VerPedidos()
        {
            Claim claim = HttpContext.User.Claims
               .SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario=
                claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>
                (jsonUsuario);
            List<CompraCubos> pedidos = await this.repo.VerPedidos(usuario.IdUsuario);
            return pedidos;
        }
        [Authorize]
        [HttpPost("[action]")]
      
        public async Task<ActionResult>RealizarPedido(ModelPedidoPost model)
        {
            Claim claim = HttpContext.User.Claims
               .SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario=
                claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>
                (jsonUsuario);
            DateTime now = DateTime.Now;
            await this.repo.InsertarPedido(1, model.idcubo, usuario.IdUsuario, now);
            return Ok(); ;
        }
    }
}
