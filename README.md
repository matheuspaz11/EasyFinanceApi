## Sobre o projeto
Esta API foi desenvolvida para simplificar o gerenciamento de despesas pessoais. Permite uma visualização das informações mais importantes sobre despesas mensais, como vencimento, valor e status. 

## Estrutura de pastas

- **EasyFinanceApi**  
  - **Context**  
    Responsável por servir como um ponto de entrada para interagir com o banco de dados, representando um contexto de banco de dados para o aplicativo.
  - **Controllers**  
    Funções de controle de rotas.
  - **Helpers**  
    Contêm funções utilitárias ou classes auxiliares que fornecem funcionalidades reutilizáveis em todo o aplicativo.
    - Util  
    - Validators  
  - **Mapper**  
    Responsáveis por definir o mapeamento entre as entidades do modelo de domínio e as tabelas do banco de dados.
  - **Migrations**  
    Responsáveis por representar e gerenciar as alterações no esquema do banco de dados ao longo do tempo.
  - **Models**  
    Representam as entidades do modelo de domínio.
    - DTOs  
    - Entities  
    - Enums  
  - **Repository**  
    Contêm interfaces/classes com métodos genéricos para realizar operações básicas de CRUD.
    - Interfaces  
  - **Services**  
    Contêm classes para lidar com a lógica de negócios da aplicação.
    - Interfaces  

## Tecnologias utilizadas
- **C#**
- **.NET 6**
- **Migrations**
- **AutoMapper**
- **PostgreSQL**

## Funcionalidades
- **Criar despesa**
- **Listar todas as despesas**
- **Listar despesa por id**
- **Deletar despesa**
- **Pagar despesa**
- **Editar despesa**
