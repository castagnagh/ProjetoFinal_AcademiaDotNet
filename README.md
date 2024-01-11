# ✍🏻 Projeto Final Academia .Net
## Sistema de Controle de Manutenção Periódica de Computadores em Locais Empresariais com controle de usuários

### 📹 Link do Vídeo de Desenvolvimento Parcial (~10 min)

[Vídeo do Youtube](https://youtu.be/q3-a63u3tsk)

## 📓 Motivo

Considerando que muitas vezes as equipes de TI nos setores empresariais não são o suficiente para a quantidade de computadores existentes, com demandas excedentes, acaba-se que por vezes as manutenções períodicas são deixadas de lado. Isso resulta em uma onda de computadores com problemas por falta de manutenções preventivas. Neste cenário, vi a oportunidade de desenvolver um sistema de controle de manutenções periódicas. Com o objetivo de fazer o registro do procedimento executado (Limpeza Completa, Troca de Pasta Térmica, etc...), também quais os computadores das determinadas seções já foram manutenidos e quais ainda não foram, como controle de data e tempo, podendo ter um controle maior sobre os computadores e podendo previnir alguma perda maior.



## 🖥 Tecnologias Utilizadas

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap
- Razor Pages
- ASP.NET Core Identity



## 🏷️ Sobre a aplicação

 A aplicação é capaz de gerenciar o registro de todos os computadores, notebooks e tablets em um ambiente corporativo, além de permitir o registro das manutenções realizadas nesses equipamentos. Utilizando o ASP.NET Core Identity, a aplicação pode gerenciar perfis de acesso e contas de usuários.
 
 >  Para facilitar o cadastro de um computador e futuramente o registro de manutenção do mesmo. Foram criadas as tabelas: 
 
 - Marcas (Dell, HP, Positivo, etc...)
 - Tipo (Desktop, Notebook, Tablet, etc...)
 - Computadores
 - Procedimentos (Limpeza memória, Aplicação de pasta térmica, etc...)
 - Seções (RH, Acessoria Jurídica, etc...)
 - Registro de Manutenção (N:N de Computadores e Procedimentos)

 #### Tabelas utilizadas com os recursos do ASP.NET Core Identity

- Roles (Funções)
- Users (Usuários)

### A aplicação oferece as seguintes funcionalidades para as respectivas tabelas:

#### Criar, Editar e Deletar:

- Marcas
- Tipo
- Procedimentos
- Seções
- Roles (Funções)
- Users (Usuários)

#### Criar, Detalhes, Editar e Deletar:

- Computadores
- Registro de Manutenção

### Criação de usuários

Ao executar o projeto pela primeira vez, os usuários são criados:
> Admin:
> - Email: admin@localhost
> - Senha: Admin123!

> User:
> - Email: usuario@localhost
> - Senha: Usuario123!

O Admin tem permissão para realizar tudo!

O User tem permissão apenas para visualizar as Index e Cadastrar

## 👨🏼‍💻 Como instalar
#### SQL Server (Criando o Banco de dados)
Copie esse comando abaixo e cole no seu SQL Server. Execute conforme as orientações:

>--Primeiro comando  
>CREATE DATABASE registroManutencao;
>
>-- Segundo comando  
>USE registroManutencao;
>
>--Terceiro comando  
>CREATE LOGIN usuarioManutencao WITH PASSWORD='senha123#';
>
>--Quarto comando    
>CREATE USER usuarioManutencao FROM LOGIN usuarioManutencao;
>
>--Quinto comando    
>EXEC sp_addrolemember 'DB_DATAREADER', 'usuarioManutencao';     
>EXEC sp_addrolemember 'DB_DATAWRITER', 'usuarioManutencao';     
>EXEC sp_addrolemember 'DB_OWNER', 'usuarioManutencao';  

#### Visual Studio (Alterando String de Conexão)

Caso você esteja utilizando SQL Developer, mantenha a string de conexão como está.

Caso você esteja utilizando SQL Express troque a string de conexão encontrada no arquivo `appsettigns.json` por:

> "ConexaoBanco": "Data Source=.\\SQLEXPRESS;Initial Catalog=registroManutencao;User ID=usuarioManutencao;Password=senha123#;language=Portuguese;Trusted_Connection=True;TrustServerCertificate=True;"

#### Execução
Após estas configurações o programa está apto para ser executado!


## 📋 Minhas considerações

O objetivo foi alcançado, levando em consideração todo o meu trajeto até aqui   
MVP (Produto Viável Mínimo) contém tudo que é viável para usar o produto

#### #Melhorias

- Um ponto a ser melhorado é o modo como os procedimentos são incluidos no registro de manutenções, sendo apenas um procedimento.
A melhoria a ser aplicada é ter a possibilidade de adicionar vários procedimentos. No ínicio programei justamente para que fosse apenas um procedimento cadastrado por registro. Exemplo:   
1 - Limpeza completa (Troca de pasta térmica, Alcool isopropilico, memória)     
2 - Limpeza memória

Depois de pronto, percebo que adicionar separadamente os procedimentos seria melhor, mas isso não impede seu funcionamento.

- Outra melhoria relevante seria ajustar melhor o front-end, deixa mais intuitivo ao usuário as páginas de edição, cadastrar e visualização.

## 👨🏻‍🦱 Autor

[Gabriel Castagna](www.github.com/castagnagh)
