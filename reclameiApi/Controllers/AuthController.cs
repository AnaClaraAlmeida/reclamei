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

            if (request.TipoUsuario == null)
            {
                request.TipoUsuario = "cliente";

                tipoUser = "CLIENTE";
            }


            if (request.TipoUsuario.ToLower() == "empresa")
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

        [HttpPost("cadastro")]
        public async Task PostCadastroAsync([FromBody] CadastroRequestDto request)
        {

            if (request == null || string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Senha))
            {
                BadRequest("Login e senha são obrigatórios.");
            }

            if (request.TipoUsuario == null)
            {
                request.TipoUsuario = "cliente";
            }

            if (request.TipoUsuario.ToLower() == "empresa")
            {
                Empresa emp = new Empresa();
                emp.Login = request.Login;
                emp.Senha = request.Senha;
                emp.Nome = request.Nome;
                emp.CNPJ = "111222333444555";

                await new EmpresaDAO().InserirAsync(emp);

            }
            else
            {
                Cliente cli = new Cliente();
                cli.Login = request.Login;
                cli.Senha = request.Senha;
                cli.Nome = request.Nome;

                await new ClienteDAO().InserirAsync(cli);

            }

        }
    }

    public class LoginRequestDto
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string TipoUsuario { get; set; }
    }

    public class CadastroRequestDto
    {
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string TipoUsuario{ get; set; }
    }

    public class LoginResponseDto
    {
        public string Id { get; set; }
        public string TipoUsuario { get; set; }
    }
}
