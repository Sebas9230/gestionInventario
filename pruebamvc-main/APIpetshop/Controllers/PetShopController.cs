using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIpetshop.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APIpetshop.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PetShopController : ControllerBase
    {

        private readonly ApplicationDBContext _db;
        protected ResultadoApi _resultadoApi;



        public PetShopController(ApplicationDBContext db)
        {
            _db = db;
            _resultadoApi = new();
        }


        // GET: api/PetShop
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {

            //return Ok(Utils.Util.productos);
            var productos = await _db.productos.ToListAsync();
            

            if (productos != null)
            {
                _resultadoApi.listaProductos = productos;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }

            return Ok(_resultadoApi);
        }

        // GET: api/PetShop/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            //Producto producto = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            Producto producto = await _db.productos.FirstOrDefaultAsync(x => x.id.Equals(id));
            if (producto != null)
            {
                _resultadoApi.producto = producto;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        // POST: api/PetShop
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Producto producto)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(producto.codigo));
            Producto producto1 = await _db.productos.FirstOrDefaultAsync(x => x.id.Equals(producto.id));
            if (producto1 == null)
            {
                await _db.productos.AddAsync(producto);
                await _db.SaveChangesAsync();
                //Utils.Util.productos.Add(producto);
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }

        }

        // PUT: api/PetShop/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Producto producto)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            Producto producto1 = await _db.productos.FirstOrDefaultAsync(x => x.id.Equals(id));

            if (producto1 != null) {
                producto1.name = producto.name != null ? producto.name : producto1.name;
                producto1.price = producto.price != null ? producto.price : producto1.price;
                producto1.quantity = producto.quantity != null ? producto.quantity : producto1.quantity;
                _db.Update(producto1);
                await _db.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        // DELETE: api/PetShop/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            Producto producto1 = await _db.productos.FirstOrDefaultAsync(x => x.id.Equals(id));
            if (producto1 != null)
            {
                _db.productos.Remove(producto1);
                await _db.SaveChangesAsync();
                //Utils.Util.productos.Remove(producto1);
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }
    }
}
