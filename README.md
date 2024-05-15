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

## Passo a passo para utilizar o sistema

1) Comece criando uma conta, clicando em "Registrar" no menu superior, lado direito.

![image](https://github.com/TiagoDoria/Mottu/assets/14184182/60c40e06-debd-4e1f-881a-a706f9e16122)

2) Preencha os campos de acordo de acordo com o tipo de usuário e será redirecionado para tela de login, em seguida, realize o login. Inicialmente, crie um usuário ADMIN para cadastrar as Motos
3) Cadastre as motos clicando em "Motos" no menu superior.

![image](https://github.com/TiagoDoria/Mottu/assets/14184182/daa264ed-0e7c-4c39-8083-577af681ee92)

4) Agora clique em "Cadastrar Moto", no canto esquerdo da tabela que exibe as motos cadastradas.

![image](https://github.com/TiagoDoria/Mottu/assets/14184182/5adafe69-b5b1-49a3-8088-68d650d3a01b)

5) Se clicar em "Locação" no menu superior, irá listar todas as locações realizadas e em "Pedidos" todos os pedidos cadastrados.

   ![image](https://github.com/TiagoDoria/Mottu/assets/14184182/93cb69d2-44dd-435d-8d4d-1e97c99c5a10)

6) Com as motos cadastradas, utilizando um usuário com perfil DELIVERYMAN, clique em "Locação" para poder registrar uma locação, será listado o tipo de plano para escolha e as motos disponíveis para locação 

![image](https://github.com/TiagoDoria/Mottu/assets/14184182/9a260673-ed78-4aff-87ab-d571947a79eb)

7) O usuário também pode realizar o UPLOAD de uma imagem clicando em "Upload CNH" no menu superior e a imagem será salva na pasta wwwroot do projeto

![image](https://github.com/TiagoDoria/Mottu/assets/14184182/a0bbd842-6874-47d7-aa3f-82b7e1c2c256)

8) Clicando em "Devolver" pode finalizar uma locação, preenchendo a data de devolução e será exibido o valor total.

   ![image](https://github.com/TiagoDoria/Mottu/assets/14184182/42af8b23-db0f-46ab-9a2d-cd868069f5d0)

   
9) Com usuário ADMIN , você pode cadastrar pedidos clicando em pedidos no menu e em seguida "Cadastrar pedidos"

    ![image](https://github.com/TiagoDoria/Mottu/assets/14184182/2a945d31-1195-40c9-a2bf-a2ae90702a94)

10) Com usuário DELIVERYMAN clique em "Pedidos" no menu e em seguida em "ACEITAR" para aceitar um pedido que esteja disponível.

    ![image](https://github.com/TiagoDoria/Mottu/assets/14184182/d7542b05-7184-4802-a383-f6be6bf40973)











