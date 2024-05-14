# DESAFIO BACKEND MOTTU

O projeto "Mottu" foi desenvolvido como desafio para a empresa Mottu, concorrendo a vaga de Desenvolvedor BackEnd.
O passo a passo para executar o sistema será descrito no final desse documento.

### Observações importantes:
 - Alguns pontos solicitados no desafio não foram implementados, são eles: Criação do serviço que monitora em tempo real cadastro de pedidos e enviados via mensageria para os entregadores, por falta de tempo.
 - Algumas funcionalidades não foram desenvolvidas, pois não constavam no desafio, para economizar tempo de focar nos requisitos principais. Exemplo: CRUD completo de Locação, Pedidos e usuários.

## O sistema foi feito utilizando as tecnologias:
1) .NET Core 8;
2) C#
3) PostgreSQL;
4) Entity Framework
5) Aplicativo WEB do ASP NET Core MVC para o FrontEnd;
6) Bootstrap 5

## Como foi densenvolvido

O sistema foi desenvolvido utilizando arquitetura de Micro Serviços, separados em: 
1) Serviço de autenticação (AuthAPI)
2) Serviço de Locação (LocationAPI)
3) Serviço de Motos (MotorcycleAPI)
4) Serviço de Pedidos (OrderAPI)

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



