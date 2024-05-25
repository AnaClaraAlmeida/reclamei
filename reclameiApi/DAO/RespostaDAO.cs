﻿using reclameiApi.Models;

namespace reclameiApi.DAO
{
    public class RespostaDAO : BaseDao<Resposta>
    {
        public override string NomeTabela => "reclamacao"; //nome da minha tabela

        public override Mapa[] Mapas => new Mapa[]{  //aqui eu declaro os artibutos da minha classe
            new() { Propriedade = "Id", Campo = "id" },
            new() { Propriedade = "Conteudo", Campo = "conteudo" },
            new() { Propriedade = "IdReclamacao", Campo = "id_reclamacao" },
            new() { Propriedade = "IdCliente", Campo = "id_cliente" },
            new() { Propriedade = "IdEmpresa", Campo = "id_empresa" }
        };
    }
}
