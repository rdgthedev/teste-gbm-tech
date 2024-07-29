
## ACME Logística Rodoviária LTDA

Aplicação feita para gerenciar o transporte de carga em uma empresa de logística rodoviária. A aplicação deve permitir o cadastro, listagem e edição de caminhões e motoristas, além de atribuir motoristas a caminhões para realizar entregas.

## 🏗️ Clean Architecture

- **Core Layer (Camada de Core):** Define as entidades de domínio e as regras de negócio da aplicação, além disso temos interfaces de repositórios de infraestrutura.

- **Application Layer (Camada de Aplicação):** Contém a lógica de negócios da aplicação e os casos de uso da aplicação, além disso serve para possibilitar a comunicação da camada de Presentation com a camada Core, mesmo sem ter conhecimento da camada de apresentação (Presentation Layer).

- **Infrastructure Layer (Camada de Infraestrutura):** Camada mais externa da Clean Architecture, pois lida com os serviços externos como Frameworks e Drivers.

- **Presentation Layer (Camada de Apresentação):** Camada que que lida com a entrada e saída de dados e se comunica com as camadas mais internas para lidarem com o processamento das informações.

- **Cross-Cutting (Corte-Transversal):** Camada que atua de transforma transversal e tem a referência de todas as camadas visando resolver aspectos que afetam todas as camadas. 

## 🛠️ Tecnologias Utilizadas 

- **ASP.NET Core Web Api:** Framework para o desenvolvimento APIs.

- **Entity Framework Core:** ORM para mapeamento objeto-relacional, simplificando o acesso a dados.

- **SQL Server:** Banco de dados utilizado para armazenamento de dados.

- **Swagger:** Ferramenta que auxilia na documentação da API, seguindo as sugestões da OpenAPI.

- **MediatR:** Biblioteca utilizada reduzir o acoplamento entre objetos e faz a abstração da comunicação entre componentes. 

- **Fluent Validations:** Biblioteca utilizada para fazer validações de entrada de uma forma mais simplificada e legível.

- **NewtonSoft:** Biblioteca utilizada para lidar com serialização e desserialização de objetos.


## ✨ Padrões de Projeto Utilizados

- **CQRS:** Padrão utilizado junto com o MediatR, visando a segregação de comandos de escrita dos comandos de leituras do banco de dados.

- **Unit Of Work:** Padrão utilizado para criar uma unidade única de trabalho, o que reduz a concorrência no banco de dados e permite lidar com múltiplas transações.

- **Domain Driven Design:** Utilizados alguns conceitos desta abordagem, visando desenvolver a aplicação dirigida ao domínio.

- **Code First:** Abordagem que visa gerar as entidades do banco de dados através das entidades presente no código.
## ⚙️ Instalação de dependências

#### Clone o repositório: 
```
git clone https://github.com/rdgthedev/teste-gbm-tech.git
```

#### Baixe o .NET 8.0.7: 
```
https://dotnet.microsoft.com/pt-br/download/dotnet/8.0
```

#### Baixe o Visual Studio Community: 
```
https://visualstudio.microsoft.com/pt-br/vs/community/
```

#### Baixe o SQL Server e utilize algum SGBD de sua preferência: 
```
https://visualstudio.microsoft.com/pt-br/vs/community/
```

#### Execute no terminal o comando abaixo para instalar o Entity Framework Tools: 
```
dotnet tool install --global dotnet-ef --version 8.0.7
```

## ▶️ Para rodar


#### Dentro da IDE e vá até o arquivo appsettings.json e você encontrará a ConnectionString do exemplo abaixo, altere pela sua. 
```
"Server=RODRIGO;Database=GbmTest;Trusted_Connection=True;TrustServerCertificate=true;"
```

#### No terminal da IDE execute o camando abaixo para construir o projeto: 
```
dotnet build
```

#### No terminal da IDE execute o camando abaixo para restaurar as dependências do projeto: 
```
dotnet restore
```

#### Dentro da IDE execute o projeto utilizando o comando abaixo: 
```
dotnet run --project .\GBMProject.API\
```

#### Abre seu navegador e acesse URL abaixo:
```
http://localhost:5281/swagger/index.html
```
