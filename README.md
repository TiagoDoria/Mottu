# DESAFIO BACKEND MOTTU

O projeto "Mottu" foi desenvolvido como desafio para a empresa Mottu, concorrendo a vaga de Desenvolvedor BackEnd.
O passo a passo para executar o sistema será descrito no final desse documento.

### Observações importantes:
 - Alguns pontos solicitados no desafio não foram implementados, são eles: Criação do serviço que monitora em tempo real cadastro de pedidos e enviados via mensageria para os entregadores, por falta de tempo. =(
 - Algumas funcionalidades não foram desenvolvidas, pois não constavam no desafio, para economizar tempo de focar nos requisitos principais. Exemplo: CRUD completo de Locação, Pedidos e usuários. =(
 - Testes estão incompletos, na API de Pedidos não foi criado os testes devido a faltar requisitos que não foram desenvolvidos. =(
 - IMPORTANTE: No cadastro do usuário é possível escolher o tipo de usuário(ADMIN OU DELIVERYMAN) adicionado essa funcionalidade apenas para facilitar os testes pela empresa, em uma situação normal o usuário não tem essa opção. =)

## O sistema foi feito utilizando as tecnologias:
1) .NET Core 8;
2) C#
3) PostgreSQL;
4) Entity Framework
5) Aplicativo WEB do ASP NET Core MVC para o FrontEnd;
6) Bootstrap 5
7) XUnit para Testes

## Como foi desenvolvido

O sistema foi desenvolvido utilizando arquitetura de Micro Serviços, separados em: 
1) Serviço de autenticação (AuthAPI)
2) Serviço de Locação (LocationAPI)
3) Serviço de Motos (MotorcycleAPI)
4) Serviço de Pedidos (OrderAPI)

Foi criado também alguns testes unitários utilizando XUnit, cada API tem uma pasta chamada "Tests" contendo  o arquivo de testes.

## AuthAPI
Serviço responsável pelo gerenciamento de usuários. A API utiliza o Identity no ASP NET Core para realização de cadastro e login.

## LocationAPI
Serviço responsável pelo gerenciamento de locação de motos pelos entregadores. Responsável pelas operações CRUD e cálculo do preço da locação.

## MotorcycleAPI
Serviço responsável pelo gerenciamento de motos. Responsável pelas operações CRUD, validação de motos disponíveis ou não para locação, verificar motos existentes disponíveis para 
os entregadores etc

## OrderAPI
Serviço responsável pelo gerenciamento de pedidos cadastrados pelos admins e permite que entregadores possam aceitar pedidos que estejam disponíveis. 

## Banco de Dados
O banco foi realizado utilizando o PostgreSQL, onde cada API possui seu banco específico:
- AuthAPI = auth_api
- LocationAPI = location_api
- MotorcycleAPI = motorcycle_api
- OrderAPI = order_api

Para criação da tabela é necessário gerar as migrations que estarão sendo enviadas no GIT apenas para facilitar os testes, futuramente será deletado do GIT.

## Passo a Passo para executar o projeto

1) Clicar com botão direito do mouse em cima da solução e ir em "Propriedades". Clique em "Vários projetos de inicialização" e marque "Iniciar" em todos.
   ![image](https://github.com/TiagoDoria/Mottu/assets/14184182/590cc7f9-5c2f-49bd-bc32-f0a5f6f7fc59)

2) Em cada API, execute o comando "add-migration NOMEDAMIGRATION" Exemplo: add-migration initial
3) Certifique de ter criado os bancos de cada API, após isso para cada API execute "update-database" para criar as tabelas.
4) Clique em "Iniciar" para executar o projeto.








