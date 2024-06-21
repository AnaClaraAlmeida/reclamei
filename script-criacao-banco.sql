create database if not exists reclamei;

use reclamei;

CREATE TABLE cliente (
  id varchar(100) NOT NULL,
  nome varchar(100) NOT NULL,
  login varchar(100) DEFAULT NULL,
  senha varchar(100) DEFAULT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


CREATE TABLE empresa (
  id varchar(100) NOT NULL,
  nome varchar(100) NOT NULL,
  cnpj varchar(100) NOT NULL,
  login varchar(100) NOT NULL,
  senha varchar(100) NOT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


CREATE TABLE reclamacao (
  id varchar(100) NOT NULL,
  titulo varchar(100) NOT NULL,
  conteudo varchar(1000) NOT NULL,
  idCliente varchar(100) NOT NULL,
  idEmpresa varchar(100) NOT NULL,
  atendida tinyint(1) DEFAULT NULL,
  PRIMARY KEY (id),
  KEY reclamacao_cliente_FK (idCliente),
  KEY reclamacao_empresa_FK (idEmpresa),
  CONSTRAINT reclamacao_cliente_FK FOREIGN KEY (idCliente) REFERENCES cliente (id),
  CONSTRAINT reclamacao_empresa_FK FOREIGN KEY (idEmpresa) REFERENCES empresa (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


CREATE TABLE resposta (
  id varchar(100) NOT NULL,
  conteudo varchar(1000) NOT NULL,
  idReclamacao varchar(100) NOT NULL,
  idEmpresa varchar(100) DEFAULT NULL,
  PRIMARY KEY (id),
  KEY resposta_reclamacao_FK (idReclamacao),
  KEY resposta_empresa_FK (idEmpresa),
  CONSTRAINT resposta_empresa_FK FOREIGN KEY (idEmpresa) REFERENCES empresa (id),
  CONSTRAINT resposta_reclamacao_FK FOREIGN KEY (idReclamacao) REFERENCES reclamacao (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;



