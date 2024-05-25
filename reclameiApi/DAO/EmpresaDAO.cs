﻿using reclameiApi.DB;
using reclameiApi.Models;
using System.Data;

namespace reclameiApi.DAO
{
    public class EmpresaDAO : BaseDao<Empresa>
    {
        public override string NomeTabela => "empresa"; //nome da minha tabela

        public override Mapa[] Mapas => new Mapa[]{  //aqui eu declaro os artibutos da minha classe
            new() { Propriedade = "Id", Campo = "id" },
            new() { Propriedade = "Nome", Campo = "nome" },
            new() { Propriedade = "CNPJ", Campo = "cnpj" },
            new() { Propriedade = "Login", Campo = "login" },
            new() { Propriedade = "Senha", Campo = "senha" }
        };
    }
}