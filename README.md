# Authentication.2FA

Esta é uma API desenvolvida em .NET Core utilizando base de dados MySql que oferece recursos de autenticação usando JWT e autenticação de dois fatores (2FA) com o Google Authenticator. A arquitetura da API segue o padrão Onion Architecture, garantindo uma separação clara das preocupações e facilitando a manutenção e extensibilidade do código.

## Configuração do Ambiente

Antes de começar, certifique-se de ter o seguinte instalado:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Google Authenticator](https://play.google.com/store/apps/details?id=com.google.android.apps.authenticator2&hl=en) (para autenticação de dois fatores)

## Instalação e Execução

1. Clone este repositório:
`git clone https://github.com/VictorHMR/Authentication.2FA.Api.git`

2. Execute os scripts Mysql presentes no arquivo `ScriptBanco.txt`

3. Configure as variáveis de ambiente necessárias, como as chaves JWT, Google 2FA e as credenciais do banco de dados, no arquivo `appsettings.json`.

4. Execute a aplicação:
`dotnet run`

A API estará acessível em `http://localhost:5000` por padrão.

## Endpoints

A API expõe os seguintes endpoints:

- `/api/User/CreateUser`: Endpoint para autenticar usuários e obter um token JWT.
- `/api/User/GenerateConfirmationQR`: Endpoint para verificar o código de autenticação de dois fatores.
- `/api/User/UserSignin`: Endpoint para realizar o login e obter o código JWT.

