# Projeto Final Academia .Net
## Sistema de Controle de Manutenção Periódica de Computadores em Locais Empresariais com controle de usuários

### Link do Vídeo de Desenvolvimento Parcial (~10 min)

[Vídeo do Youtube](https://youtu.be/q3-a63u3tsk)

### Motivo

Considerando que muitas vezes as equipes de TI nos setores empresariais não são o suficiente para a quantidade de computadores existentes, com demandas excedentes, acaba-se que por vezes as manutenções períodicas são deixadas de lado. Isso resulta em uma onda de computadores com problemas por falta de manutenções preventivas. Neste cenário, vi a oportunidade de desenvolver um sistema de controle de manutenções periódicas. Com o objetivo de fazer o registro do procedimento executado (Limpeza Completa, Troca de Pasta Térmica, etc...), também quais os computadores das determinadas seções já foram manutenidos e quais ainda não foram, como controle de data e tempo, podendo ter um controle maior sobre os computadores e podendo previnir alguma perda maior.

### Tecnologias Utilizadas

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap
- Razor Pages
- ASP.NET Core Identity

### Sobre a aplicação

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



### Autor

[Gabriel Castagna](www.github.com/castagnagh)
