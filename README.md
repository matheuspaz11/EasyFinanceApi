## Sobre o projeto
Esta API foi desenvolvida para simplificar o gerenciamento de despesas pessoais. Permite uma visualização das informações mais importantes sobre despesas mensais, como vencimento, valor e status. 

## Estrutura de pastas

- **EasyFinanceApi**  
  - **Context**  
  - **Controllers**  
  - **Helpers**  
    - Util  
    - Validators  
  - **Mapper**  
  - **Migrations**  
  - **Models**  
    - DTOs  
    - Entities  
    - Enums  
  - **Repository**  
    - Interfaces  
  - **Services**  
    - Interfaces  

## Tecnologias utilizadas
- **C#**
- **.NET 6**
- **Migrations**
- **AutoMapper**
- **PostgreSQL**

## Funcionalidades & Endpoints
- **Criar despesa**
  - POST api/Expense/CreateExpense/
- **Listar todas as despesas**
  - GET api/Expense/GetExpenses/
- **Listar despesa por id**
  - GET api/Expense/GetExpense/{id}
- **Deletar despesa**
  - DELETE api/Expense/DeleteExpense/{id}
- **Pagar despesa**
  - PATCH api/Expense/PayExpense/{id}
- **Editar despesa**
  - PUT api/Expense/ChangeExpense/
