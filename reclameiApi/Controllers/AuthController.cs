using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reclameiApi.DAO;
using reclameiApi.Models;

namespace reclameiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        

        public AuthController()
        {
        
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> PostAsync([FromBody] LoginRequestDto request)
        {
            
            if (request == null || string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Senha))
            {
                return BadRequest("Login e senha são obrigatórios.");
            }

            Cliente cliente = null;
            Empresa empresa = null;


            string id = "";
            string tipoUser = "";

            if (request.ehEmpresa)
            {
                empresa = await new EmpresaDAO().LoginAsync(request.Login, request.Senha);

                if (empresa == null)
                {
                    return Unauthorized("Login ou senha inválidos.");
                }

                id = empresa.Id;
                tipoUser = "EMPRESA";
            } else
            {
                cliente = await new ClienteDAO().LoginAsync(request.Login, request.Senha);

                if (cliente == null)
                {
                    return Unauthorized("Login ou senha inválidos.");
                }

                id = cliente.Id;
                tipoUser = "CLIENTE";
            }

            if (cliente == null && empresa == null)
            {
                return Unauthorized("Login ou senha inválidos.");
            }

            HttpContext.Session.SetString("UsuarioLogadoId", id);

            LoginResponseDto responseDto = new LoginResponseDto();
            responseDto.Id = id;
            responseDto.TipoUsuario = tipoUser;

            return Ok(responseDto);
        }
    }

    public class LoginRequestDto
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool ehEmpresa { get; set; }
    }

    public class LoginResponseDto
    {
        public string Id { get; set; }
        public string TipoUsuario { get; set; }
    }
}
