using ApiLunesCubos.Data;
using ApiLunesCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLunesCubos.Repositories
{
    public class RepositoryCubos
    {
        private CubosContext context;
        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

        public async Task<List<Cubo>> GetCubosAsync()
        {
            List<Cubo> cubos = await this.context.Cubos.ToListAsync();
            return cubos;
        }
        public async Task<Cubo> FindCuboAsync(int idCubo)
        {
            Cubo cubo = await this.context.Cubos.FirstOrDefaultAsync(x=>x.IdCubo==idCubo);
            return cubo;
        }
        public async Task<List<Cubo>> FindCubosPorMarca(string marca)
        {
            List<Cubo> cubos = await GetCubosAsync();
            List<Cubo> cubosBuenos = new List<Cubo>();
            foreach(Cubo c in cubos)
            {
                if (c.marca.Equals(marca))
                {
                    cubosBuenos.Add(c);
                }
            }

            return cubosBuenos;
        }
        public async Task InsertarCubo(int idcubo,string nombre,string marca,string imagen,int precio)
        {
            Cubo cubo = new Cubo();
            cubo.IdCubo = GetMaxIdCubo();
            cubo.nombre = nombre;
            cubo.marca = marca;
            cubo.imagen = imagen;
            cubo.precio = precio;
            this.context.Cubos.Add(cubo);
            await this.context.SaveChangesAsync();
        }

        public async Task InsertarUsuario(int idUsuario,string nombre,string email,string pass,string imagen)
        {
            Usuario user = new Usuario();
            user.IdUsuario = GetMaxIdUsuario();
            user.Nombre = nombre;
            user.Imagen = imagen;
            user.Email = email;
            user.Pass = pass;
            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            List<Usuario> users = await this.context.Usuarios.ToListAsync();
            return users;
        }
        public async Task<Usuario>FindUsuario(int idusuario)
        {
            Usuario user = await this.context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idusuario);
            return user;
        }
        public async Task<Usuario>ExisteUser(string email,int pass)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(x => x.Email == email&&x.IdUsuario==pass);
        }


        public async Task<List<CompraCubos>> VerPedidos(int idusuario)
        {
            return await this.context.CompraCubos.Where(x => x.IdUsuario == idusuario).ToListAsync();
        }
        public async Task InsertarPedido(int idpedido, int idcubo, int idusuario, DateTime fecha)
        {
            CompraCubos compra = new CompraCubos();
            compra.IdPedido = GetMaxIdCompra();
                compra.IdCubo = idcubo;
            compra.IdUsuario = idusuario;
            compra.FechaPedido = fecha;
            this.context.CompraCubos.Add(compra);
            await this.context.SaveChangesAsync();
        }
      















        public int GetMaxIdCubo()
        {
            if (this.context.Cubos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Cubos.Max(z => z.IdCubo) + 1;
            }
        }
        public int GetMaxIdUsuario()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Usuarios.Max(z => z.IdUsuario) + 1;
            }
        }
        public int GetMaxIdCompra()
        {
            if (this.context.CompraCubos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.CompraCubos.Max(z => z.IdPedido) + 1;
            }
        }
    }
}
