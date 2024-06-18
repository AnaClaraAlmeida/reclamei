using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reclameiApi.DAO;
using reclameiApi.Models;

namespace reclameiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReclamacaoController : Controller
    {
        private readonly ReclamacaoDAO dao;
        public ReclamacaoController()
        {
            dao = new ReclamacaoDAO();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reclamacao>>> GetAsync()
        {
            var objetos = await dao.GetAllAsync();

            if (objetos == null)
                return NotFound();

            return Ok(objetos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reclamacao>> GetId(string id)
        {
            var obj = await dao.RetornarPorIdAsync(id);

            if (obj == null)
                return NotFound();

            return obj;
        }

        [HttpGet("Empresa/{id}")]
        public async Task<ActionResult<IEnumerable<Reclamacao>>> GetByEmpresaId(string id)
        {
            var objs = await dao.GetAllByEmpresaAsync(id);

            return objs;
        }

        [HttpGet("Cliente/{id}")]
        public async Task<ActionResult<IEnumerable<Reclamacao>>> GetByClienteId(string id)
        {
            var objs = await dao.GetAllByClienteAsync(id);

            return objs;
        }

        [HttpPost]
        public async Task<ActionResult<Reclamacao>> PostAsync(Reclamacao obj)
        {
            await dao.InserirAsync(obj);

            return CreatedAtAction(
                nameof(GetId),
                new { id = obj.Id },
                obj
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, Reclamacao obj)
        {
            if (id != obj.Id)
                return BadRequest();

            var objOrig = await dao.RetornarPorIdAsync(id); //esse await é pq to puxando os dados

            if (objOrig == null)
                return NotFound();


            objOrig.Titulo = obj.Titulo;
            objOrig.Conteudo = obj.Conteudo;
            objOrig.Atendida = obj.Atendida;
            //objOrig.Cliente = obj.Cliente;
            objOrig.IdCliente = obj.IdCliente;
            //objOrig.Empresa = obj.Empresa;
            objOrig.IdEmpresa = obj.IdEmpresa;
            //objOrig.Respostas = obj.Respostas;



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
