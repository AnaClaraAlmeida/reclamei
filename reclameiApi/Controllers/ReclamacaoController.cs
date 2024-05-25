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
            var objetos = await dao.RetornarTodosAsync();

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
            objOrig.Reclamante = obj.Reclamante;
            objOrig.IdReclamante = obj.IdReclamante;
            objOrig.Reclamada = obj.Reclamada;
            objOrig.IdReclamada = obj.IdReclamada;
            objOrig.Respostas = obj.Respostas;



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
