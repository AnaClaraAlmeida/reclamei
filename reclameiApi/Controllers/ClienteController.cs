using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reclameiApi.DAO;
using reclameiApi.Models;

namespace reclameiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly ClienteDAO dao;
        public ClienteController()
        {
            dao = new ClienteDAO();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAsync()
        {
            var objetos = await dao.RetornarTodosAsync();

            if (objetos == null)
                return NotFound();

            return Ok(objetos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetId(string id)
        {
            var obj = await dao.RetornarPorIdAsync(id);

            if (obj == null)
                return NotFound();

            return obj;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostAsync(Cliente obj)
        {
            await dao.InserirAsync(obj);

            return CreatedAtAction(
                nameof(GetId),
                new { id = obj.Id },
                obj
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, Cliente obj)
        {
            if (id != obj.Id)
                return BadRequest();

            var objOrig = await dao.RetornarPorIdAsync(id); //esse await é pq to puxando os dados

            if (objOrig == null)
                return NotFound();

         
            objOrig.Nome = obj.Nome;
            objOrig.Login = obj.Login;
            objOrig.Senha = obj.Senha;
            

            await dao.AlterarAsync(obj); //esse await é pq to retornando os dados do banco

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var obj = await dao.RetornarPorIdAsync(id);

            if (obj == null)
                return NotFound();

            await dao.ExcluirAsync(id);

            return NoContent();
        }

    }
}
