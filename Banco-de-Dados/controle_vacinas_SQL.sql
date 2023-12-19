create database controle_vacinas;

create table secretaria
(
	id INTEGER PRIMARY KEY IDENTITY,
	nome VARCHAR(100) NOT NULL,
	username VARCHAR(60) NOT NULL,
	senha VARCHAR(60) NOT NULL
);

create table endereco
(
	id INTEGER PRIMARY KEY IDENTITY,
	cep VARCHAR(80) NOT NULL,
	rua VARCHAR(80) NOT NULL,
	numero VARCHAR(10) NOT NULL,
	bairro VARCHAR(80) NOT NULL,
	cidade VARCHAR(80) NOT NULL,
	estado VARCHAR(80) NOT NULL,
	complemento VARCHAR(80) NOT NULL
);

create table saude_posto
(
	id INTEGER PRIMARY KEY IDENTITY,
	nome VARCHAR(100) NOT NULL,
	username VARCHAR(60) NOT NULL,
	senha VARCHAR(60) NOT NULL,
	fk_endereco INTEGER,
	fk_secretaria INTEGER,
	FOREIGN KEY (fk_endereco) REFERENCES endereco(id),
	FOREIGN KEY (fk_secretaria) REFERENCES secretaria(id)
);

create table vacinador
(
	id INTEGER PRIMARY KEY IDENTITY,
	nome VARCHAR(100) NOT NULL,
	username VARCHAR(60) NOT NULL,
	senha VARCHAR(60) NOT NULL,
	fk_posto_saude INTEGER,
	FOREIGN KEY (fk_posto_saude) references saude_posto(id)
);

create table vacina
(
	id INTEGER PRIMARY KEY IDENTITY,
	nome VARCHAR(60) NOT NULL,
	descricao VARCHAR(100) NOT NULL,
	lote VARCHAR(30) NOT NULL,
	qtd_estoque INTEGER NOT NULL,
	validade DATE NOT NULL
);

create table vacinacao_sala
(
	id INTEGER PRIMARY KEY IDENTITY,
	qtd_disponivel INTEGER NOT NULL,
	horario_abertura TIME,
	horario_fechamento TIME,
	fk_vacinador INTEGER,
	fk_vacina INTEGER,
	fk_posto INTEGER,
	FOREIGN KEY (fk_vacinador) REFERENCES vacinador(id),
	FOREIGN KEY (fk_vacina) REFERENCES vacina(id),
	FOREIGN KEY (fk_posto) REFERENCES saude_posto(id)
);

CREATE LOGIN usuarioVacina WITH PASSWORD='senha123#';
CREATE USER usuarioVacina FROM LOGIN usuarioVacina;
EXEC sp_addrolemember 'DB_DATAREADER', 'usuarioVacina';
EXEC sp_addrolemember 'DB_DATAWRITER', 'usuarioVacina';