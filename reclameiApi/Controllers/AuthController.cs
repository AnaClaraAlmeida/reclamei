﻿using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<Cliente>> PostAsync([FromBody] LoginRequestDto request)
        {
            
            if (request == null || string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Senha))
            {
                return BadRequest("Login e senha são obrigatórios.");
            }

            Cliente cliente = null;
            Empresa empresa = null;


            string id = "";

            if (request.ehEmpresa)
            {
                empresa = await new EmpresaDAO().LoginAsync(request.Login, request.Senha);
                id = cliente.Id;
            } else
            {
                cliente = await new ClienteDAO().LoginAsync(request.Login, request.Senha);
                id = cliente.Id;
            }

            if (cliente == null && empresa == null)
            {
                return Unauthorized("Login ou senha inválidos.");
            }

            HttpContext.Session.SetString("UsuarioLogadoId", id);

            return Ok(cliente);
        }
    }

    public class LoginRequestDto
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool ehEmpresa { get; set; }
    }
}